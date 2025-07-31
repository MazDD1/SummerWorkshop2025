using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class crabMovement : MonoBehaviour
{
    public float crabSpeed;
    private string crabType;
    public GameObject player;
    private float timer = 0;
    public float maxDirectionSwitchTime;
    public float minDirectionSwitchTime;
    private float directionSwitchTime;
   
    // Start is called before the first frame update

    void Start()  // determine if crabs start left or right
                  // 0 crabs start left, 1 crabs start right
    {
        player = GameObject.FindWithTag("Player");
        int randNum = Random.Range(0, 2); // gives a random value of either 0 or 1

        if (randNum == 0)
        {
            crabType = "leftStart";
            transform.position = new Vector3(-10, player.GetComponent<Transform>().position.y + Random.Range(-6, 3), 0);
        }
        else
        {
            crabType = "rightStart";
            transform.position = new Vector3(10, player.GetComponent<Transform>().position.y + Random.Range(-6, 3), 0);
        }
    }

    // Update is called once per frame
    void Update() // move crab to other side of the screen
    {

        if (transform.position.y > player.GetComponent<NewBehaviourScript>().topBorder)
        {
            Destroy(gameObject);
        }
        else if (crabType == "leftStart" && transform.position.x < player.GetComponent<NewBehaviourScript>().rightBorder)
        {
            transform.Translate(Vector3.right * Time.deltaTime * crabSpeed);
        }
        else if (crabType == "rightStart" && transform.position.x > player.GetComponent<NewBehaviourScript>().leftBorder)
        {
            transform.Translate(Vector3.left * Time.deltaTime * crabSpeed);
        }
        
        else
        {
            Destroy(gameObject);
        }

        if (timer == 0)
        {
             directionSwitchTime = Random.Range(minDirectionSwitchTime, maxDirectionSwitchTime + 1);
        }
        if (timer >= directionSwitchTime)
        {
            transform.rotation = Quaternion.AngleAxis(Random.Range(-90, 90), Vector3.forward);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }


    }

}
