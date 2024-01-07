using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
  public Vector2 gridPosition;
  public RoomType type;
  public bool doorTop, doorBot, doorLeft, doorRight;

  public Room(Vector2 _gridPos, RoomType _type)
  {
		gridPosition = _gridPos;
		type = _type;
	}
}
