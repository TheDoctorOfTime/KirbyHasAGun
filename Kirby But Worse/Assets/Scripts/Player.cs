using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using MilkShake;

public class Player : MonoBehaviour 
{
    //public ParticleSystem particle1;
    private Sprite emptySprite;

    public AudioSource killedSound;
    public AudioSource hurtSound;
    public AudioSource PowerUp;

    public GameObject character;

    public GameObject pDeath;
    private ParticleSystem deathParticles;

    public Text scoreText;
    public Text highScoreText;
    public Text healthText;
    public Text FastStatus;
    public Text JumpStatus;
    public Text Special;

    public Camera cam;

    public CharacterController2D controller;
    public float speed;

    float HorizontalMove = 0f;
    bool isJumping = false;

    public int health = 3;

    private int score = 0;

    public bool highJump = false;
    public bool veryFast = false;

    private int killCount = 0;

    private float deathTimer = 2f;
    private bool isDead = false;

    private Shaker shake;
    public ShakePreset shakeSenora;

    private void Start()
    {
        Debug.Log("HighScore: " + PlayerPrefs.GetInt("HighScore", 0).ToString());
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        deathParticles = pDeath.GetComponent<ParticleSystem>();

        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Shaker>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

        if (health > 0)
        {
            if (highJump && gameObject.GetComponent<Rigidbody2D>().gravityScale == 3)
            {
                PowerUp.Play();
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 2f;
            }
            if (veryFast && speed == 50)
            {
                PowerUp.Play();
                speed = 75;
            }

            if (highJump && veryFast)
            {
                if (Special.text == " ") Special.text = "Press LShift to kill all enemies! \n you will lose your increased speed & Jump";

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        if (enemy.name == "Runner") enemy.GetComponent<EnemyRun>().kill();
                        else if (enemy.name == "Jumper") enemy.GetComponent<EnemyJump>().kill();

                        AddPoints();
                    }

                    highJump = false;
                    veryFast = false;

                    speed = 50;
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
                }
            }
            else if (Special.text == "Press LShift to kill all enemies! \n you will lose your increased speed & Jump") Special.text = " ";

            if (highJump && JumpStatus.text == "Normal Jump") JumpStatus.text = "High Jump";
            if (veryFast && FastStatus.text == "Normal Speed") FastStatus.text = "Fast Speed";

            if (!highJump && JumpStatus.text == "High Jump") JumpStatus.text = "Normal Jump";
            if (!veryFast && FastStatus.text == "Fast Speed") FastStatus.text = "Normal Speed";

            if (Input.GetMouseButtonDown(2))
            {
                highJump = false;
                veryFast = false;

                speed = 50;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
            }

            HorizontalMove = Input.GetAxisRaw("Horizontal") * speed;
            if (Input.GetButtonDown("Jump")) isJumping = true;

            healthText.text = health.ToString();
        }
        else if(health <= 0)
        {
            if (!isDead)
            {
                Destroy(gameObject.GetComponent<TrailRenderer>());
                killedSound.Play();
                healthText.text = health.ToString();
                character.GetComponent<SpriteRenderer>().sprite = emptySprite;
                deathParticles.Play();
                isDead = true;

                BoxCollider2D[] colliders = gameObject.GetComponents<BoxCollider2D>();

                foreach(BoxCollider2D collider in colliders)
                {
                    Destroy(collider);
                }

            }

            if (deathTimer >= 0) deathTimer -= Time.deltaTime;
            else
            {
                SceneManager.LoadScene("DeathScreen", LoadSceneMode.Single);
            }
        }
    }

    void FixedUpdate()
    {
        controller.Move(HorizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }

    public void AddPoints()
    {
        score += 10;
        scoreText.text = score.ToString();

        killCount++;

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = score.ToString();
        }

        if(killCount == 10)
        {
            PowerUp.Play();
            killCount = 0;
            health += 1;
        }
    }

    public void subLife()
    {
        hurtSound.Play();
        health -= 1;

        shake.Shake(shakeSenora);
    }
}
