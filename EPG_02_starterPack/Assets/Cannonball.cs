using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour {

    public float CannonballSpeed = 1f;
  

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = transform.right * -1 * CannonballSpeed;
	}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
