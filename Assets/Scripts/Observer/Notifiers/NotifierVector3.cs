using System.Collections.Generic;
using UnityEngine;

namespace ObserverTC
{
    [CreateAssetMenu(fileName = "Default Notifier Vector3", menuName = "Notifiers/Notifier Vector3")]
    public class NotifierVector3 : ScriptableObject
    {
        [SerializeField] [TextArea] string description;

        // List of observers
        List<ObserverVector3> observers = new List<ObserverVector3>();

        /// <summary>
        /// Add an observer to this notifier's list.
        /// </summary>
        /// <param name="newObserver">The observer to add.</param>
        public void Subscribe(ObserverVector3 newObserver)
        {
            if (observers.Contains(newObserver))
                return;

            observers.Add(newObserver);
        }

        /// <summary>
        /// Remove an observer from this notifier's list.
        /// </summary>
        /// <param name="observerToRemove">The observer to remove.</param>
        public void Unsubscribe(ObserverVector3 observerToRemove)
        {
            if (!observers.Contains(observerToRemove))
                return;

            observers.Remove(observerToRemove);
        }

        /// <summary>
        /// Notify all the observers.
        /// </summary>
        public void Notify(Vector3 value)
        {
            for (int i = observers.Count - 1; i >= 0; i--)
                observers[i].response?.Invoke(value);
        }

        /// <summary>
        /// Used only for debug purpose. Display the location of each observer in this notifier's list.
        /// </summary>
        public void LocateObservers()
        {
            Debug.Log($"Notifier '{name}' :");
            for (int i = 0; i < observers.Count; i++)
                Debug.Log($"Location of Observer n°{i} : {observers[i].gameObject.name}");
        }
    }
}