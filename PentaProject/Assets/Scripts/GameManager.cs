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
    public GameObject buttons;
    private Card drawnCard;
    public PlayerController playerCon;

    private void Awake()
    {
        phase = TurnPhase.Start;
    }
    private void Update()
    {
        if(phase == TurnPhase.DrawNew || phase == TurnPhase.DrawDiscarded)
        {
            buttons.SetActive(true);
        }
        else
        {
            buttons.SetActive(false);
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
            if (playerCon.canLook > 0)
            {
                playerCon.canLook--;
            }
            return;
        }       
            drawnCard = card;
            cardPreview.gameObject.SetActive(true);
            cardPreview.sprite = card.cardFace;
        
    } // Function to Show the card that was played.
    public void HideCard()
    {
        cardPreview.gameObject.SetActive(false);
    } // Function to hide the card that was shown.

    public void SwitchCard()
    {
        buttons.SetActive(false);
        phase = TurnPhase.Switch;
    }



}


[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}
