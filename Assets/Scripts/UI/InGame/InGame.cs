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

    int number; // ���丮 �ѹ�
    int branch;
    int cnt;
    List<TextInfo> crruentBranchTextInfo;
    Dictionary<int, List<TextInfo>> textDict; // ������ ���� text�� �и��ؼ� �����ص�

    GameObject BG; // ���
    GameObject Ch; // ĳ���� �̹������� ���� ������Ʈ
    GameObject textBox; // �׽�Ʈ�� ������ �ڽ�
    Text content; // ����
    Text Cname; // ���ϴ� ��� �̸�

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

        GetAllTextData(); // �ش� ���丮�� ���õ� ��� �����͸� �����´�.
        SettingText(); // ���� ����
    }

    void nextText() // ���� �ؽ�Ʈ ����
    {
        cnt++;
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
        Cname.text = info.charaterName;
        content.text = info.Ctext;
        BG.GetComponent<Image>().sprite = Single.data.spriteData.sprite[info.BG];
        // ĳ���� ���� �߰�
        if(info.eventType != 0)
        {
            SelectUI(info.eventType);
        }
        if(cnt == crruentBranchTextInfo.Count-1)
        {
            Debug.Log("�ƾ���");
            Invoke("GoMain", 1f);
        }
    }

    void GoMain()
    {
        main.UI.UIsetting(Define.UIlevel.Level1, Define.UItype.Main);
    }

    void SelectUI(int eventType)
    {
        // ������ ���� �Լ� ����
        branch = 1;
        SettingText(); // ������ �ٷ� �־������ ������ �߰��ϸ� �ű⼭ ���ý� ȣ���ؼ� ��ŵ�Ǵ� ��� ����
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