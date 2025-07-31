using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Type", menuName = "ScriptableObjects/ItemType")]
public class ItemTypeSO : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
}
