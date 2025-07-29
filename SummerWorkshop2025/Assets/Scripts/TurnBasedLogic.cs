using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TurnBasedLogic;

public class TurnBasedLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private HealthStatsScript playerStats;
    private AttackMachineScript playerAttacks;
    [SerializeField]
    private AttackMachineScript inventoryAttacks, abilityAttacks;

    [SerializeField]
    private GameObject enemy;
    private HealthStatsScript enemyStats;
    private AttackMachineScript enemyAttacks;

    [SerializeField]
    private GameObject buttons;
    [SerializeField]
    private Button buttonPrefab;
    [SerializeField]
    private Transform abilityMenu;
    [SerializeField]
    private Transform inventoryMenu;

    public enum AttackFields
    {
        Inventory,
        Ability
    }

    public enum BattleStates
    {
        Start,
        TurnStart,
        Waiting,
        PlayerAttackStart,
        EnemyAttackStart,
        End
    }

    public BattleStates currentState;

    private bool isTurnPlayer = false;
    public void Assign_Buttons(AttackFields attackField)
    {
        Transform parentObject = null;
        switch (attackField)
        {
            case AttackFields.Ability:
                parentObject = abilityMenu;
                break;

            case AttackFields.Inventory:
                parentObject = inventoryMenu;
                break;

        }
        for (int i = 0; i < abilityAttacks.attackStates.Length; i++)
        {
            if (!abilityAttacks.attackStates[i])
            {
                continue;
            }
            buttonPrefab.GetComponentInChildren<TextMeshProUGUI>().text = abilityAttacks.attackStates[i].attackName;
            int buttonIndex = i;
            GameObject buttonInstance = Instantiate(buttonPrefab.gameObject, parentObject);
            print(abilityAttacks.attackStates[i]);
            Debug.Log(i);
            buttonInstance.GetComponent<Button>().onClick.AddListener(() => Button_Pressed(buttonIndex));

        }
    }


    public void Button_Pressed(int attackIndex)
    {
        print("CLICKED");
        print(attackIndex);
        playerAttacks.currentState = abilityAttacks.attackStates[attackIndex];
        currentState = BattleStates.PlayerAttackStart;

    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats = player.GetComponent<HealthStatsScript>();
        playerAttacks = player.GetComponent<AttackMachineScript>();
        enemyStats = enemy.GetComponent<HealthStatsScript>();
        enemyAttacks = enemy.GetComponent<AttackMachineScript>();

        currentState = BattleStates.Start;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case BattleStates.Start:
                Start_Turn_Based_Combat();
                currentState = BattleStates.TurnStart;
                break;

            case BattleStates.TurnStart:
                Start_Turn();
                currentState = BattleStates.Waiting;
                break;

            case BattleStates.PlayerAttackStart:
                Play_Player_Attack();
                currentState = BattleStates.Waiting;
                break;

        }
    }
    void Start_Turn_Based_Combat()
    {
        Assign_Buttons(AttackFields.Ability);
        Assign_Buttons(AttackFields.Inventory);

    }

    void Start_Turn()
    {
        buttons.SetActive(true);
        isTurnPlayer = true;
        enemyAttacks.currentState = Pick_Attack_From_Weight();
    }

    AttackStatsScriptableObject Pick_Attack_From_Weight()
    {
        int maxPriority = 0;
        int[] attackPriority = new int[enemyAttacks.attackStates.Length];
        for (int i = 0; i < enemyAttacks.attackStates.Length; i++)
        {
            print(enemyAttacks.attackStates[i]);
            if(enemyAttacks.attackStates[i] == null)
            {
                attackPriority[i] = 0;
                print("null");
                continue;
            }
            maxPriority += enemyAttacks.attackStates[i].attackPriority;
            attackPriority[i] = maxPriority;
        }

        int randomAttack = UnityEngine.Random.Range(1, maxPriority);
        for (int i = 0; i < attackPriority.Length; i++)
        {
            if (attackPriority[i] > randomAttack)
            {
                print(attackPriority[i]);
                print(randomAttack);
                print(attackPriority[i] > randomAttack);
                continue;
            }
            print(enemyAttacks.attackStates[i]);
            return enemyAttacks.attackStates[i];
        }
        print("null");
        return null;
    }

    void Play_Player_Attack()
    {
        Animation spriteAnim = player.GetComponent<Animation>();
        spriteAnim.AddClip(playerAttacks.currentState.attackAnimationName, "CurrentAttack");
        spriteAnim.Play("CurrentAttack");
    }

}

