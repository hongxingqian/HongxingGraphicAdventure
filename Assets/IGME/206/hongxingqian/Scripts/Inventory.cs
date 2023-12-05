using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class Inventory
{
    public List<Item> items = new List<Item>();
    public List<Item> Items {  get { return items; } }
    public void AddItem(Item item)
    {
        items.Add(item);
    }
    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }
    public void ListInventory(TMP_Text console)
    {
        string output = "Your Inventory is empty.";
        if(items.Count > 0)
        {
            output = "Your inventory contains:\n";
            foreach (var item in items)
            {
                output = output + item + "\n";
            }
        }
        console.text = output;
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
}
