using System.Collections;
using UnityEngine;

public class EnemyRun : MonoBehaviour
{
    private Sprite emptySprite;

    public GameObject character;

    public GameObject pDeath;
    private ParticleSystem deathParticles;

    public AudioSource hurtSound;

    bool isDead = false;

    private const float MAX_TIME = 3f;
    private float timer = 1f;

    public CharacterController2D controller;
    private float speed;
    float direction = 0f;

    float HorizontalMove = 0f;

    Vector3 playersPos;

    private void Start()
    {
        deathParticles = pDeath.GetComponent<ParticleSystem>();
        playersPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
        speed = 60f;
    }

    void Update()
    {
        if (!isDead)
        {
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                timer = MAX_TIME;
                playersPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
            }

            Vector3 aToB = playersPos - gameObject.transform.position;

            //Move towards Player

            if (aToB.x > 0) direction = 1f;
            else if (aToB.x < 0) direction = -1f;

            HorizontalMove = direction * speed;
        }

    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            controller.Move(HorizontalMove * Time.fixedDeltaTime, false, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && !isDead) collision.gameObject.GetComponent<Player>().subLife();
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
