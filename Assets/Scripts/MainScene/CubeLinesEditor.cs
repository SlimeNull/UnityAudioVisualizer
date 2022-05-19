using UnityEngine;

#if DEBUG
using UnityEditor;
[CustomEditor(typeof(CubeLines))]
public class CubeLinesEditor : Editor
{
    CubeLines ScriptObj;
    private void OnEnable()
    {
        ScriptObj = (CubeLines)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Render content"))
        {
            ScriptObj.Start();
        }
    }
}
#endif
