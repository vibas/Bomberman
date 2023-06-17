using UnityEditor;
class MyEditorScript
{
    [MenuItem("Tools/Android Build")]
    static void PerformAndroidBuild()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/Bomberman.unity" };
        buildPlayerOptions.locationPathName = "AndroidBuild/Bomberman.apk";
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}