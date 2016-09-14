using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CubeSpawner))]
public class CubeSpawnerEditor : Editor {
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        GUILayout.Space(10);

        var t = target as CubeSpawner;

        if (GUILayout.Button("Spawn cubes"))
        {
            t.CmdSpawnCubes();
        }


        GUILayout.Space(10);
        EditorGUILayout.LabelField("Cubes spawned: " + t.SpawnCount);
    }
}
