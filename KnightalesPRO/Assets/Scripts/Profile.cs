
using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    [Header("Texts")]
    public Text email;
    public Text name;
    public Text city;
    public Text error;
    [Header("InputFields")]

    public InputField name_in;
    public InputField email_in;
    public InputField city_in;
    public InputField password_in;
    public InputField passwordConf_in;
    FirebaseUser user;

    [Header("Panel")]

    public GameObject panel;

    private string update_url = "https://knighttalesranking.000webhostapp.com/update.php";
    void Start()
    {
        user = PassData.getUser();
        StartCoroutine(PassData.Read());
        email.text = PassData.getUser().Email;
        name.text = PassData.getUser().DisplayName;
        city.text = PassData.getCity();
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        ChangeScene(1);
    }
    public void UpdateUser()
    {
        
        bool ok = true;
        bool update = false;
        foreach (PlayerInfo player in PassData.getUsersPHP())
        {
            if (player.name.ToLower() == name_in.text.ToLower())
            {
                ok = false;
                break;
            }
        }
        if (ok)
        {
            if (city_in.text != "")
            {
                PassData.updateCity(city_in.text);
                update = true;
            }

            if (name_in.text != "")
            {
                 UserProfile profile = new UserProfile
                {
                    DisplayName = name_in.text,
                };
                user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("UpdateUserProfileAsync was canceled.");
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                        error.text = "NOMBRE NO VÁLIDO";
                        return;
                    }
                    
                    Debug.Log("User profile updated successfully.");
                   
                });
                update = true;
                PassData.UpdatePlayerName(name_in.text);
                StartCoroutine(UpdatePHP());
            }
            
            if (email_in.text != "") 
            { 
                user.UpdateEmailAsync(email_in.text).ContinueWith(task => {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("UpdateEmailAsync was canceled.");
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        Debug.LogError("UpdateEmailAsync encountered an error: " + task.Exception);
                        error.text = "EMAIL NO VÁLIDO";
                        return;
                    }
                    
                    Debug.Log("User email updated successfully.");
                   
                });
                update = true;
            }
            if (password_in.text == passwordConf_in.text)
            {
                if (password_in.text != "")
                {
                    user.UpdatePasswordAsync(password_in.text).ContinueWith(task => {
                        if (task.IsCanceled)
                        {
                            Debug.LogError("UpdatePasswordAsync was canceled.");
                            return;
                        }
                        if (task.IsFaulted)
                        {
                            Debug.LogError("UpdatePasswordAsync encountered an error: " + task.Exception);
                            error.text = "CONTRASEÑA NO VÁLIDA";
                            return;
                        }

                        Debug.Log("Password updated successfully.");
                    });
                    update = true;
                }
            } else
            {
                error.text = "PASSWORDS NOT THE SAME";
                update = false;
            }

            if (update)
            {
                ChangeScene(0);
            }
        } else
        {
            error.text = "THIS USERNAME IS TAKEN";
        }


       
    }

    private IEnumerator UpdatePHP()
    {
        WWWForm form = new WWWForm();
        
        form.AddField("field", "name");
        form.AddField("player_id", PassData.getAut().CurrentUser.UserId);
        form.AddField("value", name_in.text);
        UnityWebRequest web = UnityWebRequest.Post(update_url, form);


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

    public void DeleteUser()
    {
        user.DeleteAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("DeleteAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("DeleteAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("User deleted successfully.");
        });
        PassData.DeletePlayer(PassData.getAut().CurrentUser.UserId);
        StartCoroutine(PassData.DeletePHP());
        ChangeScene(0);
    }

    public void Cancel()
    {
        panel.SetActive(false);
    }

    public void Panel()
    {
        panel.SetActive(true);
    }
    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}