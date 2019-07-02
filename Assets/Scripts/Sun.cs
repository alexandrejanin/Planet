using UnityEngine;

public class Sun : MonoBehaviour {
    [SerializeField]
    private float rate = 10f;

    [SerializeField]
    private Vector3 axis = Vector3.up;

    private void Update() {
        transform.RotateAround(transform.position, axis, rate * Time.deltaTime);
    }
}