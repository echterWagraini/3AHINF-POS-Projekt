using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float movespeed = 15f;
    public float panBorder = 10f;

    public Camera camera;

    void Awake()
    {
        transform.position = new Vector3(-667, -31, -10);
        camera.orthographicSize = 125;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a")||Input.mousePosition.x<=panBorder)
        {
            if(transform.position.x>-667)
            transform.position += Vector3.left * Time.deltaTime * movespeed;
        }
        if (Input.GetKey("d")||Input.mousePosition.x>=(Screen.width-panBorder))
        {
            if(transform.position.x<-72)
            transform.position += Vector3.right * Time.deltaTime * movespeed;
        }
    }
}
