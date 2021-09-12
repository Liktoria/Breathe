using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private Color[] sectionColors = new Color[3];
    [SerializeField] private float[] middlePoints = new float[3];
    [SerializeField] private Transform player;
    private Color nextColor;
    private int nextColorIndex = 0;
    private int sectionIndex;
    private int middlePointIndex;
    private Color currentColor;
    private float lastPlayerX;
    private float sectionLength;
    private List<float> sectionLengths = new List<float>();
    private float clampValue;
    private bool canLerp = false;
    private static ColorController Instance;
    private float sectionMiddle;
    private float nextSectionMiddle;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        directionalLight.color = sectionColors[0];
        lastPlayerX = player.position.x;
        currentColor = directionalLight.color;
        nextColor = sectionColors[0];
        for(int i = 0; i < middlePoints.Length - 1; i++)
        {
            sectionLengths.Add(middlePoints[i + 1] - middlePoints[i]);
        }
        sectionMiddle = middlePoints[middlePointIndex];
        nextSectionMiddle = middlePoints[middlePointIndex + 1];
        sectionLength = sectionLengths[sectionIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (canLerp)
        {
            if (player.position.x > lastPlayerX)
            {
                clampValue = Mathf.Abs((player.position.x - sectionMiddle) / sectionLength);
                directionalLight.color = Color.Lerp(currentColor, nextColor, clampValue );
                //from lastColor to goalColor
            }
            else if (player.position.x < lastPlayerX)
            {
                clampValue = Mathf.Abs((nextSectionMiddle - Mathf.Abs(player.position.x)) / sectionLength);
                directionalLight.color = Color.Lerp(nextColor, currentColor, clampValue);
                //switch colors: goalColor -> lastColor
            }
        }
        lastPlayerX = player.position.x;
    }

    public void NextColor()
    {
        if (nextColorIndex < sectionColors.Length - 1)
        {                  
            currentColor = nextColor;
            nextColorIndex++;
            nextColor = sectionColors[nextColorIndex];
        }
    }

    public void PreviousColor()
    {
        if (nextColorIndex > 0)
        {
            nextColor = currentColor;
            nextColorIndex--;
            if (nextColorIndex > 0)
            {
                currentColor = sectionColors[nextColorIndex - 1];
            }
            else
            {
                currentColor = sectionColors[0];
            }
        }
    }

    public void SetCanLerp (bool newValue)
    {
        canLerp = newValue;
    }

    public void NextSection()
    {
        sectionIndex++;
        sectionLength = sectionLengths[sectionIndex];
    }

    public void PreviousSection()
    {
        sectionIndex--;
        sectionLength = sectionLengths[sectionIndex];
    }

    public void NextMiddlePoint()
    {
        middlePointIndex++;
        sectionMiddle = middlePoints[middlePointIndex];
        nextSectionMiddle = middlePoints[middlePointIndex + 1];
    }

    public void PreviousMiddlePoint()
    {
        nextSectionMiddle = middlePoints[middlePointIndex];
        middlePointIndex--;
        sectionMiddle = middlePoints[middlePointIndex];
    }

    public static ColorController GetInstance()
    {
        return Instance;
    }
}
