using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Deck : MonoBehaviour
{
    public List<GameObject> Cards = new();
    SlotController SlotController;
    public int CurrentIndexForInvoke { get; set;}

    void Start()
    {
        SlotController = FindObjectOfType<SlotController>();
    }

    void Update()
    {
        
    }

    public void InvokeCard(int index)
    {
        SlotController.ShowFreeSlots();
        CurrentIndexForInvoke = index;
        /*
        Cards[index] = Instantiate(Cards[index]);
        Cards[index].SetActive(true);
        Cards[index].transform.position = new Vector3(0, 0, 0);
        */
    }

    public GameObject InvokeCard()
    {
        return Cards[CurrentIndexForInvoke];
    }
}
