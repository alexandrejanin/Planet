using UnityEngine;

public class ColorGenerator {
    private const int GradientResolution = 50;

    private ColorSettings settings;
    private Texture2D texture;
    
    private static readonly int ElevationMinMax = Shader.PropertyToID("_elevationMinMax");
    private static readonly int Texture = Shader.PropertyToID("_texture");

    public void UpdateSettings(ColorSettings newSettings) {
        settings = newSettings;

        if (texture == null) {
            texture = new Texture2D(GradientResolution, 1);
        }
    }

    public void UpdateElevation(HeightMinMax elevationHeightMinMax) {
        settings.material.SetVector(ElevationMinMax,
            new Vector4(elevationHeightMinMax.Min, elevationHeightMinMax.Max));
    }

    public void UpdateColors() {
        var colors = new Color[GradientResolution];
        for (var i = 0; i < GradientResolution; i++) {
            colors[i] = settings.gradient.Evaluate(i / (GradientResolution - 1f));
        }

        texture.SetPixels(colors);
        texture.Apply();
        settings.material.SetTexture(Texture, texture);
    }
}