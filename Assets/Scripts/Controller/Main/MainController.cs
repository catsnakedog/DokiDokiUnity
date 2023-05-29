using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    static MainController main;

    public UIcontroller UI { get; set; }
    public ResourceController resource { get; set; }


    public void Start()
    {
        init();
    }
    virtual public void init()
    {
        main = this.GetComponent<MainController>();
        resource = gameObject.AddComponent<ResourceController>();
        UI = gameObject.AddComponent<UIcontroller>();

        resource.init();
        UI.init();
    }
}
