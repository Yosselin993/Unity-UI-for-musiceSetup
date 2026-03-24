[System.Serializable]
public class SongData
{
    public string title;
    public string artist;
    public string video_id;

    public SongData(string title, string artist, string video_id)
    {
        this.title = title;
        this.artist = artist;
        this.video_id = video_id;
    }
}