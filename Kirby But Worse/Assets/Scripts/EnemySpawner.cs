using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    const int MAX_TIME = 12;
    float timer;

    int rand;
    int randLength;

    public GameObject[] Enemy;

    private void Start()
    {
        randLength = Random.Range(8, MAX_TIME);
        timer = randLength;
    }

    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else 
        {
            randLength = Random.Range(8, MAX_TIME);            
            timer = randLength;

            rand = Random.Range(0, 3);

            if (rand != 2)
            {
                GameObject enemy = Instantiate(Enemy[rand], gameObject.transform.position, gameObject.transform.rotation);

                if (rand == 0) enemy.name = "Jumper";
                else if (rand == 1) enemy.name = "Runner";
            }
        }
    }
}
