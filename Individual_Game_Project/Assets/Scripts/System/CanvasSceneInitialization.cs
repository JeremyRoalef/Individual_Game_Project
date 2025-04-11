using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSceneInitialization : MonoBehaviour
{
    [SerializeField]
    [Tooltip("DO NOT CHANGE")]
    CanvasNavigator[] canvasesInScene;

    [SerializeField]
    [Tooltip("If the first canvas in the array of canvases should be active on scene startup," +
        "check this box")]
    bool enableFirstCanvasOnSceneLoad = false;

    private void Awake()
    {
        if (CanvasManager.Instance == null)
        {
            SimpleDevTools.InitializeManagers(SceneManager.GetActiveScene());
        }
    }

    private void Start()
    {
        canvasesInScene = GameObject.FindObjectsByType<CanvasNavigator>(sortMode: FindObjectsSortMode.None);
        foreach (CanvasNavigator c in canvasesInScene)
        {
            CanvasManager.Instance.AddCanvasToScene(c.gameObject);
        }

        if (enableFirstCanvasOnSceneLoad)
        {
            CanvasManager.Instance.SetActiveCanvas(canvasesInScene[0].gameObject);
        }
    }
}
