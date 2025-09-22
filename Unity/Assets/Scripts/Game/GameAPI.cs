using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using System;

public class GameAPI : MonoBehaviour
{
    private string baseUrl = "http://localhost:4000/api";

    public IEnumerator RegisterPlayer(string playerName, string password)
    {
        var requestData = new { name = playerName, password = password };
        string jsonData = JsonConvert.SerializeObject(requestData);
        Debug.Log($"RegisterPlayer : {jsonData}");

        using (UnityWebRequest request = new UnityWebRequest($"{baseUrl}/register", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error registering player : {request.result}");

            }
            else
            {
                Debug.Log("Player register successfully");
            }
        }
    }

    public IEnumerator LoginPlayer(string playerName, string password, Action<PlayerModel> onSuccess)
    {
        var requestData = new {name = playerName, password = password};
        string jsonData = JsonConvert.SerializeObject (requestData);

        using (UnityWebRequest request = new UnityWebRequest($"{baseUrl}/register", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error registering player : {request.result}");

            }
            else
            {
                string responseBody = request.downloadHandler.text;

                try
                {
                    var responseData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

                    PlayerModel playerModel = new PlayerModel(responseData["playerName"].ToString())
                    {
                        matal = Convert.ToInt32(responseData["matal"]),
                        crystal = Convert.ToInt32(responseData["crystal"]),
                        deuterium = Convert.ToInt32(responseData["deuterium"]),
                        Planes = new List<PlaneModel>()
                    };
                    onSuccess.Invoke(playerModel);
                    Debug.Log("Login success");
                }
                catch(Exception ex)
                {
                    Debug.LogError($"Error processing login responce : {ex.Message}");
                }
            }
        }

    }
}
