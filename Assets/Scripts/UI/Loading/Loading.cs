using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Loading : MonoBehaviour
{
    DataManager Single;
    MainController mainController;

    GameObject BG; // ���
    GameObject LoadingBar; // �ε���
    TMP_Text percent; // �ۼ�Ʈ
    TMP_Text content; // �ε��� ���� ���ϴ� ������

    void Start()
    {
        Single = DataManager.Single;
        mainController = MainController.main;
        BG = transform.GetChild(0).gameObject;
        LoadingBar = transform.GetChild(1).gameObject;
        content = LoadingBar.transform.GetChild(0).GetComponent<TMP_Text>();
        percent = LoadingBar.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<TMP_Text>();

        Single.data.inGameData.crruentStat = "Loading";

        //BG.GetComponent<SpriteRenderer>().sprite = Single.data.spriteData.sprite["�⺻���_�ε�"];
        LoadingBar.GetComponent<Slider>().value = (float)Single.data.inGameData.loadingCnt / (float)Single.data.inGameData.maxCnt;
        percent.text = ((float)Single.data.inGameData.loadingCnt / (float)Single.data.inGameData.maxCnt * 100).ToString();
        content.text = "�̹����� �ҷ�������~";
    }

    void FixedUpdate()
    {
        percent.text = ((float)Single.data.inGameData.loadingCnt / (float)Single.data.inGameData.maxCnt * 100).ToString() + "%";
        content.text = "�̹����� �ҷ�������~";
        LoadingBar.GetComponent<Slider>().value = (float)Single.data.inGameData.loadingCnt / (float)Single.data.inGameData.maxCnt;

        if (Single.data.inGameData.loadingCnt == Single.data.inGameData.maxCnt)
        {
            content.text = "�ε��Ϸ�";
            Single.Save();
            mainController.UI.UIsetting(Define.UIlevel.Level1, Define.UItype.Main);
            Destroy(gameObject);
        }

        switch (Single.data.inGameData.loadingCnt)
        {
            case 1:
                content.text = "�̹��� �����͸� ����������";
                return;
            case 2:
                content.text = "���丮 �����͸� ����������";
                return;
            case 3:
                content.text = "�ؽ�Ʈ �����͸� ����������";
                return;
            case 4:
                content.text = "������ �����͸� ����������";
                return;
        }
    }
}
