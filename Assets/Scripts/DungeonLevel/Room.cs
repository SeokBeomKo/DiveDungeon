using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
  public Vector2 gridPosition;
  public RoomType type;

  public Directions direction;

  public Room(Vector2 _gridPos, RoomType _type)
  {
		gridPosition = _gridPos;
		type = _type;
	}
}
