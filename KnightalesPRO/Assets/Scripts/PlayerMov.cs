using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMov : MonoBehaviour
{
    public float moveSpeed;
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject tumba;
    public GameObject ghost;
    public Transform posiciontumba;
    public AudioClip ghostAudio;
    public Player player;
    public Vector2 movement;
    public bool attacking = false;
	
	public GameObject button;

    public GameObject dialogbx;
    public TMP_Text dialog;

    public GameObject shot;
    private float timeBtwShots;
    public float startTimeBtwShots;

    public Transform bulletSpawn;

	public IEnumerator ChuckDialogs(string uri)
    {
        
        using (UnityWebRequest request1 = UnityWebRequest.Get(uri))
        {
            yield return request1.SendWebRequest();

            if (request1.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request1.error);
            }
            else
            {
                string result = request1.downloadHandler.text;
                TMP_Text tm = dialog;
                tm.text = result;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "ChuckNorris")
        {
            if (collision == true){
                button.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.name == "ChuckNorris")
        {
            button.SetActive(false);
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
		if(button.activeSelf){
            if (Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine(ChuckDialogs("http://20.224.199.76/api/frases/rand"));
                dialogbx.SetActive(true);
            }
        }else
        {
            dialogbx.SetActive(false);
            StopCoroutine(ChuckDialogs("http://20.224.199.76/api/frases/rand"));
        }
        
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
            if (gameObject.name.Contains("magician"))
            {
                Shoot();
            }
            
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
    private void Shoot()
    {
        Instantiate(shot, transform.position, bulletSpawn.rotation);
       /* if (timeBtwShots <= 0)
        {
            Instantiate(shot, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        } */
    }
}



    
