using System.Linq;
using UnityEngine;

#if DEBUG
[ExecuteInEditMode]
#endif
public class Background : SpecVisualizer
{
    public Vector2 BaseScale = new Vector2(1, 1);
    public Vector2 AdditionScale = new Vector2(1, 1);

    private Vector2 cachedScale = new Vector2(1, 1);

    public override void ApplySpectrum(float[] spectrum)
    {
        float average = spectrum.Average();

        Vector2 oldScale = cachedScale;
        Vector3 newScale = BaseScale + AdditionScale * ScaleMul * average;

        if (newScale.x * newScale.y > oldScale.x * oldScale.y)
        {
            cachedScale = new Vector2(Mathf.Lerp(oldScale.x, newScale.x, UpChangeLerp), Mathf.Lerp(oldScale.y, newScale.y, UpChangeLerp));
        }
        else
        {
            cachedScale = new Vector2(Mathf.Lerp(oldScale.x, newScale.x, DownChangeLerp), Mathf.Lerp(oldScale.y, newScale.y, DownChangeLerp));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cachedScale = BaseScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = cachedScale;
    }
}
