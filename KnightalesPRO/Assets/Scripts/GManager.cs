using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    public GameObject pause;

    public GameObject dayNightLight;

    public static bool isDay;

    public Animator animator;
    public static bool gamePaused = false;

    private void Start()
    {
        Time.timeScale = 1;
        animator.enabled = true;
        pause.SetActive(false);
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

    public void ExitGame()
    {
        Player player = FindObjectOfType<Player>();
        PassData.UpdateTable(player.lifes, player.end_posicion, player.mapa, player.puntuacion, true);
        ChangeScene(3);
    }



    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    //public bool checkDay(){}
}
