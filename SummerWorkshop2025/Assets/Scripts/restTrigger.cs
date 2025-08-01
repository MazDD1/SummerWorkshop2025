using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restTrigger : MonoBehaviour
{
    GameObject restSite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Scene scene = SceneManager.GetActiveScene();
            GameManagerScript.instance.restSiteWindow.SetActive(true);
            if (scene.name == "CombinedScene")
            {
                SceneManager.LoadScene("Level 2");
            }
            else if (scene.name == "Level 2")
            {
                SceneManager.LoadScene("Level 3");
            }
            
            
            
        }

    }
}
