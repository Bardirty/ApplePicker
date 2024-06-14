using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Canvas loadingScreen;
    [SerializeField] private Canvas mainScreen;
    public void PlayClick()
    {
        loadingScreen.gameObject.SetActive(true);
        mainScreen.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync("GameScene");
    }
    public void ExitClick()
    {
        Application.Quit();
    }
}
