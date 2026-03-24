using UnityEngine;
using TMPro;

public class StoredSongPrefabScript : MonoBehaviour
{
    public TMP_Text label;

    private SongData song;

    public void Setup(SongData s, int index)
    {
        song = s;

        if (label != null)
            label.text = $"{s.title} - {s.artist}";
    }

    public SongData GetSong()
    {
        return song;
    }

    public void DeleteThisSong()
    {
        SongManager.Instance.DeleteSong(song);
        Destroy(gameObject);
        PlaylistUI.Instance.RefreshUI();
    }
}