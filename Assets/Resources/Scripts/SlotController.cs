using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    #region Public

    public GameObject SlotPrefab;
    List<GameObject> PlayerSlots;
    List<GameObject> PCSlots;

    GameObject PlayerDeck;

    int CurrentCardSelected { get; set; } = -1;

    /// <summary>
    /// -1 = None, 0 = Player and 1 = PC
    /// </summary>
    int IsPlayerDeckSelected { get; set; } = -1;

    #endregion

    #region Properties

    const int MaxSlots = 7;

    #endregion

    void Start()
    {
        CreateSlots();
        PlayerDeck = GameObject.FindWithTag("PlayerDeck");
    }

    void Update()
    {
        
    }

    #region Functions

    public void CreateSlots()
    {
        PlayerSlots = new List<GameObject>(MaxSlots);
        PCSlots = new List<GameObject>(MaxSlots);

        float xInitialPosition = -0.6f;
        const float yPosition = 0f;
        const float zPosition = 0.15f;

        for (int i = 0; i < MaxSlots; i++)
        {
            GameObject playerSlot = Instantiate(SlotPrefab);
            playerSlot.SetActive(true);
            playerSlot.transform.position = new Vector3(xInitialPosition, yPosition, -zPosition);
            playerSlot.GetComponent<Slot>().SlotID = i;
            PlayerSlots.Add(playerSlot);

            GameObject pcSlot = Instantiate(SlotPrefab);
            pcSlot.SetActive(true);
            pcSlot.transform.position = new Vector3(-xInitialPosition, yPosition, zPosition);
            pcSlot.GetComponent<Slot>().SlotID = i;
            PCSlots.Add(pcSlot);

            xInitialPosition += 0.2f;
        }
    }

    public void ShowFreeSlots()
    {
        foreach(var slot in PlayerSlots)
        {
            if(!slot.GetComponent<Slot>().Card)
            {
                slot.GetComponent<Slot>().SelectionEffect[2].SetActive(true);
                slot.GetComponent<Slot>().CardTemplate.SetActive(true);
                slot.GetComponent<Slot>().Status = 2;
            }
        }
    }

    public void HideFreeSlots()
    {
        foreach (var slot in PlayerSlots)
        {
            if (!slot.GetComponent<Slot>().Card)
            {
                slot.GetComponent<Slot>().SelectionEffect[2].SetActive(false);
                slot.GetComponent<Slot>().CardTemplate.SetActive(false);
                slot.GetComponent<Slot>().Status = 0;
            }
        }
    }

    public void InvokeCard(int slotId)
    {
        var card = PlayerDeck.GetComponent<Deck>().InvokeCard();

        PlayerSlots[slotId].GetComponent<Slot>().Card = Instantiate(card);
        PlayerSlots[slotId].GetComponent<Slot>().Card.SetActive(true);

        var slotPosition = PlayerSlots[slotId].GetComponent<Slot>().transform.position;
        var cardPrefabPosition = PlayerSlots[slotId].GetComponent<Slot>().Card.transform.position;

        PlayerSlots[slotId].GetComponent<Slot>().Card.transform.position = new Vector3
        (
            slotPosition.x, 
            cardPrefabPosition.y, 
            slotPosition.z
        );
        
        PlayerSlots[slotId].GetComponent<Slot>().SelectionEffect[2].SetActive(false);
        PlayerSlots[slotId].GetComponent<Slot>().CardTemplate.SetActive(false);
        PlayerSlots[slotId].GetComponent<Slot>().Status = 0;

        HideFreeSlots();
    }

    #endregion

    #region Select Card

    public Stats GetCard(int slotId, bool isPlayer)
    {
        if(isPlayer)
        {
            return PlayerSlots[slotId].GetComponent<Slot>().Card.GetComponent<Stats>();
        }
        else
        {
            return PCSlots[slotId].GetComponent<Slot>().Card.GetComponent<Stats>();
        }
    }

    public void ShowSelectionEffect(int slotId, bool isPlayer)
    {
        if (isPlayer)
        {
            PlayerSlots[slotId].GetComponent<Slot>().SelectionEffect[1].SetActive(true);
            PlayerSlots[slotId].GetComponent<Slot>().CardTemplate.SetActive(true);

            if(CurrentCardSelected != -1)
            {
                PlayerSlots[CurrentCardSelected].GetComponent<Slot>().SelectionEffect[1].SetActive(false);
                PlayerSlots[CurrentCardSelected].GetComponent<Slot>().CardTemplate.SetActive(false);
            }

            CurrentCardSelected = slotId;
        }
        else
        {
            PCSlots[slotId].GetComponent<Slot>().SelectionEffect[1].SetActive(true);
            PCSlots[slotId].GetComponent<Slot>().CardTemplate.SetActive(true);

            if (CurrentCardSelected != -1)
            {
                PCSlots[CurrentCardSelected].GetComponent<Slot>().SelectionEffect[1].SetActive(false);
                PCSlots[CurrentCardSelected].GetComponent<Slot>().CardTemplate.SetActive(false);
            }

            CurrentCardSelected = slotId;
        }
    }

    #endregion
}