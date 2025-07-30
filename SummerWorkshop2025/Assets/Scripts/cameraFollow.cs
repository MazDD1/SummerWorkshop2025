using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        transform.position = new Vector3(0,0,-10);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Transform>().position.y < 0)
        {
            transform.position = new Vector3(0, player.GetComponent<Transform>().position.y, -10);
        }
    }
}
