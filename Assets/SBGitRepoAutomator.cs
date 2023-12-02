using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using Debug = UnityEngine.Debug;

namespace Shahar.Bar.Utils
{
    public class SBGitRepoAutomator : EditorWindow
    {
        private string _nickName = "";
        private string _repoName = "MyUnityProject";
        private readonly string _repoPath = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        private string _organization = "";
        private bool _isPublic = true;

        [MenuItem("SBTools/Git Repo Automator")]
        private static void Init()
        {
            var window = (SBGitRepoAutomator)GetWindow(typeof(SBGitRepoAutomator));
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Setup Git and GitHub Repository", EditorStyles.boldLabel);
            _repoName = EditorGUILayout.TextField("Repository Name", _repoName);
            _organization = EditorGUILayout.TextField("GitHub Organization", _organization);
            _nickName = EditorGUILayout.TextField("Repository Nickname", _nickName);
            _isPublic = EditorGUILayout.Toggle("Public?", _isPublic);

            if (GUILayout.Button("Setup Repository"))
            {
                SetupRepository();
            }
        }

        private void SetupRepository()
        {
            RunCommand("git", "init", _repoPath);
            CreateFile(_repoPath, ".gitignore", GetGitIgnoreContent());
            CreateFile(_repoPath, "README.md", GetReadMeContent(_nickName, "YourGitHubUserName", GetFirstPackageFolderName())); // Replace "YourGitHubUserName" with actual username
            RunCommand("git", "checkout -b main", _repoPath);
            RunCommand("git", "add .", _repoPath);
            RunCommand("git", "commit -m \"Initial commit\"", _repoPath);

            var stringIsPublic = _isPublic ? "public" : "private";
            var ghCreateRepoCommand = $"repo create {_organization}/{_repoName} --{stringIsPublic}";
            RunCommand("gh", ghCreateRepoCommand, _repoPath);
            
            RunCommand("git", $"remote add origin git@github.com:{_organization}/{_repoName}.git", _repoPath);
            RunCommand("git", "tag 1.0.0", _repoPath);
            RunCommand("git", "push -u origin main --tags", _repoPath);
            RunCommand("git", "fetch", _repoPath);
            RunCommand("git", "status", _repoPath);
        }

