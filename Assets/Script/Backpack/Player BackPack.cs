using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();  // ��ҵ���Ʒ�б�
    public int maxInventorySize = 20;  // �����������

    public void AddItem(Item itemToAdd)
    {
        // ��鱳���Ƿ�����
        if (inventory.Count >= maxInventorySize)
        {
            Debug.Log("�����������޷������Ʒ��");
            return;
        }

        // �ڱ������������Ʒ
        inventory.Add(itemToAdd);
        Debug.Log("�������Ʒ��" + itemToAdd.name);
    }

    public void RemoveItem(Item itemToRemove)
    {
        // ��鱳�����Ƿ���ָ����Ʒ
        if (!inventory.Contains(itemToRemove))
        {
            Debug.Log("������û�и���Ʒ��");
            return;
        }

        // �ӱ������Ƴ�ָ����Ʒ
        inventory.Remove(itemToRemove);
        Debug.Log("���Ƴ���Ʒ��" + itemToRemove.name);
    }

    // ��Ʒ�࣬������Ʒ�����ƺ�����
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
