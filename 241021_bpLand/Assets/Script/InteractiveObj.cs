using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class InteractiveObj : MonoBehaviour
{
    [Header("Managing on Inspector")]
    public float durationForInteractive;
    public float canInteractiveDistance = 2;

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
    private float EaseInOutCubic(float x)
    {
        return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
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
