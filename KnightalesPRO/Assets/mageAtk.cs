using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageAtk : MonoBehaviour
{

   
    public string direction = "";
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            //transform.rotation = Quaternion.LookRotation(transform.forward, Vector2.up);
            direction = "up";
        }


        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            //transform.rotation = Quaternion.LookRotation(transform.forward, Vector2.down);
            direction = "down";
        }


        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            //transform.rotation = Quaternion.LookRotation(transform.forward, Vector2.left);
            direction = "left";
        }


        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            //transform.rotation = Quaternion.LookRotation(transform.forward, Vector2.right);
            direction = "right";
        }

       


    }
}
