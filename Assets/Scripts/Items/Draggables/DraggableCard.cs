using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class DraggableCard : DraggableObject
{
    protected int id;

    public void SetId(int _id)
    {
        id = _id;
    }

    public int GetId()
    {
        return id;
    }
}
