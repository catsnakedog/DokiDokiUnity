using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ResourceController : MonoBehaviour
{
    MainController main;

    public List<GameObject> UItype { get; set; }
    public List<Sprite> BG { get; set; }

    public void init()
    {
        UItypeSetting();
        BGSetting();
    }

    void UItypeSetting()
    {
        UItype = new List<GameObject>();
        for (int i = 0; i < (int)Define.UItype.maxCount; i++)
        {
            UItype.Add(Resources.Load<GameObject>("Prefabs/UI/" + Enum.GetName(typeof(Define.UItype), i)));
        }
    }

    void BGSetting()
    {
        BG = new List<Sprite>();
        for (int i = 0; i < (int)Define.BG.maxCount; i++)
        {
            BG.Add(Resources.Load<Sprite>("BG/" + Enum.GetName(typeof(Define.BG), i)));
        }
    }
}
