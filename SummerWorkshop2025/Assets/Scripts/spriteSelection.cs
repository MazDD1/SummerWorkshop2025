using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteSelection : MonoBehaviour
{

    public Sprite blueCrab;
    public Sprite orangeCrab;
    public Sprite purpleCrab;
    public Sprite redCrab;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int randNum = Random.Range(0, 4);
        if (randNum == 0)
        {
            spriteRenderer.sprite = blueCrab;
        }
        else if (randNum == 1)
        {
            spriteRenderer.sprite = orangeCrab;
        }
        else if (randNum == 2)
        {
            spriteRenderer.sprite = purpleCrab;
        }
        else if (randNum == 3)
        {
            spriteRenderer.sprite = redCrab;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
