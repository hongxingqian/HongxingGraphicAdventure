using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.Mathematics;
using System.Text;
public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }
    public Character player;
    public Character NPC;
    public Room environment;
    public TMP_Text status;
    public TMP_Text console;
    public TMP_Text enemyConsole;
    public TMP_Text enemyStatus;
    public GameObject winMenu;
    public GameObject dieMenu;

    public Button MoveForwardButton;
    public Button TalkButton;
    public Button ATKButton;
    public Button HealButton;
    public Button MoveButton;
    public Button GoBackButton;
    public Button SearchButton;
    public Button RestButton;
    public Button InventoryButton;
    public Button LookAroundButton;
    public Button GainButton;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }


        if (enemyStatus == null)
        {

        }
        else
        {
            enemyStatus.gameObject.SetActive(false);
        }


        if (NPC == null)
        {

        }
        else
        {
            NPC.gameObject.SetActive(false);
        }

        player.gameObject.SetActive(true);
        LookAroundButton.gameObject.SetActive(false);
        ATKButton.gameObject.SetActive(false);
        HealButton.gameObject.SetActive(false);
        SearchButton.gameObject.SetActive(false);
        TalkButton.gameObject.SetActive(false);
        MoveButton.gameObject.SetActive(false);
        GoBackButton.gameObject.SetActive(false);
        RestButton.gameObject.SetActive(false);
        SearchButton.gameObject.SetActive(false);
        GainButton.gameObject.SetActive(false);
        winMenu.gameObject.SetActive(false);
        dieMenu.gameObject.SetActive(false);



        ATKButton.onClick.AddListener(ATKButtonFunction);
        InventoryButton.onClick.AddListener(InventoryButtonFunction);
        HealButton.onClick.AddListener(HealButtonFunction);
        MoveButton.onClick.AddListener(MoveButtonFunction);
        MoveForwardButton.onClick.AddListener(MoveForwardButtonFunction);
        TalkButton.onClick.AddListener(TalkButtonFunction);
        RestButton.onClick.AddListener(RestButtonFunction);
        SearchButton.onClick.AddListener(SearchButtonFunction);
        //GoBackButton.onClick.AddListener(GoBackButtonFunction);
        LookAroundButton.onClick.AddListener(LookAroundFunction);
        GainButton.onClick.AddListener(GainButtonFunction);





    }
    //*---------------------------------------------------*
    private void Start()
    {
        console.SetText(environment.m_sPreDescription);
        player.PrintPlayerStatus();
        UpdateEnemyStatus();


    }
    public void UpdatePlayerStatus()
    {

        status.text = "Name: " + player.Name + "\n"
            + "Level: " + player.Level + "\n"
            + "HP: " + player.HP + "/" + player.MaxHP + "\n"
            + "Attack: " + player.Attack + "\n"
            + "Defense: " + player.Defense + "\n"
            + "Heal Ability: " + player.Heal + "\n"
            + "Inventory:" + PrintInventory();
    }

    public string PrintInventory()
    {
        string statusTextBuffer = status.text;

        if (player.m_iInventory == null)
        {
            return "";
        }
        bool isEmpty = true;
        StringBuilder inventorylist = new StringBuilder();
        for (int i = 0; i < player.m_iInventory.Items.Count; i++)
        {
            if (player.m_iInventory.Items[i] != null)
            {
                inventorylist.AppendLine(player.m_iInventory.Items[i].m_sName + " ");
                isEmpty = false;
            }
        }

        if (isEmpty)
        {
            inventorylist.AppendLine("empty");
        }
        return inventorylist.ToString();
    }


    public void UpdateEnemyStatus()
    {

        if (NPC == null)
        {

        }

        else if (NPC.m_iKey == null)
        {
            enemyStatus.text = "Name: " + NPC.Name + "\n"
       + "Level: " + NPC.Level + "\n"
       + "HP: " + NPC.HP + "/" + NPC.MaxHP + "\n"
       + "Attack: " + NPC.Attack + "\n"
       + "Defense: " + NPC.Defense + "\n"
       + "Heal Ability: " + NPC.Heal + "\n"
       + "Inventory:" + NPC.m_iDropableItem.LootName;
        }

        else
        {
            enemyStatus.text = "Name: " + NPC.Name + "\n"
            + "Level: " + NPC.Level + "\n"
            + "HP: " + NPC.HP + "/" + NPC.MaxHP + "\n"
            + "Attack: " + NPC.Attack + "\n"
            + "Defense: " + NPC.Defense + "\n"
            + "Heal Ability: " + NPC.Heal + "\n"
            + "Inventory:" + NPC.m_iDropableItem.LootName + ", " + NPC.m_iKey.LootName;
        }

    }
    //*---------------------------------------------------*
    public void ATKButtonFunction()
    {

        int enemyChoice = NPC.random.Next(0, 2);
        if (player.HP > 0 && NPC.HP > 0)
        {
            int damage = player.Fight(NPC);
            Debug.Log("Player hp: " + player.m_fHP);
            console.SetText("You did " + damage + " damage!");
            NPC.PrintEnemyStatus();


            if (NPC.HP <= 0)
            {

                if (NPC.IsBoss())
                {
                    WinMenuDisplay(true);
                }

                Debug.Log("NPC died");
                console.text += "\nYou win!";
                player.Levelup();
            
                console.text += "\n-----You Win!";
                console.text += "\nLevel Up: Lv " + player.Level + " --> " + "Lv " + (player.Level + 1);
                Item droppedItem = NPC.DropLoot();
                Item droppedKey = NPC.DropKey();
                CharacterDisplay(false);
                EnemyInteractionDisplay(false);
                MoveFuncitonDisplay(true);

                if (droppedItem != null)
                {
                    console.text += "\nThe enemy dropped a loot: " + droppedItem.LootName;
                    console.text += "\nAttack: " + droppedItem.Attack;
                    console.text += "\nDefense: " + droppedItem.Defense;
                    console.text += "\nHP: " + droppedItem.HP;
                    player.m_iInventory.AddItem(droppedItem);
                    player.AddItemStatusToCharacter(droppedItem);
                    player.PrintPlayerStatus();
                }
                else
                {
                    console.text += "\nNo loot was dropped by the enemy.";
                    player.PrintPlayerStatus();
                }

                if (droppedKey != null)
                {
                    console.text += "\nThe enemy dropped a key-shape thing: " + droppedKey.LootName;
                    player.m_iInventory.AddItem(droppedKey);
                    player.AddItemStatusToCharacter(droppedKey);
                    player.PrintPlayerStatus();
                    NPC.PrintEnemyStatus();

                }

                return;
            }



            else if (enemyChoice == 0)
            {
                int e_damage = NPC.Fight(player);
                console.text += "\nEnemy did " + e_damage + " damage!";
                player.PrintPlayerStatus();

            }

            else
            {
                console.text += "\nEnermy heal " + NPC.Heal ;
                NPC.Healing();
                NPC.PrintEnemyStatus();

            }

            if (player.HP <= 0)
            {
                Debug.Log("Player should die");
                DieMenuDisplay(true);

                return;
            }
        }
    }
    public void HealButtonFunction()
    {
        console.SetText("You heal " + player.Heal + "health.");
        player.Healing();


        int enemyChoice = NPC.random.Next(0, 2);
        if (player.HP > 0 && NPC.HP > 0)
        {

            Debug.Log("Player hp: " + player.m_fHP);
            console.SetText("You healed " + player.Heal + " HP!");
            NPC.PrintEnemyStatus();


            if (NPC.HP <= 0)
            {

                if (NPC.IsBoss())
                {
                    WinMenuDisplay(true);
                }

                Debug.Log("NPC died");
                console.text += "\nYou win!";
                player.Levelup();

                console.text += "\n-----You Win!";
                console.text += "\nLevel Up: Lv " + player.Level + " --> " + "Lv " + (player.Level + 1);
                Item droppedItem = NPC.DropLoot();
                Item droppedKey = NPC.DropKey();
                CharacterDisplay(false);
                EnemyInteractionDisplay(false);
                MoveFuncitonDisplay(true);

                if (droppedItem != null)
                {
                    console.text += "\nThe enemy dropped a loot: " + droppedItem.LootName;
                    console.text += "\n" + droppedItem.Attack;
                    console.text += "\n" + droppedItem.Defense;
                    console.text += "\n" + droppedItem.HP;
                    player.m_iInventory.AddItem(droppedItem);
                    player.AddItemStatusToCharacter(droppedItem);
                    player.PrintPlayerStatus();
                }
                else
                {
                    console.text += "\nNo loot was dropped by the enemy.";
                    player.PrintPlayerStatus();
                }

                if (droppedKey != null)
                {
                    console.text += "\nThe enemy dropped a key-shape thing: " + droppedKey.LootName;
                    player.m_iInventory.AddItem(droppedKey);
                    player.AddItemStatusToCharacter(droppedKey);
                    player.PrintPlayerStatus();
                    NPC.PrintEnemyStatus();

                }
                return;
            }



            else if (enemyChoice == 0)
            {
                int e_damage = NPC.Fight(player);
                console.text += "\nEnemy did " + e_damage + " damage!";
                player.PrintPlayerStatus();

            }

            else
            {
                console.text += "\nEnermy heal " + NPC.Heal + "HP!";
                NPC.Healing();
                NPC.PrintEnemyStatus();

            }

            if (player.HP <= 0)
            {
                Debug.Log("Player should die");
                DieMenuDisplay(true);

                return;
            }
        }
    }

    public void MoveButtonFunction()
    {
        console.SetText("Move to the next level");
        if(environment.requiredKey != null)
        {
            if (player.Inventory.ContainsItem(environment.requiredKey))
            {
                player.SaveStatus();
                player.gameObject.SetActive(false);
                environment.GoToNextLevel();
            }
            else
            {
                console.SetText("You need key to pass");
            }

        }
        else
        {
            player.SaveStatus();
            player.gameObject.SetActive(false);
            environment.GoToNextLevel();
        }

    }

    public void MoveForwardButtonFunction()
    {
        console.SetText(environment.m_sDescription);
        MoveForwardDisplay(false);
        LookAroundDisplay(true);

    }

    //public void GoBackButtonFunction()
    //{
    //    if (environment.previousRoom == "") { console.SetText("There is no way to go back!"); }

    //}
    public void InventoryButtonFunction()
    {
        console.SetText(PrintInventory());
    }

    public void TalkButtonFunction()
    {
        enemyConsole.SetText(NPC.m_sConversation);
        if (NPC.m_iRelationship == 0)
        {
            //console.SetText("You should take it.");
            //ConversationDisplay(false);
            FriendInteractionDisplay(true);
        }
        else
        {
            console.SetText("Prepare to fight!");
            ConversationDisplay(false);
            EnemyInteractionDisplay(true);
        }

    }
    public void SearchButtonFunction()
    {

    }
    public void RestButtonFunction() 
    {
        console.text += "You healed 20 HP after take a good sleep!";
        player.Resting();
        UpdatePlayerStatus();
        NoOneSearchDisplay(false);

    }

    public void GainButtonFunction()
    {

    }
    public void LookAroundFunction()
    {
        if(NPC == null)
        {
            console.SetText("It seems no one is here.");


            LookAroundDisplay(false);
            NoOneSearchDisplay(true);
            MoveFuncitonDisplay(true);


        }
        else
        {

            console.SetText(NPC.m_sDescription);
            CharacterDisplay(true);
            LookAroundDisplay(false);
            ConversationDisplay(true);

        }
    }
    //*---------------------------------------------------*
    private void MoveForwardDisplay(bool display)
    {
        MoveForwardButton.gameObject.SetActive(display);
        //GoBackButton.gameObject.SetActive(display);
    }
    private void LookAroundDisplay(bool display)
    {
        LookAroundButton.gameObject.SetActive(display);
    }
    private void NoOneSearchDisplay(bool display)
    {
        //SearchButton.gameObject.SetActive(display);
        RestButton.gameObject.SetActive(display);
    }

    private void ConversationDisplay(bool display)
    {
        TalkButton.gameObject.SetActive(display);
    }

    private void FriendInteractionDisplay(bool display)
    {
        //GainButton.gameObject.SetActive(display);
        MoveButton.gameObject.SetActive(display);
    }
    private void EnemyInteractionDisplay(bool display)
    {
        ATKButton.gameObject.SetActive(display);
        HealButton.gameObject.SetActive(display);
    }

    private void MoveFuncitonDisplay(bool display)
    {
        MoveButton.gameObject.SetActive(display);
    }

    private void WinMenuDisplay(bool display)
    {
        winMenu.gameObject.SetActive(display);
    }
    private void DieMenuDisplay(bool display)
    {
        dieMenu.gameObject.SetActive(display);
    }

    private void CharacterDisplay(bool display)
    {
        NPC.gameObject.SetActive(display);
        enemyStatus.gameObject.SetActive(display);

    }
    //*---------------------------------------------------*


}
