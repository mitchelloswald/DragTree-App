using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;

public class TreeControler : MonoBehaviour
{
    public StageControl stageControl;
    
    // The Random Start Varible
    public float randomStart;

    // Has the random start number been created
    public bool randomStartGenerated = false;

    //Has Tree Dropped
    public bool hasTreeFired = false;
    
    // Different Bulbs
    public GameObject rightFirstBulb;
    public GameObject rightSecondBulb;
    public GameObject rightThirdBulb;
    public GameObject rightGreenBulb;
    public GameObject rightRedBulb;


    // Color Matrials
    public Material yellow;
    public Material unlit;
    public Material red;
    public Material green;

    // Different Tree Toggles
    public Toggle is5Pro;
    public Toggle is4Pro;
    public Toggle is5Full;


    // Loop Timer
    public static float timer = 0f;

    // Time When the Tree was First Fired
    public static float RTtimer;

    // Roll Out slider
    public Slider rollOutSlider;
    
    // RollOut value
    public float rollOut = .3f;

    public Text rollOutText;
    
    // Overall Reaction Time
    public static float reactionTime;

    // Use For Five Tenths Pro Tree
    public float fiveTenthsPro = .5f;

    // Use For Five Tenths Full Tree
    public float fiveTenthsFull = 1.5f;

    // Use For Four Tenths
    public float fourTenths = .4f;


    void Start()
    {
        // Sets Rollout With Slider
        // If There is No RollOut Value it Defaults to .3f
        rollOutSlider.value = PlayerPrefs.GetFloat("RollOut", .3f);

        // RollOut Number To UI
        rollOutText.text = PlayerPrefs.GetFloat("RollOut", .3f).ToString();
    }


