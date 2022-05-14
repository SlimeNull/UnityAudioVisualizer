using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MusicLoad : MonoBehaviour
{
    private MusicPlay musicPlay;
    public GameObject MusicPlayObject;
    
    // Start is called before the first frame update
    void Start()
    {
        musicPlay = MusicPlayObject.GetComponent<MusicPlay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadMusicInner(Uri path)
    {
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.UNKNOWN);

        yield return request.SendWebRequest();
        Debug.Log("Music downloaded");

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Get music clip");
            
            AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
            musicPlay.SwitchMusic(clip);
        }
    }
    public void LoadMusic(InputField inputField)
    {
        string path = inputField.text.Replace('\\', '/');
        Uri uri = new Uri(path);

        Debug.Log($"LoadMusic: {path}");
        StartCoroutine(LoadMusicInner(uri));
    }
}
