using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _selectionPrefab;

    private GameObject _selectionObject;

    public bool isSelected;
    public bool isMonsterHere = false;
    public bool isBigMonsterHere = false;

    //scripts
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        GenerateSelectionBoxes();
    }

    //auto selection boxes generation
    void GenerateSelectionBoxes()
    {
        _selectionObject = Instantiate(_selectionPrefab, transform.position, Quaternion.identity);
        _selectionObject.transform.SetParent(gameObject.transform);
        _selectionObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isSelected = !isSelected;

        //if there are 2 boxes select, deselect the first one
        if(gameManager.selectedBoxesList.Count == 2 && isSelected == true)
        {
            SelectionSwitch();
        }

        SelectionBoxLogic();
    }

    public void SelectionBoxLogic()
    {
        if (isSelected)
        {
            _selectionObject.SetActive(true);
            gameManager.selectedBoxesList.Add(this.gameObject);
        }
        else
        {
            _selectionObject.SetActive(false);
            gameManager.selectedBoxesList.Remove(this.gameObject);
        }
    }

    void SelectionSwitch()
    {
        OnClickHandler tempComponent = gameManager.selectedBoxesList[0].GetComponent<OnClickHandler>();
        tempComponent.isSelected = false;
        tempComponent._selectionObject.SetActive(false);
        gameManager.selectedBoxesList.Remove(tempComponent.gameObject);
    }

    public void GameManagerSelectionLogic()
    {
        isSelected = false;
        isMonsterHere = !isMonsterHere;
        _selectionObject.SetActive(false);
    }
}
