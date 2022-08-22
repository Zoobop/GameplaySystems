using UnityEngine;

namespace Utility.ExtensionMethods
{
    public static class Vector3ExtensionMethods
    {
        /// <summary>
        /// 
        /// Checks if the original vector has reach its designated location while applying the stopping distance
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="to"></param>
        /// <param name="stoppingDistance"></param>
        /// <returns></returns>
        public static bool Reach(this Vector3 original, Vector3 to, float stoppingDistance)
        {
            return (original - to).sqrMagnitude < stoppingDistance * stoppingDistance;
        }

        /// <summary>
        /// 
        /// Gets a random point inside the specified radius
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Vector3 RandWithin(this Vector3 original, float radius)
        {
            var adjustedRadius = radius - .5f;

            double randomAngleX = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            double randomAngleZ = Random.Range(0f, 360f) * Mathf.Deg2Rad;

            var x = (float) (adjustedRadius * System.Math.Sin(randomAngleX));
            var z = (float) (adjustedRadius * System.Math.Cos(randomAngleZ));

            return new Vector3(original.x + x, original.y, original.z + z);
        }

        /// <summary>
        /// 
        /// Checks if the specified vector is within range determined by the radius
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="comparable"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static bool InRange(this Vector3 original, Vector3 comparable, float radius)
        {
            var x = false;
            var z = false;

            if (original.x + radius >= comparable.x && original.x - radius <= comparable.x)
                x = true;

            if (original.z + radius >= comparable.z && original.z - radius <= comparable.z)
                z = true;

            return x && z;
        }

        public static Vector3 ReflectIdentity(this Vector3 original, Vector3 other)
        {
            return original.x * other + original.y * other + original.z * other;
        }

        public static Vector3 InvertIdentity(this Vector3 original)
        {
            if (original != Vector3.up || original != Vector3.forward || original != Vector3.right) return original;

            var x = original.x == 0 ? 1 : 0;
            var y = original.y == 0 ? 1 : 0;
            var z = original.z == 0 ? 1 : 0;
            return new(x, y, z);
        }

        public static float Distance(this Vector3 original, in Vector3 other)
        {
            return (original - other).sqrMagnitude;
        }
    }
}