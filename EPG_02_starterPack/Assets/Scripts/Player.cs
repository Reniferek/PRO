using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 2f;
    public float jump = 400f;
    private bool isGrounded = true;
    private Rigidbody2D rb;
    private float x_displacement;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        x_displacement = 0;
    }

    void Update() {
        x_displacement = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.position += new Vector3(x_displacement, 0, 0);

        if (Input.GetKey("space") && isGrounded) {
            isGrounded = false;
            rb.AddForce(new Vector2(0, jump));
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Cannonball"))
        {
            Destroy(gameObject);
        }
        isGrounded = true;
    }

}
