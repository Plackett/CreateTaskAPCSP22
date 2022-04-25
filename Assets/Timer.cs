using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public bool counting;
    public float time;
    public GameObject GS;

    void Update()
    {
        if(counting == true) // make a way to stop the timer
        {
            time += Time.deltaTime; //adds time every realtime second
            GS.GetComponent<GameScript>().UpdateTimer((int)time); // updates timer when time is updated
        }
    }
}
