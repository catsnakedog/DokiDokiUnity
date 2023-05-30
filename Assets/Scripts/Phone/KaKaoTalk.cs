using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaKaoTalk
{
    Transform _RoomsTransform { get; set; }
    GameObject _roomObj { get; set; }

    public void Init(Transform parent)
    {
        _RoomsTransform = parent;
        _roomObj = Resources.Load<GameObject>("Phone/Room");
        SetFiveRooms();
    }

    void SetFiveRooms()
    {
        for(int i = 0;i<5;i++) 
        {
            GameObject go = GameObject.Instantiate(_roomObj, _RoomsTransform);
            go.GetComponent<Room>()._name.text = $"º∫≈√{i}";

        }
    }
}
