using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOn : MonoBehaviour
{
    public string something;
    public GameObject[] lighting;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals(something))
        {
            for (int i = 0; i < lighting.Length; i++)
            {
                lighting[i].SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals(something))
        {
            for (int i = 0; i < lighting.Length; i++)
            {
                lighting[i].SetActive(false);
            }
        }
    }
}
