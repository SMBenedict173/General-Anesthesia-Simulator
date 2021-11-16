using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SafetyGuideInteractions : MonoBehaviour
{
    [SerializeField]
    public List<SerializableSection> SafetySections = new List<SerializableSection>();

    public List<bool> SectionCompletion; 

    public SafetyGuideInteractions()
    {
        SectionCompletion = Enumerable.Repeat(false, SafetySections.Count).ToList();
    }

}

[System.Serializable]
public class SerializableSection
{
    [SerializeField]
    public List<SerializableStep> Steps = new List<SerializableStep>();

    public List<bool> StepCompletion; 

    public SerializableSection()
    {
        StepCompletion = Enumerable.Repeat(false, Steps.Count).ToList();
    }


    public bool IsCompleted()
    {
        if( StepCompletion.All(a => a == true))
        {
            return true;
        }

        return false;
    }
}

[System.Serializable]
public class SerializableStep
{
    [SerializeField]
    public List<SerializableSubstep> Substeps;

    public List<bool> SubstepCompletion; 

    public SerializableStep()
    {
        SubstepCompletion = Enumerable.Repeat(false, Substeps.Count).ToList();
    }

    public bool IsCompleted()
    {
        if (SubstepCompletion.All(a => a == true))
        {
            return true;
        }

        return false;
    }
}

[System.Serializable]
public class SerializableSubstep
{
    [SerializeField]
    public List<GameObject> InteractionObjects;

    public List<bool> InteractionCompletion;

    public SerializableSubstep()
    {
        InteractionCompletion = Enumerable.Repeat(false, InteractionObjects.Count).ToList();
    }



    public void CompleteInteraction(GameObject interactableObject)
    {
        int index = InteractionObjects.IndexOf(interactableObject);
        InteractionCompletion[index] = true;
    }

    public bool IsCompleted()
    {
        if (InteractionCompletion.All(a => a == true))
        {
            return true;
        }

        return false;
    }
}
