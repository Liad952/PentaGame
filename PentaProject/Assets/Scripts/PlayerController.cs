using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gm;
    public GameObject[] playerHand = new GameObject[4];
    private bool firstTurn = true;
    private RaycastHit2D ray;
    private Vector3 mousePos;
    private GameObject selectedCard;

    private void Start()
    {
        GameObject[] playerCards = new GameObject[4];
        for(int i = 0;i< 4;i++)
        {
            playerCards[i] = playerHand[i].GetComponentInChildren<GameObject>().gameObject;
        }
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,-10));
        ray = Physics2D.Raycast(mousePos, Vector2.zero);
        if (Input.GetMouseButtonDown(0))
        {
            Hit();
        }
        if(firstTurn)
        {
            ShowCards();
        }
    }

    void ShowCards()
    {
        int x = 2;
        while (x != 0)
        {
            if(Input.GetMouseButtonDown(0))
            {

            }
        }

    }

    void Hit()
    {
        if (ray.collider == null)
            return;
        else
        {            
            if (ray.collider.transform.parent.tag == "PlayerHand")
            {
                if (gm.phase == TurnPhase.PlayerTurn)
                {
                    selectedCard = ray.collider.gameObject.GetComponentInChildren<GameObject>();
                }
            }else
            if (ray.collider.transform.parent.tag == "Deck")
            {
                if (gm.phase == TurnPhase.PlayerTurn)
                {
                    selectedCard = ray.collider.gameObject.GetComponentInChildren<GameObject>();
                }
            }else
            if (ray.collider.transform.parent.tag == "DiscardPile")
            {
                if (gm.phase == TurnPhase.PlayerTurn)
                {
                    selectedCard = ray.collider.gameObject.GetComponentInChildren<GameObject>();
                }

            }
            else
            {
                return;
            }
        }
        print(selectedCard.name);
    }

}
