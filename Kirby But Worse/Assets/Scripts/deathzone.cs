using UnityEngine;
using UnityEngine.SceneManagement;

public class deathzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player") SceneManager.LoadScene("DeathScreen", LoadSceneMode.Single);
    }
}
