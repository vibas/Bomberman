using UnityEditor;
class MyEditorScript
{
    [MenuItem("Tools/Windows Build")]
    static void PerformWindowsBuild()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/Bomberman.unity" };
        buildPlayerOptions.locationPathName = "Win/Bomberman";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}