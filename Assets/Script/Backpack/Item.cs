using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string itemName;  // ��Ʒ����
    public int itemID;       // ��ƷID
    public int price;        // ��Ʒ�۸�
    public Sprite image;     // ��Ʒͼ��
    public virtual void Use()
    {
        // ����ʵ�־����ʹ���߼�
    }
}