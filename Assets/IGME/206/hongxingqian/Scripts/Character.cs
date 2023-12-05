using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Character : MonoBehaviour
{
    public Inventory m_iInventory;
    public Item m_iDropableItem;
    public Item m_iKey;
    // check th relationship of the character, 1 = enemy, 0 = friendly.
    public int m_iRelationship = 0;
    // check if the character died or still alive.
    public string m_sName;
    public string m_sDescription;
    public string m_sConversation;
    public int m_fHP;
    public int m_fMaxHP;
    public int m_fDefense;
    public int m_fAttack;
    public int m_nLevel;
    public int m_fHeal;
    public System.Random random = new System.Random();



    public virtual bool IsBoss()
    {
        return false;
    }
    public Item DropKey()
    {
        return m_iKey;
    }

    public int Level
    {
        get { return m_nLevel; }
    }
    public int HP
    {
        get { return m_fHP; }
    }
    public int MaxHP
    {
        get { return m_fMaxHP; }
    }
    public int Defense
    {
        get { return m_fDefense; }
    }
    public int Attack
    {
        get { return m_fAttack; }
    }
    public int Heal
    {
        get { return m_fHeal; }
    }
    public string Name
    {
        get { return m_sName; }
    }
    public int Relationship
    {
        get { return m_iRelationship; }
    }

    public string Description
    {
        get { return m_sDescription; }
    }
    public string Conversation
    {
        get { return m_sConversation; }
    }

    public Inventory Inventory
    {
        get { return m_iInventory; }
    }

    // Character cause damage -> Enemy
    public int Fight(Character a_character)
    {
        int damage = a_character.ReceiveDamge(this);
        return damage;
    }

    // Character <- receive damage from Enemy
    private int ReceiveDamge(Character a_character)
    {
        int damage = a_character.DamageCaused(this.m_fDefense);
        Debug.Log(damage);
        if (damage > 0)
        {
            Debug.Log(m_fHP);
            m_fHP -= damage;

        }
        return damage;
    }
    // Character cause damage -> Enemy
    public int DamageCaused(int characterDefense)
    {
        //Debug.Log(m_fAttack);
        return UnityEngine.Random.Range(m_fAttack-5, m_fAttack+5) - characterDefense;
    }

    public int Healing()
    {
        if ((m_fHP + this.Heal) <= m_fMaxHP)
        {
           int heal = m_fHP += this.Heal;
            return this.Heal;
        }
        else
        {
            int heal = m_fMaxHP - m_fHP;
            m_fHP = m_fMaxHP;
            return heal;
        }
    }
    //public int HealAmount()
    //{
    //    if ((m_fHP + this.Heal) <= m_fMaxHP)
    //    {
    //        return this.Heal;
    //    }
    //    else
    //    {
    //        return m_fMaxHP - m_fHP;
    //    }
    //}
    public void Resting()
    {
        if (m_fHP + 20 <= m_fMaxHP)
        {
            m_fHP += 20;
        }
        else
        {
            m_fHP = m_fMaxHP;
        }
    }


    public virtual void Levelup()
    {
        m_fMaxHP += 5;
        m_nLevel += 1;
        m_fAttack += 3;
        m_fDefense += 2;
        m_fHeal += 3;
    }

    public Item DroppableItem
    {
        get { return m_iDropableItem; }
    }

    //public void DeleteItem()
    //{
    //    m_iDropableItem = null;
    //}

    //public void DeleteKey()
    //{
    //    m_iKey = null;
    //}

    public Item DropLoot()
    {
        // If the character has a droppable item, then drop it
        if (m_iDropableItem != null)
        {
            int var = UnityEngine.Random.Range(0, 2);
            if (var == 0) { return m_iDropableItem; }
            else
            {
                return null;
            }
        }

        else
        {
            return null;
        }
    }


    //public void GainLoot(Item loot)
    //{
    //    PutInInventory(loot);
    //    AddItemStatusToCharacter(loot);
    //}

    //public void PutInInventory(Item loot)
    //{
    //    if (loot != null)
    //    {
    //        bool itemAdded = false;
    //        for (int i = 0; i < m_iInventory.Items.Count; i++)
    //        {
    //            if (m_iInventory.Items[i] == null)
    //            {
    //                m_iInventory.Items[i] = loot;
    //                Console.WriteLine("You gained " + loot.LootName);
    //                itemAdded = true;
    //                break;
    //            }
    //        }
    //        if (!itemAdded)
    //        {
    //            Console.WriteLine("\n-----Your Inventory is full!");
    //        }
    //    }
    //    else
    //    {
    //        Console.WriteLine("The Item you gain is null!");
    //    }
    //}


    public void AddItemStatusToCharacter(Item loot)
    {
        {
            m_fMaxHP += loot.HP;
            m_fAttack += loot.Attack;
            m_fDefense += loot.Defense;
        }
    }

    private bool IsItemInInventory(Item loot)
    {
        for (int i = 0; i < m_iInventory.Items.Count; i++)
        {
            if (m_iInventory.Items[i] == loot)
            {
                return true;
            }
        }
        return false;
    }

    //public void DropFromInventory(Item loot)
    //{
    //    if (RemoveFromInventory(loot))
    //    {
    //        SubtractItemStatusFromCharacter(loot);
    //    }
    //}

    //public bool RemoveFromInventory(Item loot)
    //{
    //    bool itemRemoved = false;
    //    for (int i = 0; i < m_iInventory.Items.Count; i++)
    //    {
    //        if (m_iInventory.Items[i] == loot)
    //        {
    //            Console.WriteLine("You dropped " + loot.LootName);
    //            m_iInventory.Items[i] = null;
    //            itemRemoved = true;
    //            break;
    //        }
    //    }
    //    if (!itemRemoved)
    //    {
    //        Console.WriteLine("Item not found in inventory!");
    //    }
    //    return itemRemoved;
    //}

    public void SubtractItemStatusFromCharacter(Item loot)
    {
        if (IsItemInInventory(loot))
        {
            m_fMaxHP -= loot.HP;
            m_fAttack -= loot.Attack;
            m_fDefense -= loot.Defense;
        }
        else
        {
            Console.WriteLine("The item " + loot.LootName + " is not in the inventory. Stats not updated.");
        }
    }
    //public void CreateLootForEnemy(string name, int atk, int def, int hp)
    //{
    //    m_iDropableItem = new Item(name, null, atk, def, hp);
    //}

    public void PrintPlayerStatus()
    {
        Debug.Log("PrintPlayerStatus");
        UI.Instance.UpdatePlayerStatus();
    }

    public void PrintEnemyStatus()
    {
        Debug.Log("PrintEnemyStatus");
        UI.Instance.UpdateEnemyStatus();
    }

    public void SaveStatus()
    {
        PlayerData.m_fAttack = this.m_fAttack;
        PlayerData.m_fDefense = this.m_fDefense;
        PlayerData.m_fHP = this.m_fHP;
        PlayerData.m_fMaxHP = this.m_fMaxHP;
        PlayerData.m_fHeal = this.m_fHeal;
        PlayerData.m_nLevel = this.m_nLevel;

    }
}
