using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CubeMovement))]
public class CubeMovementEditor : Editor
{

    private Vector3 _forceAdd;
    private Vector3 _torqueAdd;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(10);

        var t = target as CubeMovement;

        EditorGUILayout.LabelField("Apply force and torque", EditorStyles.boldLabel);
        _forceAdd = EditorGUILayout.Vector3Field("Force", _forceAdd);
        _torqueAdd = EditorGUILayout.Vector3Field("Torque", _torqueAdd);
        if (GUILayout.Button("Apply"))
        {
            t.GetComponent<Rigidbody>().AddForce(_forceAdd);
            t.GetComponent<Rigidbody>().AddTorque(_torqueAdd);
        }

    }
}
