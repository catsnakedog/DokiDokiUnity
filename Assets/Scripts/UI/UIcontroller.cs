using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Experimental.SceneManagement;

public class UIcontroller : MonoBehaviour
{
    List<GameObject> Levels = new List<GameObject>();
    List<GameObject> UItype = new List<GameObject>();

    void Start()
    {
        Levels.Add(GameObject.FindWithTag("Level1"));
        Levels.Add(GameObject.FindWithTag("Level2"));
        Levels.Add(GameObject.FindWithTag("Level3"));
        UItypeSetting();
    }

    void UItypeSetting()
    {
        UItype = new List<GameObject>();
        for(int i=0; i<(int)Define.UItype.maxCount; i++)
        {
            UItype.Add(Resources.Load<GameObject>("Prefabs/UI/" + Enum.GetName(typeof(Define.UItype), i)));
        }
    }

    void UIsetting(Define.UIlevel UIlevel, Define.UItype UItype)
    {
        Transform[] childList = Levels[(int)UIlevel].GetComponentsInChildren<Transform>();
        if(childList != null)
        {
            for (int i=1; i<childList.Length; i++)
            {
                if (childList[i] != Levels[(int)UIlevel].transform) Destroy(childList[i].gameObject);
            }
        }
        GameObject target = this.UItype[(int)UItype];
        Instantiate(target, target.transform.position, Quaternion.identity).transform.SetParent(Levels[(int)UIlevel].transform, false);
    }

    public void Test()
    {
        UIsetting(Define.UIlevel.Level1, Define.UItype.Main);
    }
    public void Test2()
    {
        UIsetting(Define.UIlevel.Level1, Define.UItype.Main2);
    }
}
