using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class LeftLaneStage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool isStaged = false;

    //Button Text Staged
    public Text stagedText;

    //Stage Lights
    public GameObject leftpreStageOne;
    public GameObject leftpreStageTwo;

    //Materials For Stage Lights
    public Material yellow;
    public Material unlit;

    //Time When They UnStage
    public static float buttonLetGo;

    //Recation Time Stored In String Format
    private string reactionTimeString;

    //Reaction Time Text Of This Lane
    public TextMeshProUGUI rtLeftText;

    //How Long The RT
    public double zero = 0.000;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Stage The Car
        isStaged = true;

        //Change Button Text
        stagedText.text = "Staged";

        // Light The Stage Bulbs
        leftpreStageOne.GetComponent<Renderer>().material = yellow;
        leftpreStageTwo.GetComponent<Renderer>().material = yellow;

    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        //Button Not Down Not Staged
        isStaged = false;

        //The Time Of When the Stage Button Was Last Let Go
        buttonLetGo = Time.time;
        TreeControlLAN.timer = 0f;


        // Change The Button Text
        stagedText.text = "Stage";

        // UnLight The Stage Bulbs
        leftpreStageOne.GetComponent<Renderer>().material = unlit;
        leftpreStageTwo.GetComponent<Renderer>().material = unlit;
    }
    public void displayRT()
    {
        reactionTimeString = TreeControlLAN.leftreactionTime.ToString("0.0000");
        rtLeftText.text = reactionTimeString;

        // Sets Color Reaction Time Text
        if (TreeControlLAN.leftreactionTime > 0)
        {
            rtLeftText.color = Color.green;
        }
        if (TreeControlLAN.leftreactionTime < 0)
        {
            rtLeftText.color = Color.red;
        }
    }
    public void resetRT()
    {
        reactionTimeString = zero.ToString();
        rtLeftText.text = reactionTimeString;
        rtLeftText.color = Color.white;
    }
}
