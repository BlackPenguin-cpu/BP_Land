using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class ChooseUI : MonoBehaviour
{
    public static ChooseUI instance;

    [System.Serializable]
    private class ChooseSlotUIInfo
    {
        public Image image;
        public TextMeshProUGUI tmp;
    }

    [SerializeField] private List<ChooseSlotUIInfo> chooseUiList;
    public List<string> chooseSlotStringList;
    public Image loadingCircle;
    public GameObject chooseUIParent;

    public List<Color> colorPallete;

    private float curChooseActionDuration = 0;
    private readonly float chooseActionDuration = 3;
    private Vector2 curVec;
    private bool isEnd;

    private int returnSlotValue = -1;

    private void Start()
    {
        instance = this;
        JoyStick.stickAction += SelectAxis;
    }
    public async UniTask<int> ChooseSlotInfoAddAndStart(List<string> slotNames)
    {
        chooseSlotStringList = slotNames;

        await InteractionStart();
        await UniTask.WaitUntil(() => returnSlotValue != -1);
        int returnValue = returnSlotValue;
        returnSlotValue = -1;
        return returnValue;
    }

    private async UniTask InteractionStart()
    {
        isEnd = false;
        chooseUIParent.SetActive(true);
        for (int i = 0; i < chooseUiList.Count; i++)
        {
            if (chooseSlotStringList.Count > i && chooseSlotStringList[i] != null)
            {
                chooseUiList[i].image.color = colorPallete[i];
                chooseUiList[i].tmp.text = chooseSlotStringList[i];
            }
            else
            {
                chooseUiList[i].image.color = colorPallete[4];
                chooseUiList[i].tmp.text = "x";
            }
        }

        await UniTask.WaitUntil(() => isEnd);

        isEnd = false;
        chooseUIParent.SetActive(false);
    }

    private void SelectAxis(Vector2 vec)
    {
        var index = PosChangeNumber(vec);
        if (chooseSlotStringList.Count < index + 1) return;
        if (curVec != vec || index == -1 || chooseSlotStringList[index] == "x")
        {
            curVec = vec;
            curChooseActionDuration = 0;
            loadingCircle.fillAmount = 0;
            return;
        }

        curChooseActionDuration += Time.deltaTime;

        if (curChooseActionDuration >= 0.1f)
            loadingCircle.fillAmount = Util.EaseInOutCubic(curChooseActionDuration / chooseActionDuration);

        if (curChooseActionDuration >= chooseActionDuration)
        {
            returnSlotValue = index;
            ;
            isEnd = true;
        }
    }

    private int PosChangeNumber(Vector2 vec)
    {
        Vector2 thisVec2 = vec.normalized;
        int returnValue = -1;
        if (thisVec2 == Vector2.right)
            returnValue = 0;
        else if (thisVec2 == Vector2.left)
            returnValue = 1;
        else if (thisVec2 == Vector2.up)
            returnValue = 2;
        else if (thisVec2 == Vector2.down)
            returnValue = 3;

        return returnValue;
    }
}