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
            if (!section.IsCompleted())
                s += "\n" + section.ToStringFormatted();
            else
                s += "\n<color=#007B00ff>" + section.ToStringFormatted() + "</color>";
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


    public bool IsCompleted()
    {
        isCompleted = Steps.All(a => a.isCompleted == true);
        return isCompleted;

    }

    public string GetCurrentStep()
    {
        foreach (var step in Steps)
        {
            if (!step.IsCompleted())
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

        if (!IsCompleted())
            s += "<b>" + title + "</b>";

        if (IsCompleted())
            s += "<color=#007B00ff><b>" + title + "</color></b>";

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
    public bool hasSubsteps;

    [SerializeField]
    public List<SerializableSubstep> Substeps;

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

        if (this.hasSubsteps)
        {
            foreach (var substep in Substeps)
            {
                s += "\n<margin=2em>" + (char)(Substeps.IndexOf(substep) + 97) + ". " + substep.description + "</margin>";
            }
        }

        return s;

    }

    public string ToStringFormatted()
    {
        string s = "";

        if (!IsCompleted())
            s += description;
        else
            s += "<color=#007B00ff> " + description + " </color>";

        if (this.hasSubsteps)
        {

            foreach (var substep in Substeps)
            {
                if (!substep.IsCompleted())
                    s += "\n<margin=2em>" + (char)(Substeps.IndexOf(substep) + 97) + ". " + substep.description + "</margin>";
                else
                    s += "\n<margin=2em><color=#007B00ff> " + (char)(Substeps.IndexOf(substep) + 97) + ". " + substep.description + " </color></margin>";
            }
        }

        return s;
    }

    public string GetCurrentStep()
    {
        if (Substeps.Count == 0 || !hasSubsteps)
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
    private int interactionsCompleted;

    public bool IsCompleted()
    {
        if(interactionsCompleted == InteractionObjects.Count)
        {
            isCompleted = true;
            return true;
        }

        return false;
    }

    public bool CheckCompletion(int index)
    {
        if (InteractionObjects[index].GetComponent<Interactable>().GetCompletedInteraction())
        {
            interactionsCompleted += 1;
            return true; 
        }


        return false;
    }
}
