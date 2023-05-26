using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public List<Item> allItems = new List<Item>();     // ������Ʒ���б�

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

    // ���һ������Ʒ
    public void AddItem(Item item)
    {
        allItems.Add(item);
    }

    // �������Ʋ�����Ʒ
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

    // �޸���Ʒ������
    public void ModifyItem(Item item, int price, Sprite image)
    {
        item.price = price;
        item.image = image;
    }
}