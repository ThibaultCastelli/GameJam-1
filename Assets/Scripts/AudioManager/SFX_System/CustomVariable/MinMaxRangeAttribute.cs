using UnityEngine;

namespace SFXTC
{
    /// <summary>
    /// Attribute to set the max and min values of RangedFloat
    /// </summary>
    public class MinMaxRangeAttribute : PropertyAttribute
    {
        public float Min { get; private set; }
        public float Max { get; private set; }

        public MinMaxRangeAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}
