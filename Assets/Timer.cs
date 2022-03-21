using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public bool counting;
    public float time;
    public GameObject GS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(counting == true)
        {
            time += Time.deltaTime;
            GS.GetComponent<GameScript>().UpdateTimer((int)time);
        }
    }
}
