using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnCollision : MonoBehaviour
{

    public GameObject prefabToSpawn;

    ///<summary>Spawned object's audio source's volume is set relatively to collision.relativeVelocity & this factor.</summary>
    public float audioSourceVolumeFactor = 0.1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (prefabToSpawn)
        {
            GameObject go = GameObject.Instantiate(prefabToSpawn, collision.contacts[0].point, Quaternion.identity);
            AudioSource sound = go.GetComponent<AudioSource>();
            if (sound)
            {
                sound.volume = Mathf.Clamp01(Mathf.Abs(Vector3.Dot(collision.relativeVelocity, collision.contacts[0].normal)) * audioSourceVolumeFactor);
            }
        }
    }
}
