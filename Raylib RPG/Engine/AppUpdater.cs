using Newtonsoft.Json.Linq;
using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine
{
    internal class AppUpdater
    {
        private const string GitHubRepo = "VictorFlorescu/Raylib-RPG";
        private const string CurrentVersion = "0.1";

        public static async Task Update()
        {
            await CheckForUpdatesAsync();
        }

        public static async Task CheckForUpdatesAsync()
        {
            try
            {
                var latestRelease = await GetLatestReleaseAsync(GitHubRepo);
                var latestVersion = latestRelease["version"];
                var downloadUrl = latestRelease["download_url"];

                if (IsNewerVersion(Convert.ToString(latestVersion), CurrentVersion))
                {
                    Console.WriteLine($"New version available: {latestVersion}");
                    MessageBox.Show($"New version available: {latestVersion}");
                    var filename = await DownloadUpdateAsync(Convert.ToString(downloadUrl));
                    InstallUpdate(filename);
                    Console.WriteLine(filename);
                    Console.WriteLine("Update installed successfully.");
                    MessageBox.Show("Update installed successfully.");
                }
                else
                {
                    Console.WriteLine("No updates available");
                    MessageBox.Show("No updates available");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking for updates: {ex.Message}");
                MessageBox.Show($"Error checking for updates: {ex.Message}");
            }
        }

        private static async Task<JObject> GetLatestReleaseAsync(string repo)
        {
            using (var client = new HttpClient())
            {
                var url = $"https://api.github.com/repos/{repo}/releases/latest";
                client.DefaultRequestHeaders.Add("User-Agent", "App");
                var response = await client.GetStringAsync(url);
                var releaseInfo = JObject.Parse(response);

                return new JObject
                {
                    ["version"] = releaseInfo["tag_name"],
                    ["download_url"] = releaseInfo["assets"][0]["browser_download_url"]
                };
            }
        }

        private static bool IsNewerVersion(string latestVersion, string currentVersion)
        {
            return Version.Parse(latestVersion).CompareTo(Version.Parse(currentVersion)) > 0;
        }

        private static async Task<string> DownloadUpdateAsync(string downloadUrl)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(downloadUrl);
                var filename = $"RPG Game - Update.rar";
                using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fs);
                }
                return filename;
            }
        }

        private static void InstallUpdate(string filename)
        {
            ZipFile.ExtractToDirectory(filename, ".",true);
            File.Delete(filename);
        }
        
    }
}
