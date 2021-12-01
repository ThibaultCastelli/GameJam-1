using UnityEngine;
using UnityEditor;

namespace ObserverTC
{
    [CustomEditor(typeof(Notifier))]
    public class NotifierEditor : Editor
    {
        // Variables
        Notifier _target;

        private void OnEnable()
        {
            _target = (Notifier)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Only able to click when the game is running
            GUI.enabled = Application.isPlaying;

            EditorGUILayout.Space();

            // Button to locate the observers
            if (GUILayout.Button("Locate Observers", GUILayout.Height(30)))
                _target.LocateObservers();

            EditorGUILayout.Space();

            // Button to notify observers
            if (GUILayout.Button("Notify Observers", GUILayout.Height(50)))
                _target.Notify();
        }
    }
}
