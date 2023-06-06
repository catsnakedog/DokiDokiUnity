using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[System.Serializable]
public class SaveDataClass
{
    public SaveData saveData; // 세이브 파일
    public TextData textData; // 텍스트 데이터
    public SpriteData spriteData; // 이미지 데이터
    public StoryData storyData; // 스토리 데이터
    public SelectData selectData; // 분기점 관련 데이터
    public InGameData inGameData; // 현재 진행중인 파일 데이터
    public OptionData optionData; // 옵션 관련 데이터
    public SaveDataClass(SaveData saveData, TextData textData, SpriteData spriteData, StoryData storyData, SelectData selectData, InGameData inGameData, OptionData optionData)
    {
        this.saveData = saveData;
        this.textData = textData;
        this.spriteData = spriteData;
        this.storyData = storyData;
        this.selectData = selectData;
        this.inGameData = inGameData;
        this.optionData = optionData;
    }
    public SaveDataClass()
    {
        this.saveData = new SaveData();
        this.textData = new TextData();
        this.spriteData = new SpriteData();
        this.storyData = new StoryData();
        this.selectData = new SelectData();
        this.inGameData = new InGameData();
        this.optionData = new OptionData();
    }
}

#region OptionData
[System.Serializable]
public class OptionData
{
    public float volumeBGM;
    public float volumeSFX;
    public float fontSize;
    public float textSpeed;

    public OptionData(float volumeBGM, float volumeSFX, float fontSize, float textSpeed)
    {
        this.volumeBGM = volumeBGM;
        this.volumeSFX = volumeSFX;
        this.fontSize = fontSize;
        this.textSpeed = textSpeed;
    }

    public OptionData()
    {
        this.volumeBGM = 0;
        this.volumeSFX = 0;
        this.fontSize = 0;
        this.textSpeed = 0;
    }
}
#endregion

#region InGameData
[System.Serializable]
public class InGameData
{
    public Stat stat; // 스탯
    public LovePoint lovePoint; // 호감도
    public int loadingCnt;
    public int maxCnt;
    public List<int> branch;
    public int week;
    public List<int> clearStory;
    public string crruentStat;

    public InGameData(Stat stat, LovePoint lovePoint, int loadingCnt, int maxCnt, List<int> branch, int week, List<int> clearStory, string crruentStat)
    {
        this.stat = stat;
        this.lovePoint = lovePoint;
        this.loadingCnt = loadingCnt;
        this.maxCnt = maxCnt;
        this.branch = branch;
        this.week = week;
        this.clearStory = clearStory;
        this.crruentStat = crruentStat;
    }

    public InGameData()
    {
        this.stat = new Stat();
        this.lovePoint = new LovePoint();
        this.loadingCnt = 0;
        this.maxCnt = 10;
        this.branch = new List<int>();
        this.week = 1;
        this.clearStory = new List<int>();
        this.crruentStat = "";
    }
}

[System.Serializable]
public class Stat
{
    public int plan;
    public int coding;
    public int graphic;
    public int sound;

    public Stat(int plan, int coding, int graphic, int sound)
    {
        this.plan = plan;
        this.coding = coding;
        this.graphic = graphic;
        this.sound = sound;
    }

    public Stat()
    {
        this.plan = 0;
        this.coding = 0;
        this.graphic = 0;
        this.sound = 0;
    }
}

[System.Serializable]
public class LovePoint
{
    public int c;
    public int cplus;
    public int cshop;
    public int python;
    public int java;
    public int html;

    public LovePoint(int c, int cplus, int cshop, int python, int java, int html)
    {
        this.c = c;
        this.cplus = cplus;
        this.cshop = cshop;
        this.python = python;
        this.java = java;
        this.html = html;
    }

    public LovePoint()
    {
        c = 0;
        cplus = 0;
        cshop = 0;
        python = 0;
        java = 0;
        html = 0;
    }
}
#endregion

#region SaveData
[System.Serializable]
public class SaveData
{
    public List<InGameData> save;
    public SaveData(List<InGameData> save)
    {
        this.save = save;
    }
    public SaveData()
    {
        this.save = new List<InGameData>();
    }
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
    public List<int> charaterLocationType;
    public List<float> charaterScale;
    public string charaterText;
    public string charaterVoice;
    public int selectType;
    public string BG;
    public int charaterChangeEffect;
    public int BGChangeEffect;
    public TextInfo(List<int> branch, List<string> charaterName, List<string> charaterSprite, List<int> charaterLocationType, List<float> charaterScale, string charaterText, string charaterVoice, int selectType, string BG, int BGChangeEffect, int charaterChangeEffect)
    {
        this.branch = branch;
        this.charaterName = charaterName;
        this.charaterSprite = charaterSprite;
        this.charaterLocationType = charaterLocationType;
        this.charaterScale = charaterScale;
        this.charaterText = charaterText;
        this.charaterVoice = charaterVoice;
        this.selectType = selectType;
        this.BG = BG;
        this.BGChangeEffect = BGChangeEffect;
        this.charaterChangeEffect = charaterChangeEffect;
    }

    public TextInfo()
    {
        this.branch = new List<int>();
        this.charaterName = new List<string>();
        this.charaterSprite = new List<string>();
        this.charaterLocationType = new List<int>();
        this.charaterScale = new List<float>();
        this.charaterText = "";
        this.charaterVoice = "";
        this.selectType = 0;
        this.BG = "";
        this.charaterChangeEffect = 0;
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