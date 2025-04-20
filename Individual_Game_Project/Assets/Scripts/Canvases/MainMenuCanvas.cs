using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{
    //Liseners to button OnClick events

    public void OnButtonStartClick()
    {
        Debug.Log("Playing Application");
        SceneManager.LoadScene("WorldScene");
    }
    public void OnButtonQuitClick()
    {
        Debug.Log("Quitting Application");
        Application.Quit();
    }
    public void OnButtonSettingsClick()
    {
        Debug.Log("Opening Settings");
    }
}
