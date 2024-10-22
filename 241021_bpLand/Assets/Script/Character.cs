using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 10f)]
    public float moveSpeed = 2f;
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
    }
}
