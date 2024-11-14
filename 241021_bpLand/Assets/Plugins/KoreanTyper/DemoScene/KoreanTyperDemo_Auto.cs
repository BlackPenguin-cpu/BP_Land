using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using KoreanTyper;                                                  // Add KoreanTyper namespace | 네임 스페이스 추가

//===================================================================================================================
//  Auto Demo
//  자동으로 글자가 입력되는 데모
//===================================================================================================================
public class KoreanTyperDemo_Auto : MonoBehaviour
{
    public IEnumerator TypingText(TextMeshProUGUI tmpUI, string printText)
    {
        //=======================================================================================================
        // Initializing | 초기화
        //=======================================================================================================
        tmpUI.text = "";

        //=======================================================================================================
        //  Typing effect | 타이핑 효과
        //=======================================================================================================
        int strTypingLength = printText.GetTypingLength();

        for (int i = 0; i <= strTypingLength; i++)
        {
            tmpUI.text = printText.Typing(i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}

