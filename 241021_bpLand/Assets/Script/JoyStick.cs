using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static Action<Vector2> stickAction;
    [Range(0.1f, 2f)]
    public float stickSensitive = 1f;
    [SerializeField]
    private float stickDistance;

    [Header("UI Option")]
    public Image stick;
    private RectTransform rect;
    private void Awake()
    {
    }
    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        stickAction.Invoke(stick.rectTransform.anchoredPosition);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        NowStickPosSet(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        NowStickPosSet(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        NowStickPosSet(rect.position);
    }
    private void NowStickPosSet(Vector2 pos)
    {
        var inputDir = pos - rect.anchoredPosition;
        var clampedDir = inputDir.magnitude < stickDistance ?
            inputDir : inputDir.normalized * stickDistance;

        stick.rectTransform.anchoredPosition = clampedDir;
    }
}
