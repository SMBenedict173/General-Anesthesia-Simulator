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
    public List<bool> StepsCompletion; 

    public SerializableSection()
    {
        StepsCompletion = Enumerable.Repeat(false, Steps.Count).ToList();
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
}
