using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop :MonoBehaviour
{
    public GameObject[] items;

    private void Start()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(false);
        }

        DropItem();
    }
    public void DropItem()
    {
        int dropItemId = ChooseItem();
        items[dropItemId].SetActive(true);
        items[dropItemId].transform.position = this.gameObject.transform.position;
    }

    public int ChooseItem()
    {
        int result = Random.Range(0, 210);
        if (result >= 0 && result < 5) //5%
        {
            Debug.Log("S등급 아이템");
            return 0;
        }
        if (result >= 5 && result < 15) //10%
        {            
            Debug.Log("A등급 아이템");
            return 1;
        }
        if (result >= 15 && result < 35) //20%
        {            
            Debug.Log("B등급 아이템");
            return 2;
        }
        if (result >= 35 && result < 70) //45%
        {            
            Debug.Log("C등급 아이템");
            return 3;
        }
        if (result >=70 && result < 120) //50%
        {            
            Debug.Log("D등급 아이템");
            return 4;
        }
        else
        {
            Debug.Log("E등급 아이템");

            return 5;

        }

       // Debug.Log("아이템");

    }
    /* float Choose(float[] probs)
     {

         float total = 0;

         foreach (float elem in probs)
             total += elem;

         float randomPoint = Random.value * total;

         for (int i = 0; i < probs.Length; i++)
         {
             if(randomPoint<probs[i])
             {
                 switch(i)
                 {
                     case 0:
                         Debug.Log("첫번째 아이템");
                         break;
                     case 1:
                         Debug.Log("두번째 아이템");
                         break;

                 }
                 return i;
             }
             else
             {
                 randomPoint -= probs[i];
             }
         }
         return probs.Length - 1;
     }*/
}
