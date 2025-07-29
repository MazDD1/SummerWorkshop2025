using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdMovement : MonoBehaviour
{
    public float birdSpeed;
    private string birdType;
    public GameObject player;
    
    // Start is called before the first frame update

    void Start()  // determine if birds start left or right
                  // 0 birds start left, 1 birds start right
    {
        player = GameObject.FindWithTag("Player");
        int randNum = Random.Range(0, 2); // gives a random value of either 0 or 1
        
        if (randNum == 0)
        {
            birdType = "leftStart";
            transform.position = new Vector3(-10, Random.Range(-4.5f, 4.5f), 0);
        }      
        else 
        {
            birdType = "rightStart";
            transform.position = new Vector3(10, Random.Range(-4.5f, 4.5f), 0);
        }
    }
        
    // Update is called once per frame
    void Update() // move bird to other side of the screen
    {

        if (birdType == "leftStart" && transform.position.x < player.GetComponent<NewBehaviourScript>().rightBorder)
        {
            transform.Translate(Vector3.right * Time.deltaTime * birdSpeed);
        }
        else if (birdType == "rightStart" && transform.position.x > player.GetComponent<NewBehaviourScript>().leftBorder)
        {
            transform.Translate(Vector3.left * Time.deltaTime * birdSpeed);
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}
