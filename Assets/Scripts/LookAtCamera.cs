using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Mode mode;
    private enum Mode {
    LookAt,
    LookAtInverted,
    CameraFoward,
    CameraFowardInverted,
    }

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFormCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFormCamera);
                break;
            case Mode.CameraFoward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraFowardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
            default:
                break;
        }
    }
}
