using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class PlaneWave : SpecVisualizer
{
    public int PointCount = 100;

    LineRenderer lines;
    BoxCollider2D box;

    public override void ApplySpectrum(float[] spectrum)
    {
        for (int i = 0; i < lines.positionCount; i++)
        {
            Vector3 pos = lines.GetPosition(i);
            float oldHeight = pos.y;
            float newHeight = box.bounds.min.y + spectrum[i] * ScaleMul;

            if (newHeight > oldHeight)
            {
                lines.SetPosition(i, new Vector3(pos.x, Mathf.Lerp(oldHeight, newHeight, UpChangeLerp), pos.z));
            }
            else
            {
                lines.SetPosition(i, new Vector3(pos.x, Mathf.Lerp(oldHeight, newHeight, DownChangeLerp), pos.z));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lines = GetComponent<LineRenderer>();
        box = GetComponent<BoxCollider2D>();

        lines.positionCount = PointCount;

        Bounds bounds = box.bounds;
        float y = bounds.min.y;
        float z = bounds.min.z;

        float minX = bounds.min.x;
        float maxX = bounds.max.x;
        float xOffset = maxX - minX;

        for (int i = 0; i < PointCount; i++)
        {
            lines.GetPosition(i);
            float x = minX + xOffset * i / (PointCount - 1);

            lines.SetPosition(i, new Vector3(x, y, z));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
