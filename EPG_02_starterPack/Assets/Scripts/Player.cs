using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 2f;
    private Rigidbody2D rb;
    private float x_displacement;
    private float y_displacement;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        x_displacement = 0;
    }

    void Update() {
        x_displacement = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        y_displacement = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position += new Vector3(x_displacement, y_displacement, 0);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Cannonball"))
        {
            Destroy(gameObject);
        }
    }

}
