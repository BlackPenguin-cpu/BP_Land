using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class InteractiveObj : MonoBehaviour
{
    [System.Serializable]
    public class InteractiveInfo
    {
        [Header("Managing on Inspector")] [Min(0.1f)]
        public float durationForInteractive = 1.2f;

        [Min(0.1f)] public float canInteractiveDistance = 2;

        [Header("For Debugging")] [ReadOnly] public float curDurationForInteractive;
        [ReadOnly] public bool isSelcet;
        [ReadOnly] public bool isAlreadyInteraction;

        public Vector3 loadingBarPos = Vector3.up;
        [FormerlySerializedAs("isLockMove")] public bool isLockMoving;
    }

    private Material originMat;
    private Material outlineMat;

    private Image loadingBar;
    private MainCharacter refCharacter;
    private Renderer refRenderer;

    public InteractiveInfo interactiveInfo;

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
        if (vec == Vector2.zero) return;
        if (!MainCharacter.instacne.isCanMoving) return;

        interactiveInfo.curDurationForInteractive = 0;
        OnResetInteraction();
    }

    protected virtual void Update()
    {
        if (!interactiveInfo.isAlreadyInteraction
            && interactiveInfo.curDurationForInteractive >= interactiveInfo.durationForInteractive)
        {
            if (interactiveInfo.isLockMoving)
            {
                MainCharacter.instacne.isCanMoving = false;
            }
            OnInteractionAction();
            interactiveInfo.isAlreadyInteraction = true;
            loadingBar.gameObject.SetActive(false);
        }

        TimerFunc();
    }

    protected virtual void TimerFunc()
    {
        if (Vector3.Distance(refCharacter.transform.position, transform.position) <
            interactiveInfo.canInteractiveDistance)
        {
            if (MainCharacter.nowInteractObj != this && MainCharacter.nowInteractObj != null) return;

            MainCharacter.nowInteractObj = this;
            interactiveInfo.curDurationForInteractive += Time.deltaTime;
            if (interactiveInfo.curDurationForInteractive > 0.1f)
            {
                OnStartInteraction();
            }
        }
        else
        {
            if (MainCharacter.nowInteractObj == this)
                MainCharacter.nowInteractObj = null;

            if (interactiveInfo.isAlreadyInteraction)
                OnInteractionOver();

            OnResetInteraction();
        }
    }

    protected virtual void OnInteractionOver()
    {
        interactiveInfo.isAlreadyInteraction = false;
        interactiveInfo.curDurationForInteractive = 0;
        NpcTextBox.Instance.ResetText();

        if (interactiveInfo.isLockMoving)
        {
            MainCharacter.instacne.isCanMoving = true;
        }
    }

    protected virtual void OnStartInteraction()
    {
        refRenderer.material = outlineMat;
        interactiveInfo.isSelcet = true;

        if (!loadingBar)
        {
            loadingBar = NpcLoadingBar.instance.CreateLoadingBar();
            loadingBar.rectTransform.position =
                Camera.main.WorldToScreenPoint(transform.position + interactiveInfo.loadingBarPos);
        }
        else
        {
            var value = EaseInOutCubic(interactiveInfo.curDurationForInteractive /
                                       interactiveInfo.durationForInteractive);
            loadingBar.rectTransform.position =
                Camera.main.WorldToScreenPoint(transform.position + interactiveInfo.loadingBarPos);
            loadingBar.fillAmount = value;
        }
    }

    private float EaseInOutCubic(float x)
    {
        return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }

    protected virtual void OnResetInteraction()
    {
        interactiveInfo.isSelcet = false;
        interactiveInfo.curDurationForInteractive = 0;

        if (refRenderer != null)
            refRenderer.material = originMat;

        if (loadingBar != null)
            Destroy(loadingBar.gameObject);
    }

    protected abstract void OnInteractionAction();
}