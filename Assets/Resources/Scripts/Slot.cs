using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot: MonoBehaviour
{
    #region Properties

    [Header("Properties")]
   
    public Vector3 Position;
    public bool IsPlayer;
    public int SlotID { get; set; } = -1;

    #endregion

    #region GameObjects

    [Header("GameObjects")]
    public GameObject HitEffect;
    public GameObject HealthBar;
    public GameObject CardTemplate;
    public List<GameObject> SelectionEffect;
    public GameObject Card { get; set; }
    public int Status { get; set; }

    #endregion

    public void ActiveSelectionCard()
    {
        SelectionEffect[0].SetActive(true);
    }

    public bool HaveCard()
    {
        return Card != null;
    }
}