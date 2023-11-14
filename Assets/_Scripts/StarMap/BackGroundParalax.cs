using UnityEngine;

public class BackGroundParalax : MonoBehaviour
{
    public float paralaxSpeed;

    public Transform[] backgrounds;
    private Transform cam;
    private float camOldPositionX, camOldPositionY;
    private float posDeltaX, posDeltaY;

    private void Start()
    {
        cam = Camera.main.transform;
        camOldPositionX = cam.position.x;
        camOldPositionY = cam.position.y;
    }

    public void MoveBackgroundParallaxically()
    {
        posDeltaX = cam.position.x - camOldPositionX;
        posDeltaY = cam.position.y - camOldPositionY;
        transform.Translate(posDeltaX * paralaxSpeed, posDeltaY * paralaxSpeed, 0);
        camOldPositionX = cam.position.x;
        camOldPositionY = cam.position.y;
    }
}
