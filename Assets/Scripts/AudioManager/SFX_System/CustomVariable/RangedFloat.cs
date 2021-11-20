
namespace SFXTC
{
    /// <summary>
    /// Represent a float value that can be contained between a minimum and a maximum value set by the attribute MinMaxRangeAttribute.
    /// </summary>
    [System.Serializable]
    public struct RangedFloat
    {
        public float minValue;
        public float maxValue;
    }
}
