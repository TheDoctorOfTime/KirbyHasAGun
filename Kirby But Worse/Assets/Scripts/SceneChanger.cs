using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void goToGame()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }

    public void goToDeath()
    {
        SceneManager.LoadScene("DeathScreen", LoadSceneMode.Single);
    }
    public void goToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
