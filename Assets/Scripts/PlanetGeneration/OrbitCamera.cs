using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour {
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float sensitivity = 1f;

    [SerializeField]
    private float minZoom = 2.5f, maxZoom = 50f, zoomSensitivity;

    [SerializeField]
    private float lerpRatio = .1f;

    private float zoom;
    private float angle;

    private void Start() {
        zoom = maxZoom;
        angle = 45;
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            var xMove = Input.GetAxisRaw("Mouse X") * sensitivity;
            var yMove = -Input.GetAxisRaw("Mouse Y") * sensitivity;

            target.RotateAround(Planet.Instance.transform.position, target.forward, xMove);
            target.RotateAround(Planet.Instance.transform.position, target.right, yMove);
        }

        var targetPosition = Planet.Instance.GetNearestSurfacePoint(target.position);

        target.position = Vector3.Lerp(target.position, targetPosition, lerpRatio);

        zoom = Mathf.Clamp(zoom - Input.mouseScrollDelta.y * zoomSensitivity, minZoom, maxZoom);

        // if zoomed out, center view
        angle = Math.Abs(zoom - maxZoom) < 0.01f ? 90f : 45f;

        var targetLocalPosition = new Vector3(
            0,
            Mathf.Sin(Mathf.Deg2Rad * angle) * zoom,
            -Mathf.Cos(Mathf.Deg2Rad * angle) * zoom
        );

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocalPosition, lerpRatio);

        var euler = transform.localEulerAngles;
        euler.x = Mathf.Lerp(euler.x, angle, lerpRatio);

        transform.localEulerAngles = euler;
    }
}