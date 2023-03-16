using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    int highscore;

    public Text highScoreText;

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
    int currentHealth;

    public int CurrentHealth { get { return currentHealth; } }

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }

    void Start()
    {
        highscore = PlayerPrefs.GetInt("HighScore", ScoreManager.score);

        highScoreText.text = "HighScore : " + highscore;
    }


    void Update ()
    {
        //SaveHighScore();
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;

        
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        currentHealth = data.health;
        ScoreManager.score = data.lastScore;

        Vector3 playerPos;
        playerPos.x = data.playerPos[0];
        playerPos.y = data.playerPos[1];
        playerPos.z = data.playerPos[2];

        transform.position = playerPos;
    }


    public void SaveHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (ScoreManager.score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", ScoreManager.score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", ScoreManager.score);
        }
    }

}
