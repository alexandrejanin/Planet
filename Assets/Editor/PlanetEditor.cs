using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor {
    private Planet planet;
    private Editor shapeEditor;
    private Editor colourEditor;

    public override void OnInspectorGUI() {
        using (var check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();
            if (check.changed) {
                planet.GeneratePlanet();
            }
        }

        DrawSettingsEditor(
            planet.shapeSettings,
            planet.OnShapeSettingsUpdated,
            ref planet.shapeSettingsFoldout,
            ref shapeEditor
        );
        DrawSettingsEditor(
            planet.colorSettings,
            planet.OnColorSettingsUpdated,
            ref planet.colorSettingsFoldout,
            ref colourEditor
        );

        if (GUILayout.Button("Generate Planet")) {
            planet.GeneratePlanet();
        }
    }

    private void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout,
                                    ref Editor editor) {
        if (settings == null) return;

        foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
        using (var check = new EditorGUI.ChangeCheckScope()) {
            if (!foldout) return;

            CreateCachedEditor(settings, null, ref editor);
            editor.OnInspectorGUI();

            if (!check.changed) return;

            onSettingsUpdated?.Invoke();
        }
    }

    private void OnEnable() {
        planet = (Planet) target;
    }
}