using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ResourceController : MonoBehaviour
{
    DataManager Single;
    MainController main;

    public List<GameObject> UItype { get; set; }

    public void init()
    {
        Single = DataManager.Single;
        main = MainController.main;
        UItypeSetting();
        ImageSetting();
    }

    void UItypeSetting()
    {
        UItype = new List<GameObject>();
        for (int i = 0; i < (int)Define.UItype.maxCount; i++)
        {
            UItype.Add(Resources.Load<GameObject>("Prefabs/UI/" + Enum.GetName(typeof(Define.UItype), i)));
        }
    }

    void ImageSetting()
    {
        Single.data.spriteData.sprite = new Dictionary<string, Sprite>();
        for (int i = 0; i < (int)Define.AllSprite.maxCount; i++)
        {
            Single.data.spriteData.sprite.Add(((Define.AllSprite)i).ToString(), Resources.Load<Sprite>("Sprite/" + Enum.GetName(typeof(Define.AllSprite), i)));
        }
    }
}
