using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdMovement : MonoBehaviour
{
    public float birdSpeed;
    private string birdType;
    public GameObject player;
    private SpriteRenderer spriteRenderer;
    public Sprite bird1;
    public Sprite bird2;
    public Sprite bird3;
    public float spriteSwitchTime;
    private float timer = 0;

    // Start is called before the first frame update

    void Start()  // determine if birds start left or right
                  // 0 birds start left, 1 birds start right
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        int randNum = Random.Range(0, 2); // gives a random value of either 0 or 1
        
        if (randNum == 0)
        {
            birdType = "leftStart";
            transform.position = new Vector3(-10, player.GetComponent<Transform>().position.y + Random.Range(-6, 3), 0);
            transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
        }      
        else 
        {
            birdType = "rightStart";
            transform.position = new Vector3(10, player.GetComponent<Transform>().position.y + Random.Range(-6, 3), 0);
            transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        }
    }
        
    // Update is called once per frame
    void Update() // move bird to other side of the screen
    {

        if (birdType == "leftStart" && transform.position.x < player.GetComponent<NewBehaviourScript>().rightBorder)
        {
            transform.Translate(Vector3.up * Time.deltaTime * birdSpeed);
        }
        else if (birdType == "rightStart" && transform.position.x > player.GetComponent<NewBehaviourScript>().leftBorder)
        {
            transform.Translate(Vector3.up * Time.deltaTime * birdSpeed);
        }
        else 
        {
            Destroy(gameObject);
        }

        if (timer >= spriteSwitchTime)
        {
            timer = 0;
            if (spriteRenderer.sprite == bird1)
            {
                spriteRenderer.sprite = bird2;
            }
            else if (spriteRenderer.sprite == bird2)
            {
                spriteRenderer.sprite = bird3;
            }
            else if (spriteRenderer.sprite == bird3)
            {
                spriteRenderer.sprite = bird1;
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
    void switchSprite()
    {
        
    }

}
