using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadScript : MonoBehaviour
{
    public void Awake()
    {
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManagerScript.instance.UpdateReferencesToScreens();
        GameManagerScript.instance.freeMoveScreen.SetActive(false);
        GameManagerScript.instance.turnBasedScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
