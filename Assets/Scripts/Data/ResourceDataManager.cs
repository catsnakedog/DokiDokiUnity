using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ResourceDataManager : MonoBehaviour
{
    public Sprite test;

    public void Start()
    {
        StartCoroutine(GetTexture("https://drive.google.com/file/d/1kqel1g3UorondBVjbhjFO1lzDCUwNEL0/view?usp=sharing"));
    }

    string ExtractFileId(string url)
    {
        url = url.Replace("https://drive.google.com/file/d/", "");
        url = url.Replace("/view?usp=sharing", "");

        return url;
    }

    IEnumerator GetTexture(string url)
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
            test = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            gameObject.GetComponent<SpriteRenderer>().sprite = test;
        }
    }
}