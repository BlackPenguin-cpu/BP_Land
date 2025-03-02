using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public static MainCharacter instacne;
    public static InteractiveObj nowInteractObj;

    [SerializeField] [Range(0.1f, 10f)] public float moveSpeed = 2f;
    public bool isMove = false;
    public bool isCanMoving = true;
    public GameObject eyeObj;

    private void Awake()
    {
        instacne = this;
    }

    void Start()
    {
        JoyStick.stickAction += CharMove;
        JoyStick.stickAction += EyeMove;
    }

    public void EyeMove(Vector2 pos)
    {
        var curPos = pos.normalized;
        if (pos != Vector2.zero)
            eyeObj.transform.localPosition = curPos * 0.1f;
    }

    public void CharMove(Vector2 pos)
    {
        if (!isCanMoving) return;
        transform.position += (Vector3)pos * (moveSpeed * Time.deltaTime);
        isMove = !(pos == Vector2.zero);
    }
}