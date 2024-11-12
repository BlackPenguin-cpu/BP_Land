using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public enum ECameraState
    {
        OnPlayer,
        End
    }
    public MainCamera Instance
    {
        get { return instance; }
        private set { instance = value; }
    }
    private static MainCamera instance;
    private MainCharacter mainChar;

    public ECameraState cameraState;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        mainChar = MainCharacter.instacne;
    }
    private void Update()
    {
        if (cameraState == ECameraState.OnPlayer)
        {
            FollowPlayer();
        }
    }
    private void FollowPlayer()
    {
        Vector3 vec = Vector3.Lerp(transform.position, mainChar.transform.position, Time.deltaTime * 10);
        vec.z = -10;
        transform.position = vec;
    }
}
