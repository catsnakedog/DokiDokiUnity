using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using JetBrains.Annotations;
using UnityEngine.WSA;

[System.Serializable]
public class SaveDataClass
{
    public GameData gameData;
    public TextData textData;
    public SpriteData spriteData;
    public StoryData storyData;
    public InGameData inGameData;
    public SaveDataClass(GameData gameData, TextData textData, SpriteData spriteData, StoryData storyData, InGameData inGameData)
    {
        this.gameData = gameData;
        this.textData = textData;
        this.spriteData = spriteData;
        this.storyData = storyData;
        this.inGameData = inGameData;
    }
    public SaveDataClass()
    {
        this.gameData = new GameData();
        this.textData = new TextData();
        this.spriteData = new SpriteData();
        this.storyData = new StoryData();
        this.inGameData = new InGameData();
    }
}

[System.Serializable]
public class InGameData
{
    public int loadingCnt;
    public int maxCnt;
    public int number;

    public InGameData()
    {
        this.loadingCnt = 0;
        this.maxCnt = 10;
        this.number = 0;
    }
}

#region GameData
[System.Serializable]
public class GameData
{
    public Stat stat;
    public GameData(Stat stat)
    {
        this.stat = stat;
    }
    public GameData()
    {
        stat = new Stat();
    }
}

[System.Serializable]
public class Stat
{

}
#endregion

#region TextData
[System.Serializable]
public class TextData
{
    public List<TextInfo> textInfo;
    public TextData(List<TextInfo> textInfo)
    {
        this.textInfo = textInfo;
    }
    public TextData()
    {
        this.textInfo = new List<TextInfo>();
    }
}

[System.Serializable]
public class TextInfo
{
    public List<int> branch;
    public string charaterName; // n + 캐릭터 이름들? -> 한 변수로 여러명의 캐릭터 입력받기 가능
    public string Ctext;
    public string Cvoice;
    public string BG;
    public int ClocationType;
    public int eventType; // 여러가지 기능들 짬통 ex 미니게임, 특수효과 등등
    public TextInfo(List<int> branch, string charaterName, string Ctext, string Cvoice, string BG, int ClocationType, int eventType)
    {
        this.branch = branch;
        this.charaterName = charaterName;
        this.Ctext = Ctext;
        this.Cvoice = Cvoice;
        this.BG = BG;
        this.ClocationType = ClocationType;
        this.eventType = eventType;
    }

    public TextInfo()
    {
        this.branch = new List<int>();
        this.charaterName = "";
        this.Ctext = "";
        this.Cvoice = "";
        this.BG = "";
        this.ClocationType = 0;
        this.eventType = 0;
    }
}
#endregion

#region StoryData
[System.Serializable]
public class StoryData
{
    public List<StoryInfo> storyInfo;
    public StoryData(List<StoryInfo> questInfo)
    {
        this.storyInfo = questInfo;
    }
    public StoryData()
    {
        this.storyInfo = new List<StoryInfo>();
    }
}

[System.Serializable]
public class StoryInfo
{
    public int number;
    public string img; // n + 캐릭터 이름들? -> 한 변수로 여러명의 캐릭터 입력받기 가능
    public string title;
    public string content;
    public List<int> condition1;
    public List<int> condition2;
    public bool repeat; // 여러가지 기능들 짬통 ex 미니게임, 특수효과 등등
    
    public StoryInfo(int number, string img, string title, string content, List<int> condition1, List<int> condition2, bool repeat)
    {
        this.number = number;
        this.img = img;
        this.title = title;
        this.content = content;
        this.condition1 = condition1;
        this.condition2 = condition2;
        this.repeat = repeat;
    }

    public StoryInfo()
    {
        this.number = 0;
        this.img = "";
        this.title = "";
        this.content = "";
        this.condition1 = new List<int>();
        this.condition2 = new List<int>();
        this.repeat = false;
    }
}
#endregion

#region SpriteData
[System.Serializable]
public class SpriteData
{
    public Dictionary<string, Sprite> sprite;

    public SpriteData(Dictionary<string, Sprite> sprite)
    {
        this.sprite = sprite;
    }
    public SpriteData()
    {
        sprite = new Dictionary<string, Sprite>();
    }
}
#endregion