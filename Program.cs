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

        static void Main(string[] args)
        {

        }

        static async Task Download(IEnumerable<DownloadingInfo> infos)
        {
            var downloader = new Downloader(MaxSimultaneousDownloadings);
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
        }

    }
}
