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

    public class MainWindow : EditorWindow
    {

        private SceneAsset[] sceneArray;
        private Vector2 scrollPos = Vector2.zero;
        private bool isSimpleShow = true;

        [MenuItem("Tools/Scene Launcher")]
        static void Open()
        {
            GetWindow<MainWindow>();
        }

        void OnFocus()
        {
            this.ReloadScenes();
        }

        void OnGUI()
        {
            if (this.sceneArray == null) { this.ReloadScenes(); }
            if (this.sceneArray == null) { return; }

            EditorGUILayout.LabelField("シーンを開きます");

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
            this.scrollPos = EditorGUILayout.BeginScrollView(this.scrollPos);
            foreach (var scene in this.sceneArray)
            {
                var buttonText = this.isSimpleShow ? scene.name : AssetDatabase.GetAssetPath(scene);
                var managerClassName = string.Format("{0}Manager", scene.name);

                EditorGUILayout.BeginHorizontal();

                var color = (scene.name == Config.HighlightName) ? Config.HighlightColor : GUI.color;
                if (CustomUI.ButtonColor(buttonText, color))
                {
                    EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(scene));
                }

                if (CustomUI.ButtonColor("Manager.cs", 84f, color))
                {
                    Selection.activeObject = scene;
                    ScriptOpener.OpenInEditor(managerClassName, 0);
                }
                if (CustomUI.ButtonColor(".unity", 48f, color))
                {
                    Selection.activeObject = scene;
                }

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
            EditorGUI.EndDisabledGroup();
        }

        /// <summary>
        /// シーン一覧の再読み込み
        /// </summary>
        void ReloadScenes()
        {
            this.sceneArray = GetAllSceneAssets().ToArray();
        }

        /// <summary>
        /// プロジェクト内に存在するすべてのシーンファイルを取得する
        /// </summary>
        static IEnumerable<SceneAsset> GetAllSceneAssets()
        {
            return AssetDatabase.FindAssets("t:SceneAsset")
           .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
           .Where(path => !path.Contains("Editor")) // エディターフォルダ以下無視
           .Select(path => AssetDatabase.LoadAssetAtPath(path, typeof(SceneAsset)))
           .Where(obj => obj != null)
           .Select(obj => (SceneAsset)obj);
        }

    }
}
