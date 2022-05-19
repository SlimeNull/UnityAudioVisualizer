using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicPlay : MonoBehaviour
{
    public int SampleCount = 4096;
    public int FrequencyStart = 20;
    public int FrequencyEnd = 2500;
    public SpecVisualizer[] Visualizers;
    
    AudioSource audioSource;
    float[] samples;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        samples = new float[SampleCount];

        StartCoroutine(RenderLoop());
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator RenderLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            AudioListener.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

            int frequencyStartIndex = GetFrequencyIndex(AudioSettings.outputSampleRate, samples.Length, FrequencyStart);
            int frequencyEndIndex = GetFrequencyIndex(AudioSettings.outputSampleRate, samples.Length, FrequencyEnd);
            int count = frequencyEndIndex - frequencyStartIndex + 1;

            float[] buffer = new float[count];
            Array.Copy(samples, frequencyStartIndex, buffer, 0, count);

            foreach (var visualizer in Visualizers)
            {
                visualizer.ApplySpectrum(buffer);
            }
        }
    }

    /// <summary>
    /// 获取频域数据中指定频率的索引
    /// </summary>
    /// <param name="sampleRate">采样率</param>
    /// <param name="sampleCount">采样数/频域数据数</param>
    /// <param name="frequency">所需频率</param>
    /// <returns>对应频率的索引 (如果参数错误, 则返回负值)</returns>
    int GetFrequencyIndex(int sampleRate, int sampleCount, int frequency)
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
