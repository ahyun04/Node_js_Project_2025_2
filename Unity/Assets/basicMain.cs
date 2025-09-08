using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;

public class basicMain : MonoBehaviour
{
    public Button Hello;
    public string host;
    public int port;
    public string route;

    private void Start()
    {
        this.Hello.onClick.AddListener(() => { 
            var url = string.Format("{0}:{1}/{2}", host, port, route);
            Debug.Log(url);

            StartCoroutine(this.GetBasic(url, (raw) => {
                Debug.LogFormat("{0}", raw);
            }));
        });
    }

    private IEnumerator GetBasic(string url , System.Action<string> callback)
    {
        var webrequest = UnityWebRequest.Get(url);
        yield return webrequest.SendWebRequest();

        if(webrequest.result == UnityWebRequest.Result.ConnectionError || webrequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 좋지 않아서 통신 불가");
        }
        else
        {
            callback(webrequest.downloadHandler.text);
        }
    }
}
