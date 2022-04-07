using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerClickHandler
{
	public GameObject tileparent;
	public TileScript tiles;
	public int flagsleft;

	public void OnPointerClick(PointerEventData eventData)
	{
		functionss(eventData);
	}

    public void functionss(PointerEventData eventData)
    {
		Debug.Log("Passed");
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			Debug.Log("Passed 2");
			if (tiles.mined == true)
			{
                if(tiles.gamescript.immortality == false)
                {
                    tiles.open = true;
                    tiles.tile.GetComponent<Image>().sprite = tiles.Sprites[0];
                    tiles.gamescript.End(false);
                    tiles.theweakestlink = true;
                }
            }
			else
			{
				if (tiles.flagged == false)
                {
					Debug.Log("Passed 3");
					tiles.open = true;
					tiles.updatetile();
				}
			}
		}
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			if (tiles.open == false && tiles.flagged == false)
			{
				Debug.Log("FLAGGED");
				tiles.gamescript.UpdateInputHandlers(flagsleft - 1);
				tiles.gamescript.ChangeFlags(flagsleft);
				tiles.flagged = true;
				tiles.self.GetComponent<Image>().enabled = false;
				tiles.tile.GetComponent<Image>().sprite = tiles.Sprites[9];
            } else
            {
				if(tiles.flagged == true)
                {
					Debug.Log("deFLAGGED");
					tiles.gamescript.UpdateInputHandlers(flagsleft + 1);
					tiles.gamescript.ChangeFlags(flagsleft);
					tiles.flagged = false;
					tiles.self.GetComponent<Image>().enabled = true;
					tiles.tile.GetComponent<Image>().sprite = tiles.Spriteempty;
				}
            }
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        tiles = tileparent.GetComponent<TileScript>();
	}

	public void UpdateFlg()
    {
		flagsleft = tiles.gamescript.mines;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
