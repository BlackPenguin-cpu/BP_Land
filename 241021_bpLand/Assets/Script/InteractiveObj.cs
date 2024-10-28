using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class InteractiveObj : MonoBehaviour
{
    public float durationForInteractive;
    public float canInteractiveDistance;

    private MainCharacter refCharacter;

    [SerializeField]
    private float curDurationForInteractive;
    [SerializeField]
    private bool isSelcet;
    [SerializeField]
    private bool isAlreadyInteraction;

    protected virtual void Awake()
    {
        refCharacter = MainCharacter.character;
    }
    protected virtual void Update()
    {
        if (!isAlreadyInteraction && curDurationForInteractive >= durationForInteractive)
        {
            OnInteractionAction();
            isAlreadyInteraction = true;
        }
        TimerFunc();
    }
    protected virtual void TimerFunc()
    {
        if (Vector3.Distance(refCharacter.transform.position, transform.position) < canInteractiveDistance)
        {
            curDurationForInteractive += Time.deltaTime;
            isSelcet = true;
        }
        else
        {
            isSelcet = false;
            curDurationForInteractive = 0;
        }
    }
    protected abstract void OnInteractionAction();
}
