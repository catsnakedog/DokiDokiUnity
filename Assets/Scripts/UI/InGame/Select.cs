using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    DataManager Single;
    MainController main;
    public InGame inGame;
    public int branch;

    void Start()
    {
        Single = DataManager.Single;
        main = MainController.main;
    }

    public void ChangeBranch()
    {
        inGame.branch = this.branch;
        inGame.cnt = 0;
        inGame.SettingText();
    }
}