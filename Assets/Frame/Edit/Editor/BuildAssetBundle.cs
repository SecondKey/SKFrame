using System.Collections;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace GSFramework
{
    /// <summary>
    /// 把工程中设置了AssetBundle Name的资源打包成.unity3d 到StreamingAssets目录下
    /// </summary>
    public class BuildAssetBundles
    {
        [MenuItem("工具/打包/打包Mod文件")]
        static void BuildAllAssets()
        {
            BuildPipeline.BuildAssetBundles(Application.dataPath + "/AllAssets/", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        }

        [MenuItem("工具/打包/打包游戏文件")]
        static void BuildDataAssets()
        {
            BuildPipeline.BuildAssetBundles(Application.dataPath + "/DataAssets/", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        }

        [MenuItem("工具/打包/打包更新文件")]
        static void BuildUpdateAssets()
        {
            BuildPipeline.BuildAssetBundles(Application.dataPath + "/DataAssets/", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        }
    }
}