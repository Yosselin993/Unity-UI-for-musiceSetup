using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DownloadHandler : MonoBehaviour
{
    public static DownloadHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void StartDownload(string videoId, string title)
    {
        // Check if already downloaded
        if (SongManager.Instance.downloadedById.ContainsKey(videoId))
        {
            Debug.Log("Song already downloaded, reusing file.");
            SongManager.Instance.AddDownloadedSongPath(SongManager.Instance.downloadedById[videoId]);
            return;
        }

        StartCoroutine(DownloadSong(videoId, title));
    }

    private IEnumerator DownloadSong(string videoId, string title)
    {
        string url = $"http://127.0.0.1:8000/download_song?video_id={videoId}&title={UnityWebRequest.EscapeURL(title)}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Download error: " + request.error);
                yield break;
            }

            DownloadResponse response = JsonUtility.FromJson<DownloadResponse>(request.downloadHandler.text);

            if (response.status == "success")
            {
                Debug.Log("Downloaded: " + response.path);
                // Save path by video_id
                SongManager.Instance.downloadedById[videoId] = response.path;
                SongManager.Instance.downloadedSongPaths.Add(response.path);
            }
            else
            {
                Debug.LogError("Python error: " + response.message);
            }
        }
    }
}

[System.Serializable]
public class DownloadResponse
{
    public string status;
    public string path;
    public string message;
}