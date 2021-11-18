using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SafetyGuideText : MonoBehaviour
{
    public List<Section> safetySections { get; }
    public int currentSectionIndex { get; set; }

    public SafetyGuideText()
    {
        safetySections = new List<Section>();
        currentSectionIndex = 0;

        safetySections.Add(new Section("Emergency Ventilation Equipment", new List<Step>() {
            new Step("Verify backup ventilation equipment is available and functioning.")
        }));
        safetySections.Add(new Section("High-Pressure System", new List<Step>(){
            new Step("Check Oxygen Cylinder Supply", new List<Step>(){
                new Step("Open cylinder and verify at least half full(about 1000 psi)."),
                new Step("Close cylinder.")
            }),
            new Step("Check Central Pipeline Supplies; Check that hoses are connected and pipeline gauges read about 50 psi.")
        }));
        safetySections.Add(new Section("Low-Pressure System", new List<Step>(){
            new Step("Check Initial Status of Low Pressure System", new List<Step>(){
                new Step("Close flow control valves and turn vaporizers off."),
                new Step("Check fill level and tighten vaporizers' filler caps.")
            }),
            new Step("Perform Leak Check of Machine Low Pressure System", new List<Step>(){
                new Step("Verify that the machine master switch and flow control valves are OFF."),
                new Step("Attach \"Suction Bulb\" to common Fresh) gas outlet."),
                new Step("Squeeze bulb repeatedly until fully collapsed."),
                new Step("Verify bulb stays fully collapsed for at least 10 seconds."),
                new Step("Open one vaporizer at a time and repeat 'c' and 'd' as above.", new List<Step>(){
                        new Step("Open vaporizer 1"),
                        new Step("Squeeze bulb repeatedly until fully collapsed."),
                        new Step("Verify bulb stays fully collapsed for at least 10 seconds."),
                        new Step("Open vaporizer 2"),
                        new Step("Squeeze bulb repeatedly until fully collapsed."),
                        new Step("Verify bulb stays fully collapsed for at least 10 seconds.")
                }),
                new Step("Remove suction bulb, and reconnect fresh gas hose.")
            }),
            new Step("Turn On Machine Master Switch and all other necessary electrical equipment."),
            new Step("Test Flowmeters", new List<Step>(){
                new Step("Adjust flow of all gases through their full range, checking for smooth operation of floats and undamaged flowtubes."),
                new Step("Attempt to create a hypoxic 02 / N20 mixture and verify correct changes in flow and / or alarm.")
            })
        }));
        safetySections.Add(new Section("Scavenging System", new List<Step>()
        {
            new Step("Adjust and Check Scavenging System", new List<Step>{
                new Step("Ensure proper connections between the scavenging system and both APL (pop-oft) valve and ventilator relief valve."),
                new Step("Adjust waste gas vacuum (if possible)."),
                new Step("Fury open APL valve and occlude Y - pTece."),
                new Step("With minimum 02 flow, allow scavenger reservoir bag to collapse completely and verify that absorber pressure gauge reads about zero."),
                new Step("With the 02 flush activated allow the scavenger reservoir bag to distend fully, and then verify that absorber pressure gauge reads < 10 cm H20.")
            })
        }));
        safetySections.Add(new Section("Breathing System", new List<Step>()
        {
            new Step("Calibrate 02 Monitor", new List<Step>(){
                new Step("Ensure monitor reads 21% in room air."),
                new Step("Verify low 02 alarm is enabled and functioning."),
                new Step("Reinstall sensor in circuit and flush breathing system with 02."),
                new Step("Verify that monitor now reads greater than 90%.")
            }),
            new Step("Check Initial Status of Breathing System", new List<Step>{
                new Step("Set selector switch to \"Bag\" mode."),
                new Step("Check that breathing circuit is complete, undamaged and unobstructed."),
                new Step("Verify that CO2 absorbent is adequate."),
                new Step("Install breathing circuit accessory equipment(e.g.humidifier, PEEP valve) to be used during the case.")
            }),
            new Step("Perform Leak Check of the Breathing System", new List<Step>(){
                new Step("Set all gas flows to zero(or minimum)."),
                new Step("Close APL(pop-off) valve and occlude Y-piece."),
                new Step("Pressurize breathing system to about 30 cm H20 with 02 flush."),
                new Step("Ensure that pressure remains fixed for at least 10 seconds."),
                new Step("Open APL(Pop - off) valve and ensure that pressure decreases.")
            })
        }));
        safetySections.Add(new Section("Manual and Automatic Ventilation Systems", new List<Step>()
        {
            new Step("Test Ventilation Systems and Unidirectional Valves", new List<Step>()
            {
                new Step("Place a second breathing bag on Y-piece."),
                new Step("Set appropriate ventilator parameters for next patient."),
                new Step("Switch to automatic ventilation (Ventilator) mode."),
                new Step("Fill bellows and breathing bag with 02 flush and then turn ventilator ON."),
                new Step("Set 02 flow to minimum, other gas flows to zero."),
                new Step("Verify that during inspiration bellows delivers appropriate tidal volume and that during expiration bellows fills completely."),
                new Step("Set fresh gas flow to about 5 L / min."),
                new Step("Verify that the ventilator bellows and simulated lungs fill and empty appropriately without sustained pressure at end expiration."),
                new Step("Check for proper action of unidirectional valves."),
                new Step("Exercise breathing circuit accessories to ensure proper function."),
                new Step("Turn ventilator OFF and switch to manual ventilation(Bag/ APL) mode."),
                new Step("Ventilate manually and assure inflation and deflation of artificial lungs and appropriate feel of system resistance and compliance."),
                new Step("Remove second breathing bag from Y-piece.")
            })
        }));
        safetySections.Add(new Section("Monitors", new List<Step>()
        {
            new Step("Check, Calibrate and/or Set Alarm Limits of all Monitors: Capnometer, Pulse Oximeter, Oxygen Analyzer Respiratory-Volume Monitor (Spirometer), Pressure Monitor with High and Low Airway Alarms.")
        }));
        safetySections.Add(new Section("Final Position", new List<Step>()
        {
            new Step("Check Final Status of Machine", new List<Step>()
            {
                new Step("Vaporizers off"),
                new Step("AFL valve open"),
                new Step("Selector switch to \"Bag\""),
                new Step("All flowmeters to zero"),
                new Step("Patient suction level adequate"),
                new Step("Breathing system ready to use")
            })
        }));
    }

    override public string ToString()
    {
        string s = "";

        foreach (var section in safetySections)
        {
            s += "\n" + section.ToString();
        }

        return s;
    }

    public string ToStringFormatted()
    {
        string s = "";

        foreach (var section in safetySections)
        {
            if(!section.completed)
                s += "\n" + section.ToStringFormatted();
            else
                s += "\n<color=green>" + section.ToStringFormatted() + "</color>";
        }

        return s;
    }

    public void CompleteNextItem()
    {
        safetySections[currentSectionIndex].CompleteNextItem();

        if (safetySections[currentSectionIndex].completed)
            currentSectionIndex++;
    }

    public string GetSectionString()
    {
        return safetySections[currentSectionIndex].ToString();
    }

    public string GetSectionStringFormatted()
    {
        return safetySections[currentSectionIndex].ToStringFormatted();
    }
}

