using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CircleGame : MonoBehaviour
{
    public MoveCircles[] balls;
    public ColorBlindShift colorshift;

    public int points;
    public float roundTime;
    float timer;

    [Header("Texts")]
    public TextMeshProUGUI pointsLabel;
    public TextMeshProUGUI timerLabel;
    public TextMeshProUGUI nScoreText;
    public TextMeshProUGUI pScoreText;
    public TextMeshProUGUI dScoreText;
    public TextMeshProUGUI tScoreText;
    public Color round1Color;
    public Color round2Color;


    public int gameMode;
    public bool secondRound = false;
    bool gameRunning = true;

    int pScore;
    int dScore;
    int tScore;
    int nScore;



    public TextMeshProUGUI instructions;

    public GameObject endMenu;


    // Start is called before the first frame update
    void Start()
    {
        timer = roundTime;
        SetProtanopia();

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameRunning)
            return;

        if (pointsLabel.text != points.ToString())
            pointsLabel.text = points.ToString();
        if (timerLabel.text != timer.ToString())
            timerLabel.text = timer.ToString("F2");

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            switch (gameMode)
            {
                case 0:
                    pScore = points;
                    pScoreText.text = pScore.ToString();
                    SetDeuteranopia();
                    break;
                case 1:
                    dScore = points;
                    dScoreText.text = dScore.ToString();
                    SetTritanopia();
                    break;
                case 2:
                    tScore = points;
                    tScoreText.text = tScore.ToString();
                    SetNormal();
                    break;
                case 3:
                    nScore = points;
                    nScoreText.text = nScore.ToString();
                    ShowEnd();
                    break;
                default:
                    break;
            }

            gameMode++;
            timer = roundTime;
            points = 0;
        }
    }

    void ShowEnd()
    {
        gameRunning = false;
        timer = 0;
        points = 0;
        if (pointsLabel.text != points.ToString())
            pointsLabel.text = points.ToString();
        if (timerLabel.text != timer.ToString())
            timerLabel.text = timer.ToString("F2");


        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].GetComponent<Rigidbody2D>().simulated = false;
        }
        endMenu.SetActive(true);
    }

    void SetProtanopia()
    {
        for (int i = 0; i < 4; i++)
            balls[i].Reset(ColorEnum.Red, 5);
        for (int i = 4; i < 8; i++)
            balls[i].Reset(ColorEnum.Green, -5);
        for (int i = 8; i < 12; i++)
            balls[i].Reset(ColorEnum.Blue, 0);

        instructions.text = "Red: +5 \tGreen: -5 \tBlue: 0";
        colorshift.SwitchMode(1);
    }

    void SetDeuteranopia()
    {
        for (int i = 0; i < 4; i++)
            balls[i].Reset(ColorEnum.Red, -5);
        for (int i = 4; i < 8; i++)
            balls[i].Reset(ColorEnum.Green, 5);
        for (int i = 8; i < 12; i++)
            balls[i].Reset(ColorEnum.Blue, 0);

        instructions.text = "Red: -5 \tGreen: +5 \tBlue: 0";
        colorshift.SwitchMode(2);
    }

    void SetTritanopia()
    {
        for (int i = 0; i < 4; i++)
            balls[i].Reset(ColorEnum.Red, 0);
        for (int i = 4; i < 8; i++)
            balls[i].Reset(ColorEnum.Green, -5);
        for (int i = 8; i < 12; i++)
            balls[i].Reset(ColorEnum.Blue, +5);

        instructions.text = "Red: 0 \tGreen: -5 \tBlue: +5";
        colorshift.SwitchMode(3);
    }

    void SetNormal()
    {
        for (int i = 0; i < 4; i++)
            balls[i].Reset(ColorEnum.Red, 5);
        for (int i = 4; i < 8; i++)
            balls[i].Reset(ColorEnum.Green, -5);
        for (int i = 8; i < 12; i++)
            balls[i].Reset(ColorEnum.Blue, 0);

        instructions.text = "Red: +5 \tGreen: -5 \tBlue: 0";
        colorshift.SwitchMode(0);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");

    }

    public void Retry()
    {
        secondRound = false;
        timer = roundTime;
        gameMode = 0;
        pScore = 0;
        dScore = 0;
        tScore = 0;
        nScore = 0;
        endMenu.SetActive(false);
        gameRunning = true;
        instructions.color = round1Color;
        SetProtanopia();
    }

    public void RetryFixed()
    {
        secondRound = true;
        timer = roundTime;
        gameMode = 0;
        pScore = 0;
        dScore = 0;
        tScore = 0;
        nScore = 0;
        endMenu.SetActive(false);
        gameRunning = true;
        instructions.color = round2Color;
        SetProtanopia();
    }
}
