using UnityEngine;

namespace Utility.ExtensionMethods
{
    public static class TransformExtensionMethods
    {
        public static void SetPosition(this Transform transform, Vector3 position)
        {
            transform.position = position;
        }
        
        public static void SetLocalPosition(this Transform transform, Vector3 position)
        {
            transform.localPosition = position;
        }
        
        public static void SetRotation(this Transform transform, Quaternion quaternion)
        {
            transform.rotation = quaternion;
        }
        
        public static void SetRotation(this Transform transform, Vector3 eulerAngles)
        {
            transform.rotation = Quaternion.Euler(eulerAngles);
        }
    }
}