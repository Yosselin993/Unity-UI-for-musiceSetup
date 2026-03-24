using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PlaylistUI : MonoBehaviour
{
    public static PlaylistUI Instance;

    public Transform playlistContainer;
    public GameObject storedSongPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform child in playlistContainer)
            Destroy(child.gameObject);

        var songs = SongManager.Instance.GetStoredSongs();

        for (int i = 0; i < songs.Count; i++)
        {
            var data = songs[i];
            GameObject obj = Instantiate(storedSongPrefab, playlistContainer);
            var item = obj.GetComponent<StoredSongPrefabScript>();
            item.Setup(data, i);
        }
    }

    public void UpdateOrderFromUI()
    {
        List<SongData> newOrder = new List<SongData>();

        for (int i = 0; i < playlistContainer.childCount; i++)
        {
            var item = playlistContainer.GetChild(i).GetComponent<StoredSongPrefabScript>();
            newOrder.Add(item.GetSong());
        }

        SongManager.Instance.SetSongOrder(newOrder);
    }
}