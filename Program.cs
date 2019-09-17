using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilesDownloader
{
    class Program
    {
        const string Filename = "test_tracks.txt";
        const string ResultFolder = "test_files";
        const int MaxSimultaneousDownloadings = 8;
        const char FileInfoSeparator = ';';
        const int MaxDotCount = 10;

        static void Main(string[] args)
        {
            var infos = DownloadingInfo.ReadInfos(Filename, FileInfoSeparator);
            var downloading = Download(infos, MaxSimultaneousDownloadings);
            PrintDotsWhile(downloading, MaxDotCount);
        }

        static async Task Download(IEnumerable<DownloadingInfo> infos, int maxSimultaneous)
        {
            var downloader = new Downloader(maxSimultaneous);
            await downloader.DownloadAllAsync(infos, ResultFolder);
        }

        static void PrintDotsWhile(Task task, int maxDots)
        {
            var dotCount = 0;
            while (task.Status == TaskStatus.Running)
            {
                if (dotCount == maxDots)
                {
                    dotCount = 0;
                    Console.Clear();
                }
                Console.Write(".");
                ++dotCount;
            }
            task.Wait();
        }
    }
}
