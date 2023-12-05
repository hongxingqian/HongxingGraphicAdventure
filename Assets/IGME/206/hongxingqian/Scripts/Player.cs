using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class Player : Character
{

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        this.m_iInventory = PlayerData.inventory;
        this.m_fHP = PlayerData.m_fHP;
        this.m_fMaxHP = PlayerData.m_fMaxHP;
        this.m_fAttack = PlayerData.m_fAttack;
        this.m_fDefense = PlayerData.m_fDefense;
        this.m_nLevel = PlayerData.m_nLevel;
        this.m_fHeal = PlayerData.m_fHeal;
    }
    void Start()
    {
        //this.m_iInventory = PlayerData.inventory;
        //this.m_fHP = PlayerData.m_fHP;
        //this.m_fMaxHP = PlayerData.m_fMaxHP;
        //this.m_fAttack = PlayerData.m_fAttack;
        //this.m_fDefense = PlayerData.m_fDefense;
        //this.m_nLevel = PlayerData.m_nLevel;
        //this.m_fHeal = PlayerData.m_fHeal;
    }

    public override void Levelup()
    {
        m_fMaxHP += 10;
        m_nLevel += 1;
        m_fAttack += 5;
        m_fDefense += 5;
        m_fHeal += 3;
    }

}
