using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwapButtonLogic : MonoBehaviour
{
    [SerializeField] private string stringForListDetection = "";
    [SerializeField] private GameObject Army1Parent, Army2Parent;

    //scripts
    private GameManager _gameManager;
    private BigMonstersLogic _bigMonstersLogic;

    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _bigMonstersLogic = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BigMonstersLogic>();
    }

    public void SwapMonstersInSelectedBoxes()
    { 
        int tempIndex = FindIndexInList(_gameManager.selectedBoxesList[0]); //check what army is selected first
        int tempIndex2; //variable for second selected box
        GameObject tempObject; //temp object for swaping two objects
        if (stringForListDetection == "army1")  //------------------logic for army 1-----------------
        {
            tempObject = _gameManager.Army1List[tempIndex];            
            tempIndex2 = FindIndexInList(_gameManager.selectedBoxesList[1]);
            if (stringForListDetection == "army1")
            {
                if(!CheckMonstersToSwap(tempIndex, tempIndex2)) //do when only small monsters are selected
                {
                    //change indexes in list of army
                    _gameManager.Army1List[tempIndex] = _gameManager.Army1List[tempIndex2];
                    _gameManager.Army1List[tempIndex2] = tempObject;

                    //change gameobject order in list
                    _gameManager.selectedBoxesList[1].transform.SetSiblingIndex(tempIndex);
                    _gameManager.selectedBoxesList[0].transform.SetSiblingIndex(tempIndex2);
                }
                else //do when Big monster is selected
                {
                    SwapFunctionForBigMonster(tempIndex, tempIndex2, tempObject, _gameManager.Army1List);
                }
            }
            else if(stringForListDetection == "army2")
            {
                
            }
        }
        else if(stringForListDetection == "army2") //------------------logic for army 2-----------------
        {
            tempObject = _gameManager.Army2List[tempIndex];
            tempIndex2 = FindIndexInList(_gameManager.selectedBoxesList[1]);
            if (stringForListDetection == "army2")
            {
                if (!CheckMonstersToSwap(tempIndex, tempIndex2)) //do when only small monsters are selected
                {
                    //change indexes in list of army
                    _gameManager.Army2List[tempIndex] = _gameManager.Army2List[tempIndex2];
                    _gameManager.Army2List[tempIndex2] = tempObject;

                    //change gameobject order in list
                    _gameManager.selectedBoxesList[1].transform.SetSiblingIndex(tempIndex);
                    _gameManager.selectedBoxesList[0].transform.SetSiblingIndex(tempIndex2);
                }
                else //do when Big monster is selected
                {
                    SwapFunctionForBigMonster(tempIndex, tempIndex2, tempObject, _gameManager.Army2List);
                }
            }
            else if (stringForListDetection == "army1")
            {

            }
        }
    }

    public void SwapFunctionForBigMonster(int index1, int index2, GameObject gameObject, List<GameObject> list)
    {
        //swap for selected slot
        list[index1] = list[index2];
        list[index2] = gameObject;
        //swap for second big monster slot
        GameObject tempIndexForBigMonster = list[index1 + 1]; //new variable for big monster + 1 disabled object
        list[index1 + 1] = list[index2 + 1];
        list[index2 + 1] = tempIndexForBigMonster;

        //swap siblings for second big monster slot
        list[index1 + 1].transform.SetSiblingIndex(index1 + 1);
        list[index2 + 1].transform.SetSiblingIndex(index2 + 1);
        //swap selected slots
        _gameManager.selectedBoxesList[1].transform.SetSiblingIndex(index1);
        _gameManager.selectedBoxesList[0].transform.SetSiblingIndex(index2);
    }


    public bool CheckMonstersToSwap(int index1, int index2)
    {
        if (_gameManager.Army1List[index1].GetComponent<OnClickHandler>().isBigMonsterHere == true || _gameManager.Army1List[index2].GetComponent<OnClickHandler>().isBigMonsterHere == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfMonstersCanBeSwaped(int index)
    {
        if(_gameManager.Army1List[index].GetComponent<OnClickHandler>().isBigMonsterHere == true && _gameManager.Army1List[index + 1].GetComponent<OnClickHandler>().enabled == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int FindIndexInList(GameObject gameObject)
    {
        for (int i = 0; i < 6; i++)
        {
            if (_gameManager.Army1List[i] == gameObject)
            {
                stringForListDetection = "army1";
                return i;
            }
            else if (_gameManager.Army2List[i] == gameObject)
            {
                stringForListDetection = "army2";
                return i;
            }
        }
        return -1;
    }


}
