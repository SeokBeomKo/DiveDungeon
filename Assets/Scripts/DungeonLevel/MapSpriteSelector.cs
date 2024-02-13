using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpriteSelector : MonoBehaviour
{
    public Sprite spU, spD, spR, spL, spUD, spRL, spUR, spUL, spDR, spDL, spULD, spRUL, spDRU, spLDR, spUDRL;
    public RoomType type;
    public Color enterColor, roomColor, shopColor, exitColor;
    public Directions direction;
    
    private Color mainColor;
    private SpriteRenderer rend;

    Dictionary<RoomType, Color> roomTypeToColor;
    Dictionary<Directions, Sprite> directionToSprite;

    private void Awake() 
    {
        InitializeDictionary();
    }

    void Start () 
    {
        rend = GetComponent<SpriteRenderer>();
        PickSprite();
        PickColor();
    }

    void InitializeDictionary()
    {
        roomTypeToColor = new Dictionary<RoomType, Color>()
        {
            { RoomType.ENTER, enterColor },
            { RoomType.ROOM, roomColor },
            { RoomType.SHOP, shopColor },
            { RoomType.EXIT, exitColor }
        };
        directionToSprite = new Dictionary<Directions, Sprite>()
        {
            { Directions.UP | Directions.DOWN | Directions.RIGHT | Directions.LEFT, spUDRL },
            { Directions.DOWN | Directions.RIGHT | Directions.UP, spDRU },
            { Directions.UP | Directions.LEFT | Directions.DOWN, spULD },
            { Directions.UP | Directions.DOWN, spUD },
            { Directions.RIGHT | Directions.UP | Directions.LEFT, spRUL },
            { Directions.UP | Directions.RIGHT, spUR },
            { Directions.UP | Directions.LEFT, spUL },
            { Directions.UP, spU },
            { Directions.LEFT | Directions.DOWN | Directions.RIGHT, spLDR },
            { Directions.DOWN | Directions.RIGHT, spDR },
            { Directions.DOWN | Directions.LEFT, spDL },
            { Directions.DOWN, spD },
            { Directions.RIGHT | Directions.LEFT, spRL },
            { Directions.RIGHT, spR },
            { Directions.LEFT, spL }
        };
    }

    void PickSprite() 
    { 
        rend.sprite = directionToSprite.ContainsKey(direction) ? directionToSprite[direction] : null;
    }

    void PickColor() 
    {
        // 방의 타입에 따라 색을 변경합니다.
        mainColor = roomTypeToColor.ContainsKey(type) ? roomTypeToColor[type] : roomColor;
        rend.color = mainColor;
    }
}
