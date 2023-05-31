/*
using System.Collections;                   현실적 이슈로 포기..
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ResourceDataManager
{
    public DataManager Single;

    public void GetSprite(string name, string url)
    {
        Single.SpriteDataCoroutine(name, url);
    }

    string ExtractFileId(string url)
    {
        url = url.Replace("https://drive.google.com/file/d/", "");
        url = url.Replace("/view?usp=sharing", "");

        return url;
    }

    public IEnumerator GetTexture(string name,string url)
    {
        var gdId = ExtractFileId(url);
        var prefix = "http://drive.google.com/uc?export=view&id=";
        url = prefix + gdId;

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);
        else
        {
            Texture texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Single.data.spriteData.sprite.Add(name ,Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
            Single.data.inGameData.loadingCnt++;
        }
    }
}
*/