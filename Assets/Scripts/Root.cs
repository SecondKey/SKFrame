using GSFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 游戏的入口
/// 执行Root的Awake方法游戏开始
/// </summary>
public class Root : MonoBehaviour
{
    public RecursiveScopeState<int> test = new RecursiveScopeState<int>(0);

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        LoadRoot();
    }

    void LoadRoot()
    {
        Basic.Instence.LoadLevel(AppConst.RootLevel, (s) => { Debug.Log(s + " now Load Over"); StartUpdate(); });
    }

    void StartUpdate()
    {
        Basic.Instence.StartUpdate((b) => { Debug.Log("Update Over"); LoadGeneral(); });
    }

    void LoadGeneral()
    {
        Basic.Instence.LoadLevel(AppConst.GeneralLevel, (s) => { Debug.Log(s + " now Load Over"); InitOver(); });
    }

    void InitOver()
    {
        InitIdent[] initObjects = FindObjectsOfType<InitIdent>();
        foreach (InitIdent initObject in initObjects)
        {
            foreach (IInitializedObject initializedObject in initObject.GetComponents<IInitializedObject>())
            {
                initializedObject.PerformInitialization();
            }
        }
    }

    public void TestVoid(GSFramework.EventArgs args)
    {

    }

    void BuildScriptCenter()
    {

    }

    void BuildInitializeManager()
    {

    }
}