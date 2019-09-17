using System.IO;
using System.Linq;
using System.Collections.Generic;

class DownloadingInfo
{
    public string Filename { get; set; }
    public string Url { get; set; }

    public static DownloadingInfo Parse(string strInfo)
    {
        var lastSpace = strInfo.LastIndexOf(' ');
        var url = strInfo.Substring(lastSpace + 1);
        var filename = strInfo.Substring(0, lastSpace);
        return new DownloadingInfo
        {
            Filename = filename,
            Url = url
        };
    }

    public static IEnumerable<DownloadingInfo> ReadInfos(string filename, char separator)
    {
        var content = File.ReadAllText(filename);
        return content
            .Split(separator)
            .Where(x => !System.String.IsNullOrWhiteSpace())
            .Select(Parse);
    }
}
