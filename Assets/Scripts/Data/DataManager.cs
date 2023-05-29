using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    JsonManager jsonManager; // json에서 값을 읽어오거나 저장하는 JsonManager
    GoogleSheetManager googleSheetManager; // 스프레드시트에서 데이터를 가져오는 GoogleSheetManager
    public SaveDataClass data; // 데이터를 저장하는 형식인 SaveDataClass
    public static DataManager Single;

    void Awake()
    {
        if (Single == null) // DataManager의 유일성 보장
        {
            Single = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        jsonManager = new JsonManager();
        data = new SaveDataClass();
        googleSheetManager = new GoogleSheetManager();

        Load();

        googleSheetManager.Single = Single;
        StartCoroutine(googleSheetManager.GoogleSheetDataSetting());
    }

    public void Save() // saveData에 기록된 데이터들을 json에 저장한다
    {
        jsonManager.SaveJson(data);
    }

    public void Load() // json에 기록돼있는 데이터들을 saveData에 볼러온다
    {
        data = jsonManager.LoadSaveData();
    }
}
