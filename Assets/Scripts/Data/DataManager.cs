using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    JsonManager jsonManager; // json���� ���� �о���ų� �����ϴ� JsonManager
    public SaveDataClass data; // �����͸� �����ϴ� ������ SaveDataClass
    public static DataManager Single;

    public GoogleSheetManager googleSheetManager { get; set; } // ���������Ʈ���� �����͸� �������� GoogleSheetManager
    public ResourceDataManager resourceDataManager { get; set; }

    void Awake()
    {
        if (Single == null) // DataManager�� ���ϼ� ����
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
        resourceDataManager = new ResourceDataManager();

        Load();

        googleSheetManager.Single = Single;
        resourceDataManager.Single = Single;
        StartCoroutine(LoadAllData());
    }

    public void Save() // saveData�� ��ϵ� �����͵��� json�� �����Ѵ�
    {
        jsonManager.SaveJson(data);
    }

    public void Load() // json�� ��ϵ��ִ� �����͵��� saveData�� �����´�
    {
        data = jsonManager.LoadSaveData();
    }

    public void SpriteDataCoroutine(string name, string url)
    {
        StartCoroutine(resourceDataManager.GetTexture(name, url));
    }

    public void SpriteDataProcessing(string[] strings)
    {
        StartCoroutine(googleSheetManager.SpriteDataProcessing(strings));
    }

    IEnumerator LoadAllData()
    {
        StartCoroutine(googleSheetManager.GoogleSheetDataSetting(0));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(googleSheetManager.GoogleSheetDataSetting(1));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(googleSheetManager.GoogleSheetDataSetting(2));
    }
}
