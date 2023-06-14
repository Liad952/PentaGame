using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gm;
    public GameObject[] playerHand = new GameObject[4];
    
    public int canLook = 2;


    private void Start()
    {
        Card[] playerCards = new Card[4];
        for (int i = 0; i < 4; i++)
        {
            playerCards[i] = playerHand[i].GetComponentInChildren<Card>();
        }
    }

    private void Update()
    {
        if(canLook== 0)
        {
            gm.phase = TurnPhase.PlayerTurn;
        }
    }





}
