using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(TurretScript))]
public class TurretEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //Reference to script in editor
        TurretScript turretScript = (TurretScript)target;

        //Fill in other fields that are not changed?
        DrawDefaultInspector();

        //Whitespace in editor
        EditorGUILayout.Space();

        //Show AutoRotate stuff based on boolean field for it
        if (turretScript.autoRotate)
        {
            turretScript.autoRotateMinAngle = EditorGUILayout.FloatField("Auto Rotate Min Angle", turretScript.autoRotateMinAngle);
            turretScript.autoRotateMaxAngle = EditorGUILayout.FloatField("Auto Rotate Max Angle", turretScript.autoRotateMaxAngle);
            turretScript.autoRotateSpeed = EditorGUILayout.FloatField("Auto Rotate Speed", turretScript.autoRotateSpeed);
        }

        //Apply changes in editor
        serializedObject.ApplyModifiedProperties();
    }
}
