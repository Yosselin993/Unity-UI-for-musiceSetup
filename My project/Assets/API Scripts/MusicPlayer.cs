using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;


[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private int currentIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayCurrentSong();
    }

    void PlayCurrentSong()
    {
        if (SongManager.Instance.downloadedSongPaths.Count == 0)
            return;
        
        string path = SongManager.Instance.downloadedSongPaths[currentIndex];
        StartCoroutine(LoadAndPlay(path));
    }


    IEnumerator LoadAndPlay(string path)
    {

        if (!Path.IsPathRooted(path))
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            path = Path.Combine(projectRoot, path);
        }

        if (!File.Exists(path))
        {
            Debug.LogError("File not found: " + path);
            yield break;
        }

        string uri = new System.Uri(path).AbsoluteUri;

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(uri, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);

                if (clip == null)
                {
                    Debug.LogError("AudioClip is null.");
                    yield break;
                }

                audioSource.clip = clip;
                audioSource.Play();
                StartCoroutine(WaitForSongToEnd());
            }
            else
            {
                Debug.LogError("There is something wrong with www: " + www.error);
            }
        }
    }

    IEnumerator WaitForSongToEnd()
    {
        while (audioSource.isPlaying)
            yield return null;

        PlayNextSong();
    }

    void PlayNextSong()
    {
        currentIndex++;
        if (currentIndex >= SongManager.Instance.downloadedSongPaths.Count)
        {
            Debug.Log("End of playlist.");
            return;
        }
        PlayCurrentSong();
    }
}