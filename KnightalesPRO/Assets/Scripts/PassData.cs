using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using Newtonsoft.Json;

public static class PassData
{
    private static FirebaseAuth auth;
    private static FirebaseUser User;
    private static FirebaseFirestore db;
    private static int lifes = 10;

    private static string read_url = "https://knighttalesranking.000webhostapp.com/read.php";
    private static string delete_url = "https://knighttalesranking.000webhostapp.com/delete.php";
    private static string city = "";
    private static string posicion;
    private static int puntuacion = 0;
    private static int character = 0;
    private static bool banned = false;
    private static bool savedGame = false;
    private static string mapa = "castillo";
	private static string player_name = "";
    private static string player_id = "";
    private static List<PlayerInfo> users = new List<PlayerInfo>();
    private static Dictionary<string, string> usersInfo = new Dictionary<string, string>();
    public static void setUserData(FirebaseAuth aut, FirebaseUser usuario)
    {
        User = usuario;
        auth = aut;
        ReadTable();
    }

    public static void setCharacter(int num)
    {
        character = num;
    }
   
    public static string getMapa()
    {
        return mapa;
    }
    public static List<PlayerInfo> getUsersPHP()
    {
        return users;
    }
    public static bool getBan()
    {
        return banned;
    }
    public static void setCity(string c_name)
    {
        city = c_name;
    }

    public static int getCharacter()
    {
        return character;
    }
    public static FirebaseAuth getAut()
    {
        return auth;
    }

    public static FirebaseUser getUser() {
        return User;    
    }

   
    public static string getCity() {
        return city;
    }
    
    public static int getLife()
    {
        return lifes;
    }
	
	public static Dictionary<string, string> getAllUsers()
    {
        return usersInfo;
    }

    public static bool getSavedGame()
    {
        return savedGame;
    }
    public static float[] getPos()
    {
        string[] a = posicion.Split(';');
        float[] b = new float[a.Length];
        for(int i = 0; i < a.Length; i++)
        {
            b[i] = float.Parse(a[i]);
        }
        return b;
    }

    public static int getPuntos()
    {
        return puntuacion;
    }

