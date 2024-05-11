using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryViewManager : MonoBehaviour
{
    #region Singleton
    public static InventoryViewManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField]
    private GameObject _InventoryUI;

    public void OpenInventory()
    {
        _InventoryUI.SetActive(true);
    }
}
