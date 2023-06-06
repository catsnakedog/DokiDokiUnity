using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class InGame : MonoBehaviour
{
    DataManager Single;
    MainController main;

    public float textShowDelay;
    public int number; // A_B_C (A)
    public int branch; // A_B_C (B)
    public int cnt; // A_B_C (C)
    public int BGChangeSpeed;
    public float BGChangeSecond;
    public int charaterChangeSpeed;
    public float charaterChangeSecond;
    bool isTextShow;
    bool isBGChange;
    List<TextInfo> crruentBranchTextInfo;
    public List<TextInfo> logTextInfo;
    public List<string> crruentIamgeList = new List<string>();
    Dictionary<int, List<TextInfo>> textDict; // 선택지 별로 text를 분리해서 저장해둠
    Dictionary<string, GameObject> imageDict;

    Image defaultBG;
    Image BG; // 배경
    GameObject Ch; // 캐릭터 이미지들이 들어가는 오브젝트
    GameObject textBox; // 테스트가 나오는 박스
    GameObject select;
    GameObject option;
    TMP_Text content; // 내용
    TMP_Text Cname; // 말하는 사람 이름

    Coroutine textCoru;
    Coroutine imageCoru;

    void Start()
    {
        Single = DataManager.Single;
        main = MainController.main;
        defaultBG = transform.GetChild(0).GetComponent<Image>();
        BG = transform.GetChild(1).GetComponent<Image>();
        Ch = transform.GetChild(2).gameObject;
        textBox = transform.GetChild(3).gameObject;
        select = transform.GetChild(4).gameObject;
        option = transform.GetChild(5).gameObject;
        content = textBox.transform.GetChild(0).GetComponent<TMP_Text>();
        Cname = textBox.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        number = Single.data.inGameData.branch[0];
        branch = Single.data.inGameData.branch[1];
        cnt = Single.data.inGameData.branch[2];
        textShowDelay = 0.05f;
        isTextShow = false;
        logTextInfo = new List<TextInfo>();
        Single.data.inGameData.crruentStat = "InGame";

        textBox.GetComponent<Button>().onClick.AddListener(nextText);
        option.GetComponent<Button>().onClick.AddListener(Option);

        GetAllTextData(); // 해당 스토리에 관련된 모든 데이터를 가져온다.
    }

    void nextText() // 다음 텍스트 실행
    {
        if(isBGChange)
        {
            return;
        }
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

    void Option()
    {
        main.UI.UIsetting(Define.UIlevel.Level3, Define.UItype.Option);
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
        BG.sprite = Single.data.spriteData.sprite[crruentBranchTextInfo[cnt].BG];
        SettingUI(crruentBranchTextInfo[cnt]); // 최초 세팅
    }

    void SettingUI(TextInfo info)
    {
        Single.data.inGameData.branch = new List<int> { number, branch, cnt };
        if (!isTextShow)
        {
            textCoru = StartCoroutine(TextShow(content, info.charaterText));
        }
        else
        {
            if (textCoru != null)
            {
                StopCoroutine(textCoru);
            }
            if (imageCoru != null)
            {
                StopCoroutine(imageCoru);
            }
            content.text = info.charaterText;
            foreach(string temp in crruentIamgeList)
            {
                imageDict[temp].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
            isTextShow = false;
        }
        Cname.text = info.charaterName[0]; // 임시값

        defaultBG.sprite = BG.sprite;
        isBGChange = true;
        defaultBG.color = new Color(1f, 1f, 1f, 1f);
        BG.color = new Color(1f, 1f, 1f, 1f);
        StartCoroutine("ChangeBG" + info.BGChangeEffect);
        CharaterSetting(info);
    }

    void CharaterSetting(TextInfo info)
    {
        List<string> imageList = new List<string>();
        for(int i = 0; i < info.charaterSprite.Count; i++)
        {
            imageList.Add(info.charaterSprite[i]);
            Vector3 location = GetCharaterLocation(info.charaterLocationType[i]);
            GameObject temp = imageDict[info.charaterSprite[i]];
            if ((temp.transform.localPosition == location) && (temp.transform.localScale == new Vector3(info.charaterScale[i], info.charaterScale[i], 1f)))
            {
                temp.SetActive(true);
                continue;
            }
            temp.SetActive(false);
            temp.transform.localPosition = location;
            temp.transform.localScale = new Vector3(info.charaterScale[i], info.charaterScale[i], 1f);
            ImageEffect(temp.GetComponent<Image>(), info);
        }
        foreach(string temp in crruentIamgeList)
        {
            if(!imageList.Contains(temp))
            {
                imageDict[temp].SetActive(false);
            }
        }
        crruentIamgeList = imageList;
    }

    void ImageEffect(Image image, TextInfo info)
    {
        Color color1 = image.color;
        color1.a = 0;
        image.color = color1;
        image.gameObject.SetActive(true);
        imageCoru = StartCoroutine("ChangeImage" + info.charaterChangeEffect, image);
    }

    IEnumerator ChangeImage0(Image image)
    {
        Color color1 = image.color;
        color1.a = 1f;
        yield return new WaitForSeconds(0f);
        image.color = color1;
    }
    IEnumerator ChangeImage1(Image image)
    {
        Color color1 = image.color;
        for (int i = 0; i < (float)charaterChangeSpeed; i++)
        {
            color1.a += 1 / (float)charaterChangeSpeed;
            image.color = color1;
            yield return new WaitForSeconds(charaterChangeSecond / (float)charaterChangeSpeed);
        }
    }

    Vector3 GetCharaterLocation(int n)
    {
        switch(n)
        {
            case 0:
                return new Vector3(-750f, -540f, 0f);
            case 1:
                return new Vector3(-500f, -540f, 0f);
            case 2:
                return new Vector3(-250f, -540f, 0f);
            case 3:
                return new Vector3(0f, -540f, 0f);
            case 4:
                return new Vector3(250f, -540f, 0f);
            case 5:
                return new Vector3(500f, -540f, 0f);
            default:
                return new Vector3(0f, -540f, 0f);
        }
    }

    IEnumerator ChangeBG0()
    {
        BG.sprite = Single.data.spriteData.sprite[crruentBranchTextInfo[cnt].BG];
        yield return new WaitForSeconds(0f);
        isBGChange = false;
    }
    IEnumerator ChangeBG1()
    {
        Color color1 = BG.color;
        Color color2 = defaultBG.color;
        color1.a = 0f;
        color2.a = 1f;
        BG.sprite = Single.data.spriteData.sprite[crruentBranchTextInfo[cnt].BG];
        for (int i = 0; i < 1 / (float)BGChangeSpeed; i++)
        {
            color1.a += 1 / (float)BGChangeSpeed;
            color2.a -= 1 / (float)BGChangeSpeed;
            BG.color = color1;
            defaultBG.color = color2;
            yield return new WaitForSeconds(BGChangeSecond / (float)BGChangeSpeed);
        }
        isBGChange = false;
    }
    IEnumerator ChangeBG2()
    {
        Color color1 = BG.color;
        Color color2 = defaultBG.color;
        color1 = Color.black;
        color1.a = 0f;
        BG.color = color1;
        BG.sprite = Single.data.spriteData.sprite["하얀배경"];
        for (int i = 0; i < (float)BGChangeSpeed; i++)
        {
            color1.a += 1 / (float)BGChangeSpeed;
            BG.color = color1;
            yield return new WaitForSeconds(BGChangeSecond / (float)BGChangeSpeed);
        }
        defaultBG.sprite = Single.data.spriteData.sprite[crruentBranchTextInfo[cnt].BG];
        for (int i = 0; i < (float)BGChangeSpeed; i++)
        {
            color1.a -= 1 / (float)BGChangeSpeed;
            BG.color = color1;
            yield return new WaitForSeconds(BGChangeSecond / (float)BGChangeSpeed);
        }
        BG.sprite = defaultBG.sprite;
        isBGChange = false;
    }

    IEnumerator TextShow(TMP_Text target, string text)
    {
        logTextInfo.Add(crruentBranchTextInfo[cnt]);
        isTextShow = true;
        textShowDelay = Single.data.optionData.textSpeed;
        StringBuilder sb = new StringBuilder();
        if(textShowDelay == 0f)
        {
            target.text = text;
            isTextShow = false;
        }
        else
        {
            for (int i = 0; i < text.Length; i++)
            {
                textShowDelay = Single.data.optionData.textSpeed;
                yield return new WaitForSeconds(textShowDelay);
                sb.Append(text[i]);
                target.text = sb.ToString();
            }
            isTextShow = false;
        }
    }

    void GoMain()
    {
        Single.data.inGameData.week++;
        Single.data.inGameData.clearStory.Add(number);
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
            if (textCoru != null)
            {
                StopCoroutine(textCoru);
            }
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
                selects[i].transform.GetChild(0).GetComponent<TMP_Text>().text = selectInfo.selectText[i];
                selects[i].GetComponent<Button>().onClick.AddListener(selects[i].GetComponent<Select>().ChangeBranch);
            }
        }
    }

    void GetAllTextData()
    {
        List<string> imageList = new List<string>();
        List<TextInfo> numberText = new List<TextInfo>();
        textDict = new Dictionary<int, List<TextInfo>>();
        imageDict = new Dictionary<string, GameObject>();

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
            foreach(string temp in info.charaterSprite)
            {
                if(!imageList.Contains(temp))
                {
                    imageList.Add(temp);
                    ImagePool(temp);
                }
            }
            textDict[info.branch[1]].Add(info);
        }
        SettingText(); // 최초 세팅
    }

    void ImagePool(string temp)
    {
        GameObject tempObject = Instantiate(main.resource.UItype[(int)Define.UItype.Charater], new Vector3(0f, 0f, 0f), quaternion.identity);
        Sprite image = Single.data.spriteData.sprite[temp];
        tempObject.GetComponent<RectTransform>().sizeDelta = new Vector2(image.bounds.size.x * 100, image.bounds.size.y * 100);
        tempObject.GetComponent<Image>().sprite = image;
        tempObject.transform.SetParent(Ch.transform, false);
        imageDict.Add(temp, tempObject);
        tempObject.SetActive(false);
    }
}