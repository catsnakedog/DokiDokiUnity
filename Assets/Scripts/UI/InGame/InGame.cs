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
    public int branch;
    int cnt;
    bool isTextShow;
    List<TextInfo> crruentBranchTextInfo;
    Dictionary<int, List<TextInfo>> textDict; // 선택지 별로 text를 분리해서 저장해둠

    GameObject BG; // 배경
    GameObject Ch; // 캐릭터 이미지들이 들어가는 오브젝트
    GameObject textBox; // 테스트가 나오는 박스
    GameObject select;
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
        select = transform.GetChild(3).gameObject;
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
            return;
        }
        if(!isTextShow)
        {
            cnt++;
        }
        SettingUI(crruentBranchTextInfo[cnt]);
    }
    
    public void SettingText()
    {
        Transform[] transforms = this.select.GetComponentsInChildren<Transform>();
        foreach(Transform transform in transforms)
        {
            if(transform.name != this.select.name)
            {
                Destroy(transform.gameObject);
            }
        }
        crruentBranchTextInfo = textDict[branch];
        cnt = 0;
        SettingUI(crruentBranchTextInfo[0]); // 최초 세팅
    }

    void SettingUI(TextInfo info)
    {
        if(!isTextShow)
        {
            textCoru = StartCoroutine(TextShow(content, info.charaterText));
        }
        else
        {
            StopCoroutine(textCoru);
            content.text = info.charaterText;
            isTextShow = false;
        }
        Cname.text = info.charaterName[0]; // 임시값
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
        if (crruentBranchTextInfo[cnt].selectType == 0)
        {
            GoMain();
            return;
        }
        else if (crruentBranchTextInfo[cnt].selectType == 1)
        {
            // 컷씬 실행
        }
        else
        {
            StopCoroutine(textCoru);
            isTextShow = false;
            SelectInfo selectInfo = Single.data.selectData.selectInfo[crruentBranchTextInfo[cnt].selectType];
            int count = selectInfo.selectText.Count;

            List<GameObject> selects = new List<GameObject>();

            if (count == 1)
            {
                selects.Add(Instantiate(main.resource.UItype[(int)Define.UItype.Select], new Vector3(0f, 150f, 0f), Quaternion.identity));
            }
            if (count == 2)
            {
                selects.Add(Instantiate(main.resource.UItype[(int)Define.UItype.Select], new Vector3(0f, 220f, 0f), Quaternion.identity));
                selects.Add(Instantiate(main.resource.UItype[(int)Define.UItype.Select], new Vector3(0f, 70f, 0f), Quaternion.identity));
            }
            if (count == 3)
            {
                selects.Add(Instantiate(main.resource.UItype[(int)Define.UItype.Select], new Vector3(0f, 300f, 0f), Quaternion.identity));
                selects.Add(Instantiate(main.resource.UItype[(int)Define.UItype.Select], new Vector3(0f, 150f, 0f), Quaternion.identity));
                selects.Add(Instantiate(main.resource.UItype[(int)Define.UItype.Select], new Vector3(0f, 0f, 0f), Quaternion.identity));
            }
            if (count == 4)
            {
                selects.Add(Instantiate(main.resource.UItype[(int)Define.UItype.Select], new Vector3(0f, 370f, 0f), Quaternion.identity));
                selects.Add(Instantiate(main.resource.UItype[(int)Define.UItype.Select], new Vector3(0f, 220f, 0f), Quaternion.identity));
                selects.Add(Instantiate(main.resource.UItype[(int)Define.UItype.Select], new Vector3(0f, 70f, 0f), Quaternion.identity));
                selects.Add(Instantiate(main.resource.UItype[(int)Define.UItype.Select], new Vector3(0f, -80f, 0f), Quaternion.identity));
            }

            for(int i = 0; i < selects.Count; i++)
            {
                selects[i].transform.SetParent(this.select.transform, false);
                selects[i].GetComponent<Select>().inGame = this.GetComponent<InGame>();
                selects[i].GetComponent<Select>().branch = selectInfo.branchChange[i];
                selects[i].transform.GetChild(0).GetComponent<Text>().text = selectInfo.selectText[i];
                selects[i].GetComponent<Button>().onClick.AddListener(selects[i].GetComponent<Select>().ChangeBranch);
            }
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