using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Febucci.UI;
using System.Linq;

public class NpcTextBox : MonoBehaviour
{
    public static NpcTextBox Instance { get; private set; }

    [System.Serializable]
    public class EffectTextInfo
    {
        public string text;
        public List<string> textAnimStr;
    }

    public List<EffectTextInfo> textList = new List<EffectTextInfo>();
    public KoreanTyperDemo_Auto textAuto;
    public TextMeshProUGUI tmpUI;

    private TextAnimator_TMP curTmpAnimator;
    private Coroutine textCoroutine;

    [SerializeField] private GameObject textBoxObj;

    public void Awake()
    {
        Instance = this;
    }


    public void ResetText()
    {
        textBoxObj.SetActive(false);

        if (textCoroutine != null) StopAllCoroutines();
        curTmpAnimator?.Behaviors.Initialize();
        tmpUI.text = "";
    }

    public IEnumerator StartText(List<EffectTextInfo> curTextList)
    {
        textList = curTextList;
        if (textCoroutine != null) StopAllCoroutines();
        yield return textCoroutine = StartCoroutine(StartText());
    }

    public IEnumerator StartText()
    {
        textBoxObj.SetActive(true);
        curTmpAnimator = tmpUI.GetComponent<TextAnimator_TMP>();

        var waitSec = new WaitForSeconds(2f);
        foreach (var curTextInfo in textList)
        {
            for (int i = 0; i < curTextInfo.textAnimStr.Count; i++)
            {
                curTmpAnimator.DefaultBehaviorsTags = curTextInfo.textAnimStr.ToArray();
            }

            yield return StartCoroutine(textAuto.TypingText(tmpUI, curTextInfo.text));
            yield return waitSec;

            curTmpAnimator.Behaviors.Initialize();
        }
    }
}