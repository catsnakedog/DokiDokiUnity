using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class LogTextBox : MonoBehaviour
{
    DataManager Single;
    MainController main;

    GameObject leftImage;
    GameObject rightImage;
    TMP_Text content;

    public TextInfo textInfo;

    void Start()
    {
        Single = DataManager.Single;
        main = MainController.main;
        leftImage = transform.GetChild(0).GetChild(0).gameObject;
        rightImage = transform.GetChild(0).GetChild(1).gameObject;
        content = transform.GetChild(1).GetComponent<TMP_Text>();

        SetLogText(textInfo);
    }

    public void SetLogText(TextInfo log)
    {
        if (log.charaterName[0] == "unity")
        {
            rightImage.GetComponent<Image>().sprite = Single.data.spriteData.sprite["unity"];
            rightImage.SetActive(true);
        }
        else
        {
            leftImage.GetComponent<Image>().sprite = Single.data.spriteData.sprite[log.charaterName[0]];
            leftImage.SetActive(true);
        }

        content.text = log.charaterText;
    }
}
