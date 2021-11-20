using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SFXTC
{
    /// <summary>
    /// A SFX composed of one or few clips.
    /// </summary>
    [CreateAssetMenu(fileName = "Default SFX Event", menuName = "Audio/SFX Event")]
    public class SFXEvent : ScriptableObject
    {
        #region Variables
        [TextArea] [SerializeField] string description;

        [Header("COMPONENTS")]
        [Tooltip("List of clips to be played.\n Only one will be randomly selected when playing.")]
        [SerializeField] List<AudioClip> clips = new List<AudioClip>();

        [Tooltip("Mixer's group that will be assign to the clip.")]
        [SerializeField] AudioMixerGroup mixerGroup;
        [Space]

        [Header("INFOS")]
        [Tooltip("Select if the clip should be playing automatically on start.")]
        [SerializeField] bool playOnAwake;

        [Tooltip("Select if the clip should automatically replay.")]
        [SerializeField] bool loop;

        [Tooltip("Select if the clip should be mute.")]
        [SerializeField] bool mute;

        [Tooltip("Select if the clip should ignore the effects applied to his audio source.")]
        [SerializeField] bool bypassEffects;

        [Tooltip("Select if the clip should ignore the reverb zones")]
        [SerializeField] bool bypassReverbZones;

        [Tooltip("Select if this sfx can be play in multiple audio source at the same time.")]
        [SerializeField] bool multiplePlay = true;
        [Space]

        [Header("SPECS")]
        [Tooltip("Select the priority of a clip.\nA clip with a low value will have priority on a clip with a high value.\n0 = Highest priority | 256 = Lowest priority")]
        [Range(0, 256)] [SerializeField] int priority = 128;

        // All these variables are hide in inspector to be correctly displayed with a custom editor.
        // Volume
        [HideInInspector] public bool useRandomVolume;
        [HideInInspector] public float volume = 1f;
        [MinMaxRange(0, 1)]
        [HideInInspector] public RangedFloat randomVolume;

        // Pan
        [HideInInspector] public bool useRandomPan;
        [HideInInspector] [Range(-1f, 1f)] public float pan = 0f;
        [MinMaxRange(-1, 1)]
        [HideInInspector] public RangedFloat randomPan;

        // Pitch
        [HideInInspector] public bool useRandomPitch;
        [HideInInspector] [Range(0f, 2f)] public float pitch = 1f;
        [MinMaxRange(0, 2)]
        [HideInInspector] public RangedFloat randomPitch;

        // Reverb mix
        [HideInInspector] public bool useRandomReverbZoneMix;
        [HideInInspector] [Range(0f, 1.1f)] public float reverbZoneMix = 1f;
        [MinMaxRange(0, 1.1f)]
        [HideInInspector] public RangedFloat randomReverbZoneMix;

        [HideInInspector] public AudioSource source;    // Represents the last AudioSource where this SFX was played.

        /// <summary>
        /// Represent if this SFXEvent can be played in different audio sources at the same time.
        /// </summary>
        public bool MultiplePlay => multiplePlay;
        #endregion

        #region Functions
        /// <summary>
        /// Play this SFX.
        /// </summary>
        public void Play() { SFXManager.Instance.Play(this); }

        /// <summary>
        /// Play this SFX with a delay.
        /// </summary>
        /// <param name="delay">How much delay before it plays (in seconds).</param>
        public void PlayDelayed(float delay) { SFXManager.Instance.PlayDelayed(this, delay); }

        /// <summary>
        /// Play this SFX at scheduled time.
        /// </summary>
        /// <param name="time">At which time the SFX should be played.</param>
        public void PlayScheduled(double time) { SFXManager.Instance.PlayScheduled(this, time); }

        /// <summary>
        /// Stop this SFX.
        /// </summary>
        public void Stop() { SFXManager.Instance.Stop(this); }

        /// <summary>
        /// Pause this SFX.
        /// </summary>
        public void Pause() { SFXManager.Instance.Pause(this); }

        /// <summary>
        /// Unpause this SFX.
        /// </summary>
        public void UnPause() { SFXManager.Instance.UnPause(this); }

        /// <summary>
        /// Set the audio source for this SFX given by the SFXManager.
        /// </summary>
        public void SetAudioSource()
        {
            // Prevent errors
            if (source == null)
            {
                Debug.LogError($"ERROR : There is no source for '{name}'.");
                return;
            }
            if (clips.Count == 0)
            {
                Debug.LogError($"ERROR : There is no audio clip for '{name}'.");
                return;
            }

            // Set the audio source values
            source.clip = clips[Random.Range(0, clips.Count)];
            source.outputAudioMixerGroup = mixerGroup;

            source.playOnAwake = playOnAwake;
            source.loop = loop;
            source.mute = mute;
            source.bypassEffects = bypassEffects;
            source.bypassReverbZones = bypassReverbZones;

            source.priority = priority;

            if (useRandomVolume)
                source.volume = Random.Range(randomVolume.minValue, randomVolume.maxValue);
            else
                source.volume = volume;

            if (useRandomPan)
                source.panStereo = Random.Range(randomPan.minValue, randomPan.maxValue);
            else
                source.panStereo = pan;

            if (useRandomPitch)
                source.pitch = Random.Range(randomPitch.minValue, randomPitch.maxValue);
            else
                source.pitch = pitch;

            if (useRandomReverbZoneMix)
                source.reverbZoneMix = Random.Range(randomReverbZoneMix.minValue, randomReverbZoneMix.maxValue);
            else
                source.reverbZoneMix = reverbZoneMix;
        }
        #endregion

        #region Preview Functions
        /// <summary>
        /// Only used for previews. Do not use in code !
        /// </summary>
        public void Preview(AudioSource source)
        {
            // Used only to preview on the inspector

            if (clips.Count == 0)
            {
                Debug.LogError($"ERROR : There is no audio clip for '{name}'.");
                return;
            }

            source.clip = clips[Random.Range(0, clips.Count)];
            source.outputAudioMixerGroup = mixerGroup;

            source.playOnAwake = playOnAwake;
            source.loop = loop;
            source.mute = mute;
            source.bypassEffects = bypassEffects;
            source.bypassReverbZones = bypassReverbZones;

            source.priority = priority;

            if (useRandomVolume)
                source.volume = Random.Range(randomVolume.minValue, randomVolume.maxValue);
            else
                source.volume = volume;

            if (useRandomPan)
                source.panStereo = Random.Range(randomPan.minValue, randomPan.maxValue);
            else
                source.panStereo = pan;

            if (useRandomPitch)
                source.pitch = Random.Range(randomPitch.minValue, randomPitch.maxValue);
            else
                source.pitch = pitch;

            if (useRandomReverbZoneMix)
                source.reverbZoneMix = Random.Range(randomReverbZoneMix.minValue, randomReverbZoneMix.maxValue);
            else
                source.reverbZoneMix = reverbZoneMix;

            source.Play();
        }

        /// <summary>
        /// Only used for previews. Do not use in code !
        /// </summary>
        public void StopPreview(AudioSource previewer)
        {
            previewer.Stop();
        }
        #endregion
    }
}
