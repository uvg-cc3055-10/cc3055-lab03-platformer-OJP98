using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    Rigidbody2D rb2d;
    SpriteRenderer sr;
    public Camera cam;
    private float speed = 5f;
    private float jumpForce = 250f;
    private bool facingRight = true;
    Animator anim;
    AudioSource src;
    private bool jumping = false;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        src = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        cam.transform.position = new Vector3(rb2d.transform.position.x, cam.transform.position.y, cam.transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {        

        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        if (move != 0) {
            rb2d.transform.Translate(new Vector3(1, 0, 0) * move * speed * Time.deltaTime);
            cam.transform.position = new Vector3(rb2d.transform.position.x, cam.transform.position.y, cam.transform.position.z);
            facingRight = move > 0;
        }
        
        sr.flipX = !facingRight;
        
        if (Input.GetButtonDown("Jump") && !jumping) {
            rb2d.AddForce(Vector2.up * jumpForce);
            src.Play();
            jumping = true;            
        }        
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Background"))
        {
            jumping = false;
        }
    }
}
