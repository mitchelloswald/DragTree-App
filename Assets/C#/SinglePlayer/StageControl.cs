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
    public TextMeshProUGUI botrtText;
    public double zero = 0.000;
    public GameObject rightpreStageOne;
    public GameObject rightpreStageTwo;
    public GameObject leftpreStageOne;
    public GameObject leftpreStageTwo;
    public Material yellow;
    public Material unlit;
    public static float buttonLetGo;
    public static bool isStaged = false;
    private string reactionTimeString;
    private string randomRT;

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

        StartCoroutine(botStageDelay());

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


        StartCoroutine(botUnStageDelay());
    }
    public void displayRT()
    {
        // Turn Off Bots Stage Lights
        leftpreStageOne.GetComponent<Renderer>().material = unlit;
        leftpreStageTwo.GetComponent<Renderer>().material = unlit;
        
        reactionTimeString = TreeControler.reactionTime.ToString("0.0000");
        randomRT = TreeControler.randomRT.ToString("0.0000");
        rtText.text = reactionTimeString;
        botrtText.text = randomRT;
        
        // Sets Color Reaction Time Text
        if (TreeControler.reactionTime > 0)
        {
            rtText.color = Color.green;
        }
        if (TreeControler.reactionTime < 0)
        {
            rtText.color = Color.red;
        }
        // Sets Color Reaction Time Text
        if (TreeControler.randomRT > 0)
        {
            botrtText.color = Color.green;
        }
        if (TreeControler.randomRT < 0)
        {
            botrtText.color = Color.red;
        }
    }
    public void resetRT()
    {
        reactionTimeString = zero.ToString();
        rtText.text = reactionTimeString;
        rtText.color = Color.white;
        botrtText.text = reactionTimeString;
        botrtText.color = Color.white;
    }
    IEnumerator botStageDelay()
    {
       yield return new WaitForSeconds(.2f);
       stageBot();

    }
    IEnumerator botUnStageDelay()
    {
        yield return new WaitForSeconds(.2f);
        unstageBot();

    }

    public void stageBot()
    {
        leftpreStageOne.GetComponent<Renderer>().material = yellow;
        leftpreStageTwo.GetComponent<Renderer>().material = yellow;
    }
    public void unstageBot()
    {
        leftpreStageOne.GetComponent<Renderer>().material = unlit;
        leftpreStageTwo.GetComponent<Renderer>().material = unlit;
    }


}