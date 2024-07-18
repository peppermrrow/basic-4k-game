using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlemovement : MonoBehaviour
{
    private float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        if (GameSettings.instance != null)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else
        {
            Debug.Log("instance doesn't work");
        }
    }
}
