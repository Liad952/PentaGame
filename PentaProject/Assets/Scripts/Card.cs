using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public int cardNum;
    public Sprite cardFace;
    public Sprite cardBack;

    public bool inHand = false;
    public bool dicarded = false;
    public bool selected = false;
    private bool isVisable = false;

    public float spd;
    public float dis;

    private void Update()
    {
        if(!isVisable)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = cardBack;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = cardFace;
        }


        if(inHand) 
            isVisable= false;
        if(dicarded)
            isVisable= true;

    }


    public IEnumerator MoveTo(Transform target)
    {
        while (transform.position != target.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, spd);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
}
