using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    DataManager Single;
    MainController main;
    public int number; // 인게임으로 넘어갈때 스토리 넘버 전달

    void Start()
    {
        Single = DataManager.Single;
        main = MainController.main;
    }

    public void InGameStart()
    {
        Single.data.inGameData.number = number;
        main.UI.UIsetting(Define.UIlevel.Level1, Define.UItype.InGame);
    }
}