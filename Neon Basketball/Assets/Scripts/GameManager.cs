using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
	// using region just to organize a little
	// and because i recently learned about it!
	#region Game Manager

	public static GameManager instance;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
	}

	#endregion

	public float timeRemaining = 30;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
	public TextMeshProUGUI scoreText;

    public GameObject ball;
    public Hoop hoop;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScore;
	public TextMeshProUGUI highScore;

    private void Start() {
        // starts the timer automatically
        timerIsRunning = true;
        highScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update() {
        if(timerIsRunning) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            } 
			
			else {
                timeRemaining = 0;
                timerIsRunning = false;

                LoseGame();
            }
        }
    }

    void DisplayTime(float timeToDisplay) {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

	public void AddTime() {
		timeRemaining += 10;
		DisplayTime(timeRemaining);
	}

	public void AddScore() {
		float score = float.Parse(scoreText.text);
		score++;
		scoreText.text = score.ToString();
	}

    public void RestartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public void LoseGame() {
        Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
        ballRB.isKinematic = true;
        ballRB.velocity = new Vector3(0,0,0);
        ballRB.freezeRotation = true;

        finalScore.text = "SCORE: " + scoreText.text;
        gameOverPanel.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

        int number = int.Parse(scoreText.text);
        if(number > PlayerPrefs.GetInt("HighScore", 0)) {
            PlayerPrefs.SetInt("HighScore", number);
            highScore.text = "HIGH SCORE: " + scoreText.text;
        }
    }

    public void MainMenu() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

    public void MakeFaster() {
        hoop.freq = hoop.freq + 0.3f;
    }

}