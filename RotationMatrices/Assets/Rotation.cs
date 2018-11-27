using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    public static Transform target;
    static Vector3 eulerAngles = target.rotation.eulerAngles;

    private static float[][] identityMatrix =
{
     new [] {1.0f, 0.0f, 0.0f},
     new [] {0.0f, 1.0f, 0.0f},
     new [] {0.0f, 0.0f, 1.0f}
 };
    private static float[][] RotateX =
{
     new [] {1.0f,                     0.0f,                      0.0f},
     new [] {0.0f, Mathf.Cos(eulerAngles.x), -Mathf.Sin(eulerAngles.x)},
     new [] {0.0f, Mathf.Sin(eulerAngles.x), Mathf.Cos(eulerAngles.x) }
 };

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
