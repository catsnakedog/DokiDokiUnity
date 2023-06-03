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
    public SelectData selectData;
    public InGameData inGameData;
    public SaveDataClass(GameData gameData, TextData textData, SpriteData spriteData, StoryData storyData, SelectData selectData, InGameData inGameData)
    {
        this.gameData = gameData;
        this.textData = textData;
        this.spriteData = spriteData;
        this.storyData = storyData;
        this.selectData = selectData;
        this.inGameData = inGameData;
    }
    public SaveDataClass()
    {
        this.gameData = new GameData();
        this.textData = new TextData();
        this.spriteData = new SpriteData();
        this.storyData = new StoryData();
        this.selectData = new SelectData();
        this.inGameData = new InGameData();
    }
}

#region InGameData
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
#endregion

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
    public List<string> charaterName;
    public List<string> charaterSprite;
    public string charaterText;
    public string charaterVoice;
    public string BG;
    public int charaterLocationType;
    public int selectType;
    public int BGChangeEffect;
    public TextInfo(List<int> branch, List<string> charaterName, List<string> charaterSprtie,string charaterText, string charaterVoice, string BG, int charaterLocationType, int selectType, int BGChangeEffect)
    {
        this.branch = branch;
        this.charaterName = charaterName;
        this.charaterText = charaterText;
        this.charaterVoice = charaterVoice;
        this.BG = BG;
        this.charaterLocationType = charaterLocationType;
        this.selectType = selectType;
        this.BGChangeEffect = BGChangeEffect;
    }

    public TextInfo()
    {
        this.branch = new List<int>();
        this.charaterName = new List<string>();
        this.charaterSprite = new List<string>();
        this.charaterText = "";
        this.charaterVoice = "";
        this.BG = "";
        this.charaterLocationType = 0;
        this.selectType = 0;
        this.BGChangeEffect = 0;
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
    public string image; // n + 캐릭터 이름들? -> 한 변수로 여러명의 캐릭터 입력받기 가능
    public string title;
    public string content;
    public List<int> condition1;
    public List<int> condition2;
    public List<int> condition3;
    
    public StoryInfo(int number, string image, string title, string content, List<int> condition1, List<int> condition2, List<int> condition3)
    {
        this.number = number;
        this.image = image;
        this.title = title;
        this.content = content;
        this.condition1 = condition1;
        this.condition2 = condition2;
        this.condition3 = condition3;
    }

    public StoryInfo()
    {
        this.number = 0;
        this.image = "";
        this.title = "";
        this.content = "";
        this.condition1 = new List<int>();
        this.condition2 = new List<int>();
        this.condition3 = new List<int>();
    }
}
#endregion

#region SelectData
[System.Serializable]
public class SelectData
{
    public List<SelectInfo> selectInfo;
    public SelectData(List<SelectInfo> selectInfo)
    {
        this.selectInfo = selectInfo;
    }
    public SelectData()
    {
        this.selectInfo = new List<SelectInfo>();
    }
}
[System.Serializable]
public class SelectInfo
{
    public int selectType;
    public List<string> selectText;
    public List<int> branchChange;

    public SelectInfo(int selectType, List<string> selectText, List<int> branchChange)
    {
        this.selectType = selectType;
        this.selectText = selectText;
        this.branchChange = branchChange;
    }

    public SelectInfo()
    {
        this.selectType = 0;
        this.selectText = new List<string>();
        this.branchChange = new List<int>();
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