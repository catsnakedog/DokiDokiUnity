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
    }

    void Left()
    {
        main.UI.UIsetting(Define.UIlevel.Level1, Define.UItype.Main);
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
            save[cnt].transform.GetChild(2).GetComponent<TMP_Text>().text = DateTime.Now.ToString(("yyyy-MM-dd"));
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
        Single.data.saveData.save.Add(Single.data.inGameData);
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
        Single.data.inGameData = Single.data.saveData.save[page * 6 + number];
    }
}