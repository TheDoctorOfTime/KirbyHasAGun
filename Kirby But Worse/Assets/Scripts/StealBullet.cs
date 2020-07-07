using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealBullet : MonoBehaviour
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
            if (collision.name == "Runner")
            {
                collision.GetComponent<EnemyRun>().kill();
                GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>().veryFast = true;
            }
            else if (collision.name == "Jumper") 
            { 
                collision.GetComponent<EnemyJump>().kill();
                GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>().highJump = true;
            }

            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>().AddPoints();
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>().health -= 1;

            Destroy(gameObject);

        }
    }
}
