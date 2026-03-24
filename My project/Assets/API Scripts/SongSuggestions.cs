using UnityEngine;
//We could delete this script, we have a better suited version of it in AutocompleteController, but I left it so we can agree on it
public class SongSuggestions : MonoBehaviour
{
   [System.Serializable]
    public class SongSuggestion
    {
        public string title;
        public string artist;
        public string video_id;
    }
}
