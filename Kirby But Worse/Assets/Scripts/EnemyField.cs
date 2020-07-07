using UnityEngine;

public class EnemyField : MonoBehaviour
{

    //public ParticleSystem particle1;

    const float MAX_TIME = 0.5f;
    float timer = MAX_TIME;

    public CharacterController2D controller;
    public float speed;
    float playerLoc = 0f;

    float HorizontalMove = 0f;
    bool isJumping = false;

    Vector3 playersPos;

    private void Start()
    {
        playersPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
    }

    void Update()
    {
        //Find Player Position

        Vector3 aToB = playersPos - gameObject.transform.position;

        //Move towards Player

        if (aToB.x > 0) playerLoc = 1f;
        else if (aToB.x < 0) playerLoc = -1f;

        HorizontalMove = playerLoc * speed;

        //Wait a moment before activating shield
        if (timer >= 0) timer -= Time.deltaTime;
        else
        {
            playersPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
            timer = MAX_TIME;

            // Activate Force Field - Sprite
            // Basically just make enemy invincible till it's shot at
            //I might remove this character actually. I don't know.

        }
    }

    void FixedUpdate()
    {
        controller.Move(HorizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Player>().health -= 1;
            Debug.Log(collision.gameObject.GetComponent<Player>().health.ToString());
        }
    }

    public void kill()
    {
        //Disable Sprite / Make sprite = none
        //Play Death Particles

        Destroy(gameObject);
    }
}
