namespace AD.Utils.Math
{
    using UnityEngine;

    public static class AD_MathUtils
    {
        public static bool CloseTo(float val1, float val2, float margin)
        {
            val1 = Mathf.Abs(val1);
            val2 = Mathf.Abs(val2);

            float diff = val1 - val2;

            diff = Mathf.Abs(diff);

            return diff <= margin;
        }

        public static bool DoesVectorsLookAtSameDirection(Vector3 a, Vector3 b, float margin)
        {
            float dot = Vector3.Dot(a.normalized, b.normalized);

            if(dot > 1 - margin)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

    }
}