using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;

    private List<GameObject> inventory = new List<GameObject>(); // change to array if inventory should have a max 


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddInventory(GameObject item)
    {
        inventory.Add(item);
    }

}
