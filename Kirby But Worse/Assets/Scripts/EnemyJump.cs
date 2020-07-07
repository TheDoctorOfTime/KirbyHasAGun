using System.Collections;
using UnityEngine;

public class EnemyJump : MonoBehaviour 
{
    private Sprite emptySprite;

    public GameObject character;

    public GameObject pDeath;
    private ParticleSystem deathParticles;

    public AudioSource hurtSound;

    private bool isDead = false;

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
        if (!isDead)
        {
            //Find Player Position

            Vector3 aToB = playersPos - gameObject.transform.position;

            //Move towards Player

            if (aToB.x > 0) playerLoc = 1f;
            else if (aToB.x < 0) playerLoc = -1f;

            HorizontalMove = playerLoc * speed;

            //Wait a moment before jumping
            if (timer >= 0) timer -= Time.deltaTime;
            else
            {
                playersPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
                timer = MAX_TIME;
                isJumping = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            deathParticles = pDeath.GetComponent<ParticleSystem>();
            controller.Move(HorizontalMove * Time.fixedDeltaTime, false, isJumping);
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player" && !isDead) collision.gameObject.GetComponent<Player>().subLife();
    }

    public void kill()
    {
        isDead = true;
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        hurtSound.Play();
        character.GetComponent<SpriteRenderer>().sprite = emptySprite;
        deathParticles.Play();

        StartCoroutine(killEnemy());
    }

    IEnumerator killEnemy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
