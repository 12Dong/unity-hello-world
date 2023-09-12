using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    public Image icon;
    public TextMeshProUGUI displayCount;
    public InventoryItem inventoryItem;
    void Start()
    {
        icon.enabled = false;
        displayCount.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearSlot()
    {
        icon.enabled = false;
        displayCount.enabled = false;
        inventoryItem = null;
    }

    public void DrawData(InventoryItem item)
    {

        if(item is null)
        {
            ClearSlot();
            return;
        }

        icon.enabled = true;
        displayCount.enabled = true;

        inventoryItem = item;
        icon.sprite = item.itemData.icon;
        
        displayCount.text = item.number.ToString();
    }
}
