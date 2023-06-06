using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    DataManager Single;
    MainController main;

    GameObject BG; // 배경
    GameObject charater; // 캐릭터
    GameObject story; // 스토리 목록
    GameObject table;
    GameObject content; // 스토리들이 들어갈 content
    GameObject option;

    List<TMP_Text> lovePoint;
    List<TMP_Text> stat;

    void Start()
    {
        Single = DataManager.Single;
        main = MainController.main;
        BG = transform.GetChild(0).gameObject;
        charater = transform.GetChild(1).gameObject;
        story = transform.GetChild(2).gameObject;
        table = transform.GetChild(3).gameObject;
        content = story.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        option = transform.GetChild(4).gameObject;

        lovePoint = new List<TMP_Text>();
        stat = new List<TMP_Text>();

        Transform[] transforms = table.transform.GetChild(0).GetComponentsInChildren<Transform>();
        foreach(Transform transform in transforms)
        {
            if(transform.name != table.transform.GetChild(0).name)
            {
                lovePoint.Add(transform.GetComponent<TMP_Text>());
            }
        }
        transforms = table.transform.GetChild(1).GetComponentsInChildren<Transform>();
        foreach (Transform transform in transforms)
        {
            if (transform.name != table.transform.GetChild(1).name)
            {
                stat.Add(transform.GetComponent<TMP_Text>());
            }
        }

        option.GetComponent<Button>().onClick.AddListener(Option);
        BGSetting();
        StorySetting();
        TableSetting();
    }

    void Option()
    {
        main.UI.UIsetting(Define.UIlevel.Level3, Define.UItype.Option);
    }

    void TableSetting()
    {
        lovePoint[0].text = "C호감도 " + Single.data.inGameData.lovePoint.c;
        lovePoint[1].text = "C++호감도 " + Single.data.inGameData.lovePoint.cplus;
        lovePoint[2].text = "C#호감도 " + Single.data.inGameData.lovePoint.cshop;
        lovePoint[3].text = "Python호감도 " + Single.data.inGameData.lovePoint.python;
        lovePoint[4].text = "Java호감도 " + Single.data.inGameData.lovePoint.java;
        lovePoint[5].text = "Html호감도 " + Single.data.inGameData.lovePoint.html;

        stat[0].text = "기획 " + Single.data.inGameData.stat.plan;
        stat[1].text = "코딩 " + Single.data.inGameData.stat.coding;
        stat[2].text = "그래픽 " + Single.data.inGameData.stat.graphic;
        stat[3].text = "사운드 " + Single.data.inGameData.stat.sound;
    }

    void StorySetting()
    {
        GameObject temp;
        int cnt = 0;
        int maxCnt = Single.data.storyData.storyInfo.Count;
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, maxCnt * 170 + 100);
        foreach (StoryInfo info in Single.data.storyData.storyInfo)
        {
            bool pass = true;
            if (Single.data.inGameData.clearStory.Contains(info.number)) continue;
            foreach (int condition in info.condition1)
            {
                if (!Single.data.inGameData.clearStory.Contains(condition)) pass = false;
            }
            for(int i = 0; i < 1; i++)
            {
                if (info.condition2.Count == 0) continue;
                if (info.condition2[0] <= Single.data.inGameData.lovePoint.c) pass = false;
                if (info.condition2.Count == 1) continue;
                if (info.condition2[1] <= Single.data.inGameData.lovePoint.cplus) pass = false;
                if (info.condition2.Count == 2) continue;
                if (info.condition2[2] <= Single.data.inGameData.lovePoint.cshop) pass = false;
                if (info.condition2.Count == 3) continue;
                if (info.condition2[3] <= Single.data.inGameData.lovePoint.python) pass = false;
            }
            //if (info.condition2[4] <= Single.data.gameData.lovePoint.java) pass = false;
            //if (info.condition2[5] <= Single.data.gameData.lovePoint.html) pass = false;
            for(int i = 0; i < 1; i++)
            {
                if (info.condition3.Count == 0) continue;
                if (info.condition3[0] <= Single.data.inGameData.stat.plan) pass = false;
                if (info.condition3.Count == 1) continue;
                if (info.condition3[1] <= Single.data.inGameData.stat.coding) pass = false;
                if (info.condition3.Count == 2) continue;
                if (info.condition3[2] <= Single.data.inGameData.stat.graphic) pass = false;
                if (info.condition3.Count == 3) continue;
                if (info.condition3[3] <= Single.data.inGameData.stat.sound) pass = false;
            }
            if (!pass) continue;
            cnt++;
            temp = Instantiate<GameObject>(main.resource.UItype[(int)Define.UItype.Story], new Vector3(0f, -50f + (cnt - 1) * -170f, 0f), Quaternion.identity);
            Story storyCom = temp.AddComponent<Story>();
            storyCom.number = info.number;
            temp.transform.SetParent(content.transform, false);
            temp.transform.GetChild(0).GetComponent<Image>().sprite = Single.data.spriteData.sprite[info.image];
            temp.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = info.title;
            temp.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = info.content;
            temp.GetComponent<Button>().onClick.AddListener(storyCom.InGameStart);
        }
    }

    void BGSetting()
    {
        BG.GetComponent<Image>().sprite = Single.data.spriteData.sprite["기본배경_메인"];
    }
}