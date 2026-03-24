using System.Collections.Generic;
using UnityEngine;
//used new SongData instead, If you want to use the old one, just change the type in the list and the methods
//Was getting errors related to not actually storing the songs, but storing the downloaded paths
public class SongManager : MonoBehaviour
{
    public static SongManager Instance;

    public List<SongData> selectedSongs = new List<SongData>();
    public List<string> downloadedSongPaths = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddSong(SongData song)
    {
        selectedSongs.Add(song);
    }

    public List<SongData> GetStoredSongs()
    {
        return selectedSongs;
    }

    public void SetSongOrder(List<SongData> newOrder)
    {
        selectedSongs = newOrder;
    }

    public void DeleteSong(SongData song)
    {
        selectedSongs.Remove(song);
    }
}