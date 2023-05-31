using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.UIElements;

[System.Serializable]
public class GoogleSheetManager
{
    public DataManager Single;
    const string URL = "https://script.google.com/macros/s/AKfycbytSlQ0aqSlIunUutGTm9tUNXmUbF1pDc2nUWCJxxBpjG5vA4KspSqGQOV3eMFar_VM/exec";
    string[] strings;

    public IEnumerator GoogleSheetDataSetting(int page)
    {
        WWWForm form = new WWWForm();
        form.AddField("page", page);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        strings = data.Split(',');

        if(page == 0)
        {
            TextDataProcessing(strings);
        }
        if(page == 1)
        {
            Single.SpriteDataProcessing(strings);
        }
        if(page == 2)
        {
            QuestDataProcessing(strings);
        }
    }

    void TextDataProcessing(string[] data)
    {
        List<TextInfo> texts = new List<TextInfo>();
        for(int i=0; i < data.Length/7; i++)
        {
            TextInfo text = new TextInfo();
            string[] temps = data[i * 7 + 0].Split('_');
            foreach(string temp in temps)
            {
                text.branch.Add(int.Parse(temp));
            }
            text.charaterName = data[i*7 + 1];
            text.Ctext = data[i*7 + 2];
            text.Cvoice = data[i*7 + 3];
            text.BG = data[i*7 + 4];
            text.ClocationType = int.Parse(data[i*7 + 5]);
            text.eventType = int.Parse(data[i*7 + 6]);
            texts.Add(text);
        }
        Single.data.textData.textInfo = texts;
    }

    public IEnumerator SpriteDataProcessing(string[] data)
    {
        Single.data.spriteData.sprite.Clear();
        Single.data.inGameData.maxCnt = data.Length / 2;
        for (int i = 0; i < data.Length / 2; i++)
        {
            yield return new WaitForSeconds(1f);
            Single.resourceDataManager.GetSprite(data[2 * i + 0], data[2 * i + 1]);
        }
    }

    void QuestDataProcessing(string[] data)
    {
        List<StoryInfo> texts = new List<StoryInfo>();
        for (int i = 0; i < data.Length / 7; i++)
        {
            StoryInfo text = new StoryInfo();
            text.number = int.Parse(data[i * 7 + 0]);
            text.img = data[i * 7 + 1];
            text.title = data[i * 7 + 2];
            text.content = data[i * 7 + 3];
            string[] temps = data[i * 7 + 4].Split('_');
            foreach (string temp in temps)
            {
                text.condition1.Add(int.Parse(temp));
            }
            temps = data[i * 7 + 5].Split('_');
            foreach (string temp in temps)
            {
                text.condition2.Add(int.Parse(temp));
            }
            if (data[i * 7 + 6] == "0")
            {
                text.repeat = false;
            }
            else
            {
                text.repeat = true;
            }
            texts.Add(text);
        }
        Single.data.storyData.storyInfo = texts;
    }
}