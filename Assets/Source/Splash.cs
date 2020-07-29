using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Splash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveOn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveOn()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("Main Menu");
    }

    public void Go()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("Main Menu");
    }
}
