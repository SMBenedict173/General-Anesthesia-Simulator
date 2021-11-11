using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyGuideInteractions : MonoBehaviour
{
    [SerializeField]
    public List<serializableSection> SafetySections = new List<serializableSection>();
}

[System.Serializable]
public class serializableSection
{
    [SerializeField]
    public List<serializableStep> steps = new List<serializableStep>();
}

[System.Serializable]
public class serializableStep
{
    [SerializeField]
    public List<serializableSubstep> substeps;
}

[System.Serializable]
public class serializableSubstep
{
    [SerializeField]
    public List<GameObject> interactions;
}
