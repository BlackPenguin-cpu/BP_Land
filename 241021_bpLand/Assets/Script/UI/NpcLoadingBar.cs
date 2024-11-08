using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcLoadingBar : MonoBehaviour
{
    [SerializeField]
    private Image loadingBar;
    void Start()
    {

    }
    public Image CreateLoadingBar()
    {

        loadingBar.fillAmount = 0;

    }

    void Update()
    {

    }
}
