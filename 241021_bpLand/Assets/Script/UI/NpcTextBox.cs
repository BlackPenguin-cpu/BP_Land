using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Febucci.UI;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using Cysharp.Threading.Tasks;

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

    private CancellationTokenSource source;

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

    public async UniTask StartText(List<EffectTextInfo> curTextList)
    {
        textList = curTextList;
        source.Cancel();
        await StartText();
    }

    private async UniTask StartText()
    {
        textBoxObj.SetActive(true);
        curTmpAnimator = tmpUI.GetComponent<TextAnimator_TMP>();
        var curTextList = textList;
        foreach (var curTextInfo in curTextList)
        {
            for (int i = 0; i < curTextInfo.textAnimStr.Count; i++)
            {
                curTmpAnimator.DefaultBehaviorsTags = curTextInfo.textAnimStr.ToArray();
            }

            await textAuto.TypingText(tmpUI, curTextInfo.text).WithCancellation(cancellationToken: source.Token);
            await UniTask.Delay(2000).AttachExternalCancellation(cancellationToken: source.Token);

            curTmpAnimator.Behaviors.Initialize();
        }
    }
}