# Robot Cleaner

This repository contains an attempt at solving the Robot Cleaner problem.

The solution is written in C# 8.0 and uses the .NET Core 3.1 runtime. The .NET Core SDK 3.1.100 is the minimum requirement.

For convenience, a build script is provided. To use it, you need to install the Cake bootstrapper provided as a .NET global tool.

```powershell
dotnet tool install --global Cake.Tool
```

Once the tool is installed, you can build the application by running this command on the root of the repository

```powershell
dotnet cake
```

The program will be built, tested and published in the `./outputs/publish` folder.