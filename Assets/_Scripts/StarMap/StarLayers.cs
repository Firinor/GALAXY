using System;
using UnityEngine;

public class StarLayers : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] stars;
    [SerializeField]
    private BackGroundParalax[] starsParalax;
    [SerializeField]
    private SpriteRenderer clasters;
    [SerializeField]
    private SpriteRenderer galaxy;

    [SerializeField]
    private float starsAlfaMin;
    [SerializeField]
    private float starsAlfaMax;
    [SerializeField]
    private float clastersAlfaButtomMin;
    [SerializeField]
    private float clastersAlfaButtomMax;
    [SerializeField]
    private float clastersAlfaUperMin;
    [SerializeField]
    private float clastersAlfaUperMax;
    [SerializeField]
    private float galaxyAlfaMin;
    [SerializeField]
    private float galaxyAlfaMax;

    private void Awake()
    {
        CheckStarMapLayer(Camera.main.orthographicSize);
    }

    public void CheckStarMapLayer(float cameraZoom)
    {
        GalaxyLayerCheck(cameraZoom);
        ClastersLayerCheck(cameraZoom);
        StarsLayerCheck(cameraZoom);
    }

    private void StarsLayerCheck(float cameraZoom)
    {
        bool starsEnabled = cameraZoom < starsAlfaMax;
        if (starsEnabled != stars[0].enabled)
        {
            foreach (SpriteRenderer star in stars)
            {
                star.enabled = starsEnabled;
            }
        }

        if (!starsEnabled)
            return;

        if (cameraZoom > starsAlfaMin)
        {
            float alfa = (starsAlfaMax - cameraZoom) / (starsAlfaMax - starsAlfaMin);
            Color starColor = Color.Lerp(new Color(1, 1, 1, 0), Color.white, alfa);
            foreach (SpriteRenderer star in stars)
            {
                star.color = starColor;
            }
        }
        else
        {
            foreach (SpriteRenderer star in stars)
            {
                star.color = Color.white;
            }
        }

    }

    private void ClastersLayerCheck(float cameraZoom)
    {
        clasters.enabled = cameraZoom > clastersAlfaButtomMin  && cameraZoom < clastersAlfaUperMax;
        if (!clasters.enabled)
            return;

        if (cameraZoom > clastersAlfaUperMin)
        {
            float alfa = (clastersAlfaUperMax - cameraZoom) / (clastersAlfaUperMax - clastersAlfaUperMin);
            clasters.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, alfa);
        }
        else if (cameraZoom < clastersAlfaButtomMax)
        {
            float alfa = -(clastersAlfaButtomMin - cameraZoom) / (clastersAlfaButtomMax - clastersAlfaButtomMin);
            clasters.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, alfa);
        }
        else
            clasters.color = Color.white;
    }

    private void GalaxyLayerCheck(float cameraZoom)
    {
        galaxy.enabled = cameraZoom > galaxyAlfaMin;
        if (!galaxy.enabled)
            return;
        float alfa = (cameraZoom - galaxyAlfaMin) / (galaxyAlfaMax - galaxyAlfaMin);
        alfa = Math.Clamp(alfa, 0, 1);
        galaxy.color = new Color(1, 1, 1, alfa);
    }
    
    public void MoveStarsParallaxically()
    {
        foreach(BackGroundParalax stars in starsParalax)
        {
            stars.MoveBackgroundParallaxically();
        }
    }
}