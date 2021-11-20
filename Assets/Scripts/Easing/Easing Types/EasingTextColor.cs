using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EasingTC
{
    /// <summary>
    /// Handle the animation for text colors (text or text mesh pro).
    /// </summary>
    public class EasingTextColor : EasingBase
    {
        #region Variables
        public bool useAnotherStartValue;

        public Color startColor = Color.white;
        public Color endColor = Color.white;

        protected Color defaultStartColor;
        protected Color newStartColor;

        protected Color newEndColor;

        Text text = null;
        TextMeshProUGUI TMP = null;
        #endregion

        #region Animation Choice
        protected override void Awake()
        {
            base.Awake();

            // Select the animation and intialize default values
            animationToPlay = EaseTextColor;

            // Get the TextMeshPro or Text component
            if (!TryGetComponent<TextMeshProUGUI>(out TMP))
            {
                if (!TryGetComponent<Text>(out text))
                {
                    Debug.LogError("ERROR : Can't find the TextMeshPro or the Text on this gameobject.\nLocation : " + this.gameObject.name);
                    return;
                }
                else
                    defaultStartColor = text.color;
            }
            else
                defaultStartColor = TMP.color;

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
            newStartColor = TMP != null ? TMP.color : text.color;

            base.PlayAnimationInOut();
        }

        /// <summary>
        /// The ease animation for text colors.
        /// </summary>
        /// <returns></returns>
        IEnumerator EaseTextColor()
        {
            while (true)
            {
                if (TMP != null)
                    TMP.color = Color.Lerp(newStartColor, newEndColor, easeFunc(elapsedTime / duration));
                else
                    text.color = Color.Lerp(newStartColor, newEndColor, easeFunc(elapsedTime / duration));

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