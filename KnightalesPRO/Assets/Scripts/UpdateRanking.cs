using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UpdateRanking : MonoBehaviour
{
    private string write_url = "https://knighttalesranking.000webhostapp.com/write.php";
    private string update_url = "https://knighttalesranking.000webhostapp.com/update.php";
   
    private List<PlayerInfo> data = new List<PlayerInfo>();
    public string kills = "";
    public string player_id = "";
    public string name = "";
    void Start()
    {
        name = PassData.getUser().DisplayName;
        player_id = PassData.getAut().CurrentUser.UserId;
        StartCoroutine(PassData.Read());
    }

    // Update is called once per frame
    void Update()
    {
        kills = FindObjectOfType<Player>().puntuacion.ToString();
    }

    public void StartWrite()
    {
        Debug.Log(name);
        StartCoroutine(Write(player_id, name, kills));
    }


    private IEnumerator Write(string player_id, string name, string kills)
    {
        bool found = false;
        WWWForm form = new WWWForm();
        data = PassData.getUsersPHP();
        foreach (PlayerInfo player in data)
        {
            if (player.player_id == player_id)
            {
                found = true;
                break;
            }

        }
        if (name != "admin")
        {
            if (!found)
            {

                form.AddField("player_id", player_id);
                form.AddField("name", name);
                form.AddField("kills", kills);
                UnityWebRequest web = UnityWebRequest.Post(write_url, form);


                yield return web.SendWebRequest();

                if (!web.isNetworkError && !web.isHttpError)
                {
                    Debug.Log(web.downloadHandler.text);
                }
                else
                {
                    Debug.Log("Error while reading...");
                }
            }
            else
            {
                form.AddField("field", "kills");
                form.AddField("player_id", player_id);
                form.AddField("value", kills);
                UnityWebRequest web = UnityWebRequest.Post(update_url, form);


                yield return web.SendWebRequest();

                if (!web.isNetworkError && !web.isHttpError)
                {
                    Debug.Log(web.downloadHandler.text);
                }
                else
                {
                    Debug.Log("Error while reading...");
                }
            }


        }
        
    }

}
