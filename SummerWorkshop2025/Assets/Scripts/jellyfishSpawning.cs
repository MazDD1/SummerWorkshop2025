using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jellyfishSpawning : MonoBehaviour
{
    public GameObject jellyfish;
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
            spawnJellyfish();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void spawnJellyfish()
    {
        Instantiate(jellyfish, this.transform);
    }
}
