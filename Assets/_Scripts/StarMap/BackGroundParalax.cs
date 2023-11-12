using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundParalax : MonoBehaviour
{
    public float paralaxSpeed;

    public Transform[] backgrounds;
    Transform cam;
    float camOldPositionX;
    float posDelta;

    float distanceToSlideBackground;

    void Start()
    {
        cam = Camera.main.transform;
        camOldPositionX = cam.position.x;
        distanceToSlideBackground = (backgrounds[1].position.x - backgrounds[0].position.x) * backgrounds.Length;


    }

    void Update()
    {
        MoveBackgroundParallaxically();
        CheckBorders();
    }

    void MoveBackgroundParallaxically()
    {
        posDelta = cam.position.x - camOldPositionX;
        transform.Translate(posDelta * paralaxSpeed, 0, 0);
        camOldPositionX = cam.position.x;
    }

    void CheckBorders()
    {
        for (int q = 0; q < backgrounds.Length; q++)
        {
            float diff = backgrounds[q].position.x - cam.position.x;
            if (diff > 0)
            {
                if (2 * diff > distanceToSlideBackground) backgrounds[q].Translate(-distanceToSlideBackground, 0f, 0f);
            }
            else
            {
                if (2 * diff < -distanceToSlideBackground) backgrounds[q].Translate(distanceToSlideBackground, 0f, 0f);
            }
        }
    }
}
