using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharManager : MonoBehaviour
{

    public CharacterDatabase characterDatabase;
    public Text nameText;
    public SpriteRenderer artworkSprite;
    public AnimationClip anim;
    public Button startB;
    private int selectedOption = 0;

    // Start is called before the first frame update

    void Start()
    {

        if(!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        } else
        {
            Load();
        }
        UpdateCharacter(selectedOption);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextOption();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            BackOption();
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (nameText.text == "espadachin") {
                ChangeScene(1);
            }
            

        } 

        if (nameText.text != "espadachin") { 
            startB.interactable = false;

        } else
        {
            startB.interactable = true;
        }

    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= characterDatabase.CharacterCount)
        {
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;

        if(selectedOption < 0)
        {
            selectedOption = characterDatabase.CharacterCount - 1;
        }

        UpdateCharacter(selectedOption);
    }


    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDatabase.getCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
        anim = character.characterAnimation;
        nameText.text = character.characterName;
        Save();
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
        PassData.setCharacter(selectedOption);
    }

    public void Back()
    {
        ChangeScene(3);
    }
    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
