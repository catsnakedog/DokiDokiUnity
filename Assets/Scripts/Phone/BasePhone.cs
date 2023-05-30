using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BasePhone : MonoBehaviour
{
    public GameObject NewOrange;
    //ī�� ���� ���� �ߴ� 1

    [Header("MessagePopup")]
    [SerializeField] Image _friendFace;
    [SerializeField] Text  _friendText;
    [SerializeField] RectTransform _popuptransform;
    [SerializeField] float _moveTIme;
    [SerializeField] Ease myease;


    [ContextMenu("������ ī��")]
    public void NewKakaoTalk(){
        NewOrange.SetActive(true);
        _popuptransform.DOAnchorPosY(327f, _moveTIme).SetEase(myease);
    }
}
