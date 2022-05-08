using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DeleteRanking : MonoBehaviour
{
    private string delete_url = "https://knighttalesranking.000webhostapp.com/delete.php";
    public string name = "";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        name = GameObject.FindGameObjectWithTag("Text_name").GetComponent<Text>().text;
    }

    public void StartDelete()
    {
        StartCoroutine(Delete());
    }
    private IEnumerator Delete()
    {
        
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        
        UnityWebRequest web = UnityWebRequest.Post(delete_url, form);


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
