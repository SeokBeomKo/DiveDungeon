using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] public Vector2 worldSize; // : 월드의 크기 값 ( 중앙 기준점을 중심으로 반경 )
    Room[,] rooms;      // : 2 차원 배열 방의 집합 ( 실제 방이 존재하는 그리드 값 )
    List<Vector2> takenPositions = new List<Vector2>(); // : 이미 존재하는 방의 위치를 저장하는 리스트
    int gridSizeX, gridSizeY, numberOfRooms = 20; // : 그리드 크기와 방의 개수를 정의

    public GameObject roomWhiteObj; // : 방의 프리팹 오브젝트
    public Transform mapRoot; // : 맵의 루트 Transform

    void Start () 
    {
        // : 방의 수가 그리드 크기를 초과하지 않도록 방의 수를 제한
        if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2)){
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(worldSize.x); // 그리드의 가로 크기
        gridSizeY = Mathf.RoundToInt(worldSize.y); // 그리드의 세로 크기
        CreateRooms(); // 방 생성 함수 호출
        SetRoomDoors(); // 방의 문 설정 함수 호출
        DrawMap(); // 맵 그리는 함수 호출
        // GetComponent<SheetAssigner>().Assign(rooms); // 방의 정보를 다른 스크립트로 전달하는 함수 호출
    }

    void CreateRooms()
    {
        // : 초기 설정
        rooms = new Room[gridSizeX * 2, gridSizeY * 2]; // : 그리드 크기에 맞게 방 배열 초기화
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, RoomType.ENTER); // : 중앙에 입장 방 배치
        takenPositions.Insert(0, Vector2.zero); // : 생성한 방의 위치를 takenPosition 에 추가
        Vector2 checkPos = Vector2.zero; // : 새로운 방의 위치를 저장할 변수

        // : 확률 제어용 변수
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;

        // : 방 추가
        for(int i = 0; i < numberOfRooms -1; i++)
        {
            // : 확률 제어용 변수 방 생성시 마다 시작에서 끝 값으로
            float randomPerc = ((float)i) / (((float)numberOfRooms  - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);

            // : 새로운 위치 취득
            checkPos = NewPosition();

            // : 새로운 위치 검사
            // : 이웃된 방이 있고, 일정 확률에 걸렸을 경우 ( 이웃된 방이 없는 곳으로 선택해야한다 )
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do{
                    checkPos = SelectiveNewPosition();
                    iterations++;
                }while(NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if (iterations >= 50) print("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenPositions));
            }
            
            // : 위치 확정
            rooms[(int) checkPos.x + gridSizeX, (int) checkPos.y + gridSizeY] = new Room(checkPos,RoomType.ROOM);
            takenPositions.Insert(0,checkPos);
        }

        PrintRooms();
    }

    void PrintRooms() 
    {
        for(int i=0; i<takenPositions.Count; i++) 
        {
            Vector2 pos = takenPositions[i];
            Room room = rooms[(int)pos.x + gridSizeX, (int)pos.y + gridSizeY];

            Debug.Log("Room " + (i+1) + ": Position - " + room.gridPosition.ToString() +
                      ", Type - " + room.type);
        }
    }

    // : 새로운 위치를 생성하는 함수
    Vector2 NewPosition()
    {
        // : 초기 위치 설정
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do{
            // : 이미 생성된 방 리스트에서 랜덤하게 방 하나를 선택
			int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));

            // : 선택한 방의 x, y 좌표를 캡처
			x = (int) takenPositions[index].x;
			y = (int) takenPositions[index].y;

            if (Random.value < 0.5f) // : 수직 또는 수평 축 중 랜덤하게 선택
            {
                y += (Random.value < 0.5f) ? 1 : -1; // : 그 축에서 양수 또는 음수 방향 랜덤 선택
            }
            else
            {
                x += (Random.value < 0.5f) ? 1 : -1;
            }

            // : 새로 계산한 위치를 checkingPos에 저장
			checkingPos = new Vector2(x,y);

        // : 새 위치가 이미 사용 중이거나 그리드의 범위를 벗어나면, 새 위치를 다시 계산
        }while(takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);

        // : 새 위치를 반환
		return checkingPos;
    }

    // 새로운 위치를 결정하는 두 번째 방법, 첫 번째와 약간의 차이가 있음
    Vector2 SelectiveNewPosition()
    {
        int index = 0, inc = 0;
		int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do{
            inc = 0;
			do{ 
				// : 이웃 방이 한 개만 있는 방을 선택하는 방식으로 시작
				index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
				inc ++;
			}while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);

            // : 선택한 방의 x, y 좌표를 캡처
			x = (int) takenPositions[index].x;
			y = (int) takenPositions[index].y;

            if (Random.value < 0.5f) // : 수직 또는 수평 축 중 랜덤하게 선택
            {
                y += (Random.value < 0.5f) ? 1 : -1; // : 그 축에서 양수 또는 음수 방향 랜덤 선택
            }
            else
            {
                x += (Random.value < 0.5f) ? 1 : -1;
            }

            // : 새로 계산한 위치를 checkingPos에 저장
			checkingPos = new Vector2(x,y);

        // : 새 위치가 이미 사용 중이거나 그리드의 범위를 벗어나면, 새 위치를 다시 계산
        }while(takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);

        // 무한 루프 방지를 위해 최대 100번만 반복
		if (inc >= 100)		print("Error: could not find position with only one neighbor");

        // : 새 위치를 반환
		return checkingPos;
    }

    Vector2[] directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };
    // : 특정 위치의 이웃 방의 수를 계산하는 함수
    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
    	int ret = 0;

        // : 주어진 위치의 오른쪽, 왼쪽, 위, 아래에 방이 있는지 확인
        foreach (Vector2 direction in directions)
        {
            if (usedPositions.Contains(checkingPos + direction))    ret++;
        }

        // : 이웃 방의 수를 반환
        return ret;
    }
    void SetRoomDoors()
    {
		int maxX = gridSizeX * 2;
    	int maxY = gridSizeY * 2;
	
    	for (int x = 0; x < maxX; x++) {
    	    for (int y = 0; y < maxY; y++) {
    	        Room room = rooms[x, y];
    	        if (room != null) 
                {
    	            // : 위쪽 방향의 문을 설정합니다. 
                    // : 현재 위치가 맵의 상단 경계를 넘지 않고, 위쪽에 방이 있는 경우에만 문을 만듭니다.
                    room.doorBot = y - 1 >= 0 && rooms[x, y - 1] != null;

                    // : 아래쪽 방향의 문을 설정합니다. 
                    // : 현재 위치가 맵의 하단 경계를 넘지 않고, 아래쪽에 방이 있는 경우에만 문을 만듭니다.
                    room.doorTop = y + 1 < maxY && rooms[x, y + 1] != null;

                    // : 왼쪽 방향의 문을 설정합니다.
                    // : 현재 위치가 맵의 왼쪽 경계를 넘지 않고, 왼쪽에 방이 있는 경우에만 문을 만듭니다.
                    room.doorLeft = x - 1 >= 0 && rooms[x - 1, y] != null;

                    // : 오른쪽 방향의 문을 설정합니다.
                    // : 현재 위치가 맵의 오른쪽 경계를 넘지 않고, 오른쪽에 방이 있는 경우에만 문을 만듭니다.
                    room.doorRight = x + 1 < maxX && rooms[x + 1, y] != null;
    	        }
    	    }
    	}
	}

    void DrawMap() 
    {
        foreach (Vector2 pos in takenPositions) 
        {
            Room room = rooms[(int)pos.x + gridSizeX, (int)pos.y + gridSizeY];
    
            if (room == null)
            {
                Debug.Log(null);
                continue;      
            }
    
            Vector2 drawPos = room.gridPosition;
            drawPos.x *= 16; 
            drawPos.y *= 8;  
    
            Debug.Log(drawPos);
            MapSpriteSelector mapper = Object.Instantiate(roomWhiteObj, drawPos, Quaternion.identity).GetComponent<MapSpriteSelector>();
            mapper.type = room.type;       
            mapper.up = room.doorTop;      
            mapper.down = room.doorBot;    
            mapper.right = room.doorRight; 
            mapper.left = room.doorLeft;   
    
            mapper.gameObject.transform.parent = mapRoot;
        }   
    }
}
