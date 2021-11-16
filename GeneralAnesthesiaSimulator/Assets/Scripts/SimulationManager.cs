using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{

    [SerializeField]
    public SafetyGuideText guideText;

    [SerializeField]
    public SafetyGuideInteractions guideInteractions;


    private int currentSection = 0;
    private int currentStep = 0;
    private int currentSubstep = 0;
    private int currentInteraction = 0;

    // Start is called before the first frame update
    void Start()
    {
        guideText = new SafetyGuideText();
        guideInteractions = new SafetyGuideInteractions();

    }

    // Update is called once per frame
    void Update()
    {   
        // check if the interaction was completed 
        if (IsCurrentInteractionComplete())
        {
            DisableGuidingLightForCurrent();
            currentInteraction += 1;

            UpdateIndexesAndText();
            EnableGuidingLightForCurrent();
        }

    }

    private void UpdateIndexesAndText()
    {
        if (guideInteractions.Sections[currentSection].Steps[currentStep].Substeps[currentSubstep].IsCompleted())
        {
            currentSubstep += 1;
            currentInteraction = 0;

            if (guideInteractions.Sections[currentSection].Steps[currentStep].IsCompleted())
            {
                currentStep += 1;
                currentSubstep = 0;

                if (guideInteractions.Sections[currentSection].IsCompleted())
                {
                    currentSection += 1;
                    currentStep = 0;
                }
            }
        }
    }

    private bool IsCurrentInteractionComplete()
    {
        return guideInteractions.Sections[currentSection]
            .Steps[currentStep]
            .Substeps[currentSubstep]
            .CheckCompletion(currentInteraction);

    }

    private void EnableGuidingLightForCurrent()
    {
        guideInteractions.Sections[currentSection]
            .Steps[currentStep].Substeps[currentSubstep]
            .InteractionObjects[currentInteraction]
            .GetComponent<GuidingLight>().EnableHighlight();
    }

    private void DisableGuidingLightForCurrent()
    {
        guideInteractions.Sections[currentSection]
            .Steps[currentStep].Substeps[currentSubstep]
            .InteractionObjects[currentInteraction]
            .GetComponent<GuidingLight>().DisableHighlight();
    }
}
