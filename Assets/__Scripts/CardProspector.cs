using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCardState
{
    drawpile,
    tableau,
    target,
    discard
}

public class CardProspector : Card
{ 
    [Header("Set Dynamically: CardProspector")]
    public eCardState state = eCardState.drawpile;
    public List<CardProspector> hiddenBy = new List<CardProspector>();
    public int layoutID;
    public SlotDef slotDef;

    private string archiveLayer;
    private int archiveOrder;
    private Vector3 archivePos;

    override public void OnMouseDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        archiveLayer = this.GetComponent<SpriteRenderer>().sortingLayerName;
        archiveOrder = this.GetComponent<SpriteRenderer>().sortingOrder;
        archivePos = this.transform.position;

        this.GetComponent<SpriteRenderer>().sortingLayerName = "Draw";
        this.GetComponent<SpriteRenderer>().sortingOrder = 5;
        this.transform.position = new Vector3(mousePos.x, mousePos.y, -10);        
    }
    public void OnMouseUp()
    {    
        Vector2 targetRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D targetHit = Physics2D.Raycast(targetRay, Vector2.zero);

        Vector3 pos = new Vector3(targetRay.x,targetRay.y, 0);
        Debug.Log("released on correct target");

        if (targetHit)
        {
            Target target = targetHit.collider.gameObject.GetComponent<Target>();

            if(this.suit == target.suit)
            {
                Debug.Log("released on correct target");
                target.NestCard(this);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sortingLayerName = archiveLayer;
                this.GetComponent<SpriteRenderer>().sortingOrder = archiveOrder;
                this.transform.position = archivePos;
            }
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sortingLayerName = archiveLayer;
            this.GetComponent<SpriteRenderer>().sortingOrder = archiveOrder;
            this.transform.position = archivePos;
        }
    }
}
