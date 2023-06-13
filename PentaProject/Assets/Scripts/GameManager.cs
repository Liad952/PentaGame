using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TurnPhase { Start, PlayerTurn, DrawNew, DrawDiscarded, Switch, Use, OpponentTurn, Win, Lose }

public class GameManager : MonoBehaviour
{

    public TurnPhase phase;

    private List<Message> messageList = new List<Message>();
    public int maxMessages = 25;
    public GameObject logPanel;
    public GameObject textObject;

    private void Awake()
    {
        phase = TurnPhase.Start;
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

    }

}


[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}
