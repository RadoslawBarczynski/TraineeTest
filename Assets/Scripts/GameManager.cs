using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> selectedBoxesList;
    public List<GameObject> Army1List;
    public List<GameObject> Army2List;

    //components
    [SerializeField] private Sprite _monsterX1, _monsterX2, _emptySlot;
    [SerializeField] private Button _swapBtn, _deleteBtn, _x1Btn, _x2Btn;

    //scripts
    [SerializeField] BigMonstersLogic _bigMonstersLogic;

    //update function for buttons validations
    private void Update()
    {
        if(selectedBoxesList.Count > 0)
        {
            if (BtnX1Validation())
            {
                _x1Btn.interactable = true;
            }
            else
            {
                _x1Btn.interactable = false;
            }
            if (!NegativeBtnValidation())
            {
                _deleteBtn.interactable = true;
            }
            else
            {
                _deleteBtn.interactable = false;
            }
            foreach (GameObject selectedBox in selectedBoxesList)
            {
                if (_bigMonstersLogic.CheckIfBigMonsterCanBePlaced(selectedBox) == false)
                {
                    _x2Btn.interactable = false;
                    break;
                }
                else
                {
                    _x2Btn.interactable = true;
                }
            }
            if(selectedBoxesList.Count == 2)
            {
                _swapBtn.interactable = true;
            }
            else
            {
                _swapBtn.interactable = false;
            }
        }
    }
    
    //-----------validations-----------------
    //add x1 button validation
    private bool BtnX1Validation()
    {
        foreach (GameObject selectedBox in selectedBoxesList)
        {
            if(selectedBox.GetComponent<OnClickHandler>().isMonsterHere == true)
            {
                return false;
            }
        }
            return true;
    }

    //logic for delete button to detect slots with isMonsterHere == true
    private bool NegativeBtnValidation()
    {
        foreach (GameObject selectedBox in selectedBoxesList)
        {
            if (selectedBox.GetComponent<OnClickHandler>().isMonsterHere == false)
            {
                return true;
            }
        }
        return false;
    }    


    //-----------buttons functions-----------------
    public void DeleteMonster()
    {
        foreach (GameObject selectedBox in selectedBoxesList)
        {
            if (selectedBox.GetComponent<OnClickHandler>().isBigMonsterHere == true)
            {
                CalculateNextSlotToDeleteMonster(selectedBox);
            }

            selectedBox.GetComponent<Image>().sprite = _emptySlot;
            selectedBox.GetComponent<OnClickHandler>().GameManagerSelectionLogic();
            selectedBox.GetComponent<OnClickHandler>().isBigMonsterHere = false;
        }

        selectedBoxesList.Clear();
    }

    public void InsertX1Demon()
    {
        foreach(GameObject selectedBox in selectedBoxesList)
        {
            selectedBox.GetComponent<Image>().sprite = _monsterX1;
            selectedBox.GetComponent<OnClickHandler>().GameManagerSelectionLogic();
        }

        selectedBoxesList.Clear();
    }

    public void InsertX2Demon()
    {
        foreach (GameObject selectedBox in selectedBoxesList)
        {
            CalculateNextSlotToSpawnMonster(selectedBox);

            selectedBox.GetComponent<Image>().sprite = _monsterX2;
            selectedBox.GetComponent<OnClickHandler>().isBigMonsterHere = true;
            selectedBox.GetComponent<OnClickHandler>().GameManagerSelectionLogic();           
        }

        selectedBoxesList.Clear();
    }

    //-------------calculations for x2 monster
    //calculations for X2 monster to create two slots 
    public void CalculateNextSlotToSpawnMonster(GameObject currentSlot)
    {
        int i = _bigMonstersLogic.SlotToIndex(currentSlot);
        GameObject secondSlot = null;
        if (_bigMonstersLogic.choosenList == "army1")
        {
            secondSlot = Army1List[i + 1];
        }
        else if (_bigMonstersLogic.choosenList == "army2")
        {
            secondSlot = Army2List[i + 1];
        }

        secondSlot.GetComponent<Image>().sprite = _monsterX2;
        secondSlot.GetComponent<OnClickHandler>().GameManagerSelectionLogic();
        secondSlot.GetComponent<OnClickHandler>().enabled = false;
    }

    //calculations for X2 monster to delete two slots 
    public void CalculateNextSlotToDeleteMonster(GameObject currentSlot)
    {
        int i = _bigMonstersLogic.SlotToIndex(currentSlot);
        GameObject secondSlot = null;
        if (_bigMonstersLogic.choosenList == "army1")
        {
            secondSlot = Army1List[i + 1];
        }
        else if (_bigMonstersLogic.choosenList == "army2")
        {
            secondSlot = Army2List[i + 1];
        }

        secondSlot.GetComponent<Image>().sprite = _emptySlot;
        secondSlot.GetComponent<OnClickHandler>().GameManagerSelectionLogic();
        secondSlot.GetComponent<OnClickHandler>().isBigMonsterHere = false;
        secondSlot.GetComponent<OnClickHandler>().enabled = true;
    }

}
