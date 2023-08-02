using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponantAI : GameManager
{
    public GameManager gm;
    public Deck deck;
    public Transform _drawnCard;
    public GameObject drawnCardHolder;

    public GameObject[] oppHand = new GameObject[4];
    private Card[] oppCards = new Card[4];


    private void Update()
    {
        switch (gm.phase)
        {
            case TurnPhase.PlayerFirstTurn:
                int x = 0;
                for (int i = 0; i < 4; i++)
                {
                    oppCards[i] = oppHand[i].GetComponentInChildren<Card>();
                    oppCards[i].cardPosInHand = x;
                    x++;
                }
                break;

        }
    }

    public IEnumerator RandomPlay()
    {
        yield return new WaitForSeconds(2);
        deck.DrawCard();
        Card cardToSwitch = oppCards[0];
        yield return new WaitForSeconds(2);
        gm.SwitchParents(oppHand[0].transform, drawnCardHolder.transform,
            cardToSwitch.GetComponent<Transform>(), _drawnCard);
        _drawnCard.GetComponent<Card>().cardPosInHand = 0;
        _drawnCard.GetComponent<Card>().inOpponentHand = true;
        yield return new WaitForSeconds(2);
        gm.SendToDiscardPile(cardToSwitch.transform);
        yield return new WaitForSeconds(2);
        gm.phase = TurnPhase.PlayerTurn;
        SendMessageToLog("Player's Turn");
    }

}
