using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gm;
    public GameObject[] playerHand = new GameObject[4];
    private Card[] playerCards = new Card[4];
    public GameObject deck;
    public Transform drawnCard;
    
    public int canLook = 2;


    private void Start()
    {
        
        
    }

    private void Update()
    {
        if(canLook== 0)
        {
            if (gm.phase != TurnPhase.PlayerFirstTurn)
                return;
            else
            gm.phase = TurnPhase.PlayerTurn;
        }
       
        switch(gm.phase)
        {
            case TurnPhase.PlayerFirstTurn:
                int x = 1;
                for (int i = 0; i < 4; i++)
                {
                    playerCards[i] = playerHand[i].GetComponentInChildren<Card>();
                    playerCards[i].cardPosInHand= x;
                    x++;
                }
                break;

        }

    }


    public void SwitchCards(int cardPos)
    {
        GameObject temp = playerCards[cardPos-1].gameObject;
        drawnCard.GetComponentInChildren<GameObject>().transform.parent = playerHand[cardPos-1].transform;
        temp.transform.parent = drawnCard;
    }



}
