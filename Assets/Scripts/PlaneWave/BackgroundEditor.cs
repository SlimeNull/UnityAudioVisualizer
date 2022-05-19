using System;
using UnityEditor;
using UnityEngine;

#if DEBUG
[CustomEditor(typeof(Background))]
public class BackgroundEditor : Editor
{
    Background ctrl;
    private void OnEnable()
    {
        ctrl = (Background)target;
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Apply"))
        {
            ctrl.ApplySpectrum(Array.Empty<float>());
        }
    }
}
#endif
