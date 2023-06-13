using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : GameManager
{
    public GameObject[] cards = new GameObject[14];
    private List<GameObject> deck = new List<GameObject>();
    private int x = 0;
    private int y = 54;

    public Transform[] playerCards = new Transform[4];
    public Transform[] opponentCards = new Transform[4];

    public GameManager gm;
    public GameObject player;

    void Start()
    {
        if(gm.phase == TurnPhase.Start)
        {
            gm.SendMessageToLog("Starting game...");
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
        gm.SendMessageToLog("Shuffling..");
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
        gm.SendMessageToLog("Dealing cards..");
        bool dealPlayer = true;
        int x = 0;
        for(int i=0;i<8;i++)
        {           
            if (dealPlayer)
            {
                deck[i].SetActive(true);
                deck[i].transform.position = transform.position;
                deck[i].transform.parent = playerCards[x];
                StartCoroutine(deck[i].gameObject.GetComponent<Card>().MoveTo(playerCards[x].transform));
                dealPlayer = false;
            }
            else if(!dealPlayer)
            {
                deck[i].SetActive(true);
                deck[i].transform.position = transform.position;
                deck[i].transform.parent = opponentCards[x];
                StartCoroutine(deck[i].gameObject.GetComponent<Card>().MoveTo(opponentCards[x].transform));
                dealPlayer = true;
                x++;
            }
            deck[i].gameObject.GetComponent<Card>().inHand= true;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);
        gm.phase = TurnPhase.PlayerTurn;
        gm.SendMessageToLog("Player Turn");
        //player.SetActive(true);

    }    // Deal each player a card at a time until each has 4.


}
