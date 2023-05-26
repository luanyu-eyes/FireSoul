using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public List<Item> allItems = new List<Item>();     // 所有物品的列表

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // 添加一个新物品
    public void AddItem(Item item)
    {
        allItems.Add(item);
    }

    // 根据名称查找物品
    public Item GetItemByName(string itemName)
    {
        foreach (Item item in allItems)
        {
            if (item.itemName == itemName)
            {
                return item;
            }
        }
        return null;
    }

    // 修改物品的属性
    public void ModifyItem(Item item, int price, Sprite image)
    {
        item.price = price;
        item.image = image;
    }
}