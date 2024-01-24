using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpriteSelector : MonoBehaviour
{
    public Sprite spU, spD, spR, spL, spUD, spRL, spUR, spUL, spDR, spDL, spULD, spRUL, spDRU, spLDR, spUDRL;
    public bool up, down, left, right;
    public RoomType type;
    public Color enterColor, roomColor, shopColor, exitColor;
    private Color mainColor;
    private SpriteRenderer rend;

    void Start () 
    {
        rend = GetComponent<SpriteRenderer>();
        PickSprite();
        PickColor();
    }

    void PickSprite() 
    { 
        if (up) {
            if (down) {
                if (right) {
                    rend.sprite = left ? spUDRL : spDRU;
                } else {
                    rend.sprite = left ? spULD : spUD;
                }
            } else {
                if (right) {
                    rend.sprite = left ? spRUL : spUR;
                } else {
                    rend.sprite = left ? spUL : spU;
                }
            }
        } else if (down) {
            if (right) {
                rend.sprite = left ? spLDR : spDR;
            } else {
                rend.sprite = left ? spDL : spD;
            }
        } else {
            rend.sprite = right ? (left ? spRL : spR) : spL;
        }
    }

    void PickColor() 
    {
        // 방의 타입에 따라 색을 변경합니다.
        switch (type) 
        {
            case RoomType.ENTER:
                mainColor = enterColor;
                break;
            case RoomType.ROOM:
                mainColor = roomColor;
                break;
            case RoomType.SHOP:
                mainColor = shopColor;
                break;
            case RoomType.EXIT:
                mainColor = exitColor;
                break;
            default:
                mainColor = roomColor;
                break;
        }
        rend.color = mainColor;
    }
}
