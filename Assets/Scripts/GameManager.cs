using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    private PlayerController pc;

    public AudioClip milestoneSound;
    private AudioSource gameManagerAudio;

    private float score;
    private int lastMilestoneScore = 0;

    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManagerAudio = GetComponent<AudioSource>();

        score = 0;
        lastMilestoneScore = 0;

        if (highScoreText != null)
        {
            float highScore = PlayerPrefs.GetFloat("HighScore", 0);
            highScoreText.text = "Best: " + Mathf.FloorToInt(highScore);
        }

    }

    void Update()
    {
        if (!pc.gameOver)
        {
            score += Time.deltaTime * 5;

            //update le score de lecran
            scoreText.text = "Score: " + Mathf.FloorToInt(score);

            CheckMilestone();
        }
        else
        {
            CheckHighScore();
        }
    }

    void CheckHighScore()
    {
        float currentHighScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (score > currentHighScore)
        {
            PlayerPrefs.SetFloat("HighScore", score);
            PlayerPrefs.Save();

            // Mettre à jour l'affichage
            if (highScoreText != null)
            {
                highScoreText.text = "Best: " + Mathf.FloorToInt(score);
            }
        }
    }

    void CheckMilestone()
    {
        int currentScoreInt = Mathf.FloorToInt(score);

        int currentMilestone = (currentScoreInt / 100) * 100;

        if (currentMilestone > lastMilestoneScore && currentMilestone >= 100)
        {
            if (gameManagerAudio != null && milestoneSound != null)
            {
                gameManagerAudio.PlayOneShot(milestoneSound);
            }

            lastMilestoneScore = currentMilestone;

        }
    }
}