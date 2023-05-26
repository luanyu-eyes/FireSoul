using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName;  // 物品名称
    public int itemID;       // 物品ID
    public int price;        // 物品价格
    public Sprite image;     // 物品图像
    public virtual void Use()
    {
        // 子类实现具体的使用逻辑
    }
}