using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryViewManager : MonoBehaviour
{
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

    [SerializeField]
    GameObject _InventoryView;
    [SerializeField]
    GameObject _DarkenBackground;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
           PressB();
        }
    }


    public void OpenInventory()
    {
        _DarkenBackground.SetActive(true);
        _InventoryView.SetActive(true);
        GameStateManager.Instance.PauseGame();
    }

    public void CloseInventory()
    {
        _DarkenBackground.SetActive(false);
        _InventoryView.SetActive(false);
    }

    public void PressB()
    {
        // This is to prevent player from reopening inventory while game is unpausing
        if(!_InventoryView.activeSelf && GameStateManager.Instance.IsPaused())
        {
            return;
        }

        // If cooldown is active, don't allow opening the inventory
        if (GameStateManager.Instance.GetGameState() == GameState.ChoosingNewAbility)
        {
            return;
        }

        if(_InventoryView.activeSelf)
        {
            TooltipManager.Instance.HideComponentTooltip();
        }
        
        _DarkenBackground.SetActive(!_DarkenBackground.activeSelf);
        _InventoryView.SetActive(!_InventoryView.activeSelf);

        if (_InventoryView.activeSelf)
        {
            GameStateManager.Instance.PauseGame();
        }
        else
        {
            GameStateManager.Instance.ResumeGame();
        }
    }
}
