using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterDatabase characterDatabase;
    public SpriteRenderer artworkSprite;
    private int selectedOption = 0;
    public int lifes = 3;
    public float[] start_posicion;
    public string end_posicion;
    public int puntuacion = 0;
    public string name;
    public string mapa = "castillo";
    private Transform player;
    private FirebaseAuth auth;
    private FirebaseUser User;
    // Start is called before the first frame update
    void Start()
    {
        mapa = PassData.getMapa();
        player = GameObject.FindGameObjectWithTag("Jugador").transform;
        User = PassData.getUser();
        name = User.DisplayName;
        lifes = PassData.getLife();
        start_posicion = PassData.getPos();
        
        puntuacion = PassData.getPuntos();
        Debug.Log(name + " " + lifes + " " + start_posicion + " " + puntuacion);
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = PassData.getCharacter();
        }
        else
        {
            Load();
        }
        

        player.position = new Vector3(start_posicion[0], start_posicion[1], start_posicion[2]);
        UpdateCharacter(selectedOption);
    }

    void Update()
    {
        end_posicion = player.transform.position.x + ";" + player.transform.position.y + ";" + player.transform.position.z;
        CameraValues.setCameraPos(mapa);
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.E))
        {
           PassData.UpdateTable(lifes, end_posicion, mapa, puntuacion, true);
        }
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDatabase.getCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
      
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");

    }

}
