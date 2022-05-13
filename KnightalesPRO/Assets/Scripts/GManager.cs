using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    public GameObject pause;
    private Player player;
    public GameObject deathPanel;
    public GameObject dayNightLight;
    public GameObject knight;
    public GameObject mage;
    public static bool isDay;

    public Animator animator;
    public Animator mageanim;
    public static bool gamePaused = false;
    

    private void Start()
    {
        Time.timeScale = 1;
        
        deathPanel.SetActive(false);
        animator.enabled = true;
        mageanim.enabled = true;    
        pause.SetActive(false);
        if (PassData.getCharacter() == 0)
        {
            knight.SetActive(true);
            mage.SetActive(false);
            player = knight.GetComponent<Player>();

        } else if (PassData.getCharacter() == 1)
        {
            knight.SetActive(false);
            mage.SetActive(true);
            player = mage.GetComponent<Player>();
        }

      
    }

    private void Update()
    {
        if (player.lifes <= 0)
        {
            Time.timeScale = 0;
            animator.enabled = false;
            mageanim.enabled=false;
            deathPanel.SetActive(true);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        animator.enabled = false;
        mageanim.enabled= true;
        pause.SetActive(true);

    }

    public void Reanudar() {
        pause.SetActive(false);
        animator.enabled = true;
        mageanim.enabled= true;
        Time.timeScale = 1;
        
    }

    public void BackMenu()
    {
        PassData.DeleteTable();
        ChangeScene(1);
    }
    public void ExitGame()
    {
        
        PassData.UpdateTable(player.lifes, player.end_posicion, player.mapa, player.puntuacion, true);
        ChangeScene(1);
    }



    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    //public bool checkDay(){}
}
