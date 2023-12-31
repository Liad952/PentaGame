using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    public GameObject lastCard;
    public List<Card> discardedCards = new List<Card>();

    public GameManager gm;  // The GameManager
    public GameObject player;    // The PlayerController
    public GameObject opponent;

    public void AddToPile(Card card)
    {
        discardedCards.Add(card);
        lastCard.GetComponent<SpriteRenderer>().sprite = card.cardFace;
        FixPile();
    }  // Add card to discard pile list.

    private void FixPile()
    {
        for(int i =0;i<discardedCards.Count;i++)
        {
            discardedCards[i].gameObject.SetActive(false);
        }
    }   // Arrange the pile and turn off all cards.

    private void OnMouseDown()
    {
        if (gm.phase != TurnPhase.PlayerTurn)
        {
            return;
        }

        DrawCard(player.GetComponent<PlayerController>().drawnCardHolder.transform);
    }

    public void DrawCard(Transform player)
    {
        gm.phase = TurnPhase.DrawDiscarded;
        discardedCards[discardedCards.Count-1].gameObject.SetActive(true);
        discardedCards[discardedCards.Count-1].transform.position = transform.position;
        discardedCards[discardedCards.Count-1].transform.parent = player;
        this.player.GetComponent<PlayerController>().drawnCard = discardedCards[discardedCards.Count-1].transform;
        StartCoroutine(discardedCards[discardedCards.Count-1].MoveTo(player.position));
        gm.PreviewCard(discardedCards[discardedCards.Count-1]);
        gm.SwitchCard();
    }  // Not working properly

}
