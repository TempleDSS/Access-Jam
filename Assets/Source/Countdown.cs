using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public float timer;
    public float currentTime;
    public TextMeshProUGUI timerText;
    AudioSource aSource;
    public AudioClip three, two, one, go;
    public bool done;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = timer;
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (currentTime <= 0)
        {
            aSource.PlayOneShot(go);
            done = true;
            return;
        }

        currentTime -= Time.deltaTime;

        if (timerText.text != Mathf.Ceil(currentTime).ToString())
        {
            timerText.text = Mathf.Ceil(currentTime).ToString("F0");
            if (timerText.text == "3")
                aSource.PlayOneShot(three);
            if (timerText.text == "2")
                aSource.PlayOneShot(two);
            if (timerText.text == "1")
                aSource.PlayOneShot(one);
        }

    }

    public void Reset()
    {
        currentTime = timer;
        done = false;
        gameObject.SetActive(true);
    }
}
