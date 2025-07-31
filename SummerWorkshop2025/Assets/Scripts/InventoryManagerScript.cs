using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManagerScript : MonoBehaviour
{
    public static InventoryManagerScript instance;

    [SerializeField]
    private List<ShellTypeSO> shellTypes = new List<ShellTypeSO>();

    public Dictionary<string, ShellTypeSO> inventory;

    public GameObject ShellButton;
    public ShellTypeSO currentShellType;

    [SerializeField]
    private GameObject ShellButtonToSpawn;

    [SerializeField]
    private GameObject ButtonContainer;

    [SerializeField]
    private Text EquippedText;


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = new Dictionary<string, ShellTypeSO>();
        PopulateShellInventory();
        ShellSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PrintShellInventory();
            
        }
    }

    public void PopulateShellInventory()
    {
        foreach (ShellTypeSO shellType in shellTypes)
        {
            inventory.Add(shellType.shellName,shellType);
            // error - different varaible types?
            // inventory.Add(shellType.shellNumber,shellType);
            // inventory.Add(shellType.shellImage, shellType);

        }
    }

    public void PrintShellInventory()
    {
        foreach(KeyValuePair<string,ShellTypeSO> pairs in inventory)
        {
            Debug.Log(pairs.Key);
            Debug.Log(inventory["Hard shell"]);
        }
    }

    public void ShellSpawner()
    {
        foreach(ShellTypeSO shellType in shellTypes)
        {
            ShellButton = (GameObject)Instantiate(ShellButtonToSpawn);
            ShellButton.transform.SetParent(ButtonContainer.transform);
            Text buttonText = ShellButton.GetComponentInChildren<Text>();
            buttonText.text = shellType.shellName;
            Button button = ShellButton.GetComponent<Button>();
            button.onClick.AddListener(() => ButtonOnClick(shellType.shellName));
        }
    }

    public void ButtonOnClick(string shellName)
    {
        currentShellType = inventory[shellName];
        Debug.Log(currentShellType);
        EquippedText.text = currentShellType.shellName + " equipped!";
    }


    public void test()
    {
        Dictionary<string, int> ItemInventory = new Dictionary<string, int>()
        {
            {"poitions", 10 },
            {"test", 20},
            {"hello", 40}
        };
        Debug.Log(ItemInventory["test"]);
        Debug.Log(ItemInventory["hello"]);
        Debug.Log(ItemInventory["poitions"]);
        ItemInventory["hello"] = 10;
        Debug.Log(ItemInventory["hello"]);
        Debug.Log(ItemInventory["test"] + 10);
    }

}


/*   
 *   public void MoveToInventoryScene ()
    {
        SceneManager.LoadSceneAsync("InventoryScene");
    }
    public void MoveToRestScene ()
    {
        SceneManager.LoadSceneAsync("RestScene");
    }

    public void Inventory1 ()
    {
        Debug.Log("Slot 1 Selected");
        InventorySelectionStatus.text = "Slot 1 selected";
    }
    public void Inventory2()
    {
        Debug.Log("Slot 2 Selected");
        InventorySelectionStatus.text = "Slot 2 selected";
    }
    public void Inventory3()
    {
        Debug.Log("Slot 3 Selected");
        InventorySelectionStatus.text = "Slot 3 selected";
    }
    public void Inventory4()
    {
        Debug.Log("Slot 4 Selected");
        InventorySelectionStatus.text = "Slot 4 selected";
    }
    public void Inventory5()
    {
        Debug.Log("Slot 5 Selected");
        InventorySelectionStatus.text = "Slot 5 selected";
    }
    public void Inventory6()
    {
        Debug.Log("Slot 6 Selected");
        InventorySelectionStatus.text = "Slot 6 selected";
    }
    public void Inventory7()
    {
        Debug.Log("Slot 7 Selected");
        InventorySelectionStatus.text = "Slot 7 selected";
    }
    public void Inventory8()
    {
        Debug.Log("Slot 8 Selected");
        InventorySelectionStatus.text = "Slot 8 selected";
    }
*/