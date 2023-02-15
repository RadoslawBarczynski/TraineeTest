using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/*
 *  all statements scenario:
 *  
 *  {legend: 0 - empty slot, x1 & x2 - monsters slots, S - selected slot where we want to change}
 *  
 *  - changing x1 monsters in single army and both
 *  - changing x2 monsters in single army and both
 *  - changing when x1 x2 x2 0 0 0 -> x2 x2 x1 0 0 0 
 *  - changing when 0 0 0 x2 x2 x1 -> 0 0 0 x1 x2 x2
 *  - changing x2 monster into this situation 0 S x2 x2 0 0 -> 0 x2 x2 x2 x2 0
 * 
 * 
 * 
 * 
*/
public class SwapButtonLogic : MonoBehaviour
{
    [SerializeField] private string stringForListDetection = "";
    [SerializeField] private GameObject Army1Parent, Army2Parent;

    //scripts
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void SwapMonstersInSelectedBoxes()
    { 
        int tempIndex = FindIndexInList(_gameManager.selectedBoxesList[0]); //check what army is selected first
        int tempIndex2; //variable for second selected box
        GameObject tempObject; //temp object for swaping two objects


        //------------------logic if army 1 is selected first-----------------


        if (stringForListDetection == "army1")  
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
            else if(stringForListDetection == "army2") //if first selection is in army1 and second in army2
            {
                SwapParents(tempIndex, tempIndex2, _gameManager.Army1List, _gameManager.Army2List);
            }
        }


        //------------------logic if army 2 is selected first-----------------


