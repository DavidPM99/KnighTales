using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{
    public float speed;

    private Transform ptransform;
    public Collision2D col;

    public GameObject mago;
    private Vector2 target;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("ghostWithHealth"))
        {
            other.gameObject.GetComponent<Enemy>().OnCollisionEnter2D(col);
        }
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}