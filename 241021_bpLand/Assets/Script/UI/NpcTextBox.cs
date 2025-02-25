using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using TMPro;
using Febucci.UI;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NpcTextBox : MonoBehaviour
{
    public static NpcTextBox Instance { get; private set; }

    [System.Serializable]
    public class EffectTextInfo
    {
        [TextArea] public string text;

        public List<ETextAnim> textAnimsStrList;

        public string[] GetAnimArray()
        {
            return textAnimsStrList.Select(animTxt => animTxt.ToString()).ToArray();
        }

        public enum ETextAnim
        {
            bounce,
            dangle,
            fade,
            pend,
            rainb,
            rot,
            shake,
            incr,
            slide,
            swing,
            wave,
            wiggle,
        }
    }


    public List<EffectTextInfo> textList = new List<EffectTextInfo>();
    public KoreanTyperDemo_Auto textAuto;
    public TextMeshProUGUI tmpUI;

    private TextAnimator_TMP curTmpAnimator;

    [SerializeField] private GameObject textBoxObj;

    public void Awake()
    {
        Instance = this;
    }


    public void ResetText()
    {
        textBoxObj.SetActive(false);

        tmpUI.text = "";
        curTmpAnimator.DefaultBehaviorsTags = Array.Empty<string>();
    }

    public async UniTask StartText(List<EffectTextInfo> curTextList)
    {
        textList = curTextList;
        await StartText();
        ResetText();
    }

    private async UniTask StartText()
    {
        textBoxObj.SetActive(true);
        curTmpAnimator = tmpUI.GetComponent<TextAnimator_TMP>();
        var curTextList = textList;
        foreach (var curTextInfo in curTextList)
        {
            curTmpAnimator.DefaultBehaviorsTags = curTextInfo.GetAnimArray();

            await textAuto.TypingText(tmpUI, curTextInfo.text);
            await UniTask.Delay(2000);

            curTmpAnimator.DefaultBehaviorsTags = Array.Empty<string>();
        }
    }
}