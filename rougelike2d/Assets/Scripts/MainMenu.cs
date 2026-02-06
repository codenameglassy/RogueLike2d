using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup fadeImg;
    // Start is called before the first frame update
    void Start()
    {
        fadeImg.alpha = 1.0f;
        fadeImg.DOFade(0, 1f);
    }

  
}