public class Section
{
    public string title { get; set; }

    public List<Step> steps;

    public int currentStepIndex { get; set; }
    public bool completed { get; set; }

    public Section(string title, List<Step> steps)
    {
        this.title = title;
        this.steps = steps;
        this.completed = false;
        this.currentStepIndex = 0;
    }

    public string GetCurrentStep()
    {
        foreach (var step in steps)
        {
            if (!step.completed)
                return step.description;
        }

        return "Completed Section";
    }

    override public string ToString()
    {
        string s = "<b>" + title + "</b>";

        foreach (var step in steps)
        {
            s += "\n\n" + (steps.IndexOf(step) + 1).ToString() + ". " + step.ToString();
        }

        return s;
    }

    public string ToStringFormatted()
    {
        string s = "";

        if(!completed)
            s += "<b>" + title + "</b>";

        if (completed)
            s += "<color=green><b>" + title + "</color></b>";

        foreach (var step in steps)
        {
            s += "\n\n" + (steps.IndexOf(step) + 1).ToString() + ". " + step.ToStringFormatted();

        }

        return s;
    }

    public void CompleteNextItem()
    {
        steps[currentStepIndex].CompleteNextItem();

        if (steps[currentStepIndex].substeps.All(substep => substep.completed))
            currentStepIndex += 1;

        if (steps.All(step => step.completed))
            this.completed = true;

    }
}

public class Step
{
    public string description { get; set; }
    public bool completed { get; set; }

    public List<Step> substeps;

    public int currentSubStepIndex { get; set; }

    public Step(string description)
    {
        this.description = description;
        this.substeps = new List<Step>();
        this.completed = false;
        this.currentSubStepIndex = 0;
    }
    public Step(string description, List<Step> substeps)
    {
        this.description = description;
        this.substeps = substeps;
        this.completed = false;
        this.currentSubStepIndex = 0;
    }

    override public string ToString()
    {
        string s = description;

        foreach (var substep in substeps)
        {
            s += "\n<margin=2em>" + (char)(substeps.IndexOf(substep) + 97) + ". " + substep.description + "</margin>";
        }

        return s;

    }

    public string ToStringFormatted()
    {
        string s = "";

        if (!completed)
            s += description;
        else
            s += "<color=green> " + description + " </color>";

        foreach (var substep in substeps)
        {
            if(!substep.completed)
                s += "\n<margin=2em>" + (char)(substeps.IndexOf(substep) + 97) + ". " + substep.description + "</margin>";
            else
                s += "\n<margin=2em><color=green> " + (char)(substeps.IndexOf(substep) + 97) + ". " + substep.description + " </color></margin>";
        }

        return s;
    }

    public string GetCurrentStep()
    {
        if (substeps.Count == 0)
            return description;

        foreach (var s in substeps)
        {
            if (!s.completed)
                return s.description;
        }

        return "Step Completed";
    }

    public void CompleteNextItem()
    {
        if (substeps.Count == 0)
        {
            completed = true;
            return;
        }

        for(int i = 0; i < substeps.Count; i++)
        {
            if (!substeps[i].completed)
            {
                substeps[i].completed = true;
                currentSubStepIndex += 1;
                break;
            }
        }

        if(substeps.All(step => step.completed))
        {
            completed = true;
        }
    }


}