using UnityEngine;

namespace EasingTC
{
    /// <summary>
    /// Collection of ease functions.
    /// </summary>
    public static class EasingFunctions
    {
        #region Standard Eases
        /// <summary>
        /// Linear ease
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        /// <returns></returns>
        public static float Linear(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            return Mathf.Lerp(start, end, animTime);
        }

        #region Quad
        /// <summary>
        /// Ease in quadratic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInQuad(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return end * animTime * animTime + start;
        }
        /// <summary>
        /// Ease out quadratic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseOutQuad(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return -end * animTime * (animTime - 2) + start;
        }
        /// <summary>
        /// Ease in out quadratic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInOutQuad(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp(elapsedTime / duration / 0.5f, 0, 2);
            end -= start;
            if (animTime < 1) return end * 0.5f * animTime * animTime + start;
            animTime--;
            return -end * 0.5f * (animTime * (animTime - 2) - 1) + start;
        }
        #endregion

        #region Cubic
        /// <summary>
        /// Ease in cubic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInCubic(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return end * animTime * animTime * animTime + start;
        }
        /// <summary>
        /// Ease out cubic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseOutCubic(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            animTime--;
            end -= start;
            return end * (animTime * animTime * animTime + 1) + start;
        }
        /// <summary>
        /// Ease in out cubic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInOutCubic(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp(elapsedTime / duration / 0.5f, 0, 2);
            end -= start;
            if (animTime < 1) return end * 0.5f * animTime * animTime * animTime + start;
            animTime -= 2;
            return end * 0.5f * (animTime * animTime * animTime + 2) + start;
        }
        #endregion

        #region Quart
        /// <summary>
        /// Ease in quartic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInQuart(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return end * animTime * animTime * animTime * animTime + start;
        }
        /// <summary>
        /// Ease out quartic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseOutQuart(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            animTime--;
            end -= start;
            return -end * (animTime * animTime * animTime * animTime - 1) + start;
        }
        /// <summary>
        /// Ease in out quartic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInOutQuart(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp(elapsedTime / duration / 0.5f, 0, 2);
            end -= start;
            if (animTime < 1) return end * 0.5f * animTime * animTime * animTime * animTime + start;
            animTime -= 2;
            return -end * 0.5f * (animTime * animTime * animTime * animTime - 2) + start;
        }
        #endregion

        #region Quint
        /// <summary>
        /// Ease in quintic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInQuint(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return end * animTime * animTime * animTime * animTime * animTime + start;
        }
        /// <summary>
        /// Ease out quintic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseOutQuint(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            animTime--;
            end -= start;
            return end * (animTime * animTime * animTime * animTime * animTime + 1) + start;
        }
        /// <summary>
        /// Ease in out quintic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInOutQuint(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp(elapsedTime / duration / 0.5f, 0, 2);
            end -= start;
            if (animTime < 1) return end * 0.5f * animTime * animTime * animTime * animTime * animTime + start;
            animTime -= 2;
            return end * 0.5f * (animTime * animTime * animTime * animTime * animTime + 2) + start;
        }
        #endregion

        #region Sine
        /// <summary>
        /// Ease in sine
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInSine(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return -end * Mathf.Cos(animTime * (Mathf.PI * 0.5f)) + end + start;
        }
        /// <summary>
        /// Ease out sine
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseOutSine(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return end * Mathf.Sin(animTime * (Mathf.PI * 0.5f)) + start;
        }
        /// <summary>
        /// Ease in out sine
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInOutSine(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return -end * 0.5f * (Mathf.Cos(Mathf.PI * animTime) - 1) + start;
        }
        #endregion

        #region Expo
        /// <summary>
        /// Ease in exponential
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInExpo(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return end * Mathf.Pow(2, 10 * (animTime - 1)) + start;
        }
        /// <summary>
        /// Ease out exponential
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseOutExpo(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return end * (-Mathf.Pow(2, -10 * animTime) + 1) + start;
        }
        /// <summary>
        /// Ease in out exponential
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInOutExpo(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp(elapsedTime / duration / 0.5f, 0, 2);
            end -= start;
            if (animTime < 1) return end * 0.5f * Mathf.Pow(2, 10 * (animTime - 1)) + start;
            animTime--;
            return end * 0.5f * (-Mathf.Pow(2, -10 * animTime) + 2) + start;
        }
        #endregion

        #region Circ
        /// <summary>
        /// Ease in circular
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInCirc(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            return -end * (Mathf.Sqrt(1 - animTime * animTime) - 1) + start;
        }
        /// <summary>
        /// Ease out circular
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseOutCirc(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            animTime--;
            end -= start;
            return end * Mathf.Sqrt(1 - animTime * animTime) + start;
        }
        /// <summary>
        /// Ease in out circular
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInOutCirc(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp(elapsedTime / duration / 0.5f, 0, 2);
            end -= start;
            if (animTime < 1) return -end * 0.5f * (Mathf.Sqrt(1 - animTime * animTime) - 1) + start;
            animTime -= 2;
            return end * 0.5f * (Mathf.Sqrt(1 - animTime * animTime) + 1) + start;
        }
        #endregion

        #endregion

        #region Special Eases
        /// <summary>
        /// Ease spring
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float Spring(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            animTime = (Mathf.Sin(animTime * Mathf.PI * (0.2f + 2.5f * animTime * animTime * animTime)) * Mathf.Pow(1f - animTime, 2.2f) + animTime) * (1f + (1.2f * (1f - animTime)));
            return start + (end - start) * animTime;
        }

        #region Bounce
        /// <summary>
        /// Ease out bounce
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseOutBounce(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            if (animTime < (1 / 2.75f))
            {
                return end * (7.5625f * animTime * animTime) + start;
            }
            else if (animTime < (2 / 2.75f))
            {
                animTime -= (1.5f / 2.75f);
                return end * (7.5625f * (animTime) * animTime + .75f) + start;
            }
            else if (animTime < (2.5 / 2.75))
            {
                animTime -= (2.25f / 2.75f);
                return end * (7.5625f * (animTime) * animTime + .9375f) + start;
            }
            else
            {
                animTime -= (2.625f / 2.75f);
                return end * (7.5625f * (animTime) * animTime + .984375f) + start;
            }
        }
        #endregion

        #region Back
        /// <summary>
        /// Ease in back
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInBack(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;
            float s = 1.70158f;
            return end * (animTime) * animTime * ((s + 1) * animTime - s) + start;
        }
        /// <summary>
        /// Ease out back
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseOutBack(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            float s = 1.70158f;
            end -= start;
            animTime = animTime - 1;
            return end * (animTime * animTime * ((s + 1) * animTime + s) + 1) + start;
        }
        /// <summary>
        /// Ease in out back
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInOutBack(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp(elapsedTime / duration / 0.5f, 0, 2);
            float s = 1.70158f;
            end -= start;
            if ((animTime) < 1)
            {
                s *= (1.525f);
                return end * 0.5f * (animTime * animTime * (((s) + 1) * animTime - s)) + start;
            }
            animTime -= 2;
            s *= (1.525f);
            return end * 0.5f * (animTime * animTime * (((s) + 1) * animTime + s) + 2) + start;
        }
        #endregion

        #region Elastic
        /// <summary>
        /// Ease in elastic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInElastic(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;

            float d = 1f;
            float p = d * .3f;
            float s;
            float a = 0;

            if (animTime == 0) return start;

            if ((animTime /= d) == 1) return start + end;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
            }

            return -(a * Mathf.Pow(2, 10 * (animTime -= 1)) * Mathf.Sin((animTime * d - s) * (2 * Mathf.PI) / p)) + start;
        }
        /// <summary>
        /// Ease out elastic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseOutElastic(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;

            float d = 1f;
            float p = d * .3f;
            float s;
            float a = 0;

            if (animTime == 0) return start;

            if ((animTime /= d) == 1) return start + end;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p * 0.25f;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
            }

            return (a * Mathf.Pow(2, -10 * animTime) * Mathf.Sin((animTime * d - s) * (2 * Mathf.PI) / p) + end + start);
        }
        /// <summary>
        /// Ease in out elastic
        /// </summary>
        /// <param name="start">Start value of the animation.</param>
        /// <param name="end">End value of the animation.</param>
        /// <param name="elapsedTime">Current time.</param>
        /// <param name="duration">Duration of the animation.</param>
        public static float EaseInOutElastic(float start, float end, float elapsedTime, float duration)
        {
            float animTime = Mathf.Clamp01(elapsedTime / duration);
            end -= start;

            float d = 1f;
            float p = d * .3f;
            float s;
            float a = 0;

            if (animTime == 0) return start;

            if ((animTime /= d * 0.5f) == 2) return start + end;

            if (a == 0f || a < Mathf.Abs(end))
            {
                a = end;
                s = p / 4;
            }
            else
            {
                s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
            }

            if (animTime < 1) return -0.5f * (a * Mathf.Pow(2, 10 * (animTime -= 1)) * Mathf.Sin((animTime * d - s) * (2 * Mathf.PI) / p)) + start;
            return a * Mathf.Pow(2, -10 * (animTime -= 1)) * Mathf.Sin((animTime * d - s) * (2 * Mathf.PI) / p) * 0.5f + end + start;
        }
        #endregion

        #endregion
    }
}
