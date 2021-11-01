using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestingSafetyScript : MonoBehaviour
{
    private SafetyGuideText safetySteps { get; set; }
    private TextMeshProUGUI guideText { get; set; }
    private TextMeshProUGUI debugText { get; set; }

    public void Start()
    {
        safetySteps = new SafetyGuideText();
        guideText = GameObject.Find("Guide").GetComponent<TextMeshProUGUI>();
        debugText = GameObject.Find("DebugText").GetComponent<TextMeshProUGUI>();
    }
    public void UpdateGuideText()
    {
        guideText.text = safetySteps.GetSectionStringFormatted();
    }

    public void GetSafetySteps()
    {
        guideText.text = safetySteps.ToStringFormatted();
    }

    public void CompleteStep()
    {
        safetySteps.CompleteNextItem();
        guideText.text = safetySteps.GetSectionStringFormatted();

        debugText.text = "";

        foreach(var step in safetySteps.safetySections[safetySteps.currentSectionIndex].steps)
        {
            debugText.text += (" " + step.completed.ToString());
        }


    }

    public void testColor()
    {
        debugText.text = "<color=green> This is a test </color>";
    }
}
