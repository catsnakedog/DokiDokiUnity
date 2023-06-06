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

    GameObject BG; // 배경
    GameObject LoadingBar; // 로딩바
    TMP_Text percent; // 퍼센트
    TMP_Text content; // 로딩바 위에 모하는 중인지

    void Start()
    {
        Single = DataManager.Single;
        mainController = MainController.main;
        BG = transform.GetChild(0).gameObject;
        LoadingBar = transform.GetChild(1).gameObject;
        content = LoadingBar.transform.GetChild(0).GetComponent<TMP_Text>();
        percent = LoadingBar.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<TMP_Text>();

        Single.data.inGameData.crruentStat = "Loading";

        //BG.GetComponent<SpriteRenderer>().sprite = Single.data.spriteData.sprite["기본배경_로딩"];
        LoadingBar.GetComponent<Slider>().value = (float)Single.data.inGameData.loadingCnt / (float)Single.data.inGameData.maxCnt;
        percent.text = ((float)Single.data.inGameData.loadingCnt / (float)Single.data.inGameData.maxCnt * 100).ToString();
        content.text = "이미지를 불러오는중~";
    }

    void FixedUpdate()
    {
        percent.text = ((float)Single.data.inGameData.loadingCnt / (float)Single.data.inGameData.maxCnt * 100).ToString() + "%";
        content.text = "이미지를 불러오는중~";
        LoadingBar.GetComponent<Slider>().value = (float)Single.data.inGameData.loadingCnt / (float)Single.data.inGameData.maxCnt;

        if (Single.data.inGameData.loadingCnt == Single.data.inGameData.maxCnt)
        {
            content.text = "로딩완료";
            Single.Save();
            mainController.UI.UIsetting(Define.UIlevel.Level1, Define.UItype.Main);
            Destroy(gameObject);
        }

        switch (Single.data.inGameData.loadingCnt)
        {
            case 1:
                content.text = "이미지 데이터를 가져오는중";
                return;
            case 2:
                content.text = "스토리 데이터를 가져오는중";
                return;
            case 3:
                content.text = "텍스트 데이터를 가져오는중";
                return;
            case 4:
                content.text = "선택지 데이터를 가져오는중";
                return;
        }
    }
}
