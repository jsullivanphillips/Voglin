using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Tooltip("Wall prefab is 0.25 units thick and 1 unit tall.")]
    [SerializeField] GameObject _WallTile;

    private Vector2 roomSize = new Vector2(16, 9);

    private Vector2 roomPosition = new Vector2(0, 0);

    public void InitializeRoom(Vector2 size, Vector2 position)
    {
        roomSize = size;
        roomPosition = position;
        GenerateRoom();
    }

    #region RoomSize
    public void SetRoomSize(Vector2 size)
    {
        roomSize = size;
    }

    public void SetRoomWidth(float width)
    {
        roomSize.x = width;
    }

    public void SetRoomHeight(float height)
    {
        roomSize.y = height;
    }

    public Vector2 GetRoomSize()
    {
        return roomSize;
    }

    public float GetRoomWidth()
    {
        return roomSize.x;
    }

    public float GetRoomHeight()
    {
        return roomSize.y;
    }
    #endregion

    #region RoomGeneration
    private void GenerateRoom()
    {
        // I want to generate a room the given size
        // I want to be able to add exits to the room after they are generated. 
        // Therefore, first we generate a box room the size given.
        // Afterwards, we allow the user to add exits to the room after generation.
        // Connections between rooms will be held elsewhere.
        for (int x = 0; x < GetRoomWidth(); x++)
        {
            for (int y = 0; y < GetRoomHeight(); y++)
            {
                // if we are on the edge of the room
                if (x == 0 || x == GetRoomWidth() - 1 || y == 0 || y == GetRoomHeight() - 1)
                {
                    GameObject wall;
                    float cornerOffset = 0.25f;
                    // Bottom left corner
                    if(x == 0 && y == 0)
                    {
                        // Vertical wall
                        wall = Instantiate(_WallTile, new Vector3(x, y + cornerOffset, 0) + (Vector3)roomPosition, Quaternion.identity);
                        wall.transform.localScale = new Vector3(0.25f, 0.75f, 1);
                        // Horizontal Wall
                        wall = Instantiate(_WallTile, new Vector3(x + cornerOffset, y, 0) + (Vector3)roomPosition, Quaternion.Euler(0,0,90));
                        wall.transform.localScale = new Vector3(0.25f, 0.75f, 1);
                    }
                    // Top left corner
                    else if (x == 0 && y == GetRoomHeight() - 1)
                    {
                        // Vertical wall
                        wall = Instantiate(_WallTile, new Vector3(x, y - cornerOffset, 0) + (Vector3)roomPosition, Quaternion.identity);
                        wall.transform.localScale = new Vector3(0.25f, 0.75f, 1);
                        // Horizontal Wall
                        wall = Instantiate(_WallTile, new Vector3(x + cornerOffset, y, 0) + (Vector3)roomPosition, Quaternion.Euler(0, 0, 90));
                        wall.transform.localScale = new Vector3(0.25f, 0.75f, 1);
                    }
                    // Top right corner
                    else if (x == GetRoomWidth() - 1 && y == GetRoomHeight() - 1)
                    {
                        // Vertical wall
                        wall = Instantiate(_WallTile, new Vector3(x, y - cornerOffset, 0) + (Vector3)roomPosition, Quaternion.identity);
                        wall.transform.localScale = new Vector3(0.25f, 0.75f, 1);
                        // Horizontal Wall
                        wall = Instantiate(_WallTile, new Vector3(x - cornerOffset, y, 0) + (Vector3)roomPosition, Quaternion.Euler(0, 0, 90));
                        wall.transform.localScale = new Vector3(0.25f, 0.75f, 1);
                    }
                    // Bottom Right Corner
                    else if (x == GetRoomWidth() - 1 && y == 0)
                    {
                        // Vertical wall
                        wall = Instantiate(_WallTile, new Vector3(x, y + cornerOffset, 0) + (Vector3)roomPosition, Quaternion.identity);
                        wall.transform.localScale = new Vector3(0.25f, 0.75f, 1);
                        // Horizontal Wall
                        wall = Instantiate(_WallTile, new Vector3(x - cornerOffset, y, 0) + (Vector3)roomPosition, Quaternion.Euler(0, 0, 90));
                        wall.transform.localScale = new Vector3(0.25f, 0.75f, 1);
                    }
                    // Everywhere else
                    else
                    {
                        wall = Instantiate(_WallTile, new Vector3(x, y, 0) + (Vector3)roomPosition, Quaternion.identity);
                        if (y == 0 || y == GetRoomHeight() - 1)
                        {
                            wall.transform.localScale = new Vector3(0.25f, 1f, 1);
                            wall.transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                    }
                           
                }
            }
        }
    }


    #endregion


}
