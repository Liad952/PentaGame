using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TurnPhase { Start, PlayerFirstTurn, PlayerTurn, DrawNew, DrawDiscarded, Switch, Use, OpponentTurn, Win, Lose }

public class GameManager : MonoBehaviour
{

    public TurnPhase phase;                       //  The game's current phase

    private List<Message> messageList = new List<Message>();   // List of the log messages that were sent to the log
    public int maxMessages = 25;
    public GameObject logPanel;                                // The object that projects the messages
    public GameObject textObject;

    public Image cardPreview;                                  // The object that holds the card to preview it to the player
    public Image oppCardPreview;
    public Image playerCardPreview;
    public GameObject buttons;
    private Card drawnCard;
    public PlayerController playerCon;
    public OpponantAI opponent;
    public Transform discardPile;

    private void Awake()
    {
        phase = TurnPhase.Start;
        cardPreview = playerCardPreview;
    }

    private void Update()
    {
        switch(phase)
        {
            case TurnPhase.PlayerTurn:
                cardPreview = playerCardPreview;
                break;

            case TurnPhase.OpponentTurn:
                cardPreview = oppCardPreview;
                break;
        }
    }

    public void SendMessageToLog(string message)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.RemoveAt(0);
        }
        Message newMessage = new Message();
        newMessage.text = message;

        GameObject newText = Instantiate(textObject, logPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = message;

        messageList.Add(newMessage);

    } // Function to send message to game log. Can be called from anywhere at anypoint.

    public void PreviewCard(Card card)
    {
        if (cardPreview.isActiveAndEnabled)
        {
            HideCard();
            if(phase== TurnPhase.PlayerFirstTurn)
            {
                playerCon.canLook--;
            }
        }
        else
        {
            drawnCard = card;
            cardPreview.gameObject.SetActive(true);
            cardPreview.sprite = card.cardFace;
        }     

    } // Function to Show the card that was played.

    public void HideCard()
    {
        cardPreview.gameObject.SetActive(false);

    } // Function to hide the card that was shown.

    public void SwitchCard()
    {
        buttons.SetActive(false);
        phase = TurnPhase.Switch;
    }  // This fuction is called through the UI button.

    public void Discard()
    {
        buttons.SetActive(false);
        SendToDiscardPile(drawnCard.transform);
        phase = TurnPhase.OpponentTurn;
        SendMessageToLog("Opponent's Turn");
    }

    public void UseCard()
    {
        buttons.SetActive(false);
        SendToDiscardPile(drawnCard.transform);
        // add cards abilities.
        phase = TurnPhase.OpponentTurn;
        SendMessageToLog("Opponent's Turn");
    }

    public void SwitchParents(Transform parent1, Transform parent2, Transform child1, Transform child2)
    {
        child1.SetParent(null); child2.SetParent(null);
        StartCoroutine(child1.GetComponent<Card>().MoveTo(parent2.position));
        child1.SetParent(parent2);
        StartCoroutine(child2.GetComponent<Card>().MoveTo(parent1.position)); 
        child2.SetParent(parent1);
        
        HideCard();
    }  // General function to change parents of gameobjects.

    public void SendToDiscardPile(Transform card)
    {
        card.parent = discardPile;
        StartCoroutine(card.GetComponent<Card>().MoveTo(discardPile.position));
        card.GetComponent<Card>().dicarded = true;
        card.GetComponent<Card>().inOpponentHand = false;
        card.GetComponent<Card>().inPlayerHand = false;
        discardPile.GetComponent<DiscardPile>().AddToPile(card.GetComponent<Card>());
        HideCard();
    }  // General function to discard a card.

    public void PlayAiTurn()
    {
        if (phase != TurnPhase.OpponentTurn)
            return;

        StartCoroutine(opponent.RandomPlay());
    }

}


[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}