    public static void CreateTable()
    {
        posicion = 1.274 + ";" + -0.933 + ";" + 0.04;
        db = FirebaseFirestore.DefaultInstance;
        
        DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "Lifes", lifes },
            {"City", city},
            { "Posicion", posicion },
            { "Puntuacion", puntuacion },
            { "SaveGame", savedGame },
            { "Character", character },
            { "Banned", banned },
            {"Mapa", mapa }
        };
        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the alovelace document in the users collection.");
        });

    }

    public static void updateCity(string city)
    {
        
        db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);
        Dictionary<string, object> update = new Dictionary<string, object>
        {
           { "City", city }
        };
        docRef.SetAsync(update, SetOptions.MergeAll).ContinueWithOnMainThread(task => {
            Debug.Log("City updated on users.");
        });

    }

    public static void setBan(bool bann, string playerId)
    {
        banned = bann;
        db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(playerId);
        Dictionary<string, object> update = new Dictionary<string, object>
{
        { "Banned", banned }
};
        docRef.SetAsync(update, SetOptions.MergeAll).ContinueWithOnMainThread(task => {
            Debug.Log("Ban updated.");
        });

    }
    public static void getBanState(string playerId, GameObject bt1, GameObject bt2)
    {

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(playerId);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {

                Dictionary<string, object> usuario = snapshot.ToDictionary();

                object banned_obj = usuario["Banned"];

                bool state = Convert.ToBoolean(banned_obj);

                if (state)
                {
                    bt1.SetActive(false);
                    bt2.SetActive(true);
                }
                else
                {
                    bt1.SetActive(true);
                    bt2.SetActive(false);
                }

            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });


    }

    public static void checkBan(GameObject panel)
    {
        //FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        
        DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            
            if (snapshot.Exists)
            {
                
                Dictionary<string, object> usuario = snapshot.ToDictionary();
                
                object banned_obj = usuario["Banned"];
                
                bool state = Convert.ToBoolean(banned_obj);
                
                if (state)
                {
                    panel.SetActive(true);
                }
                else
                {
                    
                    panel.SetActive(false);
                    
                    SceneManager.LoadScene(3);
                    
                }
                
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }

    public static void getextBanState(string playerId, Text txt)
    {

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(playerId);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {

                Dictionary<string, object> usuario = snapshot.ToDictionary();

                object banned_obj = usuario["Banned"];

                bool state = Convert.ToBoolean(banned_obj);

                if (state)
                {
                    txt.text = "BANNED";
                }
                else
                {
                    txt.text = "";
                }

            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });


    }
    public static void UpdateTable(int life, string pos, string mapa, int punto, bool savedGame)
    {
        db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "Lifes", life },
            { "City", city},
            { "Posicion", pos },
            { "Puntuacion", punto },
            { "SaveGame", savedGame },
            { "Character", character },
            { "Banned", banned },
            {"Mapa", mapa }
        };
        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
            Debug.Log("Data updated.");
        });
    }
    public static void ReadTable()
    {
        Debug.Log("reading");
        db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {

                Dictionary<string, object> usuario = snapshot.ToDictionary();
                object city_obj = usuario["City"];
                object lifes_obj = usuario["Lifes"];
                object posicion_obj = usuario["Posicion"];
                object puntuacion_obj = usuario["Puntuacion"];
                object saved_obj = usuario["SaveGame"];
                object character_obj = usuario["Character"];
                object banned_obj = usuario["Banned"];
                object mapa_obj = usuario["Mapa"];
                lifes = Convert.ToInt32(lifes_obj);
                if (lifes == 0) { lifes = 10; }
                
                posicion = posicion_obj.ToString();
                city = city_obj.ToString();
                character = Convert.ToInt32(character_obj);
                puntuacion = Convert.ToInt32(puntuacion_obj);
                savedGame = Convert.ToBoolean(saved_obj);
                banned = Convert.ToBoolean(banned_obj);
                mapa = mapa_obj.ToString();

                
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }

    public static void DeleteTable()
    {
        DocumentReference userRef = db.Collection("users").Document(auth.CurrentUser.UserId);
        userRef.DeleteAsync();
        CreateTable();
    }

    public static void DeletePlayer(string playerId)
    {
        DocumentReference userRef = db.Collection("users").Document(playerId);
        userRef.DeleteAsync();
        DocumentReference playerRef = db.Collection("players").Document(playerId);
        playerRef.DeleteAsync();
        
    }
    public static void UpdatePlayerName(string name)
    {
        db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("players").Document(auth.CurrentUser.UserId);
        Dictionary<string, object> update = new Dictionary<string, object>
        {
            { "PlayerName", name }
            
        };
        
        docRef.SetAsync(update, SetOptions.MergeAll).ContinueWithOnMainThread(task => {
            Debug.Log("PlayerName updated on players.");
        });
    }
	
	public static void SaveNewPlayer()
    {
        db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("players").Document(auth.CurrentUser.UserId);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "PlayerName", User.DisplayName},
            { "PlayerId", auth.CurrentUser.UserId},
            
        };
        docRef.SetAsync(user).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the alovelace document in the players collection.");
        });
    }
    public static void AllUsers()
    {
        usersInfo.Clear();

        Query usersRef = db.Collection("players").WhereNotEqualTo("PlayerName", "admin");
        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            QuerySnapshot playerQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in playerQuerySnapshot.Documents)
            {
                Dictionary<string, object> player = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in player)
                {

                    
                    if (pair.Key == "PlayerName") { 
                        player_name = pair.Value.ToString();
                        Debug.Log(String.Format("{0}", pair.Value));
                    }
                    if (pair.Key == "PlayerId")
                    {
                        player_id = pair.Value.ToString();
                        Debug.Log(String.Format("{0}", pair.Value));

                    }
                    
                    

                }
                usersInfo.Add(player_name, player_id);
            };
        });
    }

    public static IEnumerator Read()
    {

        UnityWebRequest web = UnityWebRequest.Get(read_url);

        yield return web.SendWebRequest();
        if (!web.isNetworkError && !web.isHttpError)
        {
            users = JsonConvert.DeserializeObject<List<PlayerInfo>>(web.downloadHandler.text);

        }
        else
        {
            Debug.Log("Error while reading...");
        }

    }

    public static IEnumerator DeletePHP()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", User.DisplayName);
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
}
