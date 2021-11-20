using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFXTC
{
    /// <summary>
    /// Class to create a pool of audio source to play all the SFXs.
    /// </summary>
    [RequireComponent(typeof(SFXManager))]
    public class AudioSourcesPool : MonoBehaviour
    {
        #region Variables
        [Header("POOL INFOS")]
        [SerializeField] [Range(1, 50)] int defaultPoolSize = 10;

        List<AudioSource> pool; 

        GameObject SFXPlayerGO;     // Represents the GameObject that will hold all the audio sources of the pool.
        #endregion

        private void Awake()
        {
            // Create the GameObject that will hold all the audio sources of the pool.
            SFXPlayerGO = new GameObject("SFX_Player");
            SFXPlayerGO.transform.parent = this.transform;

            // Instantiate the pool
            pool = new List<AudioSource>(defaultPoolSize);

            // Add the audio sources to the pool and to the SFXPlayerGO.
            for (int i = 0; i < defaultPoolSize; i++)
                pool.Add(SFXPlayerGO.AddComponent<AudioSource>());
        }

        /// <summary>
        /// Get an audio source where you can put a SFXEvent and play it.
        /// </summary>
        /// <returns>A free audio source.</returns>
        public AudioSource Request()
        {
            // Return an audio source that has no clip or that is not playing a clip
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].clip == null || !pool[i].isPlaying)
                    return pool[i];
            }

            // If no audio source are available, create a new one
            AudioSource newAudioSource = SFXPlayerGO.AddComponent<AudioSource>();
            pool.Add(newAudioSource);
            return newAudioSource;
        }
    }
}
