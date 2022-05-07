using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MusicLine : MonoBehaviour
{
    static float emissionMul = 1f;
    static float lightMul = 1f;

    public float EmissionMul { get => emissionMul; set => emissionMul = value; }
    public float LightMul { get => lightMul; set => lightMul = value; }

    public Light LightObj;
    
    private MeshRenderer meshRenderer;
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        float height = transform.localScale.y;

        LightObj.intensity = height * LightMul;
        //Color.RGBToHSV(material.GetColor("_EmissionColor"), out float h, out float s, out float v);
        //material.SetColor("_EmissionColor", Color.HSVToRGB(h, s, Mathf.Clamp(height / 4 * EmissionMul, 0.5f, 1)));

        material.SetFloat("_EmissionStrength", Mathf.Clamp(height / 4 * EmissionMul, 0.5f, 1));
    }
}
