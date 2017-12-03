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

    public class CustomUI
    {
        /// <summary>
        /// 色付きのボタン
        /// </summary>
        public static bool ButtonColor(string label, Color color)
        {
            var defaultColor = GUI.color;
            GUI.color = color;
            var click = GUILayout.Button(label);
            GUI.color = defaultColor;

            return click;
        }

        /// <summary>
        /// 色付きのボタン
        /// </summary>
        public static bool ButtonColor(string label, float width, Color color)
        {
            var defaultColor = GUI.color;
            GUI.color = color;
            var click = GUILayout.Button(label, GUILayout.Width(width));
            GUI.color = defaultColor;

            return click;
        }
    }
}
