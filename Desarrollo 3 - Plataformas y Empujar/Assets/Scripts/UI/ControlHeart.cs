using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ControlHeart : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite halfHeart;

    [SerializeField] bool isFull = true;

    //=============================

    public bool GetState()
    {
        return isFull;
    }

    public void ReduceLife(bool state)
    {
        if (state)
        {
            this.transform.GetComponent<Image>().sprite = fullHeart;
            isFull = true;
        }
        else
        {
            this.transform.GetComponent<Image>().sprite = halfHeart;
            isFull = false;
        }
    }

}
