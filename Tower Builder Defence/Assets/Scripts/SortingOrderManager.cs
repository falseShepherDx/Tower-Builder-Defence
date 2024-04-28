
using System;
using UnityEngine;

public class SortingOrderManager : MonoBehaviour
{ 
    private SpriteRenderer spriteRenderer;
    //bir defa çalışıp çalışmaması için bool. hareketsiz objeler için sadece bir defa çalıştırılcak hareketliler için kapalı olacak
    public bool runOnce;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void LateUpdate()
    {
        float precision=5f;
        //sorting order ı sprite ın y eksenindeki pozisyonuna göre ayarlıyoruz
        spriteRenderer.sortingOrder = (int)(-transform.position.y*precision);
        if(runOnce)Destroy(this);
    }
}
