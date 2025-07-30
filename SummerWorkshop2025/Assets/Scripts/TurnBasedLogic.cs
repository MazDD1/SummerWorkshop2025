using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
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
    private int actionCounter;
    [SerializeField]
    private int actionFrames, cooldownFrames;

    [SerializeField]
    private GameObject enemy;
    private HealthStatsScript enemyStats;
    private AttackMachineScript enemyAttacks;

    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private Button buttonPrefab;
    [SerializeField]
    private Transform abilityMenu;
    [SerializeField]
    private Transform inventoryMenu;
    [SerializeField]
    private Slider playerHealthUI;
    [SerializeField]
    private Slider enemyHealthUI;

    [SerializeField]
    private Animator actionAnimation;

    public enum AttackFields
    {
        Inventory,
        Ability
    }

    public enum BattleStates
    {
        Start,
        TurnStart,
        TurnWaiting,
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
        AttackMachineScript attackMachine = null;
        switch (attackField)
        {
            case AttackFields.Ability:
                parentObject = abilityMenu;
                attackMachine = abilityAttacks;
                break;

            case AttackFields.Inventory:
                parentObject = inventoryMenu;
                attackMachine = inventoryAttacks;
                break;

        }
        for (int i = 0; i < attackMachine.attackStates.Length; i++)
        {
            if (!attackMachine.attackStates[i])
            {
                continue;
            }
            buttonPrefab.GetComponentInChildren<TextMeshProUGUI>().text = attackMachine.attackStates[i].attackName;
            int buttonIndex = i;
            GameObject buttonInstance = Instantiate(buttonPrefab.gameObject, parentObject);
            print(attackMachine.attackStates[i]);
            Debug.Log(i);
            buttonInstance.GetComponent<Button>().onClick.AddListener(() => Button_Pressed(buttonIndex));

        }
    }

    public void Attack_Button_Pressed()
    {
        playerAttacks.currentState = playerAttacks.attackStates[0];
        currentState = BattleStates.PlayerAttackStart;
    }

    public void Defend_Button_Pressed()
    {
        playerAttacks.currentState = playerAttacks.attackStates[1];
        currentState = BattleStates.PlayerAttackStart;
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

    void Handle_Input()
    {
        if (currentState == BattleStates.TurnWaiting)
        {
            return;
        }
        if (!Input.GetButtonDown("Fire1"))
        {
            return;
        }
        if (actionCounter != -cooldownFrames)
        {
            return;
        }
        actionCounter = actionFrames;
        actionAnimation.Play("ActionAnimation");
        actionAnimation.speed = (60f/actionFrames);

    }

    private void FixedUpdate()
    {
        if (actionCounter != -cooldownFrames)
        {
            actionCounter--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Handle_Input();
        switch (currentState)
        {
            case BattleStates.Start:
                Start_Turn_Based_Combat();
                currentState = BattleStates.TurnStart;
                break;

            case BattleStates.TurnStart:
                Start_Turn();
                currentState = BattleStates.TurnWaiting;
                break;

            case BattleStates.PlayerAttackStart:
                Play_Player_Attack();
                currentState = BattleStates.Waiting;
                break;
            case BattleStates.EnemyAttackStart:
                Play_Enemy_Attack();
                currentState = BattleStates.Waiting;
                break;


        }
    }
    void Start_Turn_Based_Combat()
    {
        Assign_Buttons(AttackFields.Ability);
        Assign_Buttons(AttackFields.Inventory);
        playerStats.health = playerStats.baseStats.maxHealth;
        enemyStats.health = enemyStats.baseStats.maxHealth;
        playerHealthUI.maxValue = playerStats.baseStats.maxHealth;
        enemyHealthUI.maxValue = enemyStats.baseStats.maxHealth;
        playerHealthUI.value = playerStats.baseStats.maxHealth;
        enemyHealthUI.value = enemyStats.baseStats.maxHealth;
    }

    void Start_Turn()
    {
        Animation spriteAnim = player.GetComponent<Animation>();
        if (playerAttacks.currentState != null)
        {
            if (playerStats.isImmune)
            {
                spriteAnim.Play("DefendEndAnimation");
                playerStats.isImmune = false;
            }
        }
        Animation enemyAnimation = enemy.GetComponent<Animation>();
        if (enemyAnimation.GetClip("CurrentAttack"))
        {
            enemyAnimation.RemoveClip("CurrentAttack");
        }
        menu.SetActive(true);
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
        inventoryMenu.gameObject.SetActive(false);
        abilityMenu.gameObject.SetActive(false);
        menu.SetActive(false);
        Animation spriteAnim = player.GetComponent<Animation>();
        if (playerAttacks.currentState.statChange == AttackStatsScriptableObject.StatChange.NullifyDamage)
        {
            spriteAnim.Play("DefendStartAnimation");
            playerStats.isImmune = true;
            return;
        }
        spriteAnim.AddClip(playerAttacks.currentState.attackAnimationName, "CurrentAttack");
        spriteAnim.Play("CurrentAttack");
    }

    public void Player_Attack()
    {
        if (playerAttacks.currentState.attackTarget == AttackStatsScriptableObject.AttackTarget.Player)
        {
            playerStats.health -= playerAttacks.currentState.damage;
            playerHealthUI.value = playerStats.health;
            return;
        }
        if (actionCounter >= 0)
        {
            actionCounter = -cooldownFrames;
            enemyStats.health -= playerAttacks.currentState.damage*1.6f;
        }
        else
        {
            enemyStats.health -= playerAttacks.currentState.damage;
        }
        enemyHealthUI.value = enemyStats.health;
    }

    public void Play_Enemy_Attack()
    {
        Animation playerAnimation = player.GetComponent<Animation>();
        if (playerAnimation.GetClip("CurrentAttack"))
        {
            playerAnimation.RemoveClip("CurrentAttack");
        }
        Animation spriteAnim = enemy.GetComponent<Animation>();
        spriteAnim.AddClip(enemyAttacks.currentState.attackAnimationName, "CurrentAttack");
        spriteAnim.Play("CurrentAttack");
    }

    public void Enemy_Attack()
    {
        if (enemyAttacks.currentState.attackTarget == AttackStatsScriptableObject.AttackTarget.Player)
        {
            float damage = enemyAttacks.currentState.damage;
            if (playerStats.isImmune)
            {
                return;
            }
            if (actionCounter >= 0)
            {
                actionCounter = -cooldownFrames;
                damage *= 0.5f;

            }
            playerStats.health -= damage;
            playerHealthUI.value = playerStats.health;
            return;
        }
        enemyStats.health -= enemyAttacks.currentState.damage;
        enemyHealthUI.value = enemyStats.health;

    }

    public void Attack_End(GameObject entity)
    {
        print("attack ended");
        Animation entityAnimation = player.GetComponent<Animation>();
        entityAnimation.Stop();
        if (entity == player)
        {
            print("is a player attack");
            currentState = BattleStates.EnemyAttackStart;
        }
        else
        {
            print("is an enemy attack");
            currentState = BattleStates.TurnStart;
        }
    }

    

}

