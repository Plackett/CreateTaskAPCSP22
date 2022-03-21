using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileScript : MonoBehaviour
{
	public GameScript gamescript;
	public Image img;
	public bool mined;
	public bool open;
	public int BCount;
	int cont;
	public int flags;
	public int x;
	public int y;
	public bool BChecked;
	public Button tile;
	public Text txt;
	public GameObject self;
	public bool flagged;
	public Sprite[] Sprites = new Sprite[10];
	public Sprite Spriteempty;
	public bool gameover;

    // Update is called once per frame
    void Update()
    {
		if (open == true)
		{
			tile.GetComponent<Button>().interactable = false;
			self.GetComponent<Image>().enabled = false;

			if (mined == true)
			{
				tile.GetComponent<Image>().sprite = Sprites[0];
			}
		}
		if (open == true && mined == false && BChecked != true)
		{
			updatetile();
		}
	}

	public void updatetile()
    {
		BChecked = true;
		if (open == true && mined == false && flagged == false)
		{
			BCount = gamescript.checkTurn(self);
			if(BCount == 0)
            {
				GameObject[][] board = gamescript.board;
				// check 3x3 cross around the target
				for (int k = -1; k <= 1; k++)
				{
					for (int l = -1; l <= 1; l++)
					{
						int newx = x + k;
						int newy = y + l;
						if (gamescript.mineCheck((newx), (newy), gamescript.width, gamescript.height) == true)
                        {
							if(gamescript.board[newx][newy].GetComponent<TileScript>().mined == false)
                            {
								gamescript.board[newx][newy].GetComponent<TileScript>().open = true;
							}
						}
					}
				}
			} else
            {
				tile.GetComponent<Image>().sprite = Sprites[BCount];
				if(Flagcheck() == BCount)
                {
					for (int o = -1; o <= 1; o++)
					{
						for (int p = -1; p <= 1; p++)
						{
							int newx = x + o;
							int newy = y + p;
							if (gamescript.mineCheck((newx), (newy), gamescript.width, gamescript.height) == true)
							{
								if (gamescript.board[newx][newy].GetComponent<TileScript>().mined == false)
								{
									gamescript.board[newx][newy].GetComponent<TileScript>().open = true;
								}
							}
						}
					}
				}
			}
		}
		gamescript.endcheck();
	}

	public int Flagcheck()
    {
		for (int a = -1; a <= 1; a++)
		{
			for (int q = -1; q <= 1; q++)
			{
				int newx = x + a;
				int newy = y + q;
				if (gamescript.mineCheck((newx), (newy), gamescript.width, gamescript.height) == true)
				{
					if (gamescript.board[newx][newy].GetComponent<TileScript>().flagged == true && gamescript.board[newx][newy].GetComponent<TileScript>().mined == true)
					{
						cont++;
					}
				}
			}
		}
		return cont;
	}
	
	public void setReference(GameScript GS) {
		gamescript = GS;
	}

	
}
