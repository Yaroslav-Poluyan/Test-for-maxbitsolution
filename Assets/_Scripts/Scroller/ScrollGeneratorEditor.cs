#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace _Scripts.Scroller
{
    [CustomEditor(typeof(ScrollerGenerator))]
    public class ScrollGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var component = (ScrollerGenerator) target;
            if (GUILayout.Button("RandomizeCardDatas"))
            {
                component.RandomizeCardsData();
            }
        }
    }
}
#endif