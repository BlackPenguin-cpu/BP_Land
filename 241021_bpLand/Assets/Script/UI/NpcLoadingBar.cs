using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcLoadingBar : MonoBehaviour
{
    public static NpcLoadingBar instance;
    [SerializeField]
    private Image loadingBar;
    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        loadingBar = Resources.Load<Image>("Prefab/NpcLoadingBar");
    }
    public Image CreateLoadingBar()
    {
        var obj = Instantiate(loadingBar);
        obj.fillAmount = 0;
        obj.transform.SetParent(transform);

        return obj;
    }
}