        private void RunCommand(string command, string arguments, string workingDirectory)
        {
            var ghFullPath = "/opt/homebrew/bin/gh"; 
            var executable = command.Equals("gh") ? ghFullPath : command;

            var startInfo = new ProcessStartInfo(executable, arguments)
            {
                WorkingDirectory = workingDirectory,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            Debug.Log($"Running command: {command} {arguments}");

            using var process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
        
            process.WaitForExit();

            if (!string.IsNullOrEmpty(output))
            {
                Debug.Log("Output: " + output);
            }

            if (!string.IsNullOrEmpty(error))
            {
                Debug.LogError("Error: " + error);
            }
        }

        private void CreateFile(string path, string fileName, string content)
        {
            File.WriteAllText(Path.Combine(path, fileName), content);
        }


        private string GetFirstPackageFolderName()
        {
            var packagesDir = Path.Combine(_repoPath, "Packages");
            if (!Directory.Exists(packagesDir)) return "com.sb.example-package";
            var directories = Directory.GetDirectories(packagesDir);
            return directories.Length > 0 ? Path.GetFileName(directories[0]) : "com.sb.example-package";
        }

        private string GetReadMeContent(string repoName, string userName, string packageName)
        {
            var currentDateString = System.DateTime.Now.ToString("yyyy-MM-dd");
            return $@"
![C# Version](https://img.shields.io/badge/C%23-8.0-blue.svg)
![Unity Version](https://img.shields.io/badge/Unity-2020.1+-blue.svg)
![License](https://img.shields.io/badge/license-MIT-green.svg)
![Open Source](https://img.shields.io/badge/Open%20Source-%E2%9C%93-brightgreen.svg)
![Last Updated](https://img.shields.io/badge/last%20updated-{currentDateString}-lightgrey.svg)
![Platform](https://img.shields.io/badge/platform-Unity%20Editor-lightgrey.svg)
![GitHub repo size](https://img.shields.io/github/repo-size/{userName}/{repoName})
![GitHub Repo stars](https://img.shields.io/github/stars/{userName}/{repoName}?style=social)
![GitHub forks](https://img.shields.io/github/forks/{userName}/{repoName}?style=social)

# {repoName}

Will be used to create upm packages
![img.png](img.png)
## Installation
![OpenUPM](https://img.shields.io/badge/UPM-1.0.0-blue.svg)
![GitHub tag (latest SemVer)](https://img.shields.io/github/tag/{userName}/{repoName}?label=latest%20release)
- using OpenUPM: https://openupm.com/packages/com.sb.package-maker/
- using git:

  Add the following line to the `dependencies` section of your project's `manifest.json` file:
  ```json 
  ""{packageName}"": ""https: //github.com/{userName}/{repoName}.git?path=/Packages/{packageName}#main""

## Features

            -**User Interface:
            **Offers a simple and intuitive GUI within the Unity Editor for setting up
            and creating UPM packages.
            -**Folder Selection:
            **Allows users to select the source folder containing the Unity project to be packaged.
            -**Package Configuration:
            **Users can specify package details like name, display name, description, and version.
            -**Automatic File Organization:
            **Classifies and organizes scripts into appropriate subfolders(Editor, Tests, Runtime) based on their content.
            -**Package.json Generation:
            **Automatically generates a `package.json` file with the specified package details.
            -**Assembly Definition Files:
            **Creates `.asmdef` files for different subfolders
            to ensure correct compilation and separation of code.
            -**License and Readme:
            **Generates a standard MIT license file and a README.md for the package
            with installation instructions.

## How to Use

            1. **Open SBPackageMaker:
            **In Unity, navigate to `SBTools > UPM Package Creator` to open the SBPackageMaker window.
            2. **Select Source Folder:
            **Click on 'Select Source Folder' to choose the folder containing your Unity project.
            3. **Configure Package:
            **Fill in the package details like name, display name, description, and version.
            4. **Create Package:
            **Click on 'Create Package' to generate the UPM package in the designated output directory.

## Requirements

            -Unity 2020.1 or higher.
            -The tool must be placed within the Unity Editor project.
## Contributions
                For improvements or bug reports, please reach out to the maintainer at `https: //github.com/{userName}/{repoName}`.
            Feel free to fork and create Pull Requests.
## License
                This tool is distributed under the MIT License.See the included LICENSE.md file for more
            details.
                For more details about the license, see

            [LICENSE.md](LICENSE.md)";
        }

        private string GetGitIgnoreContent()
        {
            return @"
/[Ll]ibrary/
/[Tt]emp/
/[Oo]bj/
/[Bb]uild/
/[Bb]uilds/
/[Ll]ogs/
/[Uu]ser[Ss]ettings/
/[Mm]emoryCaptures/
/[Rr]ecordings/
/[Aa]ssets/Plugins/Editor/JetBrains*
.vs/
.gradle/
ExportedObj/
.consulo/
*.csproj
*.unityproj
*.sln
*.suo
*.tmp
*.user
*.userprefs
*.pidb
*.booproj
*.svd
*.pdb
*.mdb
*.opendb
*.VC.db
*.pidb.meta
*.pdb.meta
*.mdb.meta

sysinfo.txt
*.apk
*.aab
*.unitypackage
*.app

crashlytics-build.properties

/[Aa]ssets/[Aa]ddressable[Aa]ssets[Dd]ata/*/*.bin*
/[Aa]ssets/[Ss]treamingAssets/aa.meta
/[Aa]ssets/[Ss]treamingAssets/aa/*
.idea/.idea.GitCommander/.idea
";
        }
    }
}