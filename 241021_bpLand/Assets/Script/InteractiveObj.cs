using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class InteractiveObj : MonoBehaviour
{
    [Header("Managing on Inspector")]
    public float durationForInteractive = 1.2f;
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

    private Material originMat;
    private Material outlineMat;

    protected virtual void Start()
    {
        refRenderer = GetComponent<Renderer>();
        refCharacter = MainCharacter.character;

        outlineMat = Resources.Load<Material>("Material/Outline");
        originMat = refRenderer.material;
        JoyStick.stickAction += OnDetectPlayerMove;
    }

    private void OnDetectPlayerMove(Vector2 vec)
    {
        if (vec != Vector2.zero)
        {
            curDurationForInteractive = 0;
        }
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
            refRenderer.material = outlineMat;
        }
        else
        {
            isSelcet = false;
            curDurationForInteractive = 0;
            refRenderer.material = originMat;
        }
    }
    protected abstract void OnInteractionAction();
}
