using System.Collections.Generic;
using UnityEngine;

namespace ObserverTC
{
    [CreateAssetMenu(fileName = "Default Notifier Bool", menuName = "Notifiers/Notifier Bool")]
    public class NotifierBool : ScriptableObject
    {
        [SerializeField] [TextArea] string description;

        // List of observers
        List<ObserverBool> observers = new List<ObserverBool>();

        /// <summary>
        /// Add an observer to this notifier's list.
        /// </summary>
        /// <param name="newObserver">The observer to add.</param>
        public void Subscribe(ObserverBool newObserver)
        {
            if (observers.Contains(newObserver))
                return;

            observers.Add(newObserver);
        }

        /// <summary>
        /// Remove an observer from this notifier's list.
        /// </summary>
        /// <param name="observerToRemove">The observer to remove.</param>
        public void Unsubscribe(ObserverBool observerToRemove)
        {
            if (!observers.Contains(observerToRemove))
                return;

            observers.Remove(observerToRemove);
        }

        /// <summary>
        /// Notify all the observers.
        /// </summary>
        public void Notify(bool value)
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