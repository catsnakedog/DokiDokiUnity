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
    public SaveDataClass(GameData gameData)
    {
        this.gameData = gameData;
    }
    public SaveDataClass()
    {
        this.gameData = new GameData();
    }
}

#region GameData
[System.Serializable]
public class GameData
{
    public List<TextType> textTypes;
    public GameData(List<TextType> textTypes)
    {
        this.textTypes = textTypes;
    }
    public GameData()
    {
        this.textTypes =new List<TextType>();
    }
}

[System.Serializable]
public class TextType
{
    public int branch;
    public string charaterName; // n + ĳ���� �̸���? -> �� ������ �������� ĳ���� �Է¹ޱ� ����
    public string Ctext;
    public string Cvoice;
    public string BG;
    public int ClocationType;
    public int eventType; // �������� ��ɵ� «�� ex �̴ϰ���, Ư��ȿ�� ���
    public TextType(int branch, string charaterName, string Ctext, string Cvoice, string BG, int ClocationType, int eventType)
    {
        this.branch = branch;
        this.charaterName = charaterName;
        this.Ctext = Ctext;
        this.Cvoice = Cvoice;
        this.BG = BG;
        this.ClocationType = ClocationType;
        this.eventType = eventType;
    }

    public TextType()
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