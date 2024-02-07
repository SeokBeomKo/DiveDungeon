using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCenter : MonoBehaviour
{
    [SerializeField] public LevelGenerator levelGenerator;
    [SerializeField] public RoomGenerator roomGenerator;

    [SerializeField] public Room[,] rooms;
    [SerializeField] public List<Vector2> takenPositions = new List<Vector2>();

    [SerializeField] public int numberOfRooms;
    [SerializeField] public Vector2 worldSize;

    private void Awake() 
    {
        levelGenerator.OnGenerateLevel += SettingRooms;

        levelGenerator.numberOfRooms = numberOfRooms;
        levelGenerator.worldSize = worldSize;
    }

    private void Start() 
    {
        
    }

    void SettingRooms(Room[,] roomValue, List<Vector2> posValue)
    {
        rooms = roomValue;
        takenPositions = posValue;
    }
}
