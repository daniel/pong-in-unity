using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Paddle paddle;
    public Ball ball;

    public TextMeshProUGUI leftScoreText;
    public TextMeshProUGUI rightScoreText;
    public TextMeshProUGUI gameMessage;
    public TextMeshProUGUI restartMessage;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    int leftScore = 0;
    int rightScore = 0;

    int maxScore = 10;

    Paddle paddle1;
    Paddle paddle2;

    // Start is called before the first frame update
    void Start()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        paddle1 = Instantiate(paddle) as Paddle;
        paddle2 = Instantiate(paddle) as Paddle;
        ball = Instantiate(ball);
        paddle1.Init(true);
        paddle2.Init(false);
        InitNewGame();
    }

    public void InitNewGame() {
        leftScore = 0;
        rightScore = 0;
        UpdateScore();
        gameMessage.text = "";
        restartMessage.text = "";
        ContinueGame();
    }

    public void LeftWins() {
        ball.enabled = false;
        leftScore += 1;
        UpdateScore();
        AfterScore();
    }

    public void RightWins() {
        ball.enabled = false;
        rightScore += 1;
        UpdateScore();
        AfterScore();
    }

    void AfterScore() {
        GetComponent<AudioSource>().Play();
        if (leftScore >= maxScore) {
            GameOver("Left player wins!");
        } else if (rightScore >= maxScore) {
            GameOver("Right player wins!");
        } else {
            ContinueGame();
        }
    }

    void ContinueGame() {
        paddle1.Init(true);
        paddle2.Init(false);
        ball.Spawn();
        ball.enabled = true;

    }

    void GameOver(string message) {
        gameMessage.text = message;
        restartMessage.text = "Press space to restart";
        Time.timeScale = 0;
        ball.enabled = false;
    }

    void UpdateScore() {
        leftScoreText.text = leftScore.ToString("00");
        rightScoreText.text = rightScore.ToString("00");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            InitNewGame();
            Time.timeScale = 1;
            ball.enabled = true;
        }
    }

}
