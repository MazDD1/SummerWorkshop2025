using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngineInternal;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Sprite turtle1;
    public Sprite turtle2;
    public float playerSpeed;
    public float rightBorder;
    public float leftBorder;
    public float topBorder;
    public float bottomBorder;
    private float timer = 0;
    public float spriteSwitchTime;

    private Vector2 movement;
    float moveLimiter = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       spriteRenderer = GetComponent<SpriteRenderer>();
       playerSpeed = InventoryManagerScript.instance.currentShellType.moveSpeed;
    }

    private void FixedUpdate()
    {
        if (movement.x != 0 && movement.y != 0)
        {
            movement *= moveLimiter;
        }

        if(movement.x != 0 || movement.y != 0)
        {
            Vector3 dir = -movement;
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        
        rb.velocity = movement * playerSpeed * Time.fixedDeltaTime;
        
    }

    void switchSprite()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            if (spriteRenderer.sprite == turtle1)
            {
                spriteRenderer.sprite = turtle2;
            }
            else if (spriteRenderer.sprite = turtle2)
            {
                spriteRenderer.sprite = turtle1;
            }
        }
    }

    public void InitialiseData()
    {
        playerSpeed = InventoryManagerScript.instance.currentShellType.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (timer >= spriteSwitchTime)
        {
            switchSprite();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }

        /*
        if (Input.GetAxisRaw("Horizontal") == 1 && transform.position.x < rightBorder)
        {
            transform.Translate(new Vector3(1,0,0) * playerSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        else if (Input.GetAxisRaw("Horizontal") == -1 && transform.position.x > leftBorder)
        {
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }

        if (Input.GetAxisRaw("Vertical") == 1 && transform.position.y < topBorder)
        {
            transform.Translate(Vector3.up * playerSpeed * Time.deltaTime);
        }

        else if (Input.GetAxisRaw("Vertical") == -1 && transform.position.y > bottomBorder)
        {
            transform.Translate(Vector3.down * playerSpeed * Time.deltaTime);
        }
        */


        /*
        if (transform.position.x > 5)
        {
            Debug.Log("border hit");
            transform.position.Set(5, transform.position.y, transform.position.z);
        }
        */


    }

    /**
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
          // do something
        }
    }
    **/

}
