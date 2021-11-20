using System.Collections.Generic;
using UnityEngine;

namespace ObserverTC
{
    [CreateAssetMenu(fileName = "Default Notifier String", menuName = "Notifiers/Notifier String")]
    public class NotifierString : ScriptableObject
    {
        [SerializeField] [TextArea] string description;

        // List of observers
        List<ObserverString> observers = new List<ObserverString>();

        /// <summary>
        /// Add an observer to this notifier's list.
        /// </summary>
        /// <param name="newObserver">The observer to add.</param>
        public void Subscribe(ObserverString newObserver)
        {
            if (observers.Contains(newObserver))
                return;

            observers.Add(newObserver);
        }

        /// <summary>
        /// Remove an observer from this notifier's list.
        /// </summary>
        /// <param name="observerToRemove">The observer to remove.</param>
        public void Unsubscribe(ObserverString observerToRemove)
        {
            if (!observers.Contains(observerToRemove))
                return;

            observers.Remove(observerToRemove);
        }

        /// <summary>
        /// Notify all the observers.
        /// </summary>
        public void Notify(string value)
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