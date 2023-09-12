using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelManager : MonoBehaviour
{
    private List<InventorySlot> inventorySlots = new List<InventorySlot>(10);

    public 
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount && i < inventorySlots.Capacity; i++) 
        {

            inventorySlots.Add(transform.GetChild(i).GetComponent<InventorySlot>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void add(ItemData itemData, int number)
    {
        InventoryItem inventoryItem = new InventoryItem(itemData, number);
        Boolean sign = false;
        for(int i = 0; i < inventorySlots.Capacity; i++)
        {
            Debug.Log($"{i} && {inventorySlots[i].inventoryItem}");
            // 物品已经存在 就用Slots中已有的 和 添加的 做叠加 并写入到对应Slot中
            if(inventorySlots[i].inventoryItem is not null && inventorySlots[i].inventoryItem.itemData.id.Equals(inventoryItem.itemData.id))
            {
                sign = true;
                InventoryItem sumInventroyItem = new InventoryItem(inventoryItem.itemData, inventoryItem.number + inventorySlots[i].inventoryItem.number);
                inventorySlots[i].DrawData(sumInventroyItem);
                return;
            }
        }
        // 物品不存在 找到第一个空位置 进行写数据
        if(!sign)
        {
            for(int i = 0; i < inventorySlots.Capacity; i++)
            {
                if(inventorySlots[i].inventoryItem is null)
                {
                    inventorySlots[i].DrawData(inventoryItem);
                    return;
                }
            }
        }
    }
}
