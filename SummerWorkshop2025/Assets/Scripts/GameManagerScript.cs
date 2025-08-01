using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public GameObject freeMoveScreen;
    public GameObject turnBasedScreen;
    public GameObject restSiteScreen;

    [SerializeField]
    private TurnBasedLogic turnBasedLogicScript;

    public GameObject VictoryScreen;
    public GameObject DeathScreen;
    public GameObject restSiteWindow;

    public EnemyTypesSO enemyTypesSO;

    [SerializeField]
    private bool FreeMoveActive;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }   
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        restSiteScreen = GameObject.FindGameObjectWithTag("RestSite");
    }

    // Start is called before the first frame update
    void Start()
    {

        FreeMoveActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (FreeMoveActive)
            {
                freeMoveScreen.SetActive(false);
                turnBasedScreen.SetActive(true);
                FreeMoveActive = false;

            }
            else
            {
                freeMoveScreen.SetActive(true);
                turnBasedScreen.SetActive(false);
                FreeMoveActive = true;
            }


        }
    }

    public void SwitchToTurnBased(bool turnBased)
    {
        Debug.Log(turnBased);
        Debug.Log("Switching scenes");
        freeMoveScreen.SetActive(!turnBased);
        turnBasedScreen.SetActive(turnBased);
        if (turnBased)
        {
            FreeMoveActive = false;
            turnBasedLogicScript.currentEnemy = enemyTypesSO;
            turnBasedLogicScript.currentState = TurnBasedLogic.BattleStates.Start;
            return;
        }
    }

    public void UpdateReferencesToScreens()
    {
        turnBasedScreen = GameObject.FindGameObjectWithTag("TurnBased");
        freeMoveScreen = GameObject.FindGameObjectWithTag("FreeMove");
        turnBasedLogicScript = GameObject.FindGameObjectWithTag("TurnBasedLogic").GetComponent<TurnBasedLogic>();
    }

    public void ShowDeathScreen()
    {
        freeMoveScreen.SetActive(false);
        turnBasedScreen.SetActive(false);
        restSiteScreen.SetActive(true);
        restSiteWindow.SetActive(false);
        DeathScreen.SetActive(true);
    }
    
    public void ShowVictoryScreen()
    {
        freeMoveScreen.SetActive(false);
        turnBasedScreen.SetActive(false);
        restSiteScreen.SetActive(true);
        restSiteWindow.SetActive(false);
        VictoryScreen.SetActive(true);
    }

}
