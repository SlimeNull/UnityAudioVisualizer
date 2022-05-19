using UnityEngine;

public abstract class SpecVisualizer : MonoBehaviour
{
    
    public float ScaleMul = 200;
    public float UpChangeLerp = 0.7f;
    public float DownChangeLerp = 0.5f;
    public abstract void ApplySpectrum(float[] spectrum);
}