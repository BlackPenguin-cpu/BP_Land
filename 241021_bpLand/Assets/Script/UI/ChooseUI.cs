using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class ChooseUI : MonoBehaviour
{
    public static ChooseUI Instance;

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
        public TextMeshProUGUI tmp;
    }

    [SerializeField] private List<ChooseSlotUIInfo> chooseUiList;
    public List<ChooseSlotInfo> chooseSlotInfos = new List<ChooseSlotInfo>();
    public Image loadingCircle;
    public GameObject ChooseUIParent;

    public List<Color> colorPallete;

    private float curChooseActionDuration = 0;
    private readonly float chooseActionDuration = 3;
    private Vector2 curVec;
    private bool isEnd;

    private void Start()
    {
        Instance = this;
        JoyStick.stickAction += SelectAxis;
    }

    public void ChooseSlotReset()
    {
        chooseSlotInfos.Clear();
    }

    public IEnumerator ChooseSlotInfoAddAndStart(List<string> infoText, List<Action> action)
    {
        for (int i = 0; i < infoText.Count; i++)
        {
            chooseSlotInfos.Add(new ChooseSlotInfo(infoText[i], action[i]));
        }

        yield return StartCoroutine(InteractionStart());
    }

    private IEnumerator InteractionStart()
    {
        isEnd = false;
        ChooseUIParent.SetActive(true);
        for (int i = 0; i < chooseUiList.Count; i++)
        {
            if (chooseSlotInfos.Count > i && chooseSlotInfos[i].slotName != null)
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

        while (!isEnd)
            yield return null;
        isEnd = false;
        ChooseUIParent.SetActive(false);
    }

    private void SelectAxis(Vector2 vec)
    {
        var index = PosChangeNumber(vec);
        if (curVec != vec || index == -1)
        {
            curChooseActionDuration = 0;
            loadingCircle.fillAmount = 0;
        }

        if(index == -1 || chooseSlotInfos[index].slotName == "x")
            return;
        
        curVec = vec;

        if (index != -1)
            curChooseActionDuration += Time.deltaTime;

        if (curChooseActionDuration >= 0.1f)
            loadingCircle.fillAmount = Util.EaseInOutCubic(curChooseActionDuration / chooseActionDuration);

        if (curChooseActionDuration >= chooseActionDuration)
        {
            if (!string.IsNullOrEmpty(chooseSlotInfos[index].slotName))
            {
                chooseSlotInfos[index].onActiveAction?.Invoke();
            }

            isEnd = true;
        }
    }

    private int PosChangeNumber(Vector2 vec)
    {
        int returnValue = -1;
        if (vec == Vector2.right)
            returnValue = 0;
        else if (vec == Vector2.left)
            returnValue = 1;
        else if (vec == Vector2.up)
            returnValue = 2;
        else if (vec == Vector2.down)
            returnValue = 3;

        return returnValue;
    }
}