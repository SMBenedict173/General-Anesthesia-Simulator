using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabletScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tabletText;
    [SerializeField]
    private SafetyGuideText guideText;
    private int guideTextCurrentIndex;
    // Start is called before the first frame update
    void Start()
    {
        tabletText.text = guideText.ToStringFormatted();
        guideTextCurrentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (guideTextCurrentIndex != guideText.currentSectionIndex)
        {
            guideTextCurrentIndex = guideText.currentSectionIndex;
            tabletText.text = guideText.ToStringFormatted();
        }
    }
}
