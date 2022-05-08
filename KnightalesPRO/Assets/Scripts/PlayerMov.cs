
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float moveSpeed;
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject tumba;
    public GameObject ghost;
    public Transform posiciontumba;
    public AudioClip ghostAudio;
    private Player player;
    Vector2 movement;
    public bool attacking = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Object_6")
        {
            if (collision == true)
                GetComponent<AudioSource>().Play();
            Destroy(tumba);
            ghost.SetActive(true);
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name == "Object_36")
        {
            player.mapa = "castillo";
            transform.position = GameObject.FindGameObjectWithTag("EntradaAppear").transform.position;
        }
        if (collision.gameObject.name == "EntradaCueva")
        {
            player.mapa = "cueva";
            transform.position = GameObject.FindGameObjectWithTag("SalidaAppear").transform.position;

        }
        if (collision.gameObject.name == "TiendaEntrada")
        {
            player.mapa = "tienda";
            transform.position = GameObject.FindGameObjectWithTag("SalidaTiendaAppear").transform.position;

        }
        if (collision.gameObject.name == "SalidaTienda")
        {
            player.mapa = "castillo";
            transform.position = GameObject.FindGameObjectWithTag("TiendaEntradaAppear").transform.position;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<AudioSource> ().playOnAwake = false;
        GetComponent<AudioSource> ().clip = ghostAudio;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.R)){
            moveSpeed += 25;
        }

        if (Input.GetKeyUp(KeyCode.R)){
            moveSpeed -= 25;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Velocidah", movement.sqrMagnitude);

        if (Input.GetKeyDown("space")){
            animator.SetBool("Key", true);
            attacking = true;
        }

        if (Input.GetKeyUp("space")){
            animator.SetBool("Key", false);
            attacking = false;
        }
        /*
        if (movement.x != 0){
            movement.y = 0;
        }
        */
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed * Time.fixedDeltaTime;
    }

}
