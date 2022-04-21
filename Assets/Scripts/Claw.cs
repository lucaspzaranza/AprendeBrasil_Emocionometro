using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

public class Claw : MonoBehaviour
{
    public float speed;
    public float clawXBounds;
    public float pistonYFinalPos = 0.375f;
    public Vector2 finalPistonPos;

    [SerializeField] private GameObject piston;
    [SerializeField] private Sprite closedClawSprite;
    [SerializeField] private Image clawHandImg;
    [SerializeField] private List<GameObject> bears;

    private bool triggerPistonAnim;
    private bool moveClawToBearXPos;
    private bool returnPistonToOriginPos;

    private float initPistonYPos = 0f;

    void Start()
    {
        ButtonPress.OnArrowButtonPressed += HandleOnArrowButtonPressed;
        UIController.OnToyMachineButtonPressed += HandleOnToyMachineButtonPressed;
    }

    private void OnDestroy()
    {
        ButtonPress.OnArrowButtonPressed -= HandleOnArrowButtonPressed;
        UIController.OnToyMachineButtonPressed -= HandleOnToyMachineButtonPressed;
    }

    private void Update()
    {
        if (moveClawToBearXPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, finalPistonPos, Time.deltaTime);            
            bool reachedPosition = Vector2.Distance(finalPistonPos, transform.position) < 0.01f;

            if (reachedPosition)
            {
                moveClawToBearXPos = false;
                finalPistonPos = new Vector2(finalPistonPos.x, pistonYFinalPos);
                triggerPistonAnim = true;
                initPistonYPos = piston.transform.position.y;
            }
        }

        if (triggerPistonAnim)
        {
            piston.transform.position = Vector2.MoveTowards(piston.transform.position, finalPistonPos, Time.deltaTime);
            bool reachedPosition = Vector2.Distance(finalPistonPos, piston.transform.position) < 0.01f;

            if(reachedPosition)
            {
                clawHandImg.sprite = closedClawSprite;
                bears[UIController.instance.selectedBearIndex].transform.SetParent(clawHandImg.transform);
                triggerPistonAnim = false;
                returnPistonToOriginPos = true;
                finalPistonPos = new Vector2(finalPistonPos.x, initPistonYPos);
            }
        }

        if(returnPistonToOriginPos)
        {
            piston.transform.position = Vector2.MoveTowards(piston.transform.position, finalPistonPos, Time.deltaTime);
            bool reachedPosition = Vector2.Distance(finalPistonPos, piston.transform.position) < 0.01f;

            if (reachedPosition)
            {
                returnPistonToOriginPos = false;
                if (UIController.instance.IsDesktop)
                    Invoke(nameof(CallEmocionometerDesktop), 1f);
                else
                    Invoke(nameof(CallEmocionometerMobile), 1f);
            }
        }
    }

    public void HandleOnArrowButtonPressed(int direction)
    {
        if (transform.localPosition.x >= -clawXBounds && transform.localPosition.x <= clawXBounds)
            transform.Translate(Vector2.right * Time.deltaTime * direction * speed);
        else
        {
            if(transform.localPosition.x < -clawXBounds)
                transform.localPosition = new Vector2(-clawXBounds, transform.localPosition.y);
            else if(transform.localPosition.x > clawXBounds)
                transform.localPosition = new Vector2(clawXBounds, transform.localPosition.y);
        }
    }

    private void CallEmocionometerMobile()
    {
        UIController.instance.screen5.SetActive(true);
        UIController.instance.screen4.SetActive(false);
    }

    private void CallEmocionometerDesktop()
    {
        UIController.instance.screen5Desktop.SetActive(true);
        UIController.instance.screen4Desktop.SetActive(false);
    }

    public void HandleOnToyMachineButtonPressed()
    {
        Vector2 nearesBearPos = GetNearestBearPosition();
        finalPistonPos = new Vector2(nearesBearPos.x, transform.position.y);
        moveClawToBearXPos = true;
    }

    private Vector2 GetNearestBearPosition()
    {
        float minDistance = Mathf.Abs(Vector2.Distance(bears[0].transform.position, transform.position));
        
        Vector2 result = bears[0].transform.position;

        for (int i = 0; i < bears.Count - 1; i++)
        {
            float currentDistance = Mathf.Abs(Vector2.Distance(bears[i + 1].transform.position, transform.position));
            if(minDistance > currentDistance)
            {
                minDistance = currentDistance;
                UIController.instance.selectedBearIndex = i + 1;
            }
        }

        result = bears[UIController.instance.selectedBearIndex].transform.position;
        return result;
    }
}
