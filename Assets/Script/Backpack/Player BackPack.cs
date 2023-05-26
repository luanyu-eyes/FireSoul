using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();  // 玩家的物品列表
    public int maxInventorySize = 20;  // 背包最大容量

    public void AddItem(Item itemToAdd)
    {
        // 检查背包是否已满
        if (inventory.Count >= maxInventorySize)
        {
            Debug.Log("背包已满，无法添加物品！");
            return;
        }

        // 在背包中添加新物品
        inventory.Add(itemToAdd);
        Debug.Log("已添加物品：" + itemToAdd.name);
    }

    public void RemoveItem(Item itemToRemove)
    {
        // 检查背包中是否有指定物品
        if (!inventory.Contains(itemToRemove))
        {
            Debug.Log("背包中没有该物品！");
            return;
        }

        // 从背包中移除指定物品
        inventory.Remove(itemToRemove);
        Debug.Log("已移除物品：" + itemToRemove.name);
    }

    // 物品类，包含物品的名称和描述
    public class Item
    {
        public string name;
        public string description;

        public Item(string itemName, string itemDescription)
        {
            name = itemName;
            description = itemDescription;
        }
    }
}