        else if (stringForListDetection == "army2") 
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
            else if (stringForListDetection == "army1") //if first selection is in army2 and second in army1
            {
                SwapParents(tempIndex2, tempIndex, _gameManager.Army1List, _gameManager.Army2List);
            }
        }
    }

    //------------------------Swap function for one army-------------------------- 

    public void SwapFunctionForBigMonster(int index1, int index2, GameObject gameObject, List<GameObject> list)
    {
        if (index1 + 1 <= 5 && index2 + 1 <= 5)
        {            
            if (list[index1 + 1].GetComponent<OnClickHandler>().enabled == false &&
                list[index2 + 1].GetComponent<OnClickHandler>().enabled == false || // ^ if two elements are big monsters
                list[index1 + 1].GetComponent<OnClickHandler>().enabled == true &&
                list[index2 + 1].GetComponent<OnClickHandler>().enabled == false &&
                list[index1 + 1].GetComponent<OnClickHandler>().isBigMonsterHere == false || // ^ if one element is big monster in second selection
                list[index1 + 1].GetComponent<OnClickHandler>().enabled == false &&
                list[index2 + 1].GetComponent<OnClickHandler>().enabled == true &&
                list[index2 + 1].GetComponent<OnClickHandler>().isBigMonsterHere == false) // ^ if one element is big monster in first selection
            {
                //-------------if x2 x2   x2 x2 example------------------------------------------------               

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
            else if (list[index1 + 1].GetComponent<OnClickHandler>().enabled == true &&
                list[index2 + 1].GetComponent<OnClickHandler>().enabled == false &&
                list[index1 + 1].GetComponent<OnClickHandler>().isBigMonsterHere == true)
            {
                //-------------if x1 x2 x2 example and x2 is second selection-----------------------

                //swap for selected slot
                list[index1] = list[index2];
                list[index2] = gameObject;

                //swap for second big monster slot
                list[index2] = list[index2 + 1];
                list[index2 + 1] = gameObject;

                _gameManager.selectedBoxesList[0].transform.SetSiblingIndex(index2 + 1);
            }
            else if (list[index1 + 1].GetComponent<OnClickHandler>().enabled == false &&
                list[index2 + 1].GetComponent<OnClickHandler>().enabled == true &&
                list[index2 + 1].GetComponent<OnClickHandler>().isBigMonsterHere == true)
            {
                //-------------if x1 x2 x2 example and x2 is first selection---------------------

                //swap for selected slot
                list[index1] = list[index2];
                list[index2] = gameObject;

                //swap for second big monster slot
                gameObject = list[index1];
                list[index1] = list[index1 + 1];
                list[index1 + 1] = gameObject;

                _gameManager.selectedBoxesList[1].transform.SetSiblingIndex(index1 + 1);
            }
        }
        else
        {
        if (list[index1 - 1].GetComponent<OnClickHandler>().enabled == false &&
            list[index1 - 2].GetComponent<OnClickHandler>().isBigMonsterHere == true)
            {
                //-------------if x1 x2 x2 example and x2 is second selection-----------------------

                //swap for selected slot
                list[index1] = list[index2 + 1];
                list[index2 + 1] = list[index2];
                list[index2] = gameObject;
               
                _gameManager.selectedBoxesList[0].transform.SetSiblingIndex(index2);
            }
        else if(list[index2 - 1].GetComponent<OnClickHandler>().enabled == false &&
                list[index2 - 2].GetComponent<OnClickHandler>().isBigMonsterHere == true)
            {
                //-------------if x1 x2 x2 example and x2 is first selection-----------------------
                list[index1] = list[index2];
                list[index2] = list[index1 + 1];
                list[index1 + 1] = gameObject;

                _gameManager.selectedBoxesList[1].transform.SetSiblingIndex(index1);
            }
        }
    }

    //------------------------Swap function for two armys-------------------------- 
    public void SwapParents(int index1, int index2, List<GameObject> list1, List<GameObject> list2)
    {       
        //temporary object for slots migration
        GameObject tempForArmy1 = list1[index1];
        GameObject tempForArmy2 = list2[index2];

        if (tempForArmy1.GetComponent<OnClickHandler>().isBigMonsterHere == false && tempForArmy2.GetComponent<OnClickHandler>().isBigMonsterHere == false)
        {

            //swap parents
            list1[index1].transform.SetParent(Army2Parent.transform);
            list2[index2].transform.SetParent(Army1Parent.transform);

            //remove duplications
            list1.Remove(tempForArmy1);
            list2.Remove(tempForArmy2);

            //insert slots in their correct positions
            list1.Insert(index1, tempForArmy2);
            list2.Insert(index2, tempForArmy1);

            //move object to their positions
            tempForArmy1.transform.SetSiblingIndex(index2);
            tempForArmy2.transform.SetSiblingIndex(index1);
        }        
        else 
        {
            if (tempForArmy1.GetComponent<OnClickHandler>().isBigMonsterHere == true &&
                tempForArmy2.GetComponent<OnClickHandler>().isBigMonsterHere == false &&
                list2[index2 + 1].GetComponent<OnClickHandler>().isBigMonsterHere == true) //migration x2 from army1 to army2 without space
            {
                if(index2 + 3 <= 5) //check if u are still in list indexes
                {
                    if(index2 + 4 <= 5)  //check if the situation is 0 S x2 x2 x1 0 if yes move all one index to the right
                    {
                        SwapInOneList(list2, index2 + 3, index2 + 4);
                    }
                    SwapInOneList(list2, index2 + 2, index2 + 3);
                    SwapInOneList(list2, index2 + 1, index2 + 2);
                }
                else
                {
                    return; 
                }
            }
            else if (tempForArmy1.GetComponent<OnClickHandler>().isBigMonsterHere == false &&
                    tempForArmy2.GetComponent<OnClickHandler>().isBigMonsterHere == true &&
                    list1[index1 + 1].GetComponent<OnClickHandler>().isBigMonsterHere == true) //migration x2 from army2 to army1 without space
            {
                if (index1 + 3 <= 5) //check if u are still in list indexes
                {
                    if (index1 + 4 <= 5) //check if the situation is 0 S x2 x2 x1 0 if yes move all one index to the right
                    {
                        SwapInOneList(list1, index1 + 3, index1 + 4);
                    }
                    SwapInOneList(list1, index1 + 2, index1 + 3);
                    SwapInOneList(list1, index1 + 1, index1 + 2);
                }
                else
                {
                    return;
                }
            }
            
                //second slots of x2 monsters
                GameObject tempForArmy1x1 = list1[index1 + 1];
                GameObject tempForArmy2x1 = list2[index2 + 1];

                //swap parents
                list1[index1].transform.SetParent(Army2Parent.transform);
                list1[index1 + 1].transform.SetParent(Army2Parent.transform);
                list2[index2].transform.SetParent(Army1Parent.transform);
                list2[index2 + 1].transform.SetParent(Army1Parent.transform);

                //remove duplications
                list1.Remove(tempForArmy1);
                list1.Remove(tempForArmy1x1);
                list2.Remove(tempForArmy2);
                list2.Remove(tempForArmy2x1);

                //insert slots in their correct positions
                list2.Insert(index2, tempForArmy1x1);
                list1.Insert(index1, tempForArmy2x1);
                list1.Insert(index1, tempForArmy2);
                list2.Insert(index2, tempForArmy1);

                //move object to their positions
                tempForArmy1x1.transform.SetSiblingIndex(index2);
                tempForArmy2x1.transform.SetSiblingIndex(index1);
                tempForArmy1.transform.SetSiblingIndex(index2);
                tempForArmy2.transform.SetSiblingIndex(index1);
        }
        
    }



    public bool CheckMonstersToSwap(int index1, int index2)
    {
        if (_gameManager.Army1List[index1].GetComponent<OnClickHandler>().isBigMonsterHere == true ||
                _gameManager.Army1List[index2].GetComponent<OnClickHandler>().isBigMonsterHere == true ||
                _gameManager.Army2List[index1].GetComponent<OnClickHandler>().isBigMonsterHere == true ||
                _gameManager.Army2List[index2].GetComponent<OnClickHandler>().isBigMonsterHere == true)
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

    public List<GameObject> SwapInOneList(List<GameObject> list, int i, int j)
    {
        GameObject temp = list[i];
        list[i] = list[j];
        list[j] = temp;

        return list;
    }


}
