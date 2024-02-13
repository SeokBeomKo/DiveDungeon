using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCenter : MonoBehaviour
{
    [SerializeField] public LevelGenerator levelGenerator;
    [SerializeField] public RoomGenerator roomGenerator;

    [SerializeField] public Room[,] rooms;
    [SerializeField] public List<Vector2> takenPositions = new List<Vector2>();

    [Header("월드 크기 (반지름)")]
    [SerializeField] public Vector2 worldSize;
    [Header("방 개수")]
    [SerializeField] public int numberOfRooms;
    

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
