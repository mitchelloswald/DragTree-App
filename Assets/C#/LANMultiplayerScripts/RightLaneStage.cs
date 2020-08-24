using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



public class RightLaneStage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // True if they Are Staged
    public static bool isStaged = false;
    
    //Button Text Staged
    public Text stagedText;

    //Stage Lights
    public GameObject rightpreStageOne;
    public GameObject rightpreStageTwo;

    //Materials For Stage Lights
    public Material yellow;
    public Material unlit;

    //Time When They UnStage
    public static float buttonLetGo;

    //Recation Time Stored In String Format
    private string reactionTimeString;

    //Reaction Time Text Of This Lane
    public TextMeshProUGUI rtRightText;

    //How Long The RT
    public double zero = 0.000;




    public void OnPointerDown(PointerEventData pointerEventData )
    {
        //Stage The Car
        isStaged = true;

        //Change Button Text
        stagedText.text = "Staged";

        // Light The Stage Bulbs
        rightpreStageOne.GetComponent<Renderer>().material = yellow;
        rightpreStageTwo.GetComponent<Renderer>().material = yellow;

}

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        //Button Not Down Not Staged
        isStaged = false;

        //The Time Of When the Stage Button Was Last Let Go
        buttonLetGo = Time.time;
        TreeControler.timer = 0f;


        // Change The Button Text
        stagedText.text = "Stage";

        // UnLight The Stage Bulbs
        rightpreStageOne.GetComponent<Renderer>().material = unlit;
        rightpreStageTwo.GetComponent<Renderer>().material = unlit;
    }
    public void displayRT()
    {
        reactionTimeString = TreeControlLAN.reactionTime.ToString("0.0000");
        rtRightText.text = reactionTimeString;

        // Sets Color Reaction Time Text
        if (TreeControlLAN.reactionTime > 0)
        {
            rtRightText.color = Color.green;
        }
        if (TreeControlLAN.reactionTime < 0)
        {
            rtRightText.color = Color.red;
        }
    }
    public void resetRT()
    {
        reactionTimeString = zero.ToString();
        rtRightText.text = reactionTimeString;
        rtRightText.color = Color.white;
    }
}
