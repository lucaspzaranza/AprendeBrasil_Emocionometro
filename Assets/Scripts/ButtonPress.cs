using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public bool isPressing = false;
    public int direction = 0;

    public delegate void ArrowButtonPressed(int val);
    public static event ArrowButtonPressed OnArrowButtonPressed;

    public void SetIsPressing(bool val)
    {
        isPressing = val;
    }

    public void Update()
    {
        if (isPressing)
            OnArrowButtonPressed?.Invoke(direction);
    }
}
