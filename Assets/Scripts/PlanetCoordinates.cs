using UnityEngine;

public struct PlanetCoordinates {
    private float azimuth;
    private float inclination;

    private bool dirty;
    private Vector3 cachedPosition;

    public float Azimuth {
        get => azimuth;
        set {
            while (value < 0f) value += 360f;
            while (value >= 360f) value -= 360f;

            azimuth = value;
            dirty = true;
        }
    }

    public float Inclination {
        get => inclination;
        set {
            while (value < 0f) value += 180f;
            while (value >= 180f) value -= 180f;

            inclination = value;
            dirty = true;
        }
    }

    public Vector3 PlanetPosition {
        get {
            if (!dirty)
                return cachedPosition;

            cachedPosition = GetPosition(azimuth, inclination);
            dirty = false;

            return cachedPosition;
        }
    }

    public PlanetCoordinates(float azimuth = 0, float inclination = 0) {
        this.azimuth = azimuth;
        this.inclination = inclination;
        dirty = true;
        cachedPosition = Vector3.zero;
    }

    private static Vector3 GetPosition(float azimuth, float inclination) {
        azimuth *= Mathf.Deg2Rad;
        inclination *= Mathf.Deg2Rad;

        var unitVector = new Vector3(
            Mathf.Cos(azimuth) * Mathf.Sin(inclination),
            Mathf.Cos(inclination),
            Mathf.Sin(azimuth) * Mathf.Sin(inclination)
        );

        return Planet.Instance.GetNearestSurfacePoint(unitVector);
    }
}