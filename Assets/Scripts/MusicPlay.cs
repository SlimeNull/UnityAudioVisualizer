using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MusicPlay : MonoBehaviour
{
    public int SampleCount = 4096;
    public GameObject LinesParentObject;

    [Range(0, 1)]
    public float ChangeLerp = 0.5f;

    private AudioSource audioSource;

    public float WaveMul = 50f;
    
    float[] samples;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        samples = new float[SampleCount];

        StartCoroutine(RenderLoop());
    }

    int frequencyStart = 20;
    int frequencyEnd = 2500;

    // Update is called once per frame
    void Update()
    {
        var srcRoEl = transform.localRotation.eulerAngles;
        var newRoEl = new Vector3(srcRoEl.x, srcRoEl.y + Time.deltaTime * 5, srcRoEl.z);
        transform.rotation = Quaternion.Euler(newRoEl);
    }

    IEnumerator RenderLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);

            Transform[] children = LinesParentObject.GetComponentsInChildren<Transform>();

            AudioListener.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

            int sampleOffset = frequencyEnd - frequencyStart;
            int binSample = sampleOffset / children.Length;

            for (int i = 1; i < children.Length; i++)
            {
                int dataIndex = GetIndex(AudioSettings.outputSampleRate, samples.Length, frequencyStart + i * binSample);
                //int dataIndex = i - 1;

                if (dataIndex == 0)
                    dataIndex = 1;

                Vector3 srcScale = children[i].localScale;
                Vector3 newScale = new Vector3(srcScale.x, Mathf.Max(0.1f, samples[dataIndex] * WaveMul), srcScale.z);
                children[i].localScale = Vector3.Lerp(srcScale, newScale, ChangeLerp);
            }
        }
    }

    int GetIndex(int sampleRate, int sampleCount, int frequency)
    {
        if (frequency > sampleRate)
        {
            return -1;
        }

        if (frequency > sampleRate / 2)
        {
            return -2;
        }
        
        float binSize = sampleRate / (float)sampleCount;
        return (int)(frequency / binSize);
    }

    public void SwitchMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();

        Debug.Log("Switch song");
    }
}
