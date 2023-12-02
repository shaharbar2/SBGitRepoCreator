
![C# Version](https://img.shields.io/badge/C%23-8.0-blue.svg)
![Unity Version](https://img.shields.io/badge/Unity-2020.1+-blue.svg)
![License](https://img.shields.io/badge/license-MIT-green.svg)
![Open Source](https://img.shields.io/badge/Open%20Source-%E2%9C%93-brightgreen.svg)
![Last Updated](https://img.shields.io/badge/last%20updated-2023-12-02-lightgrey.svg)
![Platform](https://img.shields.io/badge/platform-Unity%20Editor-lightgrey.svg)
![GitHub repo size](https://img.shields.io/github/repo-size/YourGitHubUserName/Git Repo Creator)
![GitHub Repo stars](https://img.shields.io/github/stars/YourGitHubUserName/Git Repo Creator?style=social)
![GitHub forks](https://img.shields.io/github/forks/YourGitHubUserName/Git Repo Creator?style=social)

# Git Repo Creator

Will be used to create upm packages
![img.png](img.png)
## Installation
![OpenUPM](https://img.shields.io/badge/UPM-1.0.0-blue.svg)
![GitHub tag (latest SemVer)](https://img.shields.io/github/tag/YourGitHubUserName/Git Repo Creator?label=latest%20release)
- using OpenUPM: https://openupm.com/packages/com.sb.package-maker/
- using git:

  Add the following line to the `dependencies` section of your project's `manifest.json` file:
  ```json 
  "com.sb.git-repo-creator": "https: //github.com/YourGitHubUserName/Git Repo Creator.git?path=/Packages/com.sb.git-repo-creator#main"

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
                For improvements or bug reports, please reach out to the maintainer at `https: //github.com/YourGitHubUserName/Git Repo Creator`.
            Feel free to fork and create Pull Requests.
## License
                This tool is distributed under the MIT License.See the included LICENSE.md file for more
            details.
                For more details about the license, see

            [LICENSE.md](LICENSE.md)