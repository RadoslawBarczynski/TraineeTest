using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapButtonLogic : MonoBehaviour
{
    [SerializeField] private string stringForListDetection = "";

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
        int tempIndex = FindIndexInList(_gameManager.selectedBoxesList[0]);
        int tempIndex2;
        GameObject tempObject;
        Vector2 tempVector;
        //swap indexes 
        if (stringForListDetection == "army1")
        {
            tempObject = _gameManager.Army1List[tempIndex];            
            tempIndex2 = FindIndexInList(_gameManager.selectedBoxesList[1]);
            if (stringForListDetection == "army1")
            {
                _gameManager.Army1List[tempIndex] = _gameManager.Army1List[tempIndex2];               
                _gameManager.Army1List[tempIndex2] = tempObject;
                
                //utworzyæ jakiœ prefab w obiektu jednego dziecka obiektu listy, a nastêpnie wrzuciæ w jego miejsce drugi index, i to samo z pierwszym
                //po zamianie miejscami jako dzieci wszystko œmiga
            }
            else if(stringForListDetection == "army2")
            {
                _gameManager.selectedBoxesList[0] = _gameManager.Army2List[tempIndex];
                _gameManager.selectedBoxesList[1] = tempObject;
            }
        }
        else if(stringForListDetection == "army2")
        {
            stringForListDetection = "";
            if (stringForListDetection == "army1")
            {

            }
            else
            {

            }
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
