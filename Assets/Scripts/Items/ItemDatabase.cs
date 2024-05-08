using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

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

    public List<ComponentSO> components = new List<ComponentSO>();

    public ComponentSO GetRandomComponent()
    {
        return Instantiate(components[Random.Range(0, components.Count)]);
    }
   
}
