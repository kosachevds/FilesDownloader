using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.IO;

class Downloader
{
    private int _maxSimultaneous;
    private List<Task> _downloadings;

    public Downloader(int simultaneousMax)
    {
        this._maxSimultaneous = simultaneousMax;
        this._downloadings = new List<Task>(_maxSimultaneous);
    }

    public ILogger Logger {get; set; }

    public async Task DownloadAsync(string url, string filepath)
    {
        if (this._downloadings.Count >= _maxSimultaneous) {
            var cancelledDownloading = await Task.WhenAny(this._downloadings);
            this._downloadings.Remove(cancelledDownloading);
        }
        var newDownloading = DownloadFileAsync(url, filepath);
        this._downloadings.Add(newDownloading);
    }

    private async Task DownloadFileAsync(string url, string filepath)
    {
        var client = new WebClient();
        try
        {
            await client.DownloadFileTaskAsync(url, filepath);
        }
        catch (WebException e)
        {
            if (this.Logger == null)
            {
                throw;
            }
            this.Logger.LogError(e, $"Downloading of {Path.GetFileName(filepath)} failed");
        }
    }

    public async Task DownloadAllAsync(IEnumerable<DownloadingInfo> infos, string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        foreach (var item in infos)
        {
            var filename = System.IO.Path.Combine(directory, item.Filename);
            await DownloadAsync(item.Url, filename);
        }

        await Task.WhenAll(this._downloadings);
    }
}