using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UIlevel
    {
        Level1,
        Level2,
        Level3,
        maxCount
    }

    public enum UItype
    {
        Main,
        MiniGame,
        InGame,
        Loading,
        Story,
        maxCount
    }

    public enum AllSprite
    {
        기본배경_로딩,
        기본배경_인게임,
        기본배경_미니게임,
        기본배경_메인,
        cshop,
        python,
        maxCount
    }
}
