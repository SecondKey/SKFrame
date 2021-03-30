using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEditorInternal;
using System.Linq;

namespace GSFramework
{
    [CustomEditor(typeof(UINodeBase))]
    public class ScriptEditor_UIBinding : Editor
    {
        bool showComponentToken;

        private void OnEnable()
        {

            UINodeBase uiNode = target as UINodeBase;
            if (uiNode.actionComponents == null)
            {
                uiNode.actionComponents = new ObservableDictionary<string, bool>("");
                uiNode.actionComponents.CollectionChanged += (e) =>
                {
                    //KeyValuePair<string, bool> oldItem = ((KeyValuePair<string, bool>)e.OldItem);
                    //KeyValuePair<string, bool> newItem = ((KeyValuePair<string, bool>)e.NewItem);

                    switch (e.Action)
                    {
                        case CollectionChangedAction.Add:
                            if (((KeyValuePair<string, bool>)e.NewItem).Value == true && uiNode.GetComponents<UIBindingComponent>().Where(x => x.targetComponent == ((KeyValuePair<string, bool>)e.NewItem).Key).Count() == 0)
                            {
                                UIBindingComponent attributes = uiNode.gameObject.AddComponent<UIBindingComponent>();
                                attributes.targetComponent = ((KeyValuePair<string, bool>)e.NewItem).Key;
                            }
                            break;
                        case CollectionChangedAction.Remove:
                            foreach (UIBindingComponent attributes in uiNode.GetComponents<UIBindingComponent>().Where(x => x.targetComponent == ((KeyValuePair<string, bool>)e.OldItem).Key))
                            {
                                Destroy(attributes);
                            }
                            break;
                        case CollectionChangedAction.Replace:
                            if (((KeyValuePair<string, bool>)e.NewItem).Value == true)
                            {
                                if (uiNode.GetComponents<UIBindingComponent>().Where(x => x.targetComponent == ((KeyValuePair<string, bool>)e.NewItem).Key).Count() == 0)
                                {
                                    UIBindingComponent attributes = uiNode.gameObject.AddComponent<UIBindingComponent>();
                                    attributes.targetComponent = ((KeyValuePair<string, bool>)e.NewItem).Key;
                                }
                            }
                            else
                            {
                                List<UIBindingComponent> attributes = uiNode.GetComponents<UIBindingComponent>().Where(x => x.targetComponent == ((KeyValuePair<string, bool>)e.NewItem).Key).ToList();
                                while (attributes.Count > 0)
                                {
                                    DestroyImmediate(attributes[0]);
                                    attributes.RemoveAt(0);
                                }
                            }
                            break;
                    }
                };
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //获取脚本对象
            UINodeBase script = target as UINodeBase;
            SerializedProperty nodeToken = serializedObject.FindProperty("nodeToken");

            //输入
            nodeToken.stringValue = EditorGUILayout.TextField("NodeToken", nodeToken.stringValue);

            showComponentToken = EditorGUILayout.Foldout(showComponentToken, "Components");
            if (showComponentToken)
            {
                List<string> nowComponents = new List<string>(script.GetComponents<UIBehaviour>().Select(p => p.GetType().Name));
                List<string> oldComponents = new List<string>(script.actionComponents.Keys);
                if (!oldComponents.All(nowComponents.Contains) || oldComponents.Count != nowComponents.Count)
                {
                    foreach (string b in oldComponents.Where(q => !nowComponents.Contains(q)))
                    {
                        script.actionComponents.Remove(b);
                    }
                    foreach (string b in nowComponents.Where(q => !oldComponents.Contains(q)))
                    {
                        script.actionComponents.Add(b, false);
                    }
                }

                foreach (string s in nowComponents)
                {
                    script.actionComponents[s] = EditorGUILayout.ToggleLeft(s, script.GetComponents<UIBindingComponent>().Where(p => p.targetComponent == s).Count() != 0);
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomEditor(typeof(UIBindingComponent))]
    public class ScriptEditor_UIBindingAttribute : Editor
    {
        ReorderableList attributeTokens;

        private void OnDisable()
        {
            typeof(EditorWindow).Assembly.GetType("UnityEditor.LogEntries").GetMethod("Clear").Invoke(null, null);
        }

        private void OnEnable()
        {
            attributeTokens = new ReorderableList(serializedObject, serializedObject.FindProperty("AttributeTokens")
                , true, true, true, true);

            attributeTokens.drawHeaderCallback = (Rect rect) => { GUI.Label(rect, "Player Array"); };

            attributeTokens.elementHeight = 50;

            attributeTokens.drawElementCallback = (Rect rect, int index, bool selected, bool focused) =>
            {
                SerializedProperty item = attributeTokens.serializedProperty.GetArrayElementAtIndex(index);
                rect.height -= 4;
                rect.y += 2;
                EditorGUI.PropertyField(rect, item, new GUIContent("Index " + index));
            };
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //获取脚本对象
            SerializedProperty targetComponent = serializedObject.FindProperty("targetComponent");
            EditorGUILayout.LabelField(targetComponent.stringValue);

            SerializedProperty componentToken = serializedObject.FindProperty("componentToken");
            componentToken.stringValue = EditorGUILayout.TextField("ComponentToken", componentToken.stringValue);

            attributeTokens.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }



    [CustomPropertyDrawer(typeof(AttributesToken))]
    public class ComponentTokenDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                var attributeType = property.FindPropertyRelative("attribute");
                var path = property.FindPropertyRelative("path");
                attributeType.stringValue = EditorGUI.TextField(new Rect(position) { y = position.y + 5, height = position.height - 30 }, "Attribute", attributeType.stringValue);
                path.stringValue = EditorGUI.TextField(new Rect(position) { y = position.y + 25, height = position.height - 30 }, path.stringValue);
            }
        }
    }

    //private void OnEnable()
    //{
    //    componentToken = new ReorderableList(serializedObject, serializedObject.FindProperty("componentTokens")
    //        , true, true, true, true);

    //    //自定义列表名称
    //    componentToken.drawHeaderCallback = (Rect rect) => { GUI.Label(rect, "Player Array"); };

    //    componentToken.elementHeight = 50;

    //    //自定义绘制列表元素
    //    componentToken.drawElementCallback = (Rect rect, int index, bool selected, bool focused) =>
    //    {
    //        //根据index获取对应元素 
    //        SerializedProperty item = componentToken.serializedProperty.GetArrayElementAtIndex(index);
    //        rect.height -= 4;
    //        rect.y += 2;
    //        EditorGUI.PropertyField(rect, item, new GUIContent("Index " + index));
    //    };
    //}


    //if (!uiComponenets.All(nowUIComponents.Contains) || uiComponenets.Count != nowUIComponents.Count)
    //{
    //    //foreach (string b in uiComponenets.Where(q => !nowUIComponents.Contains(q)))
    //    //{
    //    //    script.UseTokenComponent.Remove(b);
    //    //}
    //    foreach (string b in nowUIComponents.Where(q => !uiComponenets.Contains(q)))
    //    {
    //        Debug.Log(0);
    //        //script.UseTokenComponent.Add(b);
    //        if (!script.componentTokens.ContainsKey(b))
    //        {
    //            Debug.Log(0);
    //            script.componentTokens.Add(b, new ComponentToken()); ;
    //        }
    //    }
    //    script.UseTokenComponent = nowUIComponents;
    //}

}