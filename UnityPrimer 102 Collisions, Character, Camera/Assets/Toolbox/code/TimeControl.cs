using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour {

    private void Update()
    {
        float scale = Mathf.Pow(1.3f, Time.deltaTime);

        if (Input.GetKey(KeyCode.UpArrow)) { Time.timeScale *= scale; }
        if (Input.GetKey(KeyCode.DownArrow)) { Time.timeScale /= scale; }
    }
}
