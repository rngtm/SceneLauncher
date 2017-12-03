///-------------------------------------
/// SceneLauncher
/// @ 2017 RNGTM(https://github.com/rngtm)
///-------------------------------------
namespace SceneLauncher
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using System.Linq;

    /// <summary>
    /// スクリプトを開く
    /// </summary>
    public class ScriptOpener
    {
        /// <summary>
        /// スクリプトを外部エディタで開く
        /// </summary>
        public static void OpenInEditor(string scriptName, int scriptLine)
        {
            scriptName = scriptName.ToLower();
            foreach (string path in AssetDatabase.GetAllAssetPaths())
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(path).ToLower();
                if (fileName.Equals(scriptName))
                {
                    MonoScript script = AssetDatabase.LoadAssetAtPath(path, typeof(MonoScript)) as MonoScript;
                    if (script != null)
                    {
                        if (!AssetDatabase.OpenAsset(script, scriptLine))
                        {
                            Debug.LogWarning("Couldn't open script : " + scriptName);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Couldn't open script : " + scriptName);
                    }
                }
            }
        }
    }
}
