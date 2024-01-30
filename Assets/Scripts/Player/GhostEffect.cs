using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    public float ghostDelay;
    private float ghostDelaySeconds;

    public GameObject ghost;
    public bool makeGhost = false;

    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    void Update()
    {
        if (makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
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

                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 0.5f);
            }
        }
    }
}
