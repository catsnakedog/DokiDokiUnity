using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

[System.Serializable]
public class GoogleSheetManager
{
    public DataManager Single;
    const string URL = "https://script.google.com/macros/s/AKfycbytSlQ0aqSlIunUutGTm9tUNXmUbF1pDc2nUWCJxxBpjG5vA4KspSqGQOV3eMFar_VM/exec";
    string[] strings;
    List<TextInfo> processingData;

    public List<TextInfo> GetData()
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
        Single.data.textData.textInfo = processingData;
    }

    List<TextInfo> DataProcessing(string[] data)
    {
        List<TextInfo> texts = new List<TextInfo>();
        for(int i=0; i < data.Length/7; i++)
        {
            TextInfo text = new TextInfo();
            text.branch = data[i*7 + 0];
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