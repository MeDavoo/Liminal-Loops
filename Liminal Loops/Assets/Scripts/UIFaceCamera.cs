using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    public Camera MainCamera;
    void LateUpdate()
    {
        transform.LookAt(MainCamera.transform.position, Vector3.up);
    }
}
