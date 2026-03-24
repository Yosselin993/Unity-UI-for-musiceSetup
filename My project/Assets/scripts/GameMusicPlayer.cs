using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// This script is supposed to be apart of the game scene, suposed to play the music in the playlist in order one after the other. 
// Supposed to attach this to the button that starts the game: 
/*public void OnStartGameButton()
{
    UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
}
*/
public class GameMusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioLoader audioLoader;

    private List<AudioClip> playlist = new List<AudioClip>();
    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(audioLoader.LoadAllSongs(OnSongsLoaded));
    }

    void OnSongsLoaded(List<AudioClip> clips)
    {
        playlist = clips;

        if (playlist.Count > 0)
        {
            PlaySong(0);
        }
        else
        {
            Debug.LogWarning("No songs loaded!");
        }
    }

    void PlaySong(int index)
    {
        currentIndex = index;

        audioSource.clip = playlist[index];
        audioSource.Play();

        StartCoroutine(WaitForSongToEnd());
    }

    IEnumerator WaitForSongToEnd()
    {
        // Wait until the current song finishes
        while (audioSource.isPlaying)
            yield return null;

        // Move to next song
        currentIndex++;

        if (currentIndex < playlist.Count)
        {
            PlaySong(currentIndex);
        }
        else
        {
            Debug.Log("Playlist finished!");
        }
    }
}