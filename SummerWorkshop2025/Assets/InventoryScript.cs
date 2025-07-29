using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    public bool IsInventoryFree = true;
    public float Item = 1;
    public float PlaceHolderHealthVariable = 100;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void healPlayer()
    {
        Debug.Log("Player healed");
        PlaceHolderHealthVariable = 100;
    }
}
