using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite sprite;

    // must be able to talk to inventory manager
    private InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         inventoryManager.AddItem(itemName, quantity, sprite);
    //         Destroy(gameObject);
    //     }
    // }
    public void AddItemToInventory()
    {
        inventoryManager.AddItem(itemName, quantity, sprite);
    }

    
}
