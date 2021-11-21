using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SafetyGuideInteractions : MonoBehaviour
{
    [SerializeField]
    public List<SerializableSection> Sections;

    public bool IsCompleted()
    {
        return Sections.All(a => a.isCompleted == true);
    }

    override public string ToString()
    {
        string s = "";

        foreach (var section in Sections)
        {
            s += "\n" + section.ToString();
        }

        return s;
    }

    public string ToStringFormatted()
    {
        string s = "";

        foreach (var section in Sections)
        {
            if (!section.isCompleted)
                s += "\n" + section.ToStringFormatted();
            else
                s += "\n<color=green>" + section.ToStringFormatted() + "</color>";
        }

        return s;
    }

    public string GetSectionString(int index)
    {
        return Sections[index].ToString();
    }

    public string GetSectionStringFormatted(int index)
    {
        return Sections[index].ToStringFormatted();
    }

}

[System.Serializable]
public class SerializableSection
{

    public bool isCompleted;
    public string title;

    [SerializeField]
    public List<SerializableStep> Steps = new List<SerializableStep>();

    public SerializableSection(string title, List<SerializableStep> steps)
    {
        this.title = title;
        this.Steps = steps;
        this.isCompleted = false;
    }


    public bool IsCompleted()
    {
        isCompleted = Steps.All(a => a.isCompleted == true);
        return isCompleted;

    }

    public string GetCurrentStep()
    {
        foreach (var step in Steps)
        {
            if (!step.isCompleted)
                return step.description;
        }

        return "Completed Section";
    }

    override public string ToString()
    {
        string s = "<b>" + title + "</b>";

        foreach (var step in Steps)
        {
            s += "\n\n" + (Steps.IndexOf(step) + 1).ToString() + ". " + step.ToString();
        }

        return s;
    }

    public string ToStringFormatted()
    {
        string s = "";

        if (!isCompleted)
            s += "<b>" + title + "</b>";

        if (isCompleted)
            s += "<color=green><b>" + title + "</color></b>";

        foreach (var step in Steps)
        {
            s += "\n\n" + (Steps.IndexOf(step) + 1).ToString() + ". " + step.ToStringFormatted();

        }

        return s;
    }
}

[System.Serializable]
public class SerializableStep
{
    public string description;
    public bool isCompleted;

    [SerializeField]
    public List<SerializableSubstep> Substeps;

    public SerializableStep(string description)
    {
        this.description = description;
        this.Substeps = new List<SerializableSubstep>();
        this.isCompleted = false;
    }
    public SerializableStep(string description, List<SerializableSubstep> substeps)
    {
        this.description = description;
        this.Substeps = substeps;
        this.isCompleted = false;
    }

    public bool IsCompleted()
    {
        isCompleted = Substeps.All(a => a.isCompleted == true);
        return isCompleted;
    }

    override public string ToString()
    {
        string s = description;

        if(Substeps.Count == 0)
            return s;

        foreach (var substep in Substeps)
        {
            s += "\n<margin=2em>" + (char)(Substeps.IndexOf(substep) + 97) + ". " + substep.description + "</margin>";
        }

        return s;

    }

    public string ToStringFormatted()
    {
        string s = "";

        if (!isCompleted)
            s += description;
        else
            s += "<color=green> " + description + " </color>";

        if (Substeps.Count == 0)
            return s;

        foreach (var substep in Substeps)
        {
            if (!substep.isCompleted)
                s += "\n<margin=2em>" + (char)(Substeps.IndexOf(substep) + 97) + ". " + substep.description + "</margin>";
            else
                s += "\n<margin=2em><color=green> " + (char)(Substeps.IndexOf(substep) + 97) + ". " + substep.description + " </color></margin>";
        }

        return s;
    }

    public string GetCurrentStep()
    {
        if (Substeps.Count == 0)
            return description;

        foreach (var s in Substeps)
        {
            if (!s.isCompleted)
                return s.description;
        }

        return "Step Completed";
    }
}

[System.Serializable]
public class SerializableSubstep
{
    public bool isCompleted;
    public string description;

    [SerializeField]
    public List<GameObject> InteractionObjects;

    public SerializableSubstep(string description)
    {
        this.description = description;
    }

    public bool IsCompleted()
    {
        isCompleted = InteractionObjects.All(a => a.GetComponent<Interactable>().GetCompletedInteraction() == true);
        return isCompleted;
    }

    public bool CheckCompletion(int index)
    {
        return InteractionObjects[index].GetComponent<Interactable>().GetCompletedInteraction();
    }
}
