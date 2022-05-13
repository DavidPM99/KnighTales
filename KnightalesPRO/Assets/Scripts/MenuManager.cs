using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public bool savedGame;
    public GameObject alert;
    public GameObject bt_users;

    private void Start()
    {
        alert.SetActive(false);
        bt_users.SetActive(false);
        Debug.Log(PassData.getUser().DisplayName);
        if (PassData.getUser().DisplayName == "admin")
        {
            bt_users.SetActive(true);
        }

    }
    private void Update()
    {
        savedGame = PassData.getSavedGame();
    }
    public void NewGame()
    {
        if (savedGame)
        {
            alert.SetActive(true);
        }
        else
        {
            ChangeScene(4);
        }
       
    }

    public void ResetNewGame()
    {
        PassData.DeleteTable();
        ChangeScene(4);

    }

    public void RankList()
    {
        ChangeScene(3);
    }
    public void UsersList() { 
        ChangeScene(5);
    }

    public void Continue()
    {
        if (!savedGame)
        {
            return;
        } else
        {
            ChangeScene(7);
        }
       
    }
    public void Cancel()
    {
        alert.SetActive(false);
    }
    public void Logout()
    {
        ChangeScene(0);
       
    }

    public void userProfile()
    {
        ChangeScene(2);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
