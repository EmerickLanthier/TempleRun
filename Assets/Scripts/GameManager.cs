using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private PlayerController pc;

    private float score;

    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        score = 0;

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
}