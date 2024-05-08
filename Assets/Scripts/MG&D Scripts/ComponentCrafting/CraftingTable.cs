using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    public static CraftingTable Instance { get; private set; }

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
    private GameObject _ItemComponentPrefab;
    [SerializeField]
    private Transform _CraftingSpace;
    [SerializeField]
    private RectTransform _BoundsBox;
    [SerializeField]
    private RectTransform _SpawnBoundsBox;
    [SerializeField]
    private Transform _ItemComponentsParent;

    private Dictionary<int, ItemComponent> _itemComponents = new Dictionary<int, ItemComponent>();

    public void SpawnRandomItem()
    {
        GameObject item = Instantiate(_ItemComponentPrefab, GetRandomSpawnPosition(), Quaternion.identity, _ItemComponentsParent);
        ItemComponent itemComponent = item.GetComponent<ItemComponent>();
        itemComponent.boundsBox = _BoundsBox;
        itemComponent.craftingArea = _CraftingSpace;
        itemComponent.originalParent = _ItemComponentsParent;
        //itemComponent.id = _itemComponents.Count;
        //_itemComponents.Add(itemComponent.id, itemComponent);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3[] corners = new Vector3[4];
        _SpawnBoundsBox.GetWorldCorners(corners);
    
        float x = Random.Range(corners[0].x, corners[2].x);
        float y = Random.Range(corners[0].y, corners[2].y);
    
        Vector3 spawnPosition = new Vector3(x, y, 0);
        return spawnPosition;
    }

}
