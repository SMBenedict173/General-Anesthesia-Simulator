using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{

    [SerializeField]
    public TextMeshProUGUI guideText;
    public SafetyGuideInteractions guideInteractions;
    public SceneLoader sceneLoader;


    private int currentSection = 0;
    private int currentStep = 0;
    private int currentSubstep = 0;
    private int currentInteraction = 0;


    // Update is called once per frame
    void Update()
    {
        // check if the interaction was completed 
        if (IsCurrentInteractionComplete())
        {
            DisableGuidingLightForCurrent();
            currentInteraction += 1;

            //update indexing
            UpdateIndexes();

            EnableGuidingLightForCurrent();
            guideText.text = guideInteractions.GetSectionStringFormatted(currentSection);
        }

    }

    public void startSimulation()
    {
        guideText.text = guideInteractions.GetSectionStringFormatted(currentSection);
        EnableGuidingLightForCurrent();
    }

    private void UpdateIndexes()
    {
        // update indexes 
        if (guideInteractions.Sections[currentSection].Steps[currentStep].Substeps[currentSubstep].IsCompleted() 
            && currentInteraction == guideInteractions.Sections[currentSection].Steps[currentStep].Substeps[currentSubstep].InteractionObjects.Count)
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

        //check completed
        if (currentSection == 8)
        {
            sceneLoader.NextScene();
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
            .GetComponent<Outline>().enabled = true;
        


        guideInteractions.Sections[currentSection]
            .Steps[currentStep].Substeps[currentSubstep]
            .InteractionObjects[currentInteraction]
            .GetComponent<Interactable>().SetActive();
    }

    private void DisableGuidingLightForCurrent()
    {
        
        guideInteractions.Sections[currentSection]
            .Steps[currentStep].Substeps[currentSubstep]
            .InteractionObjects[currentInteraction]
            .GetComponent<Outline>().enabled = false;
        

        guideInteractions.Sections[currentSection]
            .Steps[currentStep].Substeps[currentSubstep]
            .InteractionObjects[currentInteraction]
            .GetComponent<Interactable>().SetInactive();
    }

}
