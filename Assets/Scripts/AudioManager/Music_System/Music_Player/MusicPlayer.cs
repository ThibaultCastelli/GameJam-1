using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicTC
{
    /// <summary>
    /// Class to play MusicEvents.
    /// </summary>
    public class MusicPlayer : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// The MusicEvent currently playing on this MusicPlayer.
        /// </summary>
        public MusicEvent musicEvent;

        // List of the audio sources for each layer of the MusicEvent
        List<AudioSource> audioSources = new List<AudioSource>();

        public List<AudioSource> AudioSources => audioSources;
        #endregion

        #region Initialization
        private void Awake()
        {
            // Create audio sources for playing layers
            for (int i = 0; i < MusicManager.Instance.MaxLayerCount; i++)
                audioSources.Add(gameObject.AddComponent<AudioSource>());
        }
        #endregion

        #region Functions
        /// <summary>
        /// Play a MusicEvent with the given fade in time and at first layer.
        /// </summary>
        /// <param name="musicEvent">The MusicEvent to play.</param>
        /// <param name="fadeTime">How much time the fade in will take (in seconds).</param>
        public void Play(MusicEvent musicEvent, float fadeTime = 0)
        {
            // Prevent errors
            if (musicEvent == null)
            {
                Debug.LogError("ERROR : The MusicEvent you try to play is null.");
                return;
            }
            else if (fadeTime < 0)
            {
                Debug.LogError("ERROR : The fade time can't be negative.");
                return;
            }

            // Give each layer an audio source and set the default values for the audio source
            SetAudioSources(musicEvent);

            // Prevent from having two fades at the same time
            StopAllCoroutines();

            // Use different functions based on the fade time (instant or fade) and the layer type of the MusicEvent (additive or single)
            if (fadeTime == 0)
            {
                if (musicEvent.LayerType == LayerType.Additive)
                    PlayImmediatelyAdditive();
                else
                    PlayImmediatelySingle();
            }
            else
            {
                if (musicEvent.LayerType == LayerType.Additive)
                    StartCoroutine(PlayFadeAdditive(fadeTime));
                else
                    StartCoroutine(PlayFadeSingle(fadeTime));
            }
        }

        /// <summary>
        /// Play a MusicEvent with no fade in and with additive layer type.
        /// </summary>
        void PlayImmediatelyAdditive()
        {
            // Set the layers that are lower or equal to the current layer audible, mute the others
            for (int i = 0; i < musicEvent.MusicLayers.Length && i < audioSources.Count; i++)
            {
                if (musicEvent.MusicLayers[i] == null)
                    continue;

                if (i <= MusicManager.Instance.CurrentLayer)
                    audioSources[i].volume = musicEvent.DefaultVolume;
                else
                    audioSources[i].volume = 0;
            }
        }
        /// <summary>
        /// Play a MusicEvent with no fade in and with single layer type.
        /// </summary>
        void PlayImmediatelySingle()
        {
            // Set the layers that are equal to the current layer audible, mute the others
            for (int i = 0; i < musicEvent.MusicLayers.Length && i < audioSources.Count; i++)
            {
                if (musicEvent.MusicLayers[i] == null)
                    continue;

                if (i == MusicManager.Instance.CurrentLayer)
                    audioSources[i].volume = musicEvent.DefaultVolume;
                else
                    audioSources[i].volume = 0;
            }
        }

        /// <summary>
        /// Stop the current MusicEvent playing.
        /// </summary>
        /// <param name="fadeTime">How much time the fade out will take (in seconds).</param>
        public void Stop(float fadeTime)
        {
            // Prevent errors
            if (musicEvent == null)
            {
                Debug.LogError("ERROR : There is no MusicEvent currently playing.");
                return;
            }
            else if (fadeTime < 0)
            {
                Debug.LogError("ERROR : The fade time can't be negative.");
                return;
            }

            // Prevent from having two fades at the same time
            StopAllCoroutines();

            // Use different functions based on the fade time (instant or fade)
            if (fadeTime == 0)
                StopImmediately();
            else
                StartCoroutine(StopFade(fadeTime));
        }

        /// <summary>
        /// Stop the current MusicEvent playing with no fade out.
        /// </summary>
        void StopImmediately()
        {
            // Stop all the audio sources and delete the current musicEvent;
            foreach (AudioSource source in audioSources)
                source.Stop();

            musicEvent = null;
        }

        /// <summary>
        /// Give each layer of the new MusicEvent an audio source.
        /// </summary>
        /// <param name="newMusicEvent">The MusicEvent to set.</param>
        void SetAudioSources(MusicEvent newMusicEvent)
        {
            // Prevent errors
            if (newMusicEvent == null)
            {
                Debug.LogError("ERROR : The MusicEvent you try to set is null.");
                return;
            }
            if (newMusicEvent == musicEvent)
                return;

            musicEvent = newMusicEvent;

            // Give each layer an audio source and set the default values for the audio source
            for (int i = 0; i < musicEvent.MusicLayers.Length && i < audioSources.Count; i++)
            {
                audioSources[i].clip = musicEvent.MusicLayers[i];
                audioSources[i].outputAudioMixerGroup = musicEvent.MixerGroup;
                audioSources[i].loop = musicEvent.Loop;
                audioSources[i].volume = 0;
                audioSources[i].Play();
            }
        }

        /// <summary>
        /// Get the volumes of each layer when starting a fade
        /// </summary>
        /// <returns>The volume of each layer.</returns>
        float[] GetStartVolumes()
        {
            float[] startVolumes = new float[musicEvent.MusicLayers.Length];
            for (int i = 0; i < musicEvent.MusicLayers.Length && i < audioSources.Count; i++)
                startVolumes[i] = audioSources[i].volume;

            return startVolumes;
        }

        public void SetLoop(bool loop)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.loop = loop;
            }
        }
        #endregion

        #region Coroutines
        /// <summary>
        /// Play a MusicEvent with the given fade in time and with additive layer type.
        /// </summary>
        /// <param name="fadeTime">How much time the fade in/out will take (in seconds).</param>
        IEnumerator PlayFadeAdditive(float fadeTime)
        {
            // Initialization
            float elapsedTime = 0;
            float[] startVolumes = GetStartVolumes();

            while (true)
            {
                // Fade in the layers that are lower or equal to the current layer, fade out the others
                for (int i = 0; i < musicEvent.MusicLayers.Length && i < audioSources.Count; i++)
                {
                    if (musicEvent.MusicLayers[i] == null)
                        continue;

                    if (i <= MusicManager.Instance.CurrentLayer)
                        audioSources[i].volume = Mathf.Lerp(startVolumes[i], musicEvent.DefaultVolume, elapsedTime / fadeTime);
                    else
                        audioSources[i].volume = Mathf.Lerp(startVolumes[i], 0, elapsedTime / fadeTime);
                }

                // Stop the coroutine when the fade time is passed
                if (elapsedTime == fadeTime)
                    yield break;

                // Update and clamp the elapsed time
                yield return null;
                elapsedTime = Mathf.Clamp(elapsedTime + Time.deltaTime, 0, fadeTime);
            }
        }

        /// <summary>
        /// Play a MusicEvent with the given fade in time and with single layer type.
        /// </summary>
        /// <param name="fadeTime">How much time the fade in/out will take (in seconds).</param>
        IEnumerator PlayFadeSingle(float fadeTime)
        {
            // Initialization
            float elapsedTime = 0;
            float[] startVolumes = GetStartVolumes();

            while (true)
            {
                // Fade in the layers that are equal to the current layer, fade out the others
                for (int i = 0; i < musicEvent.MusicLayers.Length && i < audioSources.Count; i++)
                {
                    if (musicEvent.MusicLayers[i] == null)
                        continue;

                    if (i == MusicManager.Instance.CurrentLayer)
                        audioSources[i].volume = Mathf.Lerp(startVolumes[i], musicEvent.DefaultVolume, elapsedTime / fadeTime);
                    else
                        audioSources[i].volume = Mathf.Lerp(startVolumes[i], 0, elapsedTime / fadeTime);
                }

                // Stop the coroutine when the fade time is passed
                if (elapsedTime == fadeTime)
                    yield break;

                // Update and clamp the elapsed time
                yield return null;
                elapsedTime = Mathf.Clamp(elapsedTime + Time.deltaTime, 0, fadeTime);
            }
        }

        /// <summary>
        /// Stop the currently MusicEvent playing with the given fade out time.
        /// </summary>
        /// <param name="fadeTime">How much time the fade out will take (in seconds).</param>
        IEnumerator StopFade(float fadeTime)
        {
            // Initialization
            float elapsedTime = 0;
            float[] startVolumes = GetStartVolumes();

            while (true)
            {
                // Fade out all the layers
                for (int i = 0; i < musicEvent.MusicLayers.Length && i < audioSources.Count; i++)
                {
                    if (musicEvent.MusicLayers[i] == null)
                        continue;

                    audioSources[i].volume = Mathf.Lerp(startVolumes[i], 0, elapsedTime / fadeTime);
                }

                // Stop all the layers and delete the current MusicEvent when fade time is passed
                if (elapsedTime == fadeTime)
                {
                    foreach (AudioSource source in audioSources)
                        source.Stop();
                    musicEvent = null;

                    yield break;
                }

                // Update and clamp the elapsed time
                yield return null;
                elapsedTime = Mathf.Clamp(elapsedTime + Time.deltaTime, 0, fadeTime);
            }
        }
        #endregion
    }
}
