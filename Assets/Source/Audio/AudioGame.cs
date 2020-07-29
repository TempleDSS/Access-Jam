using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AudioGame : MonoBehaviour
{
    AudioSource aSource;
    public AudioClip redButton;
    public AudioClip greenButton;
    public AudioClip blueButton;
    public AudioClip tone;
    int round = 1;
    bool waiting;
    bool waitingInput;
    public int buttonToPress;
    public int receivedInput = 99;
    public TextMeshProUGUI subtitle;
    public GameObject wrong, right;
    public AudioClip wrongSound, rightSound;
    public GameObject endScreen;
    public GameObject particles;

    public bool secondRound;
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (round < 4 && !waiting)
        {
            waiting = true;
            StartCoroutine(PickButton());
        }

        if (round == 4)
        {
            endScreen.SetActive(true);
        }
    }

    IEnumerator PickButton()
    {
        yield return new WaitForSeconds(3);

        buttonToPress = Random.Range(0, 3);
        switch (buttonToPress)
        {
            case 0:
                aSource.PlayOneShot(redButton, secondRound ? 1 : 0.05f);
                if (secondRound)
                    subtitle.text = "At the sound of the tone, click the RED button.";
                break;
            case 1:
                aSource.PlayOneShot(greenButton, secondRound ? 1 : 0.05f);
                if (secondRound)
                    subtitle.text = "At the sound of the tone, click the GREEN button.";
                break;
            case 2:
                aSource.PlayOneShot(blueButton, secondRound ? 1 : 0.05f);
                if (secondRound)
                    subtitle.text = "At the sound of the tone, click the BLUE button.";
                break;
        }

        yield return new WaitForSeconds(Random.Range(4, 6));

        aSource.PlayOneShot(tone, secondRound ? 1 : 0.5f);
        if (secondRound)
            particles.SetActive(true);
        while (aSource.isPlaying)
        {
            waitingInput = true;
            if (receivedInput == buttonToPress)
            {
                aSource.Stop();
            }
            yield return 0;
        }
        if (secondRound)
            particles.SetActive(false);
        waitingInput = false;

        if (receivedInput == buttonToPress)
        {
            StartCoroutine(ShowRight());
        }
        else
        {
            StartCoroutine(ShowWrong());
        }


    }

    public IEnumerator ShowRight()
    {
        aSource.Stop();
        aSource.PlayOneShot(rightSound);
        right.SetActive(true);
        yield return new WaitForSeconds(3);
        Reset();
    }

    public IEnumerator ShowWrong()
    {
        aSource.Stop();
        aSource.PlayOneShot(wrongSound);
        wrong.SetActive(true);
        yield return new WaitForSeconds(3);
        Reset();
    }

    public void Reset()
    {
        right.SetActive(false);
        wrong.SetActive(false);
        round++;
        waiting = false;
        receivedInput = 99;
        subtitle.text = "";
    }

    public void SetInput(int i)
    {
        if (!waitingInput)
        {
            StopAllCoroutines();
            StartCoroutine(ShowWrong());
        }

        receivedInput = i;
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Retry()
    {
        secondRound = false;
        endScreen.SetActive(false);
        round = 0;
        Reset();
    }

    public void RetryFixed()
    {
        secondRound = true;
        music.volume = 0.45f;
        endScreen.SetActive(false);
        round = 0;
        Reset();
    }
}
