using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    Transform cam;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        offset = cam.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cam.position = target.position + offset;
    }
}
