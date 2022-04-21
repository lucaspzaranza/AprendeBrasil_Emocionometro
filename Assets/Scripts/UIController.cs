using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum EmotionType
{
    Fear = 0,
    Joy = 1,
    Anger = 2,
    Calm = 3,
    Sad = 4
}

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public int selectedBearIndex = 0;
    public float buttonPunchDuration;

    [Header("Screen 1")]
    [SerializeField] private GameObject screen1;
    [SerializeField] private GameObject screen1MobileBears;
    [SerializeField] private GameObject screen1DesktopBears;

    [Header("Screen 2")]
    [SerializeField] private GameObject screen2;
    
    [Header("Screen 3")]
    [SerializeField] private GameObject screen3;
    [SerializeField] private GameObject screen3PopupPanel;
    [SerializeField] private GameObject screen3DesktopPopupPanel;
    [SerializeField] private GameObject screen3AudioAndPlayButtonPanel;

    [Header("Screen 4")]
    public GameObject screen4;
    public GameObject screen4Desktop;
    [SerializeField] private GameObject screen4ToyMachineButton;
    [SerializeField] private Animator screen4DesktopToyMachineButtonAnimator;
    [SerializeField] private Animator screen4ToyMachineButtonAnimator;
    public delegate void ToyMachineButtonPressed();
    public static event ToyMachineButtonPressed OnToyMachineButtonPressed;

    [Header("Screen 5")]
    public GameObject screen5;
    public GameObject screen5Desktop;

    [Header("Screen 8")]
    public GameObject screenDesktop8SelectedColorOutline;
    public GameObject screenMobile8SelectedColorOutline;

    private GameObject touchedButton;

    public bool IsDesktop => Screen.width > Screen.height;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

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
        if(IsDesktop)
            screen1DesktopBears.transform.DOScale(0.5f, 1f).SetLoops(-1, LoopType.Yoyo);
        else
            screen1MobileBears.transform.DOScale(1.5f, 1f).SetLoops(-1, LoopType.Yoyo);
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
    }

    public void Screen3DesktopShowPopup(float duration)
    {
        screen3DesktopPopupPanel.transform.DOMoveY(0f, duration);
    }

    public void Screen4PressToyMachineButton(bool isDesktop)
    {
        if(isDesktop)
            screen4DesktopToyMachineButtonAnimator.SetTrigger("toy_machine_press");
        else
            screen4ToyMachineButtonAnimator.SetTrigger("toy_machine_press");
        OnToyMachineButtonPressed?.Invoke();
    }

    public void ScaleButton(GameObject gobj)
    {
        touchedButton = gobj;
        gobj.GetComponent<EventTrigger>().enabled = false;
        Vector2 halfSize = gobj.transform.localScale / 2;
        gobj.transform.DOPunchScale(halfSize, buttonPunchDuration, 5, 0f);
        Invoke(nameof(ActivateButtonComponent), buttonPunchDuration); 
    }

    private void ActivateButtonComponent()
    {
        touchedButton.GetComponent<EventTrigger>().enabled = true;
    }

    public void SelectMobileCurrentcolor(GameObject colorObject)
    {
        screenMobile8SelectedColorOutline.transform.localPosition = colorObject.transform.localPosition;
    }

    public void SelectDesktopCurrentcolor(GameObject colorObject)
    {
        screenDesktop8SelectedColorOutline.transform.localPosition = colorObject.transform.localPosition;
    }
}
