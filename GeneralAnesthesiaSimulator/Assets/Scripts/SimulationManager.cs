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
    private int currentSubSubstep = 0; 



    // Start is called before the first frame update
    void Start()
    {
        guideText = new SafetyGuideText();
        guideInteractions = new SafetyGuideInteractions();

    }

    // Update is called once per frame
    void Update()
    {
        
        /*
         * Check for new interactions 
         * If new interactions: Check step completion
         * If step completed, check section 
         * Finally, update guiding highlight
         * 
         */

    }
}
