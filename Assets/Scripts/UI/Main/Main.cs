using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    DataManager Single;

    GameObject BG;
    GameObject charater;
    GameObject quest;
    GameObject stat;
    void Start()
    {
        Single = DataManager.Single;
        BG = transform.GetChild(0).gameObject;
        charater = transform.GetChild(1).gameObject;
        quest = transform.GetChild(2).gameObject;
        stat = transform.GetChild(3).gameObject;
        BGSetting();
    }

    void BGSetting()
    {
        BG.GetComponent<Image>().sprite = Single.data.spriteData.sprite["기본배경_메인"];
    }
}
