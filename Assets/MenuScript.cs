using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuScript : MonoBehaviour
{

    public GameObject Menu;
    public GameObject GS;
    public GameObject Width;
    public GameObject Height;
    public GameObject Timer;
    public GameObject Mines;
    bool opened;

    // start of the game closes menu
    void Start()
    {
        opened = false;
    }

    // MAKE IT ACTUALLY VISIBLE
    void Update()
    {
        Menu.transform.SetAsLastSibling();
    }

    public void MenuOpen() // opens menu up when clicked
    {
        if (opened == true)
        {
            opened = false;
            Menu.SetActive(false);
            if(GS.GetComponent<GameScript>().generated == true && GS.GetComponent<GameScript>().firstclick == 1 && GS.GetComponent<GameScript>().gamehasended == false)
            {
                Timer.GetComponent<Timer>().counting = true;
            }
        }
        else
        {
            opened = true;
            Menu.SetActive(true);
            Timer.GetComponent<Timer>().counting = false;
        }
    }

    // preset sizes
    public void Beginner()
    {
        GS.GetComponent<GameScript>().ClearBoard();
        SetVars(9, 9, 10);
        GS.GetComponent<GameScript>().BoardSetup(9, 9, 10);
        MenuOpen();
    }

    void SetVars(int wi, int hi, int mi)
    {
        GS.GetComponent<GameScript>().width = wi;
        GS.GetComponent<GameScript>().height = hi;
        GS.GetComponent<GameScript>().mines = mi;
    }

    public void Intermediate()
    {
        GS.GetComponent<GameScript>().ClearBoard();
        SetVars(16, 16, 40);
        GS.GetComponent<GameScript>().BoardSetup(16, 16, 40);
        MenuOpen();
    }

    public void Expert()
    {
        GS.GetComponent<GameScript>().ClearBoard();
        SetVars(30, 16, 99);
        GS.GetComponent<GameScript>().BoardSetup(30, 16, 99);
        MenuOpen();
    }
    
    // custom size set by client
    public void Custom()
    {
        GS.GetComponent<GameScript>().ClearBoard();
        int w;
        int h;
        int m;
        bool tw = int.TryParse(Width.GetComponent<TMP_InputField>().text, out w);
        bool th = int.TryParse(Height.GetComponent<TMP_InputField>().text, out h);
        bool tm = int.TryParse(Mines.GetComponent<TMP_InputField>().text, out m);
        if (tw == true && th == true && tm == true) // makes sure the number is in the right size range
        {
            if(w > 30)
            {
                w = 30;
            }
            if(h > 24)
            {
                h = 24;
            }
            if(m > 435)
            {
                m = 435;
            }
            if(m < 10)
            {
                m = 10;
            }
            if(h < 8)
            {
                h = 9;
            }
            if(w < 8)
            {
                w = 9;
            }
            SetVars(w, h, m);
            GS.GetComponent<GameScript>().BoardSetup(w, h, m);
        }
    }
}
