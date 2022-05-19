using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if DEBUG
#endif

[ExecuteInEditMode]
public class CubeLines : SpecVisualizer
{
    public GameObject LinePrefab;

    public int LineCount = 64;
    public float CircleRadius = 20;

    private GameObject[] GeneratedLines;
    
    public override void ApplySpectrum(float[] spectrum)
    {
        for (int i = 0; i < GeneratedLines.Length; i++)
        {
            float value = spectrum[(int)(i / (float)LineCount * spectrum.Length)];
            float oldScale = GeneratedLines[i].transform.localScale.y;
            float newScale = value * ScaleMul;
            
            if (newScale > oldScale)
            {
                GeneratedLines[i].transform.localScale = new Vector3(1, Mathf.Lerp(oldScale, newScale, UpChangeLerp), 1);
            }
            else
            {
                GeneratedLines[i].transform.localScale = new Vector3(1, Mathf.Lerp(oldScale, newScale, DownChangeLerp), 1);
            }
        }
    }
    
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
        var srcRoEl = transform.localRotation.eulerAngles;
        var newRoEl = new Vector3(srcRoEl.x, srcRoEl.y + Time.deltaTime * 5, srcRoEl.z);
        transform.rotation = Quaternion.Euler(newRoEl);
    }
}

#if DEBUG
#endif
