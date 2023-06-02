using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class Main : MonoBehaviour
{
    DataManager Single;
    MainController main;

    GameObject BG; // 배경
    GameObject charater; // 캐릭터
    GameObject story; // 스토리 목록
    GameObject stat; // 스탯
    GameObject content; // 스토리들이 들어갈 content
    void Start()
    {
        Single = DataManager.Single;
        main = MainController.main;
        BG = transform.GetChild(0).gameObject;
        charater = transform.GetChild(1).gameObject;
        story = transform.GetChild(2).gameObject;
        stat = transform.GetChild(3).gameObject;
        content = story.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        BGSetting();
        StorySetting();
    }

    void StorySetting()
    {
        GameObject temp;
        int cnt = 0;
        int maxCnt = Single.data.storyData.storyInfo.Count;
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, maxCnt * 170 + 100);
        foreach (StoryInfo info in Single.data.storyData.storyInfo)
        {
            cnt++;
            temp = Instantiate<GameObject>(main.resource.UItype[(int)Define.UItype.Story], new Vector3(0f, -50f + (cnt - 1) * -170f, 0f), Quaternion.identity);
            Story storyCom = temp.AddComponent<Story>();
            storyCom.number = info.number;
            temp.transform.SetParent(content.transform, false);
            temp.transform.GetChild(0).GetComponent<Image>().sprite = Single.data.spriteData.sprite[info.image];
            temp.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = info.title;
            temp.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = info.content;
            temp.GetComponent<Button>().onClick.AddListener(storyCom.InGameStart);
        }
    }

    void BGSetting()
    {
        BG.GetComponent<Image>().sprite = Single.data.spriteData.sprite["기본배경_메인"];
        int a = Single.data.storyData.storyInfo.Count;
    }
}