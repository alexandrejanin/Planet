using UnityEngine;

public class TestCube : MonoBehaviour {
    [SerializeField]
    private float azimuth, inclination;

    private PlanetCoordinates planetCoordinates;

    private void Update() {
        planetCoordinates.Azimuth = azimuth;
        planetCoordinates.Inclination = inclination;

        transform.position = planetCoordinates.PlanetPosition;
        transform.up = transform.position - Planet.Instance.transform.position;
    }
}