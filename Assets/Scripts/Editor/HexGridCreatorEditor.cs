using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HexGridCreator))]
public class HexGridCreatorEditor : Editor
{
    HexGridCreator hexGrid;
    public void OnEnable()
    {
        hexGrid = target as HexGridCreator;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        if (GUILayout.Button("CreateMesh"))
        {
            hexGrid.CreateMesh();
        }
    }
}
