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

    public static bool isDay;

    public Animator animator;
    public static bool gamePaused = false;

    private void Start()
    {
        Time.timeScale = 1;
        player = FindObjectOfType<Player>();
        deathPanel.SetActive(false);
        animator.enabled = true;
        pause.SetActive(false);
    }

    private void Update()
    {
        if (player.lifes <= 0)
        {
            Time.timeScale = 0;
            animator.enabled = false;
            deathPanel.SetActive(true);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        animator.enabled = false;
        pause.SetActive(true);

    }

    public void Reanudar() {
        pause.SetActive(false);
        animator.enabled = true;
        Time.timeScale = 1;
        
    }

    public void BackMenu()
    {
        PassData.DeleteTable();
        ChangeScene(3);
    }
    public void ExitGame()
    {
        
        PassData.UpdateTable(player.lifes, player.end_posicion, player.mapa, player.puntuacion, true);
        ChangeScene(3);
    }



    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    //public bool checkDay(){}
}
