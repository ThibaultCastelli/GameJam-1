using UnityEngine;
using UnityEditor;

namespace ObserverTC
{
    [CustomEditor(typeof(NotifierBool))]
    public class NotifierBoolEditor : Editor
    {
        // Variables
        NotifierBool _target;
        bool valueToNotify;

        private void OnEnable()
        {
            _target = (NotifierBool)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            // Only able to click when the game is running
            GUI.enabled = Application.isPlaying;

            EditorGUILayout.Space();

            // Button to locate the observers
            if (GUILayout.Button("Locate Observers", GUILayout.Height(30)))
                _target.LocateObservers();

            EditorGUILayout.Space(10);

            // Toggle to choose the value to notify
            valueToNotify = GUILayout.Toggle(valueToNotify, " Value to notify (only for 'Notify Observers')");

            // Button to notify observers
            if (GUILayout.Button("Notify Observers", GUILayout.Height(50)))
                _target.Notify(valueToNotify);
        }
    }
}
