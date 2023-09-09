using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flower : MonoBehaviour
{
    public ItemData itemData;
    public int number;

    public UnityEvent<ItemData, int> pickUpFunction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bePickedUp()
    {
        // 资源销毁
        Destroy(gameObject);
        // 玩家拾起
        pickUpFunction?.Invoke(itemData, number);
    }
}
