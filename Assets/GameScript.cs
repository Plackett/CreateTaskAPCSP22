using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameScript : MonoBehaviour
{
    public int width;
    public int height;
    public int mines;
    public bool flagging;
    public GameObject Cell;
    public GameObject[][] board;
    public GameObject canvas;
    public GameObject btn;
    public bool generated;
    public GameObject Flags;
    public GameObject Timer;
    private TextMeshProUGUI Flagcount;
    private TextMeshProUGUI Timertext;
    public int firstclick;
    private bool minesetted;
    public bool immortality;
    public GameObject ach;
    public GameObject ach1;

    void Start()
    {
        width = 30;
        height = 24;
        mines = 99;
        firstclick = 0;
        Flagcount = Flags.GetComponent<TextMeshProUGUI>();
        Timertext = Timer.GetComponent<TextMeshProUGUI>();
        btn.GetComponent<Menu>().setReference(this);
        BoardSetup(width, height, mines);
    }

    // fix resolution to 31 by 27 by not allowing you to change window size
    void Update()
    {

        // cheats
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (immortality == false)
            {
                immortality = true;
                ach.transform.SetAsLastSibling();
                LeanTween.moveX(ach.GetComponent<RectTransform>(), 300f, 1f).setDelay(0f);
                LeanTween.moveX(ach.GetComponent<RectTransform>(), 941f, 1f).setDelay(5f);
            }
            else
            {
                if (immortality == true)
                {
                    immortality = false;
                    ach1.transform.SetAsLastSibling();
                    LeanTween.moveX(ach1.GetComponent<RectTransform>(), 300f, 1f).setDelay(0f);
                    LeanTween.moveX(ach1.GetComponent<RectTransform>(), 941f, 1f).setDelay(5f);
                }
            }
        }
    }

    public void ChangeFlags(int flg)
    {
        Flagcount.text = flg.ToString("D3");
        for(var h = 0; h < width; h++)
        {
            for(var g = 0; g < height; g++)
            {
                board[h][g].GetComponent<TileScript>().flags = flg;
            }
        }
    }

    public void UpdateTimer(float time)
    {
        if(time <= 999)
        {
            Timertext.text = time.ToString("000");
        }
    }

    public void ClearBoard()
    {
        Flagcount = Flags.GetComponent<TextMeshProUGUI>();
        generated = false;
        Flagcount.text = "000";
        Timer.GetComponent<Timer>().time = 0;
        minesetted = false;
        for(var c = 0; c < width; c++)
        {
            for(var f = 0; f < height; f++)
            {
                Destroy(board[c][f]);
            }
        }
    }

    // actually instantiates the cells and generates grid array
    // had to change from 2 dimensional array to an array of arrays to do non-square boards
    public void BoardSetup(int width, int height, int mines)
    {
        Timertext.text = "000";
        firstclick = 0;
        Timer.GetComponent<Timer>().counting = false;
        Flagcount = Flags.GetComponent<TextMeshProUGUI>();
        generated = true;
        Flagcount.text = mines.ToString("D3");
        board = new GameObject[width][];
        for(var t = 0; t < width; t++)
        {
            board[t] = new GameObject[height];
        }

        for (var i = 0; i < width; i++)
        {
            for(var v = 0; v < height; v++)
            {
                var temp = Instantiate(Cell);
                // resizes based off original size and centers it
                temp.transform.position = new Vector3(i*40 + 20, (-1*v*40) - 140, 0);
                temp.transform.SetParent(canvas.transform, false);
				temp.GetComponent<TileScript>().setReference(this);
                temp.GetComponent<TileScript>().x = i;
                temp.GetComponent<TileScript>().y = v;
                temp.transform.GetChild(0).GetComponent<InputHandler>().UpdateFlg();
                temp.GetComponent<TileScript>().flags = mines;
                board[i][v] = temp;
            }
        }
       // DON'T SET MINES TILL AFTER FIRST CLICK
    }

    public void mineSet()
    {
        // minesetted variable is so that it can only generate mines ONCE
        if(minesetted == false)
        {
            minesetted = true;
            for (var m = 0; m < mines;)
            {
                var number = Random.Range(0, width);
                var number2 = Random.Range(0, height);
                var test = number;
                var test2 = number2;
                if (board[(int) test][(int) test2].GetComponent<TileScript>().mined == false && board[(int) test][(int) test2].GetComponent<TileScript>().open == false)
                {
                    //Debug.Log("rand" + number);
                    //Debug.Log("mined" + test);
                    //Debug.Log("minedy" + test2);
                    board[(int)test][(int)test2].GetComponent<TileScript>().mined = true;
                    m++;
                }
            }
        }
    }
	
	public int checkTurn(GameObject obj)
	{
        if (firstclick == 0)
        {
            firstclick = 1;
            mineSet();
            Timer.GetComponent<Timer>().counting = true;
        }
        int posx = obj.GetComponent<TileScript>().x;
		int minecount = 0;
        int posy = obj.GetComponent<TileScript>().y;
        // check 3x3 around the target
        for (int k = -1; k <= 1; k++)
        {
            for (int l = -1; l <= 1; l++)
            {
                int newx = posx + k;
                int newy = posy + l;

                if(mineCheck(newx, newy, width, height) == true)
                {
                    if(board[newx][newy].GetComponent<TileScript>().mined == true)
                    {
                        //Debug.Log("minecount");
                        minecount = minecount + 1;
                    }
                }
            }
        }
		return minecount;
	}

    public bool mineCheck(int x, int y, int width, int height)
    {
        //Debug.Log(new string(x + "hey" + width));
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void End(bool win)
    {
        Timer.GetComponent<Timer>().counting = false;
        for (var n = 0; n < width; n++)
        {
            for (var m = 0; m < height; m++)
            {
                board[n][m].GetComponent<TileScript>().tile.GetComponent<Button>().interactable = false;
                board[n][m].GetComponent<TileScript>().tile.GetComponent<Button>().GetComponent<InputHandler>().enabled = false;
                if (board[n][m].GetComponent<TileScript>().mined == true)
                {
                    board[n][m].GetComponent<TileScript>().open = true;
                }
            }
        }
        if(win == false)
        {
            AnimEnd(1);
        } else
        {
            AnimEnd(2);
        }
    }

    public void AnimEnd(int win)
    {
        for(var g = 0; g < width; g++)
        {
            for(var h = 0; h < height; h++)
            {
                if(board[g][h].GetComponent<TileScript>().mined == true)
                {
                    board[g][h].GetComponent<TileScript>().color = win;
                }
            }
        }
    }

    public void endcheck()
    {
        int tilesflagged = 0;
        for (var y = 0; y < width; y++)
        {
            for (var aa = 0; aa < height; aa++)
            {
                if(board[y][aa].GetComponent<TileScript>().flagged == true && board[y][aa].GetComponent<TileScript>().mined == true)
                {
                    tilesflagged++;
                }
            }
        }
        if(tilesflagged == mines)
        {
            End(true);
        }
    }

    public void UpdateInputHandlers(int flgl)
    {
        for (var aaa = 0; aaa < width; aaa++)
        {
            for (var aaaa = 0; aaaa < height; aaaa++)
            {
                board[aaa][aaaa].transform.GetChild(0).GetComponent<InputHandler>().flagsleft = flgl;
            }
        }
    }
}
