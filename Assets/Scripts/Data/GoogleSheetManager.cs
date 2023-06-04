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
    string URL;

    public IEnumerator GoogleSheetDataSetting(int page)
    {
        List<string> strings = new List<string>();
        URL = "https://script.google.com/macros/s/AKfycbwoe-XZ-43s_TJzXmN30_QDLldQMoq5e5S4CYYW8fxM5Gu2pqRI5kpLv9hshtGn7fvk/exec";
        WWWForm form = new WWWForm();
        form.AddField("page", page);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;

        if(page == 3)
        {
            URL = "https://docs.google.com/spreadsheets/d/1kTMhmwrkWb2vmIk1Xly8OirtwdBzQLWDGJWJV0750AA/export?format=tsv&range=E14:E" + data + "&gid=1800569837";
            www = UnityWebRequest.Get(URL);
            yield return www.SendWebRequest();
            data = www.downloadHandler.text;
            foreach (string temp in data.Split('\n'))
            {
                foreach(string temp2 in temp.Split('\t'))
                {
                    strings.Add(temp2.Trim());
                }
            }
            ImageDataProcessing(strings);
        }
        if(page == 7)
        {
            URL = "https://docs.google.com/spreadsheets/d/1kTMhmwrkWb2vmIk1Xly8OirtwdBzQLWDGJWJV0750AA/export?format=tsv&range=C14:U" + data + "&gid=779445051";
            www = UnityWebRequest.Get(URL);
            yield return www.SendWebRequest();
            data = www.downloadHandler.text;
            foreach (string temp in data.Split('\n'))
            {
                foreach (string temp2 in temp.Split('\t'))
                {
                    strings.Add(temp2.Trim());
                }
            }
            StoryDataProcessing(strings);
        }
        if(page == 8)
        {
            URL = "https://docs.google.com/spreadsheets/d/1kTMhmwrkWb2vmIk1Xly8OirtwdBzQLWDGJWJV0750AA/export?format=tsv&range=C14:AA" + data + "&gid=690301575";
            www = UnityWebRequest.Get(URL);
            yield return www.SendWebRequest();
            data = www.downloadHandler.text;
            foreach (string temp in data.Split('\n'))
            {
                foreach (string temp2 in temp.Split('\t'))
                {
                    strings.Add(temp2.Trim());
                }
            }
            TextDataProcessing(strings);
        }
        if(page == 9)
        {
            URL = "https://docs.google.com/spreadsheets/d/1kTMhmwrkWb2vmIk1Xly8OirtwdBzQLWDGJWJV0750AA/export?format=tsv&range=C14:M" + data + "&gid=117010698";
            www = UnityWebRequest.Get(URL);
            yield return www.SendWebRequest();
            data = www.downloadHandler.text;
            foreach (string temp in data.Split('\n'))
            {
                foreach (string temp2 in temp.Split('\t'))
                {
                    strings.Add(temp2.Trim());
                }
            }
            SelectDataProcessing(strings);
        }
    }

    void StoryDataProcessing(List<string> data)
    {
        int num = 19;
        List<StoryInfo> storys = new List<StoryInfo>();
        for (int i = 0; i < data.Count / num; i++)
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

    void TextDataProcessing(List<string> data)
    {
        int num = 25;
        List<TextInfo> texts = new List<TextInfo>();
        for(int i=0; i < data.Count / num; i++)
        {
            TextInfo text = new TextInfo();
            string[] temps = data[i * num + 0].Split('_');
            foreach(string temp in temps)
            {
                text.branch.Add(int.Parse(temp));
            }
            int charCount = int.Parse(data[i * num + 1]);
            int charSprite = int.Parse(data[i * num + 2]);
            for(int j = 0; j < charCount; j++)
            {
                text.charaterName.Add(data[i * num + 3 + j * 4]);
                text.charaterLocationType.Add(int.Parse(data[i * num + 5 + j * 4]));
                text.charaterScale.Add(float.Parse(data[i * num + 6 + j * 4]));
            }
            for(int j = 0; j < charSprite; j++)
            {
                text.charaterSprite.Add(data[i * num + 4 + j * 4]);
            }
            text.charaterText = data[i * num + 19];
            text.charaterVoice = data[i * num + 20];
            text.selectType = int.Parse(data[i * num + 21]);
            text.BG = data[i * num + 22];
            text.charaterChangeEffect = int.Parse(data[i * num + 23]);
            text.BGChangeEffect = int.Parse(data[i * num + 24]);
            texts.Add(text);
        }
        Single.data.textData.textInfo = texts;
        Single.data.inGameData.loadingCnt++;
    }

    void SelectDataProcessing(List<string> data)
    {
        int num = 11;
        List<SelectInfo> selects = new List<SelectInfo>();
        for (int i = 0; i < data.Count / num; i++)
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

    void ImageDataProcessing(List<string> data)
    {
        Single.data.spriteData.sprite = new Dictionary<string, Sprite>();
        for (int i = 0; i < data.Count; i++)
        {
            Single.data.spriteData.sprite.Add(data[i], Resources.Load<Sprite>("Sprite/" + data[i]));
        }
        Single.data.inGameData.loadingCnt++;
    }
}