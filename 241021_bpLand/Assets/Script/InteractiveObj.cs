using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class InteractiveObj : MonoBehaviour
{
    [Header("Managing on Inspector")]
    public float durationForInteractive;
    public float canInteractiveDistance;

    private MainCharacter refCharacter;
    private Renderer refRenderer;

    [Header("For Debugging")]
    [SerializeField]
    private float curDurationForInteractive;
    [SerializeField]
    private bool isSelcet;
    [SerializeField]
    private bool isAlreadyInteraction;

    private Material[] originMats;
    private Material outlineMat;

    protected virtual void Start()
    {
        refRenderer = GetComponent<Renderer>();
        refCharacter = MainCharacter.character;

        originMats = refRenderer.materials;
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
            refRenderer.materials = originMats;
        }
    }
    protected abstract void OnInteractionAction();
}
