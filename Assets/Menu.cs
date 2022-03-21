using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameScript G;
    // Start is called before the first frame update

    public void setReference(GameScript GS)
    {
        G = GS;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnclick()
    {
        if(G.generated == false)
        {
            G.BoardSetup(G.width, G.height, G.mines);
        } else
        {
            G.ClearBoard();
            G.BoardSetup(G.width, G.height, G.mines);
        }
    }
}
