using UnityEngine;
using UnityEditor;

namespace ObserverTC
{
    [CustomEditor(typeof(NotifierVector3))]
    public class NotifierVector3Editor : Editor
    {
        // Variables
        NotifierVector3 _target;
        Vector3 valueToNotify;

        private void OnEnable()
        {
            _target = (NotifierVector3)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            // Only able to click when the game is running
            GUI.enabled = Application.isPlaying;

            EditorGUILayout.Space();

            // Button to locate observers
            if (GUILayout.Button("Locate Observers", GUILayout.Height(30)))
                _target.LocateObservers();

            EditorGUILayout.Space(10);

            // Vector2 field to choose the value to notify on X
            GUILayout.Label("Value to notify(only for 'Notify Observers')");
            valueToNotify = EditorGUILayout.Vector3Field("", valueToNotify);

            EditorGUILayout.Space();

            // Button to notify observers
            if (GUILayout.Button("Notify Observers", GUILayout.Height(50)))
                _target.Notify(valueToNotify);
        }
    }
}
