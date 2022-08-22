using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCameraPosition : MonoBehaviour
{
    [SerializeField] private Transform _camPosition;

    public Vector3 CameraPosition => _camPosition.position;
}
