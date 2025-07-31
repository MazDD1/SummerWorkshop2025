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
        foreach (ShellTypeSO shellType in instance.shellTypes)
        {
            instance.inventory.Add(shellType.shellName,shellType);
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
        foreach(ShellTypeSO shellType in instance.shellTypes)
        {
            ShellButton = Instantiate<GameObject>(ShellButtonToSpawn, ButtonContainer.transform);
            ShellButton.transform.SetParent(ButtonContainer.transform);
            ShellButtonScript SBS = ShellButton.GetComponent<ShellButtonScript>();
            SBS.buttonImage.sprite = shellType.shellImage;
            SBS.buttonText.text = shellType.shellName;
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