using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Balloon : MonoBehaviour
{
    public float roundTime = 30;
    public float timer = 30;
    public TextMeshProUGUI timerLabel;
    public KeyCode blowUpButton;
    public float inflationAmount = 0.25f;
    public float blowAmount = 0.025f;
    public float deflateAmount = 0.01f;
    public GameObject arrow;

    public bool gameOn;

    public Countdown count;

    AudioSource aSource;
    public AudioClip blow, pop;

    public GameObject endGame;

    public bool secondRound;
    public GameObject holdLabel;


    // Start is called before the first frame update
    void Start()
    {
        timer = roundTime;
        transform.localScale = new Vector2(inflationAmount, inflationAmount);
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (count.done == true && count.gameObject.activeSelf)
        {
            gameOn = true;
            arrow.SetActive(true);
            count.gameObject.SetActive(false);
        }

        if (timerLabel.text != timer.ToString())
            timerLabel.text = timer.ToString("F2");

        if (!gameOn)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            arrow.SetActive(false);
            EndGame();
        }

        inflationAmount -= deflateAmount * Time.deltaTime;
        if (!secondRound)
        {
            if (Input.GetKeyUp(blowUpButton))
            {
                inflationAmount += blowAmount * Time.deltaTime;
                aSource.PlayOneShot(blow);
            }
        }
        else
        {
            if (Input.GetKey(blowUpButton))
            {
                inflationAmount += blowAmount * 0.2f * Time.deltaTime;
                if (!aSource.isPlaying)
                    aSource.PlayOneShot(blow);
            }
        }

        inflationAmount = Mathf.Clamp(inflationAmount, 0.1f, 1);
        transform.localScale = new Vector2(inflationAmount, inflationAmount);

        if (inflationAmount >= 1)
        {
            aSource.Stop();
            aSource.PlayOneShot(pop);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<ParticleSystem>().Play();
            gameOn = false;
            EndGame();
        }
    }

    void EndGame()
    {
        gameOn = false;
        StartCoroutine(ShowEnd());
    }

    IEnumerator ShowEnd()
    {
        yield return new WaitForSeconds(1);
        endGame.SetActive(true);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Retry()
    {
        secondRound = false;
        count.Reset();
        timer = roundTime;
        GetComponent<SpriteRenderer>().enabled = true;
        inflationAmount = 0.25f;
        transform.localScale = new Vector2(inflationAmount, inflationAmount);
        endGame.SetActive(false);
        arrow.SetActive(true);
        holdLabel.SetActive(false);
        arrow.GetComponent<Animator>().speed = 1;
    }

    public void RetryFixed()
    {
        secondRound = true;
        count.Reset();
        timer = roundTime;
        GetComponent<SpriteRenderer>().enabled = true;
        inflationAmount = 0.25f;
        transform.localScale = new Vector2(inflationAmount, inflationAmount);
        endGame.SetActive(false);
        arrow.SetActive(true);
        holdLabel.SetActive(true);
        arrow.GetComponent<Animator>().speed = 0.1f;
    }
}
