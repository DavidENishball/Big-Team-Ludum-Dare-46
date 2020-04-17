using UnityEngine;
using UnityEditor;

public class Builder
{
    [MenuItem("Builder/Build Linux")]
    public static void StartLinux()
    {
        var path = "/home/tjern/GAMEDEV/demozone/" + Application.productName;
        var filename = Application.productName;
        BuildPlayer(BuildTarget.StandaloneLinux64, filename, path + "/");
    }

    [MenuItem("Builder/Build All")]
    public static void StartAll()
    {
        var path = "/home/tjern/GAMEDEV/demozone/" + Application.productName;
        var filename = Application.productName;
        BuildPlayer(BuildTarget.StandaloneLinux64, filename, path + "/");
        BuildPlayer(BuildTarget.StandaloneWindows64, filename, path + "/");
        //BuildPlayer(BuildTarget.StandaloneOSX, filename, path + "/");

    }

    static void BuildPlayer(BuildTarget buildTarget, string filename, string path)
    {
        string fileExtension = "";
        string dataPath = "";
        string modifier = "";

        switch (buildTarget)
        {
            case BuildTarget.StandaloneWindows64:
                modifier = "_windows";
                fileExtension = ".exe";
                dataPath = "_Data/";
                break;
            case BuildTarget.StandaloneOSX:
                // lol mac
                modifier = "_mac-osx";
                fileExtension = ".app";
                dataPath = fileExtension + "/Contents/";
                break;
            case BuildTarget.StandaloneLinux64:
                modifier = "_linux";
                fileExtension = ".x64";
                dataPath = "_Data/";
                break;
        }
        EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);

        var buildPath = path + filename + modifier + "/";
        Debug.Log("buildpath: " + buildPath);
        var playerPath = buildPath + filename + modifier + fileExtension;
        Debug.Log("playerpath: " + playerPath);

        var scenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }

        BuildPipeline.BuildPlayer(scenes, playerPath, buildTarget, buildTarget == BuildTarget.StandaloneWindows ? BuildOptions.ShowBuiltPlayer : BuildOptions.None);
    }
}