using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using UnityEngine.SocialPlatforms.Impl;
using System.Runtime.Serialization;

[System.Serializable]
public class GoogleSheetManager
{
    public DataManager Single;
    const string URL = "https://script.google.com/macros/s/AKfycbwoe-XZ-43s_TJzXmN30_QDLldQMoq5e5S4CYYW8fxM5Gu2pqRI5kpLv9hshtGn7fvk/exec";
    string[] strings;

    public IEnumerator GoogleSheetDataSetting(int page)
    {
        WWWForm form = new WWWForm();
        form.AddField("page", page);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        strings = data.Split(',');


        if(page == 3)
        {
            ImageDataProcessing(strings);
        }
        if(page == 7)
        {
            StoryDataProcessing(strings);
        }
        if(page == 8)
        {
            TextDataProcessing(strings);
        }
        if(page == 9)
        {
            SelectDataProcessing(strings);
        }
    }

    void StoryDataProcessing(string[] data)
    {
        int num = 19;
        List<StoryInfo> storys = new List<StoryInfo>();
        for (int i = 0; i < data.Length / num; i++)
        {
            StoryInfo story = new StoryInfo();
            story.number = int.Parse(data[i * num + 0]);
            story.image = data[i * num + 1];
            story.title = data[i * num + 2];
            story.content = data[i * num + 3];
            for(int j = 0; j < int.Parse(data[i * num + 4]); j++)
            {
                story.condition1.Add(int.Parse(data[i * num + 5 + j]));
            }
            for (int j = 0; j < int.Parse(data[i * num + 9]); j++)
            {
                story.condition1.Add(int.Parse(data[i * num + 10 + j]));
            }
            for (int j = 0; j < int.Parse(data[i * num + 11 + j]); j++)
            {
                story.condition1.Add(int.Parse(data[i * num + 12 + j]));
            }
            storys.Add(story);
        }
        Single.data.storyData.storyInfo = storys;
        Single.data.inGameData.loadingCnt++;
    }

    void TextDataProcessing(string[] data)
    {
        int num = 17;
        List<TextInfo> texts = new List<TextInfo>();
        for(int i=0; i < data.Length / num; i++)
        {
            TextInfo text = new TextInfo();
            string[] temps = data[i * num + 0].Split('_');
            foreach(string temp in temps)
            {
                text.branch.Add(int.Parse(temp));
            }
            for (int j = 0; j < int.Parse(data[i * num + 1]); j++)
            {
                text.charaterName.Add(data[i * num + 2 + j]);
            }
            for (int j = 0; j < int.Parse(data[i * num + 6]); j++)
            {
                text.charaterSprite.Add(data[i * num + 7 + j]);
            }
            text.charaterText = data[i * num + 11];
            text.charaterVoice = data[i * num + 12];
            text.BG = data[i * num + 13];
            text.charaterLocationType = int.Parse(data[i * num + 14]);
            text.selectType = int.Parse(data[i * num + 15]);
            text.BGChangeEffect = int.Parse(data[i * num + 16]);
            texts.Add(text);
        }
        Single.data.textData.textInfo = texts;
        Single.data.inGameData.loadingCnt++;
    }

    void SelectDataProcessing(string[] data)
    {
        int num = 11;
        List<SelectInfo> selects = new List<SelectInfo>();
        for (int i = 0; i < data.Length / num; i++)
        {
            SelectInfo select = new SelectInfo();
            select.selectType = int.Parse(data[i * num + 0]);
            for (int j = 0; j < int.Parse(data[i * num + 1]); j++)
            {
                select.selectText.Add(data[i * num + 2 + j]);
            }
            for (int j = 0; j < int.Parse(data[i * num + 6]); j++)
            {
                select.branchChange.Add(int.Parse(data[i * num + 7 + j]));
            }
            selects.Add(select);
        }
        Single.data.selectData.selectInfo = selects;
        Single.data.inGameData.loadingCnt++;
    }

    void ImageDataProcessing(string[] data)
    {
        Single.data.spriteData.sprite = new Dictionary<string, Sprite>();
        for (int i = 0; i < data.Length; i++)
        {
            Single.data.spriteData.sprite.Add(data[i], Resources.Load<Sprite>("Sprite/" + data[i]));
        }
        Single.data.inGameData.loadingCnt++;
    }
}