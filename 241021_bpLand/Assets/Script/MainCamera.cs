using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public enum ECameraState
    {
        OnPlayer,
        ProductionObject,
        Stay,
        End
    }

    public static MainCamera instance;
    private MainCharacter mainChar;

    public ECameraState cameraState;

    public GameObject targetObj;

    private void Awake()
    {
        instance = this;
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
        else if (cameraState == ECameraState.ProductionObject)
        {
            FollowObject(targetObj);
        }
    }

    private void FollowPlayer()
    {
        FollowObject(mainChar.gameObject);
    }

    private void FollowObject(GameObject obj)
    {
        var vec = Vector3.Lerp(transform.position, obj.transform.position, Time.deltaTime * 10);
        vec.z = -10;
        transform.position = vec;
    }
}