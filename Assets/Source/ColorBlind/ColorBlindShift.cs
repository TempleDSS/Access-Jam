using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wilberforce;

public class ColorBlindShift : MonoBehaviour
{
    public Colorblind colorblind;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchMode(int m)
    {
        colorblind.Type = m;
    }
}
