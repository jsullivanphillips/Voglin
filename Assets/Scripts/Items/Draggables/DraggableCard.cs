using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class DraggableCard : DraggableObject
{
    private Guid id;

    public void SetGuid(Guid guid)
    {
        id = guid;
    }

    public Guid GetGuid()
    {
        return id;
    }
}
