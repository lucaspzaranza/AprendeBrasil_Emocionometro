using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [Header("Screen 1")]
    [SerializeField] private GameObject screen1;
    [SerializeField] private GameObject screen1Bears;

    [Header("Screen 2")]
    [SerializeField] private GameObject screen2;
    
    [Header("Screen 3")]
    [SerializeField] private GameObject screen3;
    [SerializeField] private GameObject screen3PopupPanel;
    [SerializeField] private GameObject screen3AudioAndPlayButtonPanel;
    private int test = 1;

    private void Start()
    {
        Screen1AnimateBears();
    }

    public void Screen1ToggleActivation(bool value)
    {
        screen1.SetActive(value);
    }

    public void Screen1AnimateBears()
    {
        screen1Bears.transform.DOScale(3f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void Screen2ToggleActivation(bool value)
    {
        screen2.SetActive(value);
    }

    public void Screen3ToggleActivation(bool value)
    {
        screen3.SetActive(value);
    }

    public void Screen3ShowPopup(float duration)
    {
        screen3PopupPanel.transform.DOMoveY(0f, duration);
        AudioManager.instance.PlayAudio(0.5f, 1, 1);
    }
}
