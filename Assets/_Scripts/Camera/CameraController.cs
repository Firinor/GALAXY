using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CameraController : MonoBehaviour, IDragHandler, IScrollHandler
{
    [Inject]
    private Camera cam;
    [Inject]
    private StarLayers layers;
    private static CameraSetting cameraSetting;
    public static CameraSetting Setting => cameraSetting;
    private bool isLoadComplete = false;
    private static float imageCoefficient => 100;

    [Inject]
    private async void GetSettings(ServerEmulator server)
    {
        cameraSetting = await Task.Run(() => server.GetCameraSetting());
        isLoadComplete = true;
    }

    public void OnScroll(PointerEventData data)
    {
        if (!isLoadComplete)
            return;

        cam.orthographicSize *= 1 + Setting.ZoomStep * (data.scrollDelta.y > 0 ? -1 : 1);
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, Setting.MinZoom, Setting.MaxZoom);

        CheckCameraPosition(cam.transform.position);
        layers.CheckStarMapLayer(cam.orthographicSize);
    }

    public void OnDrag(PointerEventData data)
    {
        if (!isLoadComplete)
            return;

        if (data.button != PointerEventData.InputButton.Left)
            return;

        Vector3 newCameraPosition = cam.transform.position + new Vector3(
            -data.delta.x/imageCoefficient,
            -data.delta.y/imageCoefficient,
            0);

        CheckCameraPosition(newCameraPosition);
        
    }

    private void CheckCameraPosition(Vector3 newCameraPosition)
    {
        float cameraZoom = cam.orthographicSize;

        if (newCameraPosition.x + cameraZoom > Setting.GalaxyEdge)
            newCameraPosition = new Vector3(Setting.GalaxyEdge - cameraZoom, newCameraPosition.y, newCameraPosition.z);
        if (newCameraPosition.x - cameraZoom < -Setting.GalaxyEdge)
            newCameraPosition = new Vector3(-Setting.GalaxyEdge + cameraZoom, newCameraPosition.y, newCameraPosition.z);
        if (newCameraPosition.y + cameraZoom > Setting.GalaxyEdge)
            newCameraPosition = new Vector3(newCameraPosition.x, Setting.GalaxyEdge - cameraZoom, newCameraPosition.z);
        if (newCameraPosition.y - cameraZoom < -Setting.GalaxyEdge)
            newCameraPosition = new Vector3(newCameraPosition.x, -Setting.GalaxyEdge + cameraZoom, newCameraPosition.z);

        cam.transform.position = newCameraPosition;

        layers.MoveStarsParallaxically();
    }
}