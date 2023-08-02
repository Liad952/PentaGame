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
    public GameObject drawnCardHolder;
    public Transform drawnCard;
    
    public int canLook = 2;


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
                int x = 0;
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
        Card cardToSwitch = playerCards[cardPos];
        gm.SwitchParents(playerHand[cardPos].transform, drawnCardHolder.transform,
            cardToSwitch.GetComponent<Transform>(), drawnCard);
        drawnCard.GetComponent<Card>().cardPosInHand = cardPos;
        drawnCard.GetComponent<Card>().inPlayerHand = true;
        gm.SendToDiscardPile(cardToSwitch.GetComponent<Transform>());
        if(!drawnCard.gameObject.activeSelf)
        {
            drawnCard.gameObject.SetActive(true);
        }
        if (drawnCard.GetComponent<Card>().dicarded)
        {
            drawnCard.GetComponent<Card>().dicarded = false;
        }

        gm.phase = TurnPhase.OpponentTurn;
        gm.PlayAiTurn();
    }   //  Used to switch cards for the player.



}
