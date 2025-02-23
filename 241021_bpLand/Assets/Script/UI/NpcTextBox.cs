using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;

public class NpcTextBox : MonoBehaviour
{
    public static NpcTextBox Instance { get; private set; }

    [System.Serializable]
    public class EffectTextInfo
    {
        [TextArea] public string text;
        
        [Dropdown("GetTextAnimValues")]
        public string[] textAnimStr;

        private DropdownList<string> GetTextAnimValues()
        {
            return new DropdownList<string>()
            {
                { "bounce", "bounce" },
                { "Dangle", "dangle" },
                { "Fade", "fade" },
                { "Pend", "pend" },
                { "Rainbow", "rainb" },
                { "Rotation", "rot" },
                { "Shake", "shake" },
                { "Increase", "incr" },
                { "Slide", "slide" },
                { "Swing", "swing" },
                { "Wave", "wave" },
                { "Wiggle", "wiggle" },
            };
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
            // for (int i = 0; i < curTextInfo.textAnimStr.Count; i++)
            // {
            //     curTmpAnimator.DefaultBehaviorsTags = curTextInfo.textAnimStr.ToArray();
            // }

            await textAuto.TypingText(tmpUI, curTextInfo.text);
            await UniTask.Delay(2000);

            curTmpAnimator.DefaultBehaviorsTags = Array.Empty<string>();
        }
    }
}