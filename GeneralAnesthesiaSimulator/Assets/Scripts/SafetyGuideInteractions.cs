using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SafetyGuideInteractions : MonoBehaviour
{
    [SerializeField]
    public List<SerializableSection> Sections;

    public SafetyGuideInteractions()
    {
        Sections = new List<SerializableSection>();

        Sections.Add(new SerializableSection("Emergency Ventilation Equipment", new List<SerializableStep>() {
            new SerializableStep("Verify backup ventilation equipment is available and functioning.")
        }));
        Sections.Add(new SerializableSection("High-Pressure System", new List<SerializableStep>(){
            new SerializableStep("Check Oxygen Cylinder Supply", new List<SerializableSubstep>(){
                new SerializableSubstep("Open cylinder and verify at least half full(about 1000 psi)."),
                new SerializableSubstep("Close cylinder.")
            }),
            new SerializableStep("Check Central Pipeline Supplies; Check that hoses are connected and pipeline gauges read about 50 psi.")
        }));
        Sections.Add(new SerializableSection("Low-Pressure System", new List<SerializableStep>(){
            new SerializableStep("Check Initial Status of Low Pressure System", new List<SerializableSubstep>(){
                new SerializableSubstep("Close flow control valves and turn vaporizers off."),
                new SerializableSubstep("Check fill level and tighten vaporizers' filler caps.")
            }),
            new SerializableStep("Perform Leak Check of Machine Low Pressure System", new List<SerializableSubstep>(){
                new SerializableSubstep("Verify that the machine master switch and flow control valves are OFF."),
                new SerializableSubstep("Attach \"Suction Bulb\" to common Fresh) gas outlet."),
                new SerializableSubstep("Squeeze bulb repeatedly until fully collapsed."),
                new SerializableSubstep("Verify bulb stays fully collapsed for at least 10 seconds."),
                new SerializableSubstep("Open one vaporizer at a time and repeat 'c' and 'd' as above."),
                new SerializableSubstep("Remove suction bulb, and reconnect fresh gas hose.")
            }),
            new SerializableStep("Turn On Machine Master Switch and all other necessary electrical equipment."),
            new SerializableStep("Test Flowmeters", new List<SerializableSubstep>(){
                new SerializableSubstep("Adjust flow of all gases through their full range, checking for smooth operation of floats and undamaged flowtubes."),
                new SerializableSubstep("Attempt to create a hypoxic 02 / N20 mixture and verify correct changes in flow and / or alarm.")
            })
        }));
        Sections.Add(new SerializableSection("Scavenging System", new List<SerializableStep>()
        {
            new SerializableStep("Adjust and Check Scavenging System", new List<SerializableSubstep>{
                new SerializableSubstep("Ensure proper connections between the scavenging system and both APL (pop-oft) valve and ventilator relief valve."),
                new SerializableSubstep("Adjust waste gas vacuum (if possible)."),
                new SerializableSubstep("Fully open APL valve and occlude Y - pTece."),
                new SerializableSubstep("With minimum 02 flow, allow scavenger reservoir bag to collapse completely and verify that absorber pressure gauge reads about zero."),
                new SerializableSubstep("With the 02 flush activated allow the scavenger reservoir bag to distend fully, and then verify that absorber pressure gauge reads < 10 cm H20.")
            })
        }));
        Sections.Add(new SerializableSection("Breathing System", new List<SerializableStep>()
        {
            new SerializableStep("Calibrate 02 Monitor", new List<SerializableSubstep>(){
                new SerializableSubstep("Ensure monitor reads 21% in room air."),
                new SerializableSubstep("Verify low 02 alarm is enabled and functioning."),
                new SerializableSubstep("Reinstall sensor in circuit and flush breathing system with 02."),
                new SerializableSubstep("Verify that monitor now reads greater than 90%.")
            }),
            new SerializableStep("Check Initial Status of Breathing System", new List<SerializableSubstep>{
                new SerializableSubstep("Set selector switch to \"Bag\" mode."),
                new SerializableSubstep("Check that breathing circuit is complete, undamaged and unobstructed."),
                new SerializableSubstep("Verify that CO2 absorbent is adequate."),
                new SerializableSubstep("Install breathing circuit accessory equipment(e.g.humidifier, PEEP valve) to be used during the case.")
            }),
            new SerializableStep("Perform Leak Check of the Breathing System", new List<SerializableSubstep>(){
                new SerializableSubstep("Set all gas flows to zero(or minimum)."),
                new SerializableSubstep("Close APL(pop-off) valve and occlude Y-piece."),
                new SerializableSubstep("Pressurize breathing system to about 30 cm H20 with 02 flush."),
                new SerializableSubstep("Ensure that pressure remains fixed for at least 10 seconds."),
                new SerializableSubstep("Open APL(Pop - off) valve and ensure that pressure decreases.")
            })
        }));
        Sections.Add(new SerializableSection("Manual and Automatic Ventilation Systems", new List<SerializableStep>()
        {
            new SerializableStep("Test Ventilation Systems and Unidirectional Valves", new List<SerializableSubstep>()
            {
                new SerializableSubstep("Place a second breathing bag on Y-piece."),
                new SerializableSubstep("Set appropriate ventilator parameters for next patient."),
                new SerializableSubstep("Switch to automatic ventilation (Ventilator) mode."),
                new SerializableSubstep("Fill bellows and breathing bag with 02 flush and then turn ventilator ON."),
                new SerializableSubstep("Set 02 flow to minimum, other gas flows to zero."),
                new SerializableSubstep("Verify that during inspiration bellows delivers appropriate tidal volume and that during expiration bellows fills completely."),
                new SerializableSubstep("Set fresh gas flow to about 5 L / min."),
                new SerializableSubstep("Verify that the ventilator bellows and simulated lungs fill and empty appropriately without sustained pressure at end expiration."),
                new SerializableSubstep("Check for proper action of unidirectional valves."),
                new SerializableSubstep("Exercise breathing circuit accessories to ensure proper function."),
                new SerializableSubstep("Turn ventilator OFF and switch to manual ventilation(Bag/ APL) mode."),
                new SerializableSubstep("Ventilate manually and assure inflation and deflation of artificial lungs and appropriate feel of system resistance and compliance."),
                new SerializableSubstep("Remove second breathing bag from Y-piece.")
            })
        }));
        Sections.Add(new SerializableSection("Monitors", new List<SerializableStep>()
        {
            new SerializableStep("Check, Calibrate and/or Set Alarm Limits of all Monitors: Capnometer, Pulse Oximeter, Oxygen Analyzer Respiratory-Volume Monitor (Spirometer), Pressure Monitor with High and Low Airway Alarms.")
        }));
        Sections.Add(new SerializableSection("Final Position", new List<SerializableStep>()
        {
            new SerializableStep("Check Final Status of Machine", new List<SerializableSubstep>()
            {
                new SerializableSubstep("Vaporizers off"),
                new SerializableSubstep("AFL valve open"),
                new SerializableSubstep("Selector switch to \"Bag\""),
                new SerializableSubstep("All flowmeters to zero"),
                new SerializableSubstep("Patient suction level adequate"),
                new SerializableSubstep("Breathing system ready to use")
            })
        }));
    }

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
    public string title;

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
}

[System.Serializable]
public class SerializableStep
{
    [SerializeField]
    public List<SerializableSubstep> Substeps;
    public string description;
    public bool isCompleted;

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
}

[System.Serializable]
public class SerializableSubstep
{
    [SerializeField]
    public List<GameObject> InteractionObjects;

    public bool isCompleted;
    public string description;

    public SerializableSubstep(string description)
    {
        this.description = description;
    }

    public bool IsCompleted()
    {
        isCompleted = InteractionObjects.All(a => a.GetComponent<Interactable>().getCompletedInteraction() == true);
        return isCompleted;
    }

    public bool CheckCompletion(int index)
    {
        return InteractionObjects[index].GetComponent<Interactable>().getCompletedInteraction();
    }
}
