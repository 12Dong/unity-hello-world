using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{

    public ItemData itemData;
    public int number;

    public InventoryItem(ItemData itemData, int number)
    {
        this.itemData = itemData;
        this.number = number;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
