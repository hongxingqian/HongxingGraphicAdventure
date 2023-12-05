using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using TMPro;

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 0)]
public class Item : ScriptableObject
{
    public string m_sName;
    public Character m_cBelonger = null;
    public int m_fAttak;
    public int m_fDefense;
    public int m_fHP;

    public string LootName
    {
        get { return m_sName; }
    }

    public Character Belonger
    {
        get { return m_cBelonger; }
    }
    public int Attack
    {
        get { return m_fAttak; }
    }
    public int Defense
    {
        get { return m_fDefense; }
    }
    public int HP
    {
        get { return m_fHP; }
    }

}
