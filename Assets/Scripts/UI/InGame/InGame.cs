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
    int number; // ���丮 �ѹ�
    int branch;
    int cnt;
    bool isTextShow;
    List<TextInfo> crruentBranchTextInfo;
    Dictionary<int, List<TextInfo>> textDict; // ������ ���� text�� �и��ؼ� �����ص�

    GameObject BG; // ���
    GameObject Ch; // ĳ���� �̹������� ���� ������Ʈ
    GameObject textBox; // �׽�Ʈ�� ������ �ڽ�
    Text content; // ����
    Text Cname; // ���ϴ� ��� �̸�

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

        GetAllTextData(); // �ش� ���丮�� ���õ� ��� �����͸� �����´�.
        SettingText(); // ���� ����
    }

    void nextText() // ���� �ؽ�Ʈ ����
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
        SettingUI(crruentBranchTextInfo[0]); // ���� ����
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
        // ĳ���� ���� �߰�
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
            //������ ���� ��ũ��Ʈ ����
            branch = 1; // �ӽ�
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