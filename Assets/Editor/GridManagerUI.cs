using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerUI : Editor
{
    private bool dynamic = false;

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();

        var myScript = (GridManager) target;

        GUILayout.Toggle(dynamic, "Change dynamically");

        if (GUILayout.Button("Create grid"))
            myScript.CreateGrid();

        if (GUILayout.Button("Clear grid"))
        {
            myScript.DestroyGrid();
            return;
        }

        if (dynamic && EditorGUI.EndChangeCheck())
        {
            myScript.DestroyGrid();
            myScript.CreateGrid();
        }
    }
}