using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class NoiseSettings {
    public int seed;

    [Range(1, 10)]
    public int layers;

    [MinValue(0)]
    public float strength = .2f;

    [Range(0, 4)]
    public float baseRoughness = 1, roughness = 2;

    [Range(0, 1)]
    public float persistence = .5f;

    [Range(0, 2)]
    public float minValue = 1;

    public Vector3 center;
}