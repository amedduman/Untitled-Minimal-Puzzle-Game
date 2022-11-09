namespace amed.utils.math
{
    using UnityEngine;

    public static class adMath
    {
        public static bool DoesVectorsLookAtSameDirection(Vector3 a, Vector3 b, float margin)
        {
            float dot = Vector3.Dot(a.normalized, b.normalized);

            if (dot > 1 - margin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DoesVectorsPerpendicular(Vector3 a, Vector3 b, float margin)
        {
            float dot = Vector3.Dot(a.normalized, b.normalized);

            if (dot > 0)
            {
                if (dot <= margin)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (dot >= -margin)
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
}