using GSFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
namespace GSFramework
{
    public class IOCContainer : IIOCContainer
    {
        private Dictionary<string, Dictionary<string, Type>> mappings = new Dictionary<string, Dictionary<string, Type>>();

        public IOCContainer(string identify)
        {
            Dictionary<Type, Dictionary<string, Type>> mappingList;
            switch (identify)
            {
                default:
                    mappingList = new Dictionary<Type, Dictionary<string, Type>>();
                    break;
            }

            AddMapping(mappingList);
        }

        #region AddMapping
        void AddMapping(Dictionary<Type, Dictionary<string, Type>> mapping)
        {
            foreach (var kv in mapping)
            {
                foreach (var kt in kv.Value)
                {
                    AddMapping(kv.Key.FullName, kt.Value.FullName, kt.Key);
                }
            }
        }

        /// <summary>
        /// 添加抽象到具体的映射
        /// </summary>
        /// <param name="serviceType">抽象映射原的全名</param>
        /// <param name="implementationType">具体映射目标的全名</param>
        /// <param name="token">具体在映射字典中的标识</param>
        void AddMapping(string sourceType, string targetType, string token = "")
        {
            LogParameter("Binding " + targetType + " To " + sourceType + ",Token is " + token);
            if (!mappings.ContainsKey(sourceType))
            {
                mappings.Add(sourceType, new Dictionary<string, Type>() { { token, Type.GetType(targetType) } });
            }
            else
            {
                if (mappings[sourceType].ContainsKey(token))
                {
                    mappings[sourceType][token] = Type.GetType(targetType);
                }
                else
                {
                    mappings[sourceType].Add(token, Type.GetType(targetType));
                }
            }
        }
        #endregion

        #region GetInstence
        public object CreateInstence(string scriptType, string scriptToken)
        {
            if (!mappings.ContainsKey(scriptType) || !mappings[scriptType].ContainsKey(scriptToken))
            {
                return null;
            }

            return mappings[scriptType][scriptToken].CreateInstence();
        }
        #endregion

        #region Tools
        /// <summary>
        /// 输出每一次访问传入的参数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="parameter"></param>
        public void LogParameter(string text, params string[] parameter)
        {
            for (int i = 0; i < parameter.Length; i++)
            {
                text = text + " " + parameter[i];
            }
            ConditionLog.BasicLog("IOC:" + text);
        }
        #endregion
    }
}