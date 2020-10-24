using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("What's good:")]
    public string suit;

    [Header("nah")]
    public List<CardProspector> stack;
    public bool isFull;

    
    void Start()
    {
        

        isFull = false;
    }

    void Update()
    {
        if (stack.Count == 13 && isFull == false) isFull = true;
    }

    public void NestCard(CardProspector card)
    {
        if(stack[stack.Count].rank == card.rank - 1 || stack.Count == 0)
        {
            card.transform.position = this.transform.position;
            stack.Add(card);
        }
    }
}
