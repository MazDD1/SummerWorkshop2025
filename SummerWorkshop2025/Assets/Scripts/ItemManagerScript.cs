using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class ItemManagerScript : MonoBehaviour
{
    public static ItemManagerScript instance;

    [SerializeField]
    private List<ItemTypeSO> itemTypes = new List<ItemTypeSO>();

    public Dictionary<ItemTypeSO, int> ItemInventory;

    public GameObject ItemButton;
    public ItemTypeSO currentItemType;

    [SerializeField]
    private GameObject ItemButtonToSpawn;

    [SerializeField]
    private GameObject ButtonContainer;

    [SerializeField]
    private Text EquippedText;

    [SerializeField]
    private GameObject inventoryScreen;

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
    void Start()
    {
        ItemInventory = new Dictionary<ItemTypeSO, int>();
        PopulateItemInventory();
        ItemSpawner();
        //inventoryScreen.SetActive(false);

    }

    public void PopulateItemInventory()
    {
        foreach (ItemTypeSO itemType in instance.itemTypes)
        {
            if(ItemInventory.ContainsKey(itemType))
            {
                ItemInventory[itemType] += 1;
                continue;
            }
            ItemInventory.Add(itemType, 1);
            

        }
    }

    public void PrintItemInventory()
    {
        foreach (KeyValuePair<ItemTypeSO, int> pairs in ItemInventory)
        {
            Debug.Log(pairs.Key);
            
        }
    }

    public void ItemSpawner()
    {
        foreach (ItemTypeSO itemType in instance.itemTypes)
        {
            ItemButton = Instantiate<GameObject>(ItemButtonToSpawn, ButtonContainer.transform);
            ItemButton.transform.SetParent(ButtonContainer.transform);
            ItemButtonScript IBS = ItemButton.GetComponent<ItemButtonScript>();
            IBS.titleText.text = itemType.itemName;
            IBS.buttonImage.sprite = itemType.itemImage;
            IBS.quantityText.text = ItemInventory[itemType].ToString();
            Button button = ItemButton.GetComponent<Button>();
            button.onClick.AddListener(() => ButtonOnClick(itemType));
        }
    }

    public void AddToItemInventory(ItemTypeSO item)
    {
        if (ItemInventory.ContainsKey(item))
        {
            ItemInventory[item] += 1;
            return;
        }
        ItemInventory.Add(item, 1);
    }

    public void ButtonOnClick(ItemTypeSO item)
    {
        currentItemType = item;
        Debug.Log(currentItemType);
        EquippedText.text = currentItemType.itemName + " equipped!";

        if (currentItemType.itemName == "PlaceholderValue1")
        {
            Debug.Log("hello");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
