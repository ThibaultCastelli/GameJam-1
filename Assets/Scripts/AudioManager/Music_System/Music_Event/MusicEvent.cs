using UnityEngine;
using UnityEngine.Audio;

namespace MusicTC
{
    #region Enum
    /// <summary>
    /// Represents how the layer will be blend.
    /// Additive : All the layer can be play at the same time.
    /// Single : Only one layer can be play at the same time.
    /// </summary>
    public enum LayerType
    {
        Additive,
        Single
    }
    #endregion

    /// <summary>
    /// A music composed of layers.
    /// </summary>
    [CreateAssetMenu(fileName = "Default Music Event", menuName = "Audio/Music Event")]
    public class MusicEvent : ScriptableObject
    {
        #region Variables
        [SerializeField] [TextArea] string description;
        [Space]

        [Header("COMPONENTS")]
        [Tooltip("A list of audio clips that represent different layers of a music.")]
        [SerializeField] AudioClip[] musicLayers = new AudioClip[1];
        [Tooltip("Mixer's group that will be assign to each music layer.")]
        [SerializeField] AudioMixerGroup mixerGroup;
        [Space]

        [Header("MUSIC INFOS")]
        [Tooltip("Select if the layers should automatically replay.")]
        [SerializeField] bool loop = true;
        [Tooltip("Select if you want the music to stop when going from one scene to the other.")]
        [SerializeField] bool stopOnSceneChange = false;
        [Tooltip("Select the default volume for each layer.\n0 = mute | 1 = full sound")]
        [SerializeField] [Range(0, 1)] float defaultVolume = 1;
        [Tooltip("The type of layer blend: \nAdditive : All the layer can be play at the same time.\nSingle : Only one layer can be play at the same time.")]
        [SerializeField] LayerType layerType = LayerType.Additive;

        /// <summary>
        /// Only used for preview functions.
        /// </summary>
        [HideInInspector] public int currentLayer = 0;
        #endregion

        #region Properties
        /// <summary>
        /// Represents the layers (audio clip) of this MusicEvent.
        /// </summary>
        public AudioClip[] MusicLayers => musicLayers;

        /// <summary>
        /// Represents the mixer group of this MusicEvent.
        /// </summary>
        public AudioMixerGroup MixerGroup => mixerGroup;

        /// <summary>
        /// Represents the LayerType of this MusicEvent.
        /// </summary>
        public LayerType LayerType => layerType;

        /// <summary>
        /// Indicates if this MusicEvent will automatically loop.
        /// </summary>
        public bool Loop => loop;

        /// <summary>
        /// Indicates if this MusicEvent will stop when going to another scene.
        /// </summary>
        public bool StopOnSceneChange => stopOnSceneChange;

        /// <summary>
        /// Represents the default volume of this MusicEvent.
        /// </summary>
        public float DefaultVolume => defaultVolume;
        #endregion

        #region Functions
        /// <summary>
        /// Play this MusicEvent with the given fade in time and at first layer.
        /// </summary>
        /// <param name="fadeTime">How much time the fade in will take (in seconds).</param>
        public void Play(float fadeTime = 0) { MusicManager.Instance.Play(this, fadeTime); }

        /// <summary>
        /// Replay this MusicEvent with the given fade in time and at first layer.
        /// </summary>
        /// <param name="fadeTime">How much time the fade in/out will take (in seconds).</param>
        public void Replay(float fadeTime = 0) { MusicManager.Instance.Replay(this, fadeTime); }

        /// <summary>
        /// Stop this MusicEvent with the given fade out time.
        /// </summary>
        /// <param name="fadeTime">How much time the fade out will take (in seconds).</param>
        public void Stop(float fadeTime = 0) { MusicManager.Instance.Stop(this, fadeTime); }

        /// <summary>
        /// Set the layer to play with the given fade in time (different behaviour if the LayerType is Additive or Single).
        /// </summary>
        /// <param name="newLayer">Wich layer to play.</param>
        /// <param name="fadeTime">How much time the fade in will take (in seconds).</param>
        public void SetLayer(int newLayer, float fadeTime = 0) { MusicManager.Instance.SetLayer(this, newLayer, fadeTime); }

        /// <summary>
        /// Go to the next layer with the given fade in time (different behaviour if the LayerType is Additive or Single).
        /// </summary>
        /// <param name="fadeTime">How much time the fade in will take (in seconds).</param>
        public void IncreaseLayer(float fadeTime = 0) { MusicManager.Instance.IncreaseLayer(this, fadeTime); }

        /// <summary>
        /// Go to the previous layer with the given fade in time (different behaviour if the LayerType is Additive or Single).
        /// </summary>
        /// <param name="fadeTime">How much time the fade in will take (in seconds).</param>
        public void DecreaseLayer(float fadeTime = 0) { MusicManager.Instance.DecreaseLayer(this, fadeTime); }
        #endregion

        #region Preview Functions
        /// <summary>
        /// Only used for previews. Do not use it in code !
        /// </summary>
        public void PlayPreview(AudioSource[] previewers)
        {
            for (int i = 0; i < musicLayers.Length; i++)
            {
                if (musicLayers[i] == null)
                    continue;

                previewers[i].clip = musicLayers[i];
                previewers[i].volume = 0;
                previewers[i].loop = loop;
                previewers[i].Play();
            }

            SetLayersVolumePreview(previewers);
        }

        /// <summary>
        /// Only used for previews. Do not use it in code !
        /// </summary>
        public void StopPreview(AudioSource[] previewers)
        {
            foreach (AudioSource source in previewers)
                source.Stop();
        }

        /// <summary>
        /// Only used for previews. Do not use it in code !
        /// </summary>
        public void IncreaseLayerPreview(AudioSource[] previewers)
        {
            currentLayer = Mathf.Clamp(++currentLayer, 0, musicLayers.Length - 1);
            Debug.Log("Current Layer : " + currentLayer);

            SetLayersVolumePreview(previewers);
        }

        /// <summary>
        /// Only used for previews. Do not use it in code !
        /// </summary>
        public void DecreaseLayerPreview(AudioSource[] previewers)
        {
            currentLayer = Mathf.Clamp(--currentLayer, 0, musicLayers.Length - 1);
            Debug.Log("Current Layer : " + currentLayer);

            SetLayersVolumePreview(previewers);
        }

        /// <summary>
        /// Only used for previews. Do not use it in code !
        /// </summary>
        void SetLayersVolumePreview(AudioSource[] previewers)
        {
            for (int i = 0; i < musicLayers.Length; i++)
            {
                if (musicLayers[i] == null)
                    continue;

                if (layerType == LayerType.Additive)
                {
                    if (i <= currentLayer)
                        previewers[i].volume = defaultVolume;
                    else
                        previewers[i].volume = 0;
                }

                else
                {
                    if (i == currentLayer)
                        previewers[i].volume = defaultVolume;
                    else
                        previewers[i].volume = 0;
                }
            }
        }
        #endregion
    }
}
