using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    public int cardNum;                       // The card's information
    public Sprite cardFace;
    public Sprite cardBack;

    public bool inOpponentHand = false;        // The card current location
    public bool inPlayerHand = false;
    public bool dicarded = false;
    public bool selected = false;
    private bool isVisable = false;

    public float spd;                          // Variables that control card movement from the MoveTo function below
    public float dis;

    public GameManager gm;
    public PlayerController player;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    private void Update()
    {
        FlipCard();            

    }


    public IEnumerator MoveTo(Vector3 target)
    {
        while (transform.position != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, spd);
            yield return new WaitForSeconds(0.1f);
        }
    }  // Function to move a card from the current position to a target position. Can be called from anywhere at anytime.

    private void OnMouseDown()
    {       
        if (gm.phase == TurnPhase.PlayerFirstTurn && inPlayerHand)
        {
            if (gm.cardPreview.isActiveAndEnabled)
            {
                gm.HideCard();
                player.canLook--;
            }
            else 
            {
                gm.PreviewCard(cardFace);
            }
        }
        else if (gm.phase == TurnPhase.PlayerTurn)
        {
            PlayerActions();

        }
        else 
            return;
    }

    private void PlayerActions() 
    {
        if (inOpponentHand)
            return;

        if (inPlayerHand)
        {
            print("This is your turn now");          
        }
        

    }               // Players actions, can be modified

    private void FlipCard()
    {

        if (!isVisable)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = cardBack;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = cardFace;
        }


        if (inPlayerHand || inOpponentHand)
            isVisable = false;
        if (dicarded)
            isVisable = true;
    }                  // Function that checks if the card should be face up or down

}
