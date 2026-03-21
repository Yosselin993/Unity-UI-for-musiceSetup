using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  public void OpenMusic()
    {
        SceneManager.LoadScene("MusicSetup");
    }
}
