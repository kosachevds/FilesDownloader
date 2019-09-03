using System;
using System.Collections.Generic;

namespace FilesDownloader
{
    class Program
    {

        class DownloadingInfo {
            public string Filename { get; set; }
            public string Url { get; set; }

            static DownloadingInfo Parse(string strInfo) {
                var lastSpace = strInfo.LastIndexOf(' ');
                var url = strInfo.Substring(lastSpace + 1);
                var filename = strInfo.Substring(0, lastSpace);
                return new DownloadingInfo {
                    Filename = filename,
                    Url = url
                };
            }

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static IEnumerable<DownloadingInfo> ReadInfos(string filename, char separator) {

        }

    }



}
