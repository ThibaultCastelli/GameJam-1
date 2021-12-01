using System.Collections.Generic;
using UnityEngine;

namespace ObserverTC
{
    [CreateAssetMenu(fileName = "Default Notifier Float", menuName = "Notifiers/Notifier Float")]
    public class NotifierFloat : ScriptableObject
    {
        [SerializeField] [TextArea] string description;

        // List of observers
        List<ObserverFloat> observers = new List<ObserverFloat>();

        /// <summary>
        /// Add an observer to this notifier's list.
        /// </summary>
        /// <param name="newObserver">The observer to add.</param>
        public void Subscribe(ObserverFloat newObserver)
        {
            if (observers.Contains(newObserver))
                return;

            observers.Add(newObserver);
        }

        /// <summary>
        /// Remove an observer from this notifier's list.
        /// </summary>
        /// <param name="observerToRemove">The observer to remove.</param>
        public void Unsubscribe(ObserverFloat observerToRemove)
        {
            if (!observers.Contains(observerToRemove))
                return;

            observers.Remove(observerToRemove);
        }

        /// <summary>
        /// Notify all the observers.
        /// </summary>
        public void Notify(float value)
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


