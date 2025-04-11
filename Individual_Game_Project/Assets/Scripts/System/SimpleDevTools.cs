using UnityEngine.SceneManagement;

public class SimpleDevTools
{
    const string INITIALIZATION_SCENE_NAME = "Initialization";

    public static void InitializeManagers(Scene activeScene)
    {
        //Return to initialization scene & come back to active scene
        SceneManager.LoadScene(INITIALIZATION_SCENE_NAME);
        SceneManager.LoadScene(activeScene.name);
    }
    public static void ChangeScene(string newScene)
    {
        bool isValidScene = SceneExists(newScene);

        if (isValidScene) { SceneManager.LoadScene(newScene); }
    }

    /*
     * Credit for method: ChatGPT
     * Prompt given: I am working with a string scene name and trying to determine whether
     * this scene name is valid or not. I cannot reference the scene by index or through
     * serialized fields, so the name is the only method that will work for me. How do I determine
     * whether a string is a valid scene name?
     */
    public static bool SceneExists(string sceneName)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);

            if (name.Equals(sceneName))
                return true;
        }

        return false;
    }
}
