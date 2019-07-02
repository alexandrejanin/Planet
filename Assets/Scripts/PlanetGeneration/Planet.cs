using UnityEngine;

public class Planet : MonoBehaviour {
    [SerializeField]
    private bool autoUpdate;

    [SerializeField]
    private bool flatShading;

    [SerializeField, Range(2, 256)]
    private int resolution;

    [HideInInspector]
    public ShapeSettings shapeSettings;

    [HideInInspector]
    public ColorSettings colorSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout, colorSettingsFoldout;

    private readonly ShapeGenerator shapeGenerator = new ShapeGenerator();
    private readonly ColorGenerator colorGenerator = new ColorGenerator();

    [SerializeField, HideInInspector]
    private MeshFilter[] meshFilters;

    private TerrainFace[] terrainFaces;

    public static Planet Instance => instance ? instance : instance = FindObjectOfType<Planet>();
    private static Planet instance;

    private float GetHeight(Vector3 pointOnUnitSphere) => shapeGenerator.GetHeight(pointOnUnitSphere);

    public Vector3 GetNearestSurfacePoint(Vector3 worldPos, float offset = 0f) {
        var directionFromCenter = (worldPos - transform.position).normalized;
        var height = GetHeight(Quaternion.Inverse(transform.rotation) * directionFromCenter);

        height += offset;

        return directionFromCenter * height;
    }


    private void Initialize() {
        shapeGenerator.UpdateSettings(shapeSettings);
        colorGenerator.UpdateSettings(colorSettings);

        if (meshFilters == null || meshFilters.Length == 0) {
            meshFilters = new MeshFilter[6];
        }

        terrainFaces = new TerrainFace[6];

        var directions = new[] {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for (var i = 0; i < 6; i++) {
            if (meshFilters[i] == null) {
                var meshObject = new GameObject("Mesh");
                meshObject.transform.SetParent(transform);

                meshObject.AddComponent<MeshRenderer>();

                meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.material;

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    public void GeneratePlanet() {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }

    private void GenerateMesh() {
        foreach (var terrainFace in terrainFaces) {
            terrainFace.ConstructMesh(flatShading);
        }

        colorGenerator.UpdateElevation(shapeGenerator.heightMinMax);
    }

    private void GenerateColors() {
        colorGenerator.UpdateColors();
    }

    private void OnValidate() {
        GeneratePlanet();
    }

    public void OnColorSettingsUpdated() {
        if (!autoUpdate) return;
        Initialize();
        GenerateColors();
    }

    public void OnShapeSettingsUpdated() {
        if (!autoUpdate) return;
        Initialize();
        GenerateMesh();
    }
}