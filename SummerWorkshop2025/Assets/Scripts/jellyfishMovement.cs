using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jellyfishMovement : MonoBehaviour
{
    public float jellyfishSpeed;
    public GameObject player;

    // Start is called before the first frame update

    void Start()  // determine if birds start left or right
                  // 0 birds start left, 1 birds start right
    {
        player = GameObject.FindWithTag("Player");
        transform.position = new Vector3(Random.Range(-4, 4), -20 , 0);
    }

    // Update is called once per frame
    void Update() // move bird to other side of the screen
    {

        if ( transform.position.y < player.GetComponent<NewBehaviourScript>().topBorder)
        {
            transform.Translate(Vector3.up * Time.deltaTime * jellyfishSpeed);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
