using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    public string m_sName;
    public string m_sDescription;
    public string m_sPreDescription;
    public Item requiredKey;
    //public string nextRoom;
    //public string previousRoom;
    public bool HasRightKeyToMove(List<Item> inventory)
    {
        return inventory.Contains(requiredKey);
    }
    // Start is called before the first frame update

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
