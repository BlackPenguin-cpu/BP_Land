using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractiveObj : MonoBehaviour
{
    [Header("Managing on Inspector")]
    [Min(0.1f)]
    public float durationForInteractive = 1.2f;
    [Min(0.1f)]
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

    private Image loadingBar;
    [SerializeField]
    private Vector3 loadingBarPos = Vector3.up;

    protected virtual void Start()
    {
        refRenderer = GetComponent<Renderer>();
        refCharacter = MainCharacter.instacne;

        outlineMat = Resources.Load<Material>("Material/Outline");
        originMat = refRenderer.material;
        JoyStick.stickAction += OnDetectPlayerMove;
    }

    private void OnDetectPlayerMove(Vector2 vec)
    {
        if (vec != Vector2.zero)
        {
            curDurationForInteractive = 0;
            OnResetInteraction();
        }
    }
    protected virtual void Update()
    {
        if (!isAlreadyInteraction && curDurationForInteractive >= durationForInteractive)
        {
            OnInteractionAction();
            isAlreadyInteraction = true;
            loadingBar.gameObject.SetActive(false);
        }
        TimerFunc();
    }
    protected virtual void TimerFunc()
    {
        if (Vector3.Distance(refCharacter.transform.position, transform.position) < canInteractiveDistance)
        {
            curDurationForInteractive += Time.deltaTime;
            if (curDurationForInteractive > 0.1f)
            {
                OnStartInteraction();
            }
        }
        else
        {
            if (isAlreadyInteraction)
            {
                isAlreadyInteraction = false;
                NpcTextBox.Instance.ResetText();
            }
            OnResetInteraction();
        }
    }
    protected virtual void OnStartInteraction()
    {
        refRenderer.material = outlineMat;
        isSelcet = true;
        if (loadingBar == null)
        {
            loadingBar = NpcLoadingBar.instance.CreateLoadingBar();
            loadingBar.rectTransform.position = Camera.main.WorldToScreenPoint(transform.position + loadingBarPos);
        }
        else
        {
            var value = EaseInOutCubic(curDurationForInteractive / durationForInteractive);
            loadingBar.fillAmount = value;
        }
    }
    private float EaseInOutCubic(float x)
    {
        return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }

    protected virtual void OnResetInteraction()
    {
        isSelcet = false;
        curDurationForInteractive = 0;
        refRenderer.material = originMat;

        if (loadingBar != null) Destroy(loadingBar.gameObject);
    }
    protected abstract void OnInteractionAction();
}
