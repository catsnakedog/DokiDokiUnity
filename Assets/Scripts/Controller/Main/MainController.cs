using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public static MainController main;

    public UIcontroller UI { get; set; }
    public ResourceController resource { get; set; }


    void Start()
    {
        init();
    }
    public void init()
    {
        main = this.GetComponent<MainController>();
        resource = gameObject.AddComponent<ResourceController>();
        UI = gameObject.AddComponent<UIcontroller>();

        resource.init();
        UI.init();
    }

    public void Test()
    {
        UI.UIsetting(Define.UIlevel.Level1, Define.UItype.Main);
    }
}
