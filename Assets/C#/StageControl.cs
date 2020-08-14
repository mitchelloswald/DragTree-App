using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



public class StageControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Text stagedText;
    public TextMeshProUGUI rtText;
    public double zero = 0.000;
    public GameObject rightpreStageOne;
    public GameObject rightpreStageTwo;
    public GameObject leftpreStageOne;
    public GameObject leftpreStageTwe;
    public Material yellow;
    public Material unlit;
    public static float buttonLetGo;
    public static bool isStaged = false;
    private string reactionTimeString;
    float countDownTimer = TreeControler.timer;




    public void OnPointerDown(PointerEventData pointerEventData)
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
        reactionTimeString = TreeControler.reactionTime.ToString("0.0000");
        rtText.text = reactionTimeString;
        
        // Sets Color Reaction Time Text
        if (TreeControler.reactionTime > 0)
        {
            rtText.color = Color.green;
        }
        if (TreeControler.reactionTime < 0)
        {
            rtText.color = Color.red;
        }
    }
    public void resetRT()
    {
        reactionTimeString = zero.ToString();
        rtText.text = reactionTimeString;
        rtText.color = Color.white;
    }
}