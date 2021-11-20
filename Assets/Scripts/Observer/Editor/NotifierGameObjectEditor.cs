using UnityEngine;
using UnityEditor;

namespace ObserverTC
{
    [CustomEditor(typeof(NotifierGameObject))]
    public class NotifierGameObjectEditor : Editor
    {
        // Variables
        NotifierGameObject _target;
        GameObject valueToNotify;

        private void OnEnable()
        {
            _target = (NotifierGameObject)target;
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

            // Toggle to choose the value to notify
            valueToNotify = (GameObject)EditorGUILayout.ObjectField("Value to notify (only for 'Notify Observers')", valueToNotify, typeof(GameObject), true);

            EditorGUILayout.Space();

            // Button to notify observers
            if (GUILayout.Button("Notify Observers", GUILayout.Height(50)))
                _target.Notify(valueToNotify);
        }
    }
}
