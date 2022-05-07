using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if DEBUG
using UnityEditor;
#endif

[ExecuteInEditMode]
public class LineGen : MonoBehaviour
{
    public GameObject LinePrefab;

    public int LineCount = 30;
    public float CircleRadius = 10;

    [NonSerialized]
    public GameObject[] GeneratedLines;
    
    // Start is called before the first frame update
    public void Start()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        GeneratedLines = new GameObject[LineCount];
        
        float perLineAngle = Mathf.PI * 2 / LineCount;
        for (int i = 0; i < LineCount; i++)
        {
            float angle = -(perLineAngle * i);
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * CircleRadius;
            GameObject line = Instantiate(LinePrefab, transform);
            line.transform.localPosition = pos;

            GeneratedLines[i] = line;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

#if DEBUG
[CustomEditor(typeof(LineGen))]
public class LineGenEditor : Editor
{
    LineGen ScriptObj;
    private void OnEnable()
    {
        ScriptObj = (LineGen)target;
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
