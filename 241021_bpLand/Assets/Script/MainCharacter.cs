using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public static MainCharacter instacne;

    [SerializeField]
    [Range(0.1f, 10f)]
    public float moveSpeed = 2f;
    public bool isMove = false;
    private void Awake()
    {
        instacne = this;
    }
    void Start()
    {
        JoyStick.stickAction += CharMove;
    }

    void Update()
    {
    }
    public void CharMove(Vector2 pos)
    {
        transform.position += (Vector3)pos * moveSpeed * Time.deltaTime;
        isMove = !(pos == Vector2.zero);
    }
}
