using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;
    public float smoothTime = 0.5f;
    private Vector3 velocity;

    public float maxZoom = 7f;
    public float minZoom = 7f;
    public float zoomLimiter = 10f;

    public Vector2 boundsMin;
    public Vector2 boundsMax;

    private Camera cam;


    private void Start()
    {
        cam = GetComponent<Camera>();
    }


    private void LateUpdate()
    {
        if (targets.Count == 0)
            return;

        Move();
        Zoom();
    }
        

    private void Move()
    {

        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, boundsMin.x, boundsMax.x), Mathf.Clamp(transform.position.y, boundsMin.y, boundsMax.y), transform.position.z);
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
                
        cam.orthographicSize = newZoom;
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;

    }
}
