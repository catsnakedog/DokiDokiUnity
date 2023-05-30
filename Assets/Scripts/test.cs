using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    DataManager Single;

    public void Start()
    {
        Single = DataManager.Single;
    }
    public void test1(int n)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Single.data.spriteData.sprite["Å×½ºÆ®"+n];
    }
}
