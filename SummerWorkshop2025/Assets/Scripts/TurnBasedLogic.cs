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
    private GameObject playerHealthFill;
    [SerializeField] 
    private TextMeshProUGUI playerDefenseUI;
    [SerializeField]
    private Slider enemyHealthUI;
    [SerializeField] 
    private GameObject enemyHealthFill;
    [SerializeField]
    private TextMeshProUGUI enemyDefenseUI;

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
        Animate_Healthbar();
        if (actionCounter != -cooldownFrames)
        {
            actionCounter--;
        }
    }

    private void Animate_Healthbar()
    {
        enemyDefenseUI.text = "Fragility: " + (enemyStats.defense).ToString();
        playerDefenseUI.text = "Fragility: " + (playerStats.defense).ToString();

        enemyHealthUI.value = Mathf.Lerp(enemyHealthUI.value, enemyStats.health, 0.1f);
        playerHealthUI.value = Mathf.Lerp(playerHealthUI.value, playerStats.health, 0.1f);
        if (playerHealthUI.value <= 0)
        {
            playerHealthFill.SetActive(false);

        }
        else
        {
            playerHealthFill.SetActive(true);
        }
        if (enemyHealthUI.value <= 0)
        {
            enemyHealthFill.SetActive(false);
        }
        else
        {
            enemyHealthFill.SetActive(true);
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
                currentState = BattleStates.Waiting;
                Play_Player_Attack();
                break;
            case BattleStates.EnemyAttackStart:
                currentState = BattleStates.Waiting;
                Play_Enemy_Attack();
                break;
            case BattleStates.End:
                print("ENDING BATTLE");
                GameManagerScript.instance.SwitchToTurnBased(false);
                currentState = BattleStates.Waiting;
                break;


        }
    }
    void Start_Turn_Based_Combat()
    {
        Assign_Buttons(AttackFields.Ability);
        Assign_Buttons(AttackFields.Inventory);
        playerStats.defense = playerStats.baseStats.defenseMultiplier;
        playerStats.health = playerStats.baseStats.maxHealth;
        enemyStats.defense = enemyStats.baseStats.defenseMultiplier;
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
        print("Max Priority: " + maxPriority.ToString());
        int randomAttack = UnityEngine.Random.Range(0, maxPriority);
        print("Random attack: " + randomAttack.ToString());
        for (int i = 0; i < attackPriority.Length; i++)
        {
            if (attackPriority[i] <= randomAttack)
            {
                print("Not selected attack: " + attackPriority[i].ToString());
                print(attackPriority[i]);
                print(randomAttack);
                print(attackPriority[i] > randomAttack);
                continue;
            }
            int finalIndex = i;
            print("ATTACK SELECTED: " + (attackPriority[i] > randomAttack).ToString());
            print("Selected attack: " + attackPriority[finalIndex].ToString());
            return enemyAttacks.attackStates[finalIndex];
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

    void Player_Damage_Defense()
    {
        if (playerAttacks.currentState.attackTarget == AttackStatsScriptableObject.AttackTarget.Player)
        {
            playerStats.defense -= playerAttacks.currentState.damage;
            return;
        }
        enemyStats.defense -= playerAttacks.currentState.damage;


    }

    public void Player_Attack()
    {
        if (playerAttacks.currentState.statChange == AttackStatsScriptableObject.StatChange.Defense)
        {
            Player_Damage_Defense();
            return;
        }
        if (playerAttacks.currentState.attackTarget == AttackStatsScriptableObject.AttackTarget.Player)
        {
            playerStats.health -= playerAttacks.currentState.damage;
            return;
        }
        if (actionCounter >= 0)
        {
            actionCounter = -cooldownFrames;
            enemyStats.health -= (playerAttacks.currentState.damage*1.6f)* enemyStats.defense;
        }
        else
        {
            enemyStats.health -= playerAttacks.currentState.damage * enemyStats.defense;
        }
    }

    public void Play_Enemy_Attack()
    {
        if (enemyStats.health <= 0)
        {
            print("ENDING BATTLE");
            currentState = BattleStates.End;
            return;
        }
        Animation playerAnimation = player.GetComponent<Animation>();
        if (playerAnimation.GetClip("CurrentAttack"))
        {
            playerAnimation.RemoveClip("CurrentAttack");
        }
        Animation spriteAnim = enemy.GetComponent<Animation>();
        spriteAnim.AddClip(enemyAttacks.currentState.attackAnimationName, "CurrentAttack");
        spriteAnim.Play("CurrentAttack");
    }

    void Enemy_Damage_Defense()
    {
        if (enemyAttacks.currentState.attackTarget == AttackStatsScriptableObject.AttackTarget.Enemy)
        {
            enemyStats.defense -= enemyAttacks.currentState.damage;
            return;
        }
        playerStats.defense -= enemyAttacks.currentState.damage;


    }

    public void Enemy_Attack()
    {
        if (enemyAttacks.currentState.statChange == AttackStatsScriptableObject.StatChange.Defense)
        {
            Enemy_Damage_Defense();
            return;
        }
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
            return;
        }
        enemyStats.health -= enemyAttacks.currentState.damage * playerStats.defense;

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

