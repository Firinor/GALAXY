using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CameraController : MonoBehaviour, IDragHandler, IScrollHandler
{
    [Inject]
    private Camera cam;
    private static CameraSetting cameraSetting;
    public static CameraSetting Setting => cameraSetting;
    private bool isLoadComplete = false;
    private static float zPosition => -10;
    private static float imageCoefficient => 100;

    [Inject]
    private async void GetSettings(ServerEmulator server)
    {
        cameraSetting = await Task.Run(() => server.GetCameraSetting());
        isLoadComplete = true;
    }

    internal void SetScale(float scale)
    {
        if (!isLoadComplete)
            return;

        cam.orthographicSize = Mathf.Clamp(scale, cameraSetting.MinZoom, cameraSetting.MaxZoom);
    }

    internal void SetPosition(Vector2 anchoredPosition)
    {
        if (!isLoadComplete)
            return;

        cam.transform.position = new Vector3(anchoredPosition.x, anchoredPosition.y, zPosition);
    }

    public void OnScroll(PointerEventData data)
    {
        //pointBeforeScaling
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(starMapSettings.RectTransform,
        //        Input.mousePosition, data.pressEventCamera, out Vector2 pointBeforeScaling);

        //Scaling
        float scale = ZoomScroll(Input.mouseScrollDelta);

        //pointAfterScaling
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(starMapSettings.RectTransform,
        //        Input.mousePosition, data.pressEventCamera, out Vector2 pointAfterScaling);

        //Vector2 delta = pointBeforeScaling - pointAfterScaling;

        //We shift the map to the mouse cursor
        //content.anchoredPosition -= delta;
        //cameraController.SetPosition(content.anchoredPosition);
    }
    public void OnDrag(PointerEventData data)
    {
        if (data.button != PointerEventData.InputButton.Left)
            return;

        cam.transform.position += new Vector3(
            -data.delta.x/imageCoefficient,
            -data.delta.y/imageCoefficient,
            0);        
    }

    private float ZoomScroll(Vector2 mouseScrollDelta)
    {
        cam.orthographicSize += Setting.ZoomStep * (mouseScrollDelta.y > 0 ? -1 : 1);
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, Setting.MinZoom, Setting.MaxZoom);
        return cam.orthographicSize;
    }
}
