using UnityEngine;

public class ShapeGenerator {
    private ShapeSettings shapeSettings;
    private NoiseFilter noiseFilter;
    public HeightMinMax heightMinMax;

    public void UpdateSettings(ShapeSettings newShapeSettings) {
        shapeSettings = newShapeSettings;

        noiseFilter = new NoiseFilter(newShapeSettings.noiseSettings);

        heightMinMax = new HeightMinMax();
    }

    public float GetHeight(Vector3 pointOnUnitSphere) {
        var elevation = noiseFilter.Evaluate(pointOnUnitSphere);

        elevation = shapeSettings.radius * (1 + elevation);

        return elevation;
    }

    public Vector3 GetPoint(Vector3 pointOnUnitSphere, bool addToMinMax = false) {
        var height = GetHeight(pointOnUnitSphere);

        if (addToMinMax) heightMinMax.AddValue(height);

        return pointOnUnitSphere * height;
    }
}