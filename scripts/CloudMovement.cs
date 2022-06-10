using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public Transform endpoint;
    public Transform spawnpoint;

    public float movespeed=2f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * movespeed;
        if(transform.position.x<=endpoint.position.x)
        {
            transform.position = new Vector3(spawnpoint.position.x, transform.position.y, transform.position.z);
        }
    }
}
