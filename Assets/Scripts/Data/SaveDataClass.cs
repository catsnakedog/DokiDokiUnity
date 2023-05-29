using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using JetBrains.Annotations;


[System.Serializable]
public class SaveDataClass
{
    public GameData gameData;
    public TextData textData;
    public SaveDataClass(GameData gameData, TextData textData)
    {
        this.gameData = gameData;
        this.textData = textData;
    }
    public SaveDataClass()
    {
        this.gameData = new GameData();
        this.textData = new TextData();
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
    public int branch;
    public string charaterName; // n + ĳ���� �̸���? -> �� ������ �������� ĳ���� �Է¹ޱ� ����
    public string Ctext;
    public string Cvoice;
    public string BG;
    public int ClocationType;
    public int eventType; // �������� ��ɵ� «�� ex �̴ϰ���, Ư��ȿ�� ���
    public TextInfo(int branch, string charaterName, string Ctext, string Cvoice, string BG, int ClocationType, int eventType)
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
        this.branch = 0;
        this.charaterName = "";
        this.Ctext = "";
        this.Cvoice = "";
        this.BG = "";
        this.ClocationType = 0;
        this.eventType = 0;
    }
}
#endregion