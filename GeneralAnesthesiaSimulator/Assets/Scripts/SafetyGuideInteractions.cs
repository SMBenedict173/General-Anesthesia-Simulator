using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SafetyGuideInteractions : MonoBehaviour
{
    [SerializeField]
    public List<SerializableSection> Sections = new List<SerializableSection>();

    public bool isCompleted;

    public bool IsCompleted()
    {
        return Sections.All(a => a.isCompleted == true);
    }

}

[System.Serializable]
public class SerializableSection
{
    [SerializeField]
    public List<SerializableStep> Steps = new List<SerializableStep>();

    public bool isCompleted;


    public bool IsCompleted()
    {
        return Steps.All(a => a.isCompleted == true);
    }
}

[System.Serializable]
public class SerializableStep
{
    [SerializeField]
    public List<SerializableSubstep> Substeps;

    public bool isCompleted;

    public bool IsCompleted()
    {
        return Substeps.All(a => a.isCompleted == true);
    }
}

[System.Serializable]
public class SerializableSubstep
{
    [SerializeField]
    public List<GameObject> InteractionObjects;

    public bool isCompleted;

    public bool IsCompleted()
    {
        return InteractionObjects.All(a => a.GetComponent<Interactable>().getCompletedInteraction() == true);
    }
}
