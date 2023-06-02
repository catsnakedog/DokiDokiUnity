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
        List<StoryInfo> storys = new List<StoryInfo>();
        for (int i = 0; i < data.Length / 19; i++)
        {
            StoryInfo story = new StoryInfo();
            story.number = int.Parse(data[i * 19 + 0]);
            story.image = data[i * 19 + 1];
            story.title = data[i * 19 + 2];
            story.content = data[i * 19 + 3];
            for(int j = 0; j < int.Parse(data[i * 19 + 4]); j++)
            {
                story.condition1.Add(int.Parse(data[i * 19 + 5 + j]));
            }
            for (int j = 0; j < int.Parse(data[i * 19 + 9]); j++)
            {
                story.condition1.Add(int.Parse(data[i * 19 + 10 + j]));
            }
            for (int j = 0; j < int.Parse(data[i * 19 + 11 + j]); j++)
            {
                story.condition1.Add(int.Parse(data[i * 19 + 12 + j]));
            }
            storys.Add(story);
        }
        Single.data.storyData.storyInfo = storys;
        Single.data.inGameData.loadingCnt++;
    }

    void TextDataProcessing(string[] data)
    {
        List<TextInfo> texts = new List<TextInfo>();
        for(int i=0; i < data.Length/16; i++)
        {
            TextInfo text = new TextInfo();
            string[] temps = data[i * 16 + 0].Split('_');
            foreach(string temp in temps)
            {
                text.branch.Add(int.Parse(temp));
            }
            for (int j = 0; j < int.Parse(data[i * 16 + 1]); j++)
            {
                text.charaterName.Add(data[i * 16 + 2 + j]);
            }
            for (int j = 0; j < int.Parse(data[i * 16 + 6]); j++)
            {
                text.charaterSprite.Add(data[i * 16 + 7 + j]);
            }
            text.charaterText = data[i * 16 + 11];
            text.charaterVoice = data[i * 16 + 12];
            text.BG = data[i * 16 + 13];
            text.charaterLocationType = int.Parse(data[i * 16 + 14]);
            text.selectType = int.Parse(data[i * 16 + 15]);
            texts.Add(text);
        }
        Single.data.textData.textInfo = texts;
        Single.data.inGameData.loadingCnt++;
    }

    void SelectDataProcessing(string[] data)
    {
        List<SelectInfo> selects = new List<SelectInfo>();
        for (int i = 0; i < data.Length / 11; i++)
        {
            SelectInfo select = new SelectInfo();
            select.selectType = int.Parse(data[i * 11 + 0]);
            for (int j = 0; j < int.Parse(data[i * 11 + 1]); j++)
            {
                select.selectText.Add(data[i * 11 + 2 + j]);
            }
            for (int j = 0; j < int.Parse(data[i * 11 + 6]); j++)
            {
                select.branchChange.Add(int.Parse(data[i * 11 + 7 + j]));
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