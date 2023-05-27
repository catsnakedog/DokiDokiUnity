using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

[System.Serializable]
public class GoogleSheetManager
{
    public DataManager Data;
    const string URL = "https://script.google.com/macros/s/AKfycbytSlQ0aqSlIunUutGTm9tUNXmUbF1pDc2nUWCJxxBpjG5vA4KspSqGQOV3eMFar_VM/exec";
    string[] strings;
    List<TextType> processingData;

    public List<TextType> GetData()
    {
        return processingData;
    }

    public IEnumerator GoogleSheetDataSetting()
    {
        WWWForm form = new WWWForm();
        form.AddField("page", 0);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        strings = data.Split(',');

        processingData = DataProcessing(strings);
        Data.saveData.gameData.textTypes = processingData;
    }

    List<TextType> DataProcessing(string[] data)
    {
        List<TextType> texts = new List<TextType>();
        for(int i=0; i < data.Length/7; i++)
        {
            TextType text = new TextType();
            text.branch = int.Parse(data[i*7 + 0]);
            text.charaterName = data[i*7 + 1];
            text.Ctext = data[i*7 + 2];
            text.Cvoice = data[i*7 + 3];
            text.BG = data[i*7 + 4];
            text.ClocationType = int.Parse(data[i*7 + 5]);
            text.eventType = int.Parse(data[i*7 + 6]);
            texts.Add(text);
        }
        return texts;
    }
}