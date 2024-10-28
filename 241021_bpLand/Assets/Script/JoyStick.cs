using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static Action<Vector2> stickAction;
    [Range(0.1f, 2f)]
    public float stickSensitive = 1f;
    [Header("UI Option")]
    public Image stick;

    [SerializeField]
    private float stickDistance;
    private Vector3 stickPosition;
    private RectTransform rect;
    private void Awake()
    {
    }
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        stickPosition = rect.position;

    }
    private void Update()
    {
        stickAction.Invoke(stick.rectTransform.anchoredPosition);

        InputFuncForKeyboard();
        NowStickPosSet(stickPosition);
    }

    private void InputFuncForKeyboard()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        if (!Input.GetMouseButton(0) && Input.touchCount < 1)
        {
            stickPosition = rect.anchoredPosition + new Vector2(hor, ver) * 100;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        stickPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        stickPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        stickPosition = rect.position;
    }
    private void NowStickPosSet(Vector2 pos)
    {
        var inputDir = pos - rect.anchoredPosition;
        var clampedDir = inputDir.magnitude < stickDistance ?
            inputDir : inputDir.normalized * stickDistance;

        stick.rectTransform.anchoredPosition = clampedDir;
    }
}
