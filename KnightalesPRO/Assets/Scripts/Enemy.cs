using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float stoppingDistance;
    public float nearDistance;
    public float startTimeBtwShots;
    private float timeBtwShots;
	
	public GameObject indicatorMiss;
    public GameObject indicatorHit;
    public GameObject indicatorCrit;
    public GameObject indicatorMCrit;
    
    [Header("References")]
    public GameObject shot;
    private Transform ptransform;
    private PlayerMov player;
   
    public int lifes = 10;
    void Start()
    {
        startTimeBtwShots = 4;
       
        ptransform = FindObjectOfType<Player>().transform;
        player = FindObjectOfType<PlayerMov>();
        timeBtwShots = startTimeBtwShots;
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Vector2.Distance(transform.position, ptransform.position) < nearDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, ptransform.position, speed * Time.deltaTime);
            
            Shoot();
        }
        else if (Vector2.Distance(transform.position, ptransform.position) < stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, ptransform.position, speed * Time.deltaTime);
            Shoot();
        }
        else if (Vector2.Distance(transform.position, ptransform.position) < stoppingDistance && Vector2.Distance(transform.position, ptransform.position) > nearDistance)
        {
            transform.position = this.transform.position;
        }

        if (lifes <= 0)
        {
            Destroy(gameObject);
            player.player.puntuacion++;
        }

    }

    IEnumerator lifeEnemy(Collision2D collisionTemp){
        int number = Random.Range(0,4);
        if (collisionTemp.gameObject.tag == "Jugador")
        {
            if (player.attacking)
            {
                if (number == 0)
                {
                    Debug.Log("a");
                    indicatorMiss.SetActive(true);
                    yield return new WaitForSeconds(0.3f);
                    indicatorMiss.SetActive(false);                 
                }else if (number == 1)
                {
                    Debug.Log("b");
                    indicatorHit.SetActive(true);
                    lifes-=1;
                    yield return new WaitForSeconds(0.3f);
                    indicatorHit.SetActive(false);
                }else if (number == 2)
                {
                    Debug.Log("c");
                    lifes-=2;
                    indicatorCrit.SetActive(true);
                    yield return new WaitForSeconds(0.3f);
                    indicatorCrit.SetActive(false);
                }else if (number == 3)
                {
                    Debug.Log("d");              
                    lifes-=3;
                    indicatorMCrit.SetActive(true);
                    yield return new WaitForSeconds(0.3f);
                    indicatorMCrit.SetActive(false);
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(lifeEnemy(collision));
    }

    private void Shoot()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(shot, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

}
