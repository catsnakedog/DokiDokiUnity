using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Option : MonoBehaviour
{
    DataManager Single;
    MainController main;

    GameObject BGM;
    GameObject SFX;
    GameObject fontSize;
    GameObject textSpeed;
    GameObject left;
    GameObject auto;

    void Start()
    {
        Time.timeScale = 0;
        Single = DataManager.Single;
        main = MainController.main;
        BGM = transform.GetChild(0).GetChild(0).gameObject;
        SFX = transform.GetChild(0).GetChild(1).gameObject;
        fontSize = transform.GetChild(0).GetChild(2).gameObject;
        textSpeed = transform.GetChild(0).GetChild(3).gameObject;
        left = transform.GetChild(0).GetChild(4).gameObject;
        auto = transform.GetChild(0).GetChild(5).gameObject;

        BGM.GetComponent<Slider>().onValueChanged.AddListener(BGMChange);
        SFX.GetComponent<Slider>().onValueChanged.AddListener(SFXChange);
        fontSize.GetComponent<Slider>().onValueChanged.AddListener(FontSizeChange);
        textSpeed.GetComponent<Slider>().onValueChanged.AddListener(TextSpeedChange);
        left.GetComponent<Button>().onClick.AddListener(Left);
        auto.GetComponent<Button>().onClick.AddListener(Auto);

        init();
    }

    void Auto()
    {
        if(Single.data.optionData.auto)
        {
            Single.data.optionData.auto = false;
            auto.transform.GetChild(0).GetComponent<TMP_Text>().text = "OFF";
        }
        else
        {
            Single.data.optionData.auto = true;
            auto.transform.GetChild(0).GetComponent<TMP_Text>().text = "ON";
        }
    }

    void Left()
    {
        Time.timeScale = 1f;
        Single.Save();
        Destroy(this.gameObject);
    }

    void init()
    {
        BGM.GetComponent<Slider>().value = Single.data.optionData.volumeBGM;
        BGM.transform.GetChild(0).GetComponent<TMP_Text>().text = ((int)(Single.data.optionData.volumeBGM * 100)).ToString();
        SFX.GetComponent<Slider>().value = Single.data.optionData.volumeSFX;
        SFX.transform.GetChild(0).GetComponent<TMP_Text>().text = ((int)(Single.data.optionData.volumeSFX * 100)).ToString();

        int temp1 = 0;
        if (Single.data.optionData.fontSize == 30)
        {
            temp1 = 0;
            fontSize.transform.GetChild(0).GetComponent<TMP_Text>().text = "작음";
        }
        if (Single.data.optionData.fontSize == 60)
        {
            temp1 = 1;
            fontSize.transform.GetChild(0).GetComponent<TMP_Text>().text = "중간";
        }
        if (Single.data.optionData.fontSize == 90)
        {
            temp1 = 2;
            fontSize.transform.GetChild(0).GetComponent<TMP_Text>().text = "크다";
        }
        fontSize.GetComponent<Slider>().value = temp1;
        int temp2 = 3;
        if(Single.data.optionData.textSpeed == 0.1f)
        {
            temp2 = 0;
            textSpeed.transform.GetChild(0).GetComponent<TMP_Text>().text = "느림";
        }
        if (Single.data.optionData.textSpeed == 0.05f)
        {
            temp2 = 1;
            textSpeed.transform.GetChild(0).GetComponent<TMP_Text>().text = "중간";
        }
        if (Single.data.optionData.textSpeed == 0.02f)
        {
            temp2 = 2;
            textSpeed.transform.GetChild(0).GetComponent<TMP_Text>().text = "빠름";
        }
        if (Single.data.optionData.textSpeed == 0f)
        {
            temp2 = 3;
            textSpeed.transform.GetChild(0).GetComponent<TMP_Text>().text = "없음";
        }
        textSpeed.GetComponent<Slider>().value = temp2;

        if(Single.data.optionData.auto)
        {
            auto.transform.GetChild(0).GetComponent<TMP_Text>().text = "ON";
        }
        else
        {
            auto.transform.GetChild(0).GetComponent<TMP_Text>().text = "OFF";
        }
    }

    void BGMChange(float num)
    {
        Single.data.optionData.volumeBGM = num;
        main.sound.BGMSource.volume = num;
        BGM.transform.GetChild(0).GetComponent<TMP_Text>().text = ((int)(num * 100)).ToString();
    }

    void SFXChange(float num)
    {
        Single.data.optionData.volumeSFX = num;
        main.sound.SFXSource.volume = num;
        SFX.transform.GetChild(0).GetComponent<TMP_Text>().text = ((int)(num * 100)).ToString();
    }

    void FontSizeChange(float num)
    {
        if ((int)num == 0)
        {
            fontSize.transform.GetChild(0).GetComponent<TMP_Text>().text = "작음";
            Single.data.optionData.fontSize = 30;
        }
        if ((int)num == 1)
        {
            fontSize.transform.GetChild(0).GetComponent<TMP_Text>().text = "중간";
            Single.data.optionData.fontSize = 60;
        }
        if ((int)num == 2)
        {
            fontSize.transform.GetChild(0).GetComponent<TMP_Text>().text = "크다";
            Single.data.optionData.fontSize = 90;
        }
    }

    void TextSpeedChange(float num)
    {
        if((int)num == 0)
        {
            textSpeed.transform.GetChild(0).GetComponent<TMP_Text>().text = "느림";
            Single.data.optionData.textSpeed = 0.1f;
        }
        if ((int)num == 1)
        {
            textSpeed.transform.GetChild(0).GetComponent<TMP_Text>().text = "중간";
            Single.data.optionData.textSpeed = 0.05f;
        }
        if ((int)num == 2)
        {
            textSpeed.transform.GetChild(0).GetComponent<TMP_Text>().text = "빠름";
            Single.data.optionData.textSpeed = 0.02f;
        }
        if ((int)num == 3)
        {
            textSpeed.transform.GetChild(0).GetComponent<TMP_Text>().text = "없음";
            Single.data.optionData.textSpeed = 0f;
        }
    }
}