public class SuggestionItem : MonoBehaviour
{ // We can also delete this, lines 83-92 in AutocompleteController.cs are doing the same thing now, its a bit more direct
    public TextMeshProUGUI label;

    private string videoId;
    private string title;
    private string artist;

    public void Setup(string title, string artist, string videoId)
    {
        this.title = title;
        this.artist = artist;
        this.videoId = videoId;

        if (label != null)
            label.text = $"{title} - {artist}";
    }

    public void OnClick()
    { 
        DownloadHandler.Instance.StartDownload(videoId, title, artist);
    }
}