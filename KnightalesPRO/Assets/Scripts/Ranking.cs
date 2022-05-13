using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInfo
{ 
    public string player_id;
    public string name;
    public string kills;

}
public class Ranking : MonoBehaviour
{
   
    private string read_url = "https://knighttalesranking.000webhostapp.com/read.php";
    private List<PlayerInfo> data = new List<PlayerInfo>();
    void Start()
    {
        //StartCoroutine(Write("kfroe", "bernat", 5));
        StartRead();
        
    }

    

    private IEnumerator Read()
    {
        
       UnityWebRequest web = UnityWebRequest.Get(read_url);

        yield return web.SendWebRequest();
        if (!web.isNetworkError && !web.isHttpError)
        {
            //Debug.Log(web.downloadHandler.text);
            data = JsonConvert.DeserializeObject<List<PlayerInfo>>(web.downloadHandler.text);
            Display();
        } else
        {
            Debug.Log("Error while reading...");
        }
        
    }

    private void Display()
    {

        Debug.Log(data.Count);
        GameObject buttonContent = transform.GetChild(0).gameObject;
        buttonContent.SetActive(true);
        GameObject g;
        int i = 1;
        foreach (PlayerInfo player in data)
        {
            g = Instantiate(buttonContent, transform);
            g.transform.GetChild(0).GetComponent<Text>().text = player.name;
            g.transform.GetChild(1).GetComponent<Text>().text = i.ToString();
            g.transform.GetChild(2).GetComponent<Text>().text = "KILLS:";
            g.transform.GetChild(3).GetComponent<Text>().text = player.kills;
            i++;

        }


        Destroy(buttonContent);
    }

   
    public void Back()
    {
        ChangeScene(1);
    }
    public void StartRead()
    {
        StartCoroutine(Read());
    }
   
    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }



}
