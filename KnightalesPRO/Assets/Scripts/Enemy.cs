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
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Jugador")
        {
            if (player.attacking)
            {
                lifes--;
            }
        }

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
