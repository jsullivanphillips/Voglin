using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomGenerator : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;

    void Start()
    {
        GenerateRooms();
    }


    void GenerateRooms()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject roomInstance = Instantiate(roomPrefab, new Vector3(i * 20, 0, 0), Quaternion.identity);
            Room room = roomInstance.GetComponent<Room>();
            room.InitializeRoom(new Vector2(18, 15), new Vector3(i * 20, 0, 0));
        }
    }

    // Up next:
    // 1. Add a way to generate random room sizes
    // 2. Add a way to generate random room positions without overlapping rooms
    // 3. Connect rooms by adding doors to the rooms and corridors between them

}
