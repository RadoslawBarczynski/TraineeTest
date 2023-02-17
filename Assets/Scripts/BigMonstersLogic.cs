using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonstersLogic : MonoBehaviour
{
    public string choosenList = "";

    //scripts
    GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    //validation for checking if selected boxes +1 index is empty
    public bool CheckIfBigMonsterCanBePlaced(GameObject SelectedBoxObject)
    {
        int indexOfSelectedSlot = SlotToIndex(SelectedBoxObject);
        if(SelectedBoxObject.GetComponent<OnClickHandler>().isMonsterHere == true)
        {
            return false;
        }
        if (indexOfSelectedSlot < _gameManager.Army1List.Count - 1)
        {
            if (choosenList == "army1")
            {
                GameObject NextSlotToCheck = _gameManager.Army1List[indexOfSelectedSlot + 1];
                if (NextSlotToCheck.GetComponent<OnClickHandler>().isSelected == false && NextSlotToCheck.GetComponent<OnClickHandler>().isMonsterHere == false)
                {
                    return true;
                }
            }
            else if(choosenList == "army2")
            {
                GameObject NextSlotToCheck = _gameManager.Army2List[indexOfSelectedSlot + 1];
                if (NextSlotToCheck.GetComponent<OnClickHandler>().isSelected == false && NextSlotToCheck.GetComponent<OnClickHandler>().isMonsterHere == false)
                {
                    return true;
                }
            }                     
        }
        return false;
    }

    //check the index of seletect slot in army slots
    public int SlotToIndex(GameObject gameObject)
    {
        for (int i = 0; i < _gameManager.Army1List.Count; i++)
        {
            if (_gameManager.Army1List[i] == gameObject)
            {
                choosenList = "army1";
                return i;
            }
            else if (_gameManager.Army2List[i] == gameObject)
            {
                choosenList = "army2";
                return i;
            }
        }
        return 1;
    }

}
