using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Log : MonoBehaviour
{
    DataManager Single;
    MainController main;

    GameObject left;
    GameObject logBox;
    List<GameObject> log;

    void Start()
    {
        Single = DataManager.Single;
        main = MainController.main;
        left = transform.GetChild(1).GetChild(0).gameObject;
        logBox = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        log = new List<GameObject>();

        left.GetComponent<Button>().onClick.AddListener(Left);

        SetLog(Single.data.inGameData.textLog);
        Time.timeScale = 0f;
    }

    void Left()
    {
        Destroy(gameObject);
        Time.timeScale = 1f;
    }

    public void SetLog(List<TextInfo> log)
    {
        int x = 0;
        int y = log.Count * 100 + 150;
        int cnt = 0;

        logBox.GetComponent<RectTransform>().sizeDelta = new Vector2(logBox.GetComponent<RectTransform>().sizeDelta.x, log.Count * 200 + 600);

        foreach(TextInfo info in log)
        {
            this.log.Add(Instantiate<GameObject>((main.resource.UItype[(int)Define.UItype.LogTextBox]), new Vector3(x, y, 0), Quaternion.identity));
            this.log[cnt].transform.SetParent(logBox.transform, false);
            this.log[cnt].GetComponent<LogTextBox>().textInfo = info;
            y -= 200;
            cnt++;
        }
    }
}