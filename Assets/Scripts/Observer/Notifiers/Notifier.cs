using System.Collections.Generic;
using UnityEngine;

namespace ObserverTC
{
    [CreateAssetMenu(fileName = "Default Notifier", menuName = "Notifiers/Notifier")]
    public class Notifier : ScriptableObject
    {
        [SerializeField] [TextArea] string description;

        // List of observers
        List<Observer> observers = new List<Observer>();

        /// <summary>
        /// Add an observer to this notifier's list.
        /// </summary>
        /// <param name="newObserver">The observer to add.</param>
        public void Subscribe(Observer newObserver)
        {
            if (observers.Contains(newObserver))
                return;

            observers.Add(newObserver);
        }

        /// <summary>
        /// Remove an observer from this notifier's list.
        /// </summary>
        /// <param name="observerToRemove">The observer to remove.</param>
        public void Unsubscribe(Observer observerToRemove)
        {
            if (!observers.Contains(observerToRemove))
                return;

            observers.Remove(observerToRemove);
        }

        /// <summary>
        /// Notify all the observers.
        /// </summary>
        public void Notify()
        {
            for (int i = observers.Count - 1; i >= 0; i--)
                observers[i].response?.Invoke();
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
