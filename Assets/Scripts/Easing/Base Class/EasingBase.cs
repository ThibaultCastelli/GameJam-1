using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EasingTC
{
    #region Enums
    /// <summary>
    /// Represents the ease type.
    /// </summary>
    public enum AnimationType
    {
        EaseIn,
        EaseOut,
        EaseInOut,
        SpecialEase,
        Mirror
    }
    /// <summary>
    /// Represents the ease in curves.
    /// </summary>
    public enum EaseIn
    {
        Linear,

        EaseInQuad,
        EaseInCubic,
        EaseInQuart,
        EaseInQuint,
        EaseInCirc,
        EaseInExpo,
    }
    /// <summary>
    /// Represents the ease out curves.
    /// </summary>
    public enum EaseOut
    {
        EaseOutQuad,
        EaseOutCubic,
        EaseOutQuart,
        EaseOutQuint,
        EaseOutCirc,
        EaseOutExpo,
    }
    /// <summary>
    /// Represents the ease in out curves.
    /// </summary>
    public enum EaseInOut
    {
        EaseInOutQuad,
        EaseInOutCubic,
        EaseInOutQuart,
        EaseInOutQuint,
        EaseInOutCirc,
    }
    /// <summary>
    /// Represents the special ease curves.
    /// </summary>
    public enum SpecialEase
    {
        EaseInBack,
        EaseOutBack,
        EaseOutBounce,
    }
    /// <summary>
    /// Represents the miror ease curves.
    /// </summary>
    public enum MirorType
    {
        Linear,
        EaseQuad,
        EaseCubic,
        EaseQuart,
        EaseQuint,
        EaseCirc,
        EaseExpo,
    }
    /// <summary>
    /// Represents the behaviour of the loop.
    /// </summary>
    public enum LoopType
    {
        Simple,
        Mirror,
    }
    #endregion

    /// <summary>
    /// Base class to perform ease animations.
    /// </summary>
    public abstract class EasingBase : MonoBehaviour
    {
        #region Variables
        public AnimationType animationType;
        public EaseIn easeInType;
        public EaseOut easeOutType;
        public EaseInOut easeInOutType;
        public MirorType mirorType;
        public SpecialEase specialEaseType;

        public bool playOnAwake;
        public bool loop;
        public LoopType loopType;
        [Range(0.1f, 20)] public float duration = 1;

        public bool followEndValue; // If selected in Inspector, show the end state.

        /// <summary>
        /// Represent the animation to play
        /// </summary>
        protected Func<IEnumerator> animationToPlay;
        /// <summary>
        /// Represent the curves to use for the animation
        /// </summary>
        protected Func<float, float> easeFunc;

        // Use to know how much time is passed since the begining of the animation
        protected float elapsedTime = 0;

        // Flags
        protected bool _isInTransition;
        bool _hasPlayed;

        // Special eases
        protected const float s = 1.70158f;
        protected float t;
        #endregion

        #region Animation Choice
        protected virtual void Awake()
        {
            // Select which ease function will be used
            switch (animationType)
            {
                case AnimationType.EaseIn:
                    switch (easeInType)
                    {
                        case EaseIn.Linear:
                            easeFunc = Linear;
                            break;
                        case EaseIn.EaseInQuad:
                            easeFunc = EaseInQuad;
                            break;
                        case EaseIn.EaseInCubic:
                            easeFunc = EaseInCubic;
                            break;
                        case EaseIn.EaseInQuart:
                            easeFunc = EaseInQuart;
                            break;
                        case EaseIn.EaseInQuint:
                            easeFunc = EaseInQuint;
                            break;
                        case EaseIn.EaseInCirc:
                            easeFunc = EaseInCirc;
                            break;
                        case EaseIn.EaseInExpo:
                            easeFunc = EaseInExpo;
                            break;
                    }
                    break;

                case AnimationType.EaseOut:
                    switch (easeOutType)
                    {
                        case EaseOut.EaseOutQuad:
                            easeFunc = EaseOutQuad;
                            break;
                        case EaseOut.EaseOutCubic:
                            easeFunc = EaseOutCubic;
                            break;
                        case EaseOut.EaseOutQuart:
                            easeFunc = EaseOutQuart;
                            break;
                        case EaseOut.EaseOutQuint:
                            easeFunc = EaseOutQuint;
                            break;
                        case EaseOut.EaseOutCirc:
                            easeFunc = EaseOutCirc;
                            break;
                        case EaseOut.EaseOutExpo:
                            easeFunc = EaseOutExpo;
                            break;
                    }
                    break;

                case AnimationType.EaseInOut:
                    switch (easeInOutType)
                    {
                        case EaseInOut.EaseInOutQuad:
                            easeFunc = EaseInOutQuad;
                            break;
                        case EaseInOut.EaseInOutCubic:
                            easeFunc = EaseInOutCubic;
                            break;
                        case EaseInOut.EaseInOutQuart:
                            easeFunc = EaseInOutQuart;
                            break;
                        case EaseInOut.EaseInOutQuint:
                            easeFunc = EaseInOutQuint;
                            break;
                        case EaseInOut.EaseInOutCirc:
                            easeFunc = EaseInOutCirc;
                            break;
                    }
                    break;

                case AnimationType.Mirror:
                    switch (mirorType)
                    {
                        case MirorType.Linear:
                            easeFunc = LinearMirror;
                            break;
                        case MirorType.EaseQuad:
                            easeFunc = MirrorQuad;
                            break;
                        case MirorType.EaseCubic:
                            easeFunc = MirrorCubic;
                            break;
                        case MirorType.EaseQuart:
                            easeFunc = MirrorQuart;
                            break;
                        case MirorType.EaseQuint:
                            easeFunc = MirrorQuint;
                            break;
                        case MirorType.EaseCirc:
                            easeFunc = MirrorCirc;
                            break;
                        case MirorType.EaseExpo:
                            easeFunc = MirrorExpo;
                            break;
                    }
                    break;
            }
        }
        #endregion

        #region Start & Update
        private void OnEnable()
        {
            // Play the animation as soon as the object is enable if playOnAwake is selected on the editor
            if (playOnAwake)
                PlayAnimation();
        }

        private void Update()
        {
            // Replay automatically the animation if loop is selected on the editor
            if (loop && !_isInTransition && _hasPlayed)
            {
                if (loopType == LoopType.Simple)
                    PlayAnimation();
                else
                    PlayAnimationInOut();
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Play the animation set in the inspector.
        /// </summary>
        public void PlayAnimation()
        {
            _isInTransition = true;
            elapsedTime = 0;

            StartCoroutine(animationToPlay?.Invoke());
            _hasPlayed = true;
        }

        /// <summary>
        /// Play the animation set in the inspector in mirror.
        /// </summary>
        /// <example>
        /// On the first call, play the animation. On the second call, play the animation in reverse, etc etc...
        /// </example>
        public virtual void PlayAnimationInOut()
        {
            if (animationType == AnimationType.Mirror)
            {
                Debug.LogError("ERROR : Can't play in reverse for a mirror animation type.\nFrom : " + this.gameObject.name);
                return;
            }
            else if (!_hasPlayed)
            {
                PlayAnimation();
                return;
            }
            else if (_isInTransition)
            {
                StopAllCoroutines();
            }

            PlayAnimation();
        }
        #endregion

        #region Easing Types
        float Linear(float t) => t;

        float EaseInQuad(float t) => t * t;
        float EaseInCubic(float t) => t * t * t;
        float EaseInQuart(float t) => t * t * t * t;
        float EaseInQuint(float t) => t * t * t * t;
        float EaseInCirc(float t) => 1 - Mathf.Sqrt(1 - (t * t));
        float EaseInExpo(float t) => Mathf.Pow(2, 10 * (t - 1));

        float EaseOutQuad(float t) => t * (2 - t);
        float EaseOutCubic(float t) => (--t) * t * t + 1;
        float EaseOutQuart(float t) => 1 - (--t) * t * t * t;
        float EaseOutQuint(float t) => 1 + (--t) * t * t * t * t;
        float EaseOutCirc(float t) => Mathf.Sqrt(1 - (t - 1) * (t - 1));
        float EaseOutExpo(float t) => (-Mathf.Pow(2, -10 * t) + 1);

        float EaseInOutQuad(float t) => t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;
        float EaseInOutCubic(float t) => t < 0.5f ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
        float EaseInOutQuart(float t) => t < 0.5f ? 8 * t * t * t * t : 1 - 8 * (--t) * t * t * t;
        float EaseInOutQuint(float t) => t < 0.5f ? 16 * t * t * t * t * t : 1 + 16 * (--t) * t * t * t * t;
        float EaseInOutCirc(float t) => t < 0.5f ? (1 - Mathf.Sqrt(1 - (2 * t) * (2 * t))) / 2 : (Mathf.Sqrt(1 - (-2 * t + 2) * (-2 * t + 2)) + 1) / 2;
        #endregion

        #region Mirror
        float LinearMirror(float t) => t < 0.5f ? Linear(t / 0.5f) : Linear((1 - t) / 0.5f);
        float MirrorQuad(float t) => t < 0.5f ? EaseInQuad(t / 0.5f) : EaseInQuad((1 - t) / 0.5f);
        float MirrorCubic(float t) => t < 0.5f ? EaseInCubic(t / 0.5f) : EaseInCubic((1 - t) / 0.5f);
        float MirrorQuart(float t) => t < 0.5f ? EaseInQuart(t / 0.5f) : EaseInQuart((1 - t) / 0.5f);
        float MirrorQuint(float t) => t < 0.5f ? EaseInQuint(t / 0.5f) : EaseInQuint((1 - t) / 0.5f);
        float MirrorCirc(float t) => t < 0.5f ? EaseInCirc(t / 0.5f) : EaseInCirc((1 - t) / 0.5f);
        float MirrorExpo(float t) => t < 0.5f ? EaseInExpo(t / 0.5f) : EaseInExpo((1 - t) / 0.5f);
        #endregion

        protected IEnumerator NullAnimation() { yield break; }
    }
}
