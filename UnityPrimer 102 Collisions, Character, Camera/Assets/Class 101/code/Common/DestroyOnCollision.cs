using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {

    public float speedThreshold = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vel0 = GetComponent<Rigidbody>().velocity;
        //Debug.Log("velo " + vel0);
	}

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 vel0 = GetComponent<Rigidbody>().velocity;
        if (collision.other && collision.other.GetComponent<Rigidbody>())
        {
            Vector3 vel1 = collision.other.GetComponent<Rigidbody>().velocity;
        }

        //Debug.Log("cling");

        float collisionSpeed = Vector3.Dot(collision.relativeVelocity, collision.contacts[0].normal);
        if (collisionSpeed > speedThreshold)
        {
            //Destroy(gameObject);
            StartCoroutine(ScaleDownAndDestroy());
        }
    }

    IEnumerator ScaleDownAndDestroy()
    {
        float timeEleapsed = 0f;
        float period = 0.3f;

        Vector3 startScale = transform.localScale;

        while(timeEleapsed < period)
        {
            timeEleapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, timeEleapsed / period);
            yield return null;
        }

        Destroy(gameObject);
    }
}
