using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class InGame : MonoBehaviour
{
    DataManager Single;
    MainController main;

    public float textShowDelay;
    int number; // 스토리 넘버
    int branch;
    int cnt;
    bool isTextShow;
    List<TextInfo> crruentBranchTextInfo;
    Dictionary<int, List<TextInfo>> textDict; // 선택지 별로 text를 분리해서 저장해둠

    GameObject BG; // 배경
    GameObject Ch; // 캐릭터 이미지들이 들어가는 오브젝트
    GameObject textBox; // 테스트가 나오는 박스
    Text content; // 내용
    Text Cname; // 말하는 사람 이름

    Coroutine textCoru;

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
        textShowDelay = 0.05f;
        isTextShow = false;

        textBox.GetComponent<Button>().onClick.AddListener(nextText);

        GetAllTextData(); // 해당 스토리에 관련된 모든 데이터를 가져온다.
        SettingText(); // 최초 세팅
    }

    void nextText() // 다음 텍스트 실행
    {
        if(cnt == crruentBranchTextInfo.Count - 1 && !isTextShow)
        {
            SelectUI();
        }
        if(!isTextShow)
        {
            cnt++;
        }
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
        if(!isTextShow)
        {
            textCoru = StartCoroutine(TextShow(content, info.Ctext));
        }
        else
        {
            StopCoroutine(textCoru);
            content.text = info.Ctext;
            isTextShow = false;
        }
        Cname.text = info.charaterName;
        BG.GetComponent<Image>().sprite = Single.data.spriteData.sprite[info.BG];
        // 캐릭터 세팅 추가
    }

    IEnumerator TextShow(Text target, string text)
    {
        isTextShow = true;
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i<text.Length;i++)
        {
            yield return new WaitForSeconds(textShowDelay);
            sb.Append(text[i]);
            target.text = sb.ToString();
        }
        isTextShow = false;
    }

    void GoMain()
    {
        main.UI.UIsetting(Define.UIlevel.Level1, Define.UItype.Main);
    }

    void SelectUI()
    {
        if (crruentBranchTextInfo[cnt].eventType == 0)
        {
            GoMain();
            return;
        }
        else
        {
            StopCoroutine(textCoru);
            isTextShow = false;
            //선택지 관련 스크립트 실행
            branch = 1; // 임시
            SettingText();
            return;
        }
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
        }
    }
}