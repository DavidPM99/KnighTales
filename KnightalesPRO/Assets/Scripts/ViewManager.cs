using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate() { OnClick(param); });
    }
}
public class ViewManager : MonoBehaviour
{
    //public GameObject view_content;
    public GameObject editUserPanel;
    public GameObject ban_bt;
    public GameObject unban_bt;
    public int numOfListItems;
    private bool done = false;
    public Text actualUser;

    private string delete_url = "https://knighttalesranking.000webhostapp.com/delete.php";
    private Dictionary<string, string> users = new Dictionary<string, string>();

    void Start()
    {
        PassData.AllUsers();
        editUserPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!done)
        {
            
            users = PassData.getAllUsers();

            numOfListItems = users.Count;
            if (numOfListItems > 0)
            {
                Debug.Log(numOfListItems);
                Display();
                done = true;
            }
        }
        
    }

    public void BanPlayer()
    {
        string playerId = users[actualUser.text];
        PassData.setBan(true, playerId);
        ChangeScene(5);

    }

    public void UnBanPlayer()
    {
        string playerId = users[actualUser.text];
        PassData.setBan(false, playerId);
        ChangeScene(5);

    }

    public void DeletePlayer()
    {
        string playerId = users[actualUser.text];
        PassData.DeletePlayer(playerId);
        
        StartCoroutine(Delete());
        ChangeScene(5);
    }
    public void Back()
    {
        ChangeScene(1);
    }
    public void Cancel()
    {
        editUserPanel.SetActive(false);
    }
    private void Display()
    {

        Debug.Log(users.Count);
        GameObject buttonContent = transform.GetChild(0).gameObject;
        GameObject g;
        foreach (string user in users.Keys)
        {
            g = Instantiate(buttonContent, transform);
            g.transform.GetChild(0).GetComponent<Text>().text = user;
            g.transform.GetChild(1).GetComponent<Text>().text = users[user];
            PassData.getextBanState(users[user], g.transform.GetChild(2).GetComponent<Text>());
            g.GetComponent<Button>().AddEventListener(user, ItemClicked);
            
            
        }


        Destroy(buttonContent);
    }


    private void ItemClicked(string user)
    {
        actualUser.text = user;
        PassData.getBanState(users[user], ban_bt, unban_bt);
        editUserPanel.SetActive(true);
        
    }

    private IEnumerator Delete()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", actualUser.text);
        UnityWebRequest web = UnityWebRequest.Post(delete_url, form);


        yield return web.SendWebRequest();

        if (!web.isNetworkError && !web.isHttpError)
        {
            Debug.Log(web.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error while deleting...");
        }
    }
    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }



}
