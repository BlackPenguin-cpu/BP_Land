using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class ChooseUI : MonoBehaviour
{
    public class ChooseSlotInfo
    {
        public string slotName;
        public Action onActiveAction;

        public ChooseSlotInfo(string name, Action action)
        {
            slotName = name;
            onActiveAction = action;
        }
    }

    [System.Serializable]
    private class ChooseSlotUIInfo
    {
        public Image image;
        [FormerlySerializedAs("text")] public TextMeshProUGUI tmp;
    }

    [SerializeField] private List<ChooseSlotUIInfo> chooseUiList;
    public List<ChooseSlotInfo> chooseSlotInfos = new List<ChooseSlotInfo>(4);
    public Image loadingCircle;

    public List<Color> colorPallete;

    private float curChooseActionDuration = 0;
    private readonly float chooseActionDuration = 3;
    private Vector2 curVec;

    public void ChooseSlotReset()
    {
        chooseSlotInfos.Clear();
    }

    public void ChooseSlotAdd(List<string> infoText, List<Action> action)
    {
        for (int i = 0; i < infoText.Count; i++)
        {
            chooseSlotInfos.Add(new ChooseSlotInfo(infoText[i], action[i]));
        }

        InteractionStart();
    }

    private void InteractionStart()
    {
        for (int i = 0; i < chooseSlotInfos.Count; i++)
        {
            if (chooseSlotInfos[i].slotName != null)
            {
                chooseUiList[i].image.color = colorPallete[i];
                chooseUiList[i].tmp.text = chooseSlotInfos[i].slotName;
            }
            else
            {
                chooseUiList[i].image.color = colorPallete[4];
                chooseUiList[i].tmp.text = "x";
            }
        }
    }

    private void Start()
    {
        JoyStick.stickAction += SelectAxis;
    }

    private void SelectAxis(Vector2 vec)
    {
        if (curVec != vec || vec.normalized.x + vec.normalized.y >= 2)
            curChooseActionDuration = 0;
        curVec = vec;

        if (vec.normalized.x + vec.normalized.y < 2)
            curChooseActionDuration += Time.deltaTime;

        if (curChooseActionDuration >= chooseActionDuration)
        {
            if (vec == Vector2.right)
            {
                chooseSlotInfos[0].onActiveAction?.Invoke();
            }
            else if (vec == Vector2.left)
            {
                chooseSlotInfos[1].onActiveAction?.Invoke();
            }
            else if (vec == Vector2.up)
            {
                chooseSlotInfos[2].onActiveAction?.Invoke();
            }
            else if (vec == Vector2.down)
            {
                chooseSlotInfos[3].onActiveAction?.Invoke();
            }
        }
    }
}