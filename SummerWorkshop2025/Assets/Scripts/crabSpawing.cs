using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crabSpawing : MonoBehaviour
{
    public GameObject crab;
    private float timer = 0;
    public float respawnTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= respawnTime)
        {
            spawnCrab();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void spawnCrab()
    {
        Instantiate(crab);
    }
}
