using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameScript G;

    // sets the gamescript reference
    public void setReference(GameScript GS)
    {
        G = GS;
    }

    public void btnclick() // called on click on the generate button
    {
        if(G.generated == false)
        {
            G.BoardSetup(G.width, G.height, G.mines); // generates a board
        } else
        {
            G.ClearBoard();
            G.BoardSetup(G.width, G.height, G.mines); // clears and regenerates a board
        }
    }
}
