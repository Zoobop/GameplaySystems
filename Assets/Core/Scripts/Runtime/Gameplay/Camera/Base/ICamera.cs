using Cinemachine;
using UnityEngine;

namespace CameraSystem
{
    public interface ICamera
    {
        public void SetFocus(Transform transform);
        public ICinemachineCamera GetCamera();
        public void Enable();
        public void Disable();
    }
}