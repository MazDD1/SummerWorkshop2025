using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
// uses UnityEngine.UI for Text
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    /* public static InventoryScript Instance { get; private set; } */

    // PlayerEquipStatus allow3s for text to display which shell the user has current equiped or has not equiped
    public Text PlayerEquipStatus;
    // Shell name is a variable so it can be changed easily
    public string ShellName;

    /* private void Awake()
    {

        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject); 
    } */

    // this script changes the layers used by the game object (Which is accessable for other scripts) to keep track of which items are equiped.
    /*layer 6 is Item 0 (nothing equiped)
      layer 7 is Item 1 (item 1 equiped)
      layer 8 is Item 2
      layer 9 is Item 3
      layer 10 is Item 4
    Each item is a different shell with different qabilities*/

    // Start is called before the first frame update
    void Start()
    {
        /* gameObject.layer = Instance.Layer;
        Debug.Log(Layer); */

        // this is the inital text that will be dispalyed initially.
        if (gameObject.layer == 6)
        {
            PlayerEquipStatus.text = "No shell equiped";
        }
        else
        {
            PlayerEquipStatus.text = "Shell " + ShellName + " equiped";
        }
    } 

    // Update is called once per frame
    void Update()
    {

    }


    // These procedures will set what is currently equiped by the user.
    // When this gameObject is on layer 6 it is unequiped and is allowed to change shells/items.
    // These procedures will be called when they are pressed by the user
    public void SetItem0()
    {
        Debug.Log("Player Item Unequiped, layer 6");
        gameObject.layer = 6;
        PlayerEquipStatus.text = "No shell equiped";
    }

    public void SetItem1 ()
    {
        if (gameObject.layer == 6)
        {
            ShellName = "1";
            Debug.Log("Shell 1 equiped, layer 7");
            gameObject.layer = 7;
            PlayerEquipStatus.text = "Shell " + ShellName + " equiped";
        }
        else
        {
            Debug.Log("De-equip your item! (you already have this equiped or you have something else equiped)");
        }
    }

    public void SetItem2 () 
    {
        if (gameObject.layer == 6)
        {
            ShellName = "2";
            Debug.Log("Shell 3 equiped, layer 8");
            gameObject.layer = 8;
            PlayerEquipStatus.text = "Shell " + ShellName + " equiped";
        }
        else
        {
            Debug.Log("De-equip your item! (you already have this equiped or you have something else equiped)");
        }
    }

    public void SetItem3()
    {
        if (gameObject.layer == 6)
        {
            ShellName = "3";
            Debug.Log("Shell 3 equiped, layer 9");
            gameObject.layer = 9;
            PlayerEquipStatus.text = "Shell " + ShellName + " equiped";
        }
        else
        {
            Debug.Log("De-equip your item! (you already have this equiped or you have something else equiped)");
        }
    }
}
