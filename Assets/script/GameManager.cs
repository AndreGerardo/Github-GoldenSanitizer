using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoSingleton<GameManager>
{
    public bool isPlaying = true;
    public bool isPaused = false;
    //Scoring
    public int score = 0;
    public int bestScore = 0;
    private int prevScore;
    private float timer = 0f;
    public TextMeshProUGUI scoreText;
    [Header("DeadMenu")]
    public TextMeshProUGUI deathScoreText;
    public TextMeshProUGUI bestScoreText;


    private void Start()
    {
        prevScore = score;
        scoreText.text = score.ToString();
        if (PlayerPrefs.HasKey("keandre"))
        {
            bestScore = PlayerPrefs.GetInt("keandre");
        }

        bestScoreText.text = bestScore.ToString();
    }

    private void Update()
    {
        if (isPlaying)
        {
            //Scoring
            if (timer <= 2f)
            {
                timer += Time.deltaTime;
            }

            if (timer >= 1f)
            {
                score++;
                timer = 0;
            }

            if (prevScore != score)
            {
                scoreText.text = score.ToString();
                prevScore = score;
            }
        }else
        {
            if (score > bestScore)
            {
                bestScore = score;
                PlayerPrefs.SetInt("keandre", score);
            }

            deathScoreText.text = score.ToString();
            bestScoreText.text = bestScore.ToString();

        }


    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
