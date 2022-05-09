using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;
    public GameObject panel5;
    public GameObject panel6;
    public GameObject button;
    public GameObject player;
    public GameObject ghost;
    public Animator animator;
    private bool spawn = false;
    private bool change = false;
    private List<GameObject> list = new List<GameObject>();
    private int i = 0;
    Stopwatch sp = new Stopwatch();
    Stopwatch sp2 = new Stopwatch();
    Stopwatch sp3 = new Stopwatch();
    Stopwatch sp4 = new Stopwatch();
    Stopwatch sp5 = new Stopwatch();
    Stopwatch sp6 = new Stopwatch();
    Stopwatch sp7 = new Stopwatch();
    void Start()
    {
        player.transform.position = new Vector3(1.274f, -0.933f, 0.04f);
        
       
        list.Add(panel3);
        list.Add(panel4);
        list.Add(panel5);

        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);
        panel5.SetActive(false);
        panel6.SetActive(false);
        panel1.SetActive(true);

        button.GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (sp.ElapsedMilliseconds > 4000)
        {
            sp.Stop();
            sp.Reset();
            spawn = true;
            player.transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
            sp2.Start();
        }

        if (sp2.ElapsedMilliseconds > 3000 && spawn && i == 0)
        {
            sp2.Stop();
            sp2.Reset();
            Time.timeScale = 0;
            animator.enabled = false;
            list[i].SetActive(true);
            i++;
            sp3.Start();
        }

        if (sp3.ElapsedMilliseconds > 2000 && spawn && i == 1)
        {
            sp3.Stop();
            sp3.Reset();
            Time.timeScale = 1;
            animator.enabled = true;
            list[i-1].SetActive(false);
            i++;
            sp4.Start();
        }
        if (sp4.ElapsedMilliseconds > 3000 && spawn && i == 2)
        {
            sp4.Stop();
            sp4.Reset();
            Time.timeScale = 0;
            animator.enabled = false;
            list[i - 1].SetActive(true);
            i++;
            sp5.Start();

        }

        if (sp5.ElapsedMilliseconds > 2000 && spawn && i == 3)
        {
            sp5.Stop();
            sp5.Reset();
            Time.timeScale = 1;
            animator.enabled = true;
            list[i - 2].SetActive(false);
            i++;

        }
        if (ghost == null && i == 4)
        {
            list[i-2].SetActive(true);
            Time.timeScale = 0;
            animator.enabled = false;
            sp6.Start();
            i++;
        }
        
        
        if (sp6.ElapsedMilliseconds > 2000 && spawn && i == 5)
        {
            sp6.Stop();
            sp6.Reset();
            Time.timeScale = 1;
            animator.enabled = true;
            list[i - 3].SetActive(false);
            i++;

        }

        if (sp7.ElapsedMilliseconds > 3000 && change)
        {
            sp7.Stop();
            sp7.Reset();
            ChangeScene(1);
        }
        
        if (player.GetComponent<PlayerMov>().button.active)
        {
            button.GetComponent<Button>().interactable = true;
        }

    }

   
    public void StartGame()
    {
        panel6.SetActive(true);
        button.SetActive(false);
        sp7.Start();
        change = true;
    }
    public void Panel2()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }

    public void Done()
    {
        panel2.SetActive(false);
        
        sp.Start();
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
