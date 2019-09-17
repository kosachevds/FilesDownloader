using System.Collections.Generic;
using System.Net;

class Downloader
{
    private int _maxSimultaneous;
    private List<Task> _downloadings;

    public Downloader(int simultaneousMax)
    {
        
    }

    private async Task DownloadFileAsync(string url, string filepath)
    {
        var client = new WebClient();
        await client.DownloadFileTaskAsync(url, filepath);
    }
}