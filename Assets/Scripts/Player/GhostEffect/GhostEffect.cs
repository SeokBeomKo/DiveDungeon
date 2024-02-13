using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    public float ghostDelay;
    private float ghostDelayTime;
    public float deleteTime;
    public GameObject ghost;
    public bool makeGhost = false;

    void Start()
    {
        ghostDelayTime = ghostDelay;
    }

    void Update()
    {
        if (makeGhost)
        {
            if (ghostDelayTime > 0)
            {
                ghostDelayTime -= Time.deltaTime;
            }
            else
            {
                // Generate a ghost
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
               
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;

                bool isFlipX = GetComponent<SpriteRenderer>().flipX;
                currentGhost.GetComponent<SpriteRenderer>().flipX = isFlipX;
                //currentGhost.transform.localScale = this.transform.localScale;

                ghostDelayTime = ghostDelay;
                Destroy(currentGhost, deleteTime);
            }
        }
    }
}
