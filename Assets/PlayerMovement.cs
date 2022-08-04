using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2;
    public float jumpValue = 1; 
    public Rigidbody2D rb;
    public Transform position; 

    // Start is called before the first frame update
    void Start()
    {
        position = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        position.position += new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime, 0);
        rb.velocity += new Vector2(0, vertical * jumpValue * Time.deltaTime); 


    }
}
