using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class InGame : MonoBehaviour
{
    DataManager Single;
    MainController main;

    int number; // 스토리 넘버
    int branch;
    int cnt;
    List<TextInfo> crruentBranchTextInfo;
    Dictionary<int, List<TextInfo>> textDict; // 선택지 별로 text를 분리해서 저장해둠

    GameObject BG; // 배경
    GameObject Ch; // 캐릭터 이미지들이 들어가는 오브젝트
    GameObject textBox; // 테스트가 나오는 박스
    Text content; // 내용
    Text Cname; // 말하는 사람 이름

    void Start()
    {
        Single = DataManager.Single;
        main = MainController.main;
        BG = transform.GetChild(0).gameObject;
        textBox = transform.GetChild(1).gameObject;
        Ch = transform.GetChild(2).gameObject;
        content = textBox.transform.GetChild(0).GetComponent<Text>();
        Cname = textBox.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        number = Single.data.inGameData.number;
        branch = 0;

        textBox.GetComponent<Button>().onClick.AddListener(nextText);

        GetAllTextData(); // 해당 스토리에 관련된 모든 데이터를 가져온다.
        SettingText(); // 최초 세팅
    }

    void nextText() // 다음 텍스트 실행
    {
        cnt++;
        SettingUI(crruentBranchTextInfo[cnt]);
    }
    
    void SettingText()
    {
        crruentBranchTextInfo = textDict[branch];
        cnt = 0;
        SettingUI(crruentBranchTextInfo[0]); // 최초 세팅
    }

    void SettingUI(TextInfo info)
    {
        Cname.text = info.charaterName;
        content.text = info.Ctext;
        BG.GetComponent<Image>().sprite = Single.data.spriteData.sprite[info.BG];
        // 캐릭터 세팅 추가
        if(info.eventType != 0)
        {
            SelectUI(info.eventType);
        }
        if(cnt == crruentBranchTextInfo.Count-1)
        {
            Debug.Log("컷씬끝");
            Invoke("GoMain", 1f);
        }
    }

    void GoMain()
    {
        main.UI.UIsetting(Define.UIlevel.Level1, Define.UItype.Main);
    }

    void SelectUI(int eventType)
    {
        // 선택지 관련 함수 실행
        branch = 1;
        SettingText(); // 지금은 바로 넣어놨지만 선택지 추가하면 거기서 선택시 호출해서 스킵되는 경우 방지
    }

    void GetAllTextData()
    {
        List<TextInfo> numberText = new List<TextInfo>();
        textDict = new Dictionary<int, List<TextInfo>>();

        foreach (TextInfo info in Single.data.textData.textInfo)
        {
            if (info.branch[0] == number)
            {
                numberText.Add(info);
            }
            if (!textDict.Keys.Contains(info.branch[1]))
            {
                textDict.Add(info.branch[1], new List<TextInfo>());
            }
        }
        foreach(TextInfo info in numberText)
        {
            textDict[info.branch[1]].Add(info);
            Debug.Log("");
        }
    }
}