using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject _target;
    public float _easing = 0.5f;

    void Update()
    {
        if (_target == null) return;

        Vector3 targetPosition = _target.transform.position;
        Vector3 cameraPosition = transform.position;

        Vector3 newCameraPosition = Vector3.Lerp(cameraPosition, targetPosition, _easing);

        newCameraPosition.y = cameraPosition.y;

        transform.position = newCameraPosition;
    }
}
