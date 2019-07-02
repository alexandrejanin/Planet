using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class ShapeSettings : ScriptableObject {
    [MinValue(0.1f)]
    public float radius = 1.0f;

    public NoiseSettings noiseSettings;
}