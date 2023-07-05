using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Save : MonoBehaviour
{
    DataManager Single;
    MainController main;

    GameObject slot;
    GameObject BT;
    GameObject leftBT;
    GameObject pageDownBT;
    GameObject pageUpBT;
    GameObject saveBT;

    List<GameObject> save;

    int page;

    void Start()
    {
        Single = DataManager.Single;
        main = MainController.main;

        slot = transform.GetChild(0).gameObject;
        BT = transform.GetChild(1).gameObject;
        pageDownBT = BT.transform.GetChild(0).gameObject;
        pageUpBT = BT.transform.GetChild(1).gameObject;
        saveBT = BT.transform.GetChild(2).gameObject;
        leftBT = BT.transform.GetChild(3).gameObject;

        page = 0;
        save = new List<GameObject>();

        int cnt = 1;
        foreach(Transform transform in slot.GetComponentsInChildren<Transform>())
        {
            if(transform.name == "Slot" + cnt)
            {
                int temp = cnt - 1;
                save.Add(transform.gameObject);
                transform.GetComponent<Button>().onClick.AddListener(() => LoadInGamedata(temp));
                transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => DeleteSave(temp));
                cnt++;
            }
        }

        leftBT.GetComponent<Button>().onClick.AddListener(Left);
        pageDownBT.GetComponent<Button>().onClick.AddListener(PageDown);
        pageUpBT.GetComponent<Button>().onClick.AddListener(PageUp);
        saveBT.GetComponent<Button>().onClick.AddListener(SaveInGameData);

        SettingSave();
        Time.timeScale = 0f;
    }

    void Left()
    {
        if (Single.data.inGameData.textLog.Count >= 1)
        {
            Single.data.inGameData.textLog.RemoveAt(Single.data.inGameData.textLog.Count - 1);
        }
        main.UI.UIsetting(Define.UIlevel.Level1, (Define.UItype)Enum.Parse(typeof(Define.UItype), Single.data.inGameData.crruentStat));
        Time.timeScale = 1f;
        Destroy(gameObject);
    }

    void PageUp()
    {
        if(page == 5)
        {
            return;
        }
        page++;
        SettingSave();
    }

    void PageDown()
    {
        if(page == 0)
        {
            return;
        }
        page--;
        SettingSave();
    }

    void SettingSave()
    {
        int cnt = -1;
        for(int i = page * 6; i < Single.data.saveData.save.Count; i++)
        {
            cnt++;
            if(cnt >= 6)
            {
                break;
            }
            save[cnt].transform.GetChild(1).GetComponent<TMP_Text>().text = (page * 6 + cnt + 1).ToString() + "번 세이브 : " + Single.data.saveData.save[i].week.ToString() + "주차";
            save[cnt].transform.GetChild(2).GetComponent<TMP_Text>().text = Single.data.saveData.save[page * 6 + cnt].year;
        }

        for(int i = cnt + 1; i < 6; i++)
        {
            save[i].transform.GetChild(1).GetComponent<TMP_Text>().text = (page * 6 + i + 1).ToString() + "번 세이브 : N주차";
            save[i].transform.GetChild(2).GetComponent<TMP_Text>().text = "yyyy-MM-dd";
        }
    }

    void DeleteSave(int number)
    {
        if(Single.data.saveData.save.Count - 1 < (page * 6 + number))
        {
            return;
        }
        Single.data.saveData.save.RemoveAt(page * 6 + number);
        Single.Save();
        SettingSave();
    }

    void SaveInGameData()
    {
        InGameData temp = new InGameData();
        Single.data.inGameData.year = DateTime.Now.ToString(("yyyy-MM-dd"));
        Copy(temp, Single.data.inGameData);
        Single.data.saveData.save.Add(temp);
        Single.Save();
        SettingSave();
    }

    void LoadInGamedata(int number)
    {
        if (Single.data.saveData.save.Count - 1 < (page * 6 + number))
        {
            return;
        }
        // 확인창 팝업
        Copy(Single.data.inGameData, Single.data.saveData.save[page * 6 + number]);
        main.UI.UIsetting(Define.UIlevel.Level1, (Define.UItype)Enum.Parse(typeof(Define.UItype), Single.data.inGameData.crruentStat));
    }

    void Copy(InGameData A, InGameData B)
    {
        A.stat.plan = B.stat.plan;
        A.stat.coding = B.stat.coding;
        A.stat.graphic = B.stat.graphic;
        A.stat.sound = B.stat.sound;
        A.lovePoint.c = B.lovePoint.c;
        A.lovePoint.cplus = B.lovePoint.cplus;
        A.lovePoint.cshop = B.lovePoint.cshop;
        A.lovePoint.python = B.lovePoint.python;
        A.lovePoint.java = B.lovePoint.java;
        A.lovePoint.html = B.lovePoint.html;
        A.loadingCnt = B.loadingCnt;
        A.maxCnt = B.maxCnt;
        A.branch.Clear();
        A.branch.Add(B.branch[0]);
        A.branch.Add(B.branch[1]);
        A.branch.Add(B.branch[2]);
        A.week = B.week;
        A.clearStory.Clear();
        foreach(int temp in B.clearStory)
        {
            A.clearStory.Add(temp);
        }
        A.crruentStat = B.crruentStat;
        A.textLog.Clear();
        foreach(TextInfo log in B.textLog)
        {
            A.textLog.Add(log);
        }
        A.year = B.year;
    }
}