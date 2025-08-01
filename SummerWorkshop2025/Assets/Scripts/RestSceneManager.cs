using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestSceneManager : MonoBehaviour
{
    public GameObject RestScene;
    public GameObject InventoryScene;

    public GameObject FreeMoveScreen;
    public GameObject RestAndInventoryScreen;

    // this manager will be used to correctly display the inventory and rest 'scenes' when the appropriate buttons are pressed.
    // this will be called when the player reaches the rest scene
    // Should be ingored outside of rest scene


    public int playerHealth = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToInventoryScene()
    {
        RestScene.SetActive(false);
        InventoryScene.SetActive(true);
    }

    public void MoveToRestScene()
    {
        InventoryScene.SetActive(false);
        RestScene.SetActive(true);
    }

    public void LeaveRestScene()
    {
        InventoryScene.SetActive(false);
        RestScene.SetActive(false);
    }

    public void SwitchToFreeMove()
    {

        GameManagerScript.instance.restSiteWindow.SetActive(false);
        GameManagerScript.instance.freeMoveScreen.SetActive(true);
    }
}
