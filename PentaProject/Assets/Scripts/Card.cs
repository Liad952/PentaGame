using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{

    public int cardNum;                       // The card's information
    public Sprite cardFace;
    public Sprite cardBack;
    public int cardPosInHand;

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
            transform.position = Vector3.MoveTowards(transform.position, target, dis);
            yield return new WaitForSeconds(0.1f);
        }
    }  // Function to move a card from the current position to a target position. Can be called from anywhere at anytime.

    private void OnMouseDown()
    {
        if (inOpponentHand)
            return;

        if (gm.phase == TurnPhase.Switch)
        {
            player.SwitchCards(cardPosInHand);
            return;
        }

        if (player.canLook != 0)
        {
            gm.PreviewCard(this);
        }

    }



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


        if (dicarded)
            isVisable = true;
        else
            isVisable = false;
    }                  // Function that checks if the card should be face up or down


}