    void Update()
    {
        // Check If Player is Staged
        if (StageControl.isStaged == true)
        {
            // Check to see if a start time was Generated if not generate a random start time
            if (randomStartGenerated == false)
            {
                randomStart = randomStartTime();
            }
            if (hasTreeFired == false && randomStartGenerated == true)
            {
                timer += Time.deltaTime;
                if (timer > randomStart)
                {                   
                    if (is5Pro.isOn)
                    {
                        ProTreeFire();
                    }
                    else if (is4Pro.isOn)
                    {
                        FourTenthsProTreeFire();
                    }
                    else if (is5Full.isOn)
                    {
                        fullTreeFire();
                    }
                }

            }
        }
    }
    void ProTreeFire()
    {
        // Light 3 Bulbs Pro Tree
        rightFirstBulb.GetComponent<Renderer>().material = yellow;
        rightSecondBulb.GetComponent<Renderer>().material = yellow;
        rightThirdBulb.GetComponent<Renderer>().material = yellow;

        //Time When Tree Dropped
        RTtimer = Time.time;

        //Tree Dropped
        hasTreeFired = true;

        //Wait .5 for Five Tenth Pro Tree
        StartCoroutine(FiveTenthsProTree());
    }
    void FourTenthsProTreeFire()
    {
        // Light 3 Bulbs Pro Tree
        rightFirstBulb.GetComponent<Renderer>().material = yellow;
        rightSecondBulb.GetComponent<Renderer>().material = yellow;
        rightThirdBulb.GetComponent<Renderer>().material = yellow;

        //Time When Tree Dropped
        RTtimer = Time.time;

        //Tree Dropped
        hasTreeFired = true;

        //Wait .4 for Four Tenth Pro Tree
        StartCoroutine(FourTenthsProTree());
    }
    void fullTreeFire()
    {
        //Time When Tree Dropped
        RTtimer = Time.time;

        // Light First Bulb
        rightFirstBulb.GetComponent<Renderer>().material = yellow;

        // Keep it on for .5 Seconds
        StartCoroutine(FiveTenthsFullTree());

        //Tree Dropped
        hasTreeFired = true;
    }
    void fullTreeFire2()
    {
        // Light Second Bulb
        rightSecondBulb.GetComponent<Renderer>().material = yellow;

        // Keep it on for .5 Seconds
        StartCoroutine(FiveTenthsFullTree2());

    }
    void fullTreeFire3()
    {

        // Light Bottom Third Bulb
        rightThirdBulb.GetComponent<Renderer>().material = yellow;
       
        // Keep it on for .5 Seconds
        StartCoroutine(FiveTenthsFullTree3());

    }
    void RT()
    {
        if (is5Pro.isOn)
        {
            //Unlight Bulbs
            rightFirstBulb.GetComponent<Renderer>().material = unlit;
            rightSecondBulb.GetComponent<Renderer>().material = unlit;
            rightThirdBulb.GetComponent<Renderer>().material = unlit;

            //Figure RT Pro .5 Tree Also Using Rollout from save
            reactionTime = StageControl.buttonLetGo - RTtimer + rollOut - fiveTenthsPro;
            stageControl.displayRT();

            // Red or Green? 
            redOrGreen();
        }
        else if (is5Full.isOn)
        {
            //Figure RT Full Tree
          reactionTime = StageControl.buttonLetGo - RTtimer + rollOut - fiveTenthsFull;
          stageControl.displayRT();
            // Red or Green? 
            redOrGreen();
        }
        else if (is4Pro.isOn)
        {
            //Unlight Bulbs
            rightFirstBulb.GetComponent<Renderer>().material = unlit;
            rightSecondBulb.GetComponent<Renderer>().material = unlit;
            rightThirdBulb.GetComponent<Renderer>().material = unlit;

            reactionTime = StageControl.buttonLetGo - RTtimer + rollOut - fourTenths;
            stageControl.displayRT();

            // Red or Green? 
            redOrGreen();
        }


    }
    void redOrGreen()
    {
        if (reactionTime < 0)
        {
            rightRedBulb.GetComponent<Renderer>().material = red;

        }
        else
        {
            rightGreenBulb.GetComponent<Renderer>().material = green;

        }
        //Wait To Reset the tree
        StartCoroutine(endOfTreePause());
    }
    public float randomStartTime()
    {
        // Generate Random Start of the Tree from half a second to three seconds
        float randomStart = Random.Range(1f, 2f);
        Debug.Log(randomStart);
        randomStartGenerated = true;
        return randomStart;
    }
    IEnumerator FiveTenthsProTree()
    {
        yield return new WaitForSeconds(.5f);
        RT();
    }
    IEnumerator FiveTenthsFullTree()
    {
        yield return new WaitForSeconds(.5f);
        rightFirstBulb.GetComponent<Renderer>().material = unlit;
        fullTreeFire2();

    }
    IEnumerator FiveTenthsFullTree2()
    {
        yield return new WaitForSeconds(.5f);
        rightSecondBulb.GetComponent<Renderer>().material = unlit;
        fullTreeFire3();

    }
    IEnumerator FiveTenthsFullTree3()
    {
        yield return new WaitForSeconds(.5f);
        rightThirdBulb.GetComponent<Renderer>().material = unlit;
        RT();

    }
    IEnumerator FourTenthsProTree()
    {
        yield return new WaitForSeconds(.4f);
        RT();
    }
    
    // Resets The Tree and RT so the tree can be Fired Again
    public void resetTree()
    {
        randomStartGenerated = false;
        hasTreeFired = false;
        timer = 0;
        randomStart = 0;

        // Make Sure the Green and Red get Unlit
        rightGreenBulb.GetComponent<Renderer>().material = unlit;
        rightRedBulb.GetComponent<Renderer>().material = unlit;

        //Reset the RT to zero
        stageControl.resetRT();
    }
  
    IEnumerator endOfTreePause()
    {
     
        yield return new WaitForSeconds(1.5f);
        resetTree();
    }

    //Changes RollOut From Slider In Settings
    public void ChangeRollOut(float SliderValue)
    {
        // Sets RollOut To New Value
        rollOut = SliderValue;
        
        // Saves RollOut value To File 
        PlayerPrefs.SetFloat("RollOut", SliderValue);

        // RollOut Number To UI
        rollOutText.text = PlayerPrefs.GetFloat("RollOut", .3f).ToString();


    }



}

