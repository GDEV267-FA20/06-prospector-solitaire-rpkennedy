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

    private Vector3 archive0;
    private int archive1;
    override public void OnMouseDrag()
    {
        archive0 = this.transform.position;
        archive1 = this.slotDef.layerID;

        this.slotDef.layerID = 5;
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);        
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
                this.transform.position = archive0;
                this.slotDef.layerID = archive1;
            }
        }
    }
}
