using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_ : MonoBehaviour
{
    [SerializeField] GameObject tower;
    [SerializeField] GameObject ObjectsParent;
    private SpriteRenderer sr;
    private Color mainColor;
    public Color triggeredColor;
    private bool isMouseOn;
    private bool isBuildAble = true;
    public bool isBuildMod = true;

    void Start()
    {
        sr= GetComponent<SpriteRenderer>();
        mainColor = sr.color;

        ObjectsParent = GameObject.Find("ObjectsParent");
    }

 
    void Update()
    {
        if(isMouseOn && isBuildMod) 
        {
            sr.color = triggeredColor;
        }
        else if (!isMouseOn && isBuildMod)
        {
            sr.color= mainColor;    
        }
    }
    private void OnMouseOver()
    {
        isMouseOn = true;  
    }
    private void OnMouseExit()
    {
        isMouseOn= false;
    }
    private void OnMouseDown()
    {
        if(isBuildMod) 
        {
            Build();
        }

    }
    private void Build()
    {
        if (isMouseOn && isBuildAble)
        {
            GameObject tower_ = Instantiate(tower, this.transform.position, Quaternion.identity);
            isBuildAble = false;
            tower_.transform.parent = ObjectsParent.transform;
        }
    }


}
