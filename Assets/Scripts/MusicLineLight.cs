using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class MusicLineLight : MonoBehaviour
{
    Light lightCom;
    MusicLine musicLineCom;
    //Light light;
    public GameObject MusicLineObject;
    // Start is called before the first frame update
    void Start()
    {
        lightCom = GetComponent<Light>();
        musicLineCom = MusicLineObject.GetComponent<MusicLine>();
    }

    // Update is called once per frame
    void Update()
    {
        lightCom.range = MusicLineObject.gameObject.transform.localScale.y / 3;
    }
}
