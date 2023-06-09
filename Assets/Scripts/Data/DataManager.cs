using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    JsonManager jsonManager; // json에서 값을 읽어오거나 저장하는 JsonManager
    public SaveDataClass data; // 데이터를 저장하는 형식인 SaveDataClass
    public static DataManager Single;

    public GoogleSheetManager googleSheetManager { get; set; } // 스프레드시트에서 데이터를 가져오는 GoogleSheetManager
    //public ResourceDataManager resourceDataManager { get; set; } 현실적 이슈로 포기..

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
        //resourceDataManager = new ResourceDataManager(); 현실적 이슈로 포기..

        Load();

        data.inGameData.loadingCnt = 0;
        data.inGameData.maxCnt = 4;
        googleSheetManager.Single = Single;
        //resourceDataManager.Single = Single; 현실적 이슈로 포기..
        StartCoroutine(LoadAllData());
    }

    public void Save() // saveData에 기록된 데이터들을 json에 저장한다
    {
        jsonManager.SaveJson(data);
    }

    public void Load() // json에 기록돼있는 데이터들을 saveData에 볼러온다
    {
        data = jsonManager.LoadSaveData();
    }

    /*
    public void SpriteDataCoroutine(string name, string url)
    {
        StartCoroutine(resourceDataManager.GetTexture(name, url));
    } 현실적 이슈로 포기..
    */

    /*
    public void SpriteDataProcessing(string[] strings)
    {
        StartCoroutine(googleSheetManager.SpriteDataProcessing(strings));
    } 현실적 이슈로 포기..
    */

    IEnumerator LoadAllData()
    {
        StartCoroutine(googleSheetManager.GoogleSheetDataSetting(3));
        yield return new WaitForSeconds(1f);
        StartCoroutine(googleSheetManager.GoogleSheetDataSetting(7));
        yield return new WaitForSeconds(1f);
        StartCoroutine(googleSheetManager.GoogleSheetDataSetting(8));
        yield return new WaitForSeconds(1f);
        StartCoroutine(googleSheetManager.GoogleSheetDataSetting(9));
    }
}
