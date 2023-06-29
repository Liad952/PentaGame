using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : GameManager
{
    public GameObject[] cards = new GameObject[14];       // Card prefabs
    public Transform discardPile;
    private List<GameObject> deck = new List<GameObject>();   // Virtual Deck
    private int y = 54;       // Number of cards in deck

    public Transform[] playerCards = new Transform[4];   // Player's card holders
    public Transform[] opponentCards = new Transform[4];  // Opponent's card holders

    public GameManager gm;  // The GameManager
    public GameObject player;    // The PlayerController
    public GameObject opponent;


    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(gm.phase == TurnPhase.Start)
        {
            SendMessageToLog("Starting game...");       // Send message to log and start the game
            StartCoroutine(AddToDeck());
        }
        
    }

    IEnumerator AddToDeck()
    {
        if (deck.Count == 54)
            yield return null;

        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject t = Instantiate(cards[i],transform);
                t.SetActive(false);
                deck.Add(t);
            }              
        }
        GameObject n = Instantiate(cards[13],transform);
        n.SetActive(false);
        deck.Add(n);
        GameObject u = Instantiate(cards[13],transform);
        u.SetActive(false);
        deck.Add(u);

        yield return new WaitForSeconds(0.5f);

        deck = Shuffle();

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(DealPlayers());

    }       // ObjectPooling all of the cards to the deck list and SetActive to false.

    List<GameObject> Shuffle()
    {
        SendMessageToLog("Shuffling..");
        int x = 0;
        List<GameObject> temp = new List<GameObject>();
        y = 54;
        for (int i = 0; i < 54; i++)
        {
            x = Random.Range(0, y);
            temp.Add(deck[x]);
            deck.Remove(deck[x]);
            y--;
        }
        return temp;
    }    // Shuffling the list to a new temporary list and returning the new shuffled list to the deck.
    
    IEnumerator DealPlayers()
    {
        SendMessageToLog("Dealing cards..");
        bool dealPlayer = true;
        int x = 0;
        for(int i=0;i<8;i++)
        {           
            if (dealPlayer)
            {
                deck[i].SetActive(true);
                deck[i].transform.position = transform.position;
                deck[i].transform.parent = playerCards[x];
                StartCoroutine(deck[i].gameObject.GetComponent<Card>().MoveTo(playerCards[x].transform.position));
                deck[i].gameObject.GetComponent<Card>().inPlayerHand = true;
                dealPlayer = false;
            }
            else if(!dealPlayer)
            {
                deck[i].SetActive(true);
                deck[i].transform.position = transform.position;
                deck[i].transform.parent = opponentCards[x];
                StartCoroutine(deck[i].gameObject.GetComponent<Card>().MoveTo(opponentCards[x].transform.position));
                deck[i].gameObject.GetComponent<Card>().inOpponentHand = true;
                dealPlayer = true;
                x++;
            }
            deck.RemoveAt(i);
            yield return new WaitForSeconds(0.2f);
        }
        deck[0].SetActive(true);
        deck[0].transform.position = discardPile.position;
        deck[0].transform.parent = discardPile;
        StartCoroutine(deck[0].gameObject.GetComponent<Card>().MoveTo(discardPile.position));
        deck[0].gameObject.GetComponent<Card>().dicarded = true;
        deck.RemoveAt(0);
        yield return new WaitForSeconds(0.5f);
        gm.phase = TurnPhase.PlayerFirstTurn;
        SendMessageToLog("Player Turn");       

    }    // Deal each player a card at a time until each has 4. Add a card to the discard pile.

    private void OnMouseDown()
    {
        if(gm.phase == TurnPhase.PlayerTurn)
        {
            DrawCard(player.GetComponent<PlayerController>().drawnCard);           
        }
        if (gm.phase == TurnPhase.OpponentTurn)
        {
            DrawCard(this.opponent.transform);
        }
    }
    public void DrawCard(Transform player)
    {
        deck[0].SetActive(true);
        deck[0].transform.position = transform.position;
        deck[0].transform.parent = player;
        StartCoroutine(deck[0].gameObject.GetComponent<Card>().MoveTo(player.position));
        gm.PreviewCard(deck[0].GetComponent<Card>());
        gm.phase = TurnPhase.DrawNew;
    }
}
