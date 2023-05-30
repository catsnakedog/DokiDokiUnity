using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BasePhone : MonoBehaviour
{
    static BasePhone s_instance = null;
    static BasePhone instance { get { Init(); return s_instance; } }
    //½Ì±ÛÅÏ


    KaKaoTalk _kakao = new KaKaoTalk();
    public static KaKaoTalk KaKao { get { return instance._kakao; } }



    public GameObject NewOrange;
    //Ä«Åå »õ·Î ¿À¸é ¶ß´Â 1

    [Header("MessagePopup")]
    [SerializeField] Image _friendFace;
    [SerializeField] Text  _friendText;
    [SerializeField] RectTransform _popuptransform;
    [SerializeField] float _moveTIme;
    [SerializeField] Ease myease;

    [Header("Rooms")]
    static Transform _roomsTransform;
    public Transform _roomsTr;


    [ContextMenu("¼ºÅÃÀÌ Ä«Åå")]
    public void NewKakaoTalk(){
        StartCoroutine(KaKaoPopup());
    }

    IEnumerator KaKaoPopup()
    {
        NewOrange.SetActive(true);
        _popuptransform.DOAnchorPosY(340f, _moveTIme).SetEase(myease);
        yield return new WaitForSeconds(2f);
        _popuptransform.DOAnchorPosY(744f, _moveTIme).SetEase(myease);
    }

    private void Start()
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("PhoneBackImg");
            if (go == null)
            {
                go = new GameObject { name = "dfsd" };
                go.AddComponent<BasePhone>();
            }
            s_instance = go.GetComponent<BasePhone>();
            _roomsTransform = s_instance._roomsTr;
            s_instance._kakao.Init(_roomsTransform);
        }
    }
}
