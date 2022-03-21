using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : MonoBehaviour
{
    GameScript GS;
    public GameObject control;

    // Start is called before the first frame update
    void Start()
    {
        GS = control.GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleFlag(bool toggle)
    {
        if(toggle == true)
        {
            GS.flagging = true;
        } else
        {
            GS.flagging = false;
        }
    }
}
