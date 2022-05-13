using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{
    public float speed;
    PlayerMov player;
    mageAtk magician;
    Enemy e;
    public Rigidbody2D rb;
    void Start()
    {

        player = FindObjectOfType<PlayerMov>();
        magician = FindObjectOfType<mageAtk>();
        if (magician.direction == "up")
        {
            transform.rotation = Quaternion.LookRotation(transform.forward, Vector2.left);
            rb.velocity = player.transform.up * speed;
        } else if (magician.direction == "right")
        {
            transform.rotation = Quaternion.LookRotation(transform.forward, Vector2.up);
            rb.velocity = player.transform.right * speed;
        }
        else if (magician.direction == "left")
        {
            transform.rotation = Quaternion.LookRotation(transform.forward, Vector2.down);
            rb.velocity = - player.transform.right * speed;
        }
        else if (magician.direction == "down")
        {
            transform.rotation = Quaternion.LookRotation(transform.forward, Vector2.right);
            rb.velocity = - player.transform.up * speed;
        }
        


    }

    // Update is called once per frame
    void Update()
    {
    }

    
    IEnumerator lifeEnemy(Collider2D collisionTemp)
    {
        int number = Random.Range(0, 4);
        
        e = collisionTemp.gameObject.GetComponent<Enemy>();
            
        if (number == 0)
        {
            Debug.Log("a");
            e.indicatorMiss.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            e.indicatorMiss.SetActive(false);
        }
        else if (number == 1)
        {
            Debug.Log("b");
            e.indicatorHit.SetActive(true);
            e.lifes -= 1;
            yield return new WaitForSeconds(0.3f);
            e.indicatorHit.SetActive(false);
        }
        else if (number == 2)
        {
            Debug.Log("c");
            e.lifes -= 2;
            e.indicatorCrit.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            e.indicatorCrit.SetActive(false);
        }
        else if (number == 3)
        {
            Debug.Log("d");
            e.lifes -= 3;
            e.indicatorMCrit.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            e.indicatorMCrit.SetActive(false);
        }
            
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(lifeEnemy(other));
            DestroyProjectile();

        }
    }

    /*public void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(lifeEnemy(collision));
    }*/

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}