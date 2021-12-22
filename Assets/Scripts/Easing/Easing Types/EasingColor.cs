using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EasingTC
{
    /// <summary>
    /// Handle the animation for colors (renderer or image).
    /// </summary>
    public class EasingColor : EasingBase
    {
        #region Variables
        public bool useAnotherStartValue;

        public Color startColor = Color.white;
        public Color endColor = Color.white;

        protected Color defaultStartColor;
        protected Color newStartColor;

        protected Color newEndColor;

        new SpriteRenderer renderer = null;
        Image image = null;
        #endregion

        #region Animation Choice
        protected override void Awake()
        {
            base.Awake();

            // Select the animation and intialize default values
            animationToPlay = EaseColor;

            // Get the renderer or image component
            if (!TryGetComponent<SpriteRenderer>(out renderer))
            {
                if (!TryGetComponent<Image>(out image))
                {
                    Debug.LogError("ERROR : Can't find the renderer or the image on this gameobject.\nLocation : " + this.gameObject.name);
                    return;
                }
                else
                    defaultStartColor = image.color;
            }
            else
            {
                defaultStartColor = renderer.color;
            }

            if (useAnotherStartValue)
                defaultStartColor = startColor;

            newStartColor = defaultStartColor;
            newEndColor = endColor;

            // Select which special ease function will be used
            if (animationType == AnimationType.SpecialEase)
            {
                Debug.LogError("ERROR : Can't change the color with specials ease.\nLocation : " + this.gameObject.name);
                animationToPlay = NullAnimation;
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Play the animation set in the inspector in mirror.
        /// </summary>
        /// <example>
        /// On the first call, play the animation. On the second call, play the animation in reverse, etc etc...
        /// </example>
        public override void PlayAnimationInOut()
        {
            newEndColor = newEndColor == endColor ? defaultStartColor : endColor;
            newStartColor = renderer != null ? renderer.color : image.color;

            base.PlayAnimationInOut();
        }

        public override void StopAnimation()
        {
            base.StopAnimation();

            if (renderer != null)
            {
                renderer.color = defaultStartColor;
            }
            else if (image != null)
            {
                image.color = defaultStartColor;
            }
        }

        /// <summary>
        /// The ease animation for color.
        /// </summary>
        IEnumerator EaseColor()
        {
            while (true)
            {
                if (renderer != null)
                    renderer.color = Color.Lerp(newStartColor, newEndColor, easeFunc(elapsedTime / duration));
                else if (image != null)
                    image.color = Color.Lerp(newStartColor, newEndColor, easeFunc(elapsedTime / duration));

                if (elapsedTime == duration)
                {
                    _isInTransition = false;
                    yield break;
                }

                yield return null;
                elapsedTime = Mathf.Clamp(elapsedTime += Time.deltaTime, 0, duration);
            }
        }
        #endregion
    }
}