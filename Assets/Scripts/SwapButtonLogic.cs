using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapButtonLogic : MonoBehaviour
{
    private string stringForListDetection = "";

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
        //swap indexes 
        if(stringForListDetection == "army1")
        {
            GameObject tempIndex = _gameManager.Army1List[FindIndexInList(_gameManager.selectedBoxesList[0])];
            _gameManager.selectedBoxesList[0] = _gameManager.Army1List[FindIndexInList(_gameManager.selectedBoxesList[1])];
            _gameManager.selectedBoxesList[1] = tempIndex;
        }
        else if(stringForListDetection == "army2")
        {
            GameObject tempIndex = _gameManager.Army2List[FindIndexInList(_gameManager.selectedBoxesList[0])];
        }
        //swap postion
        Vector2 tempObject;
        tempObject = _gameManager.selectedBoxesList[0].transform.position;
        _gameManager.selectedBoxesList[0].transform.position = _gameManager.selectedBoxesList[1].transform.position;
        _gameManager.selectedBoxesList[1].transform.position = tempObject;
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
