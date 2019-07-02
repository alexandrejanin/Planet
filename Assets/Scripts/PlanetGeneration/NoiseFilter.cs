using UnityEngine;

public class NoiseFilter {
    private readonly NoiseSettings noiseSettings;

    private readonly Noise noise;

    public NoiseFilter(NoiseSettings noiseSettings) {
        this.noiseSettings = noiseSettings;
        noise = new Noise(noiseSettings.seed);
    }

    public float Evaluate(Vector3 point) {
        var noiseValue = 0f;
        var amplitude = 1f;
        var frequency = noiseSettings.baseRoughness;

        for (var i = 0; i < noiseSettings.layers; i++) {
            var v = noise.Evaluate(point * frequency + noiseSettings.center);
            noiseValue += (v + 1) / 2 * amplitude;
            frequency *= noiseSettings.roughness;
            amplitude *= noiseSettings.persistence;
        }

        noiseValue = Mathf.Max(noiseValue - noiseSettings.minValue, 0);

        return noiseValue * noiseSettings.strength;
    }
}