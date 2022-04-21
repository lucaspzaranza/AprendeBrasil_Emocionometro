using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    public List<Color> colors = new List<Color>();

    public Camera m_camera;
    public GameObject brush;
    public GameObject drawParent;
    public Stack<GameObject> linesStack = new Stack<GameObject>(); 

    private LineRenderer currentLineRenderer;
    private Vector2 lastPos;

    private bool isDrawing = true;
    private bool isErasing = false;
    private int selectedColorIndex = 0;

    public void BeginDraw()
    {
        CreateBrush();
        Draw();
    }

    public void Draw()
    {
        if (isErasing) return;

        if (!isDrawing)
            isDrawing = true;

        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        if(mousePos != lastPos)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }

    public void EndDraw()
    {
        currentLineRenderer = null;
        isDrawing = false;
    }

    private void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush, drawParent.transform);
        linesStack.Push(brushInstance);

        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.startColor = colors[selectedColorIndex];
        currentLineRenderer.endColor = colors[selectedColorIndex];

        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        currentLineRenderer.SetPosition(0, mousePos);
    }

    private void AddAPoint(Vector2 pointPos)
    {
        if (currentLineRenderer == null) return;
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    public void EraseLastLine()
    {
        if (linesStack.Count == 0) return;

        GameObject lastLine = linesStack.Pop();
        Destroy(lastLine);
    }

    public void EraseEverything()
    {
        if (linesStack.Count == 0) return;

        foreach (var line in linesStack)
        {
            Destroy(line);
        }
    }

    public void PickupColor(int colorIndex)
    {
        selectedColorIndex = colorIndex;
    }

    public void Screenshot(bool isDesktop)
    {
        string date = DateTime.Now.ToString().Replace('/', '-').Replace(':', '-');
        string dataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
        ScreenCapture.CaptureScreenshot(dataPath + "/screenshot_" + date + ".png");
    }
}
