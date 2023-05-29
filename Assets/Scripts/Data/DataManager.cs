using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    JsonManager jsonManager; // json���� ���� �о���ų� �����ϴ� JsonManager
    GoogleSheetManager googleSheetManager; // ���������Ʈ���� �����͸� �������� GoogleSheetManager
    public SaveDataClass data; // �����͸� �����ϴ� ������ SaveDataClass
    public static DataManager Single;

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

        Load();

        googleSheetManager.Single = Single;
        StartCoroutine(googleSheetManager.GoogleSheetDataSetting());
    }

    public void Save() // saveData�� ��ϵ� �����͵��� json�� �����Ѵ�
    {
        jsonManager.SaveJson(data);
    }

    public void Load() // json�� ��ϵ��ִ� �����͵��� saveData�� �����´�
    {
        data = jsonManager.LoadSaveData();
    }
}
