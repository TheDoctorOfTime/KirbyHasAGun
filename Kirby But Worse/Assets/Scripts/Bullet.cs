using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float MAX_TIME = 0.8f;
    float timer = MAX_TIME;

    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // I should've made a parent class instead but I'm lazy rn so N o
        if (collision.tag == "Enemy")
        {
                 if (collision.name == "Runner") collision.GetComponent<EnemyRun>().kill();
            else if (collision.name == "Jumper") collision.GetComponent<EnemyJump>().kill();
            else if (collision.name == "Defend") collision.GetComponent<EnemyField>().kill();

            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>().AddPoints();

        }    
    }
}
