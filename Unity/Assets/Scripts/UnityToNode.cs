using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;
using UnityEditor.Experimental.GraphView;

public class UnityToNode : MonoBehaviour
{
    public Button btnGetExzmple;
    public Button btnPostExzmple;
    public Button btnResDataExzmple;
    public string host;
    public int port;
    public string route;

    public string posturl;
    public string resurl;
    public int id;
    public string data;

    public void Start()
    {
        btnGetExzmple.onClick.AddListener(() => { 
            var url = string.Format("{0}:{1}/{2}", host, port, route);

            Debug.Log(url);
            StartCoroutine(GetData(url, (raw) => { 
                var res = JsonConvert.DeserializeObject<Protocols.Packets.common>(raw);
                Debug.LogFormat("{0}, {1}", res.cmd, res.message);
            }));
        });

        btnPostExzmple.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, posturl);
            Debug.Log(url);
            var req = new Protocols.Packets.req_data();
            req.cmd = 1000;
            req.id = id;
            req.data = data;
            var json = JsonConvert.SerializeObject(req);

             Debug.Log(json);

            StartCoroutine(PostData(url, json, (raw) =>
            {
                Protocols.Packets.common res = JsonConvert.DeserializeObject<Protocols.Packets.common>(raw);
                Debug.LogFormat("{0}, {1}", res.cmd, res.message);
            }));
        });

        btnResDataExzmple.onClick.AddListener(() => {
            var url = string.Format("{0}:{1}/{2}", host, port, resurl);

            Debug.Log(url);
            StartCoroutine(GetData(url, (raw) => {
                var res = JsonConvert.DeserializeObject<Protocols.Packets.res_data>(raw);

                foreach(var user in res.result)
                {
                    Debug.LogFormat("{0}, {1}", user.id, user.data);
                }
            }));
        });
    }

    private IEnumerator GetData(string url, System.Action<string> callback)
    {
        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        Debug.Log("Get : " + webRequest.downloadHandler.text);
        if(webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 좋지 않아서 통신 불가능 ");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator PostData(string url, string json, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 좋지 않아서 통신 불가능 ");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }

        webRequest.Dispose();
    }
}
