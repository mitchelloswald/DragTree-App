using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;

public class TreeControlLAN : MonoBehaviour
{
    // Other Classes
    public RightLaneStage rightlanestage;
    public LeftLaneStage leftlanestage;

    // The Random Start Varible
    public float randomStart;

    // Has the random start number been created
    public bool randomStartGenerated = false;

    //Has Tree Dropped
    public bool hasTreeFired = false;
    
    // Right Bulbs
    public GameObject rightFirstBulb;
    public GameObject rightSecondBulb;
    public GameObject rightThirdBulb;
    public GameObject rightGreenBulb;
    public GameObject rightRedBulb;

    // Left Bulbs
    public GameObject leftFirstBulb;
    public GameObject leftSecondBulb;
    public GameObject leftThirdBulb;
    public GameObject leftGreenBulb;
    public GameObject leftRedBulb;


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
    public Slider leftrollOutSlider;


    // RollOut value
    public float rollOut = .3f;
    public float leftrollOut = .3f;

    public Text rollOutText;
    public Text leftrollOutText;

    // Right Reaction Time
    public static float reactionTime;

    // Left Reaction Time
    public static float leftreactionTime;

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
        rollOutSlider.value = PlayerPrefs.GetFloat("RightLANRollOut", .3f);
        leftrollOutSlider.value = PlayerPrefs.GetFloat("LeftLANRollOut", .3f);


        // RollOut Number To UI
        rollOutText.text = PlayerPrefs.GetFloat("RightLANRollOut", .3f).ToString();
        leftrollOutText.text = PlayerPrefs.GetFloat("LeftLANRollOut", .3f).ToString();

    }


    void Update()
    {
        // Check If Both Player's are Staged
        if (RightLaneStage.isStaged && LeftLaneStage.isStaged == true)
        {
            // Check to see if a start time was Generated if not generate a random start time
            if (randomStartGenerated == false)
            {
                randomStart = randomStartTime();
            }
            if (hasTreeFired == false && randomStartGenerated == true)
            {
                // Start Counting To Get Up To The Random Start Time
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
        // Light 3 Right Bulbs Pro Tree
        rightFirstBulb.GetComponent<Renderer>().material = yellow;
        rightSecondBulb.GetComponent<Renderer>().material = yellow;
        rightThirdBulb.GetComponent<Renderer>().material = yellow;

        // Light 3 Lefts Bulbs Pro Tree
        leftFirstBulb.GetComponent<Renderer>().material = yellow;
        leftSecondBulb.GetComponent<Renderer>().material = yellow;
        leftThirdBulb.GetComponent<Renderer>().material = yellow;

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

        leftFirstBulb.GetComponent<Renderer>().material = yellow;
        leftSecondBulb.GetComponent<Renderer>().material = yellow;
        leftThirdBulb.GetComponent<Renderer>().material = yellow;

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
        leftFirstBulb.GetComponent<Renderer>().material = yellow;


        // Keep it on for .5 Seconds
        StartCoroutine(FiveTenthsFullTree());

        //Tree Dropped
        hasTreeFired = true;
    }
    void fullTreeFire2()
    {
        // Light Second Bulb
        rightSecondBulb.GetComponent<Renderer>().material = yellow;
        leftSecondBulb.GetComponent<Renderer>().material = yellow;


        // Keep it on for .5 Seconds
        StartCoroutine(FiveTenthsFullTree2());

    }
    void fullTreeFire3()
    {

        // Light Bottom Third Bulb
        rightThirdBulb.GetComponent<Renderer>().material = yellow;
        leftThirdBulb.GetComponent<Renderer>().material = yellow;



        // Keep it on for .5 Seconds
        StartCoroutine(FiveTenthsFullTree3());

    }
    void RT()
    {
        if (is5Pro.isOn)
        {
            //Unlight Right Bulbs
            rightFirstBulb.GetComponent<Renderer>().material = unlit;
            rightSecondBulb.GetComponent<Renderer>().material = unlit;
            rightThirdBulb.GetComponent<Renderer>().material = unlit;

            //Unlight Left Bulbs
            leftFirstBulb.GetComponent<Renderer>().material = unlit;
            leftSecondBulb.GetComponent<Renderer>().material = unlit;
            leftThirdBulb.GetComponent<Renderer>().material = unlit;

            //Figure RT Pro .5 Tree Also Using Rollout from save
            reactionTime = RightLaneStage.buttonLetGo - RTtimer + rollOut - fiveTenthsPro;
            leftreactionTime = LeftLaneStage.buttonLetGo - RTtimer + leftrollOut - fiveTenthsPro;
            rightlanestage.displayRT();
            leftlanestage.displayRT();

            // Red or Green? 
            redOrGreen();
        }
        else if (is5Full.isOn)
        {
            //Figure RT Full Tree
            reactionTime = RightLaneStage.buttonLetGo - RTtimer + rollOut - fiveTenthsFull;
            leftreactionTime = LeftLaneStage.buttonLetGo - RTtimer + leftrollOut - fiveTenthsFull;

            rightlanestage.displayRT();
            leftlanestage.displayRT();


            // Red or Green? 
            redOrGreen();
        }
        else if (is4Pro.isOn)
        {
            //Unlight Bulbs
            rightFirstBulb.GetComponent<Renderer>().material = unlit;
            rightSecondBulb.GetComponent<Renderer>().material = unlit;
            rightThirdBulb.GetComponent<Renderer>().material = unlit;

            leftFirstBulb.GetComponent<Renderer>().material = unlit;
            leftSecondBulb.GetComponent<Renderer>().material = unlit;
            leftThirdBulb.GetComponent<Renderer>().material = unlit;

            reactionTime = RightLaneStage.buttonLetGo - RTtimer + leftrollOut - fourTenths;
            leftreactionTime = LeftLaneStage.buttonLetGo - RTtimer + leftrollOut - fourTenths;

            rightlanestage.displayRT();
            leftlanestage.displayRT();


            // Red or Green? 
            redOrGreen();
        }


    }

    // Waits For Player To UnStage To Then Figure Reaction Time
    IEnumerator WaitForPlayerToUnStage()
    {
        yield return new WaitUntil(() => RightLaneStage.isStaged == false && LeftLaneStage.isStaged == false);
        if (is5Full.isOn)
        {
            rightThirdBulb.GetComponent<Renderer>().material = unlit;
            leftThirdBulb.GetComponent<Renderer>().material = unlit;

            RT();
        }
        else
        {
            RT();
        }
    }
    void redOrGreen()
    {
        if (reactionTime < 0 && leftreactionTime < 0)
        {
            rightRedBulb.GetComponent<Renderer>().material = red;
            leftRedBulb.GetComponent<Renderer>().material = red;


        }
        if (reactionTime > 0 && leftreactionTime > 0)
        {
            rightGreenBulb.GetComponent<Renderer>().material = green;
            leftGreenBulb.GetComponent<Renderer>().material = green;
        }
        if (reactionTime < 0 && leftreactionTime > 0)
        {
            rightGreenBulb.GetComponent<Renderer>().material = red;
            leftGreenBulb.GetComponent<Renderer>().material = green;
        }
        if (reactionTime > 0 && leftreactionTime < 0)
        {
            rightGreenBulb.GetComponent<Renderer>().material = green;
            leftGreenBulb.GetComponent<Renderer>().material = red;
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
        StartCoroutine(WaitForPlayerToUnStage());
       
    }
    IEnumerator FiveTenthsFullTree()
    {
        yield return new WaitForSeconds(.5f);
        rightFirstBulb.GetComponent<Renderer>().material = unlit;
        leftFirstBulb.GetComponent<Renderer>().material = unlit;


        //Check To See If They RedLighted Before Lighting Next Bulb
        if (RightLaneStage.isStaged == false)
        {
            RT();
        }
        else
        {
            fullTreeFire2();
        }

    }
    IEnumerator FiveTenthsFullTree2()
    {
        yield return new WaitForSeconds(.5f);
        rightSecondBulb.GetComponent<Renderer>().material = unlit;
        leftSecondBulb.GetComponent<Renderer>().material = unlit;


        //Check To See If They RedLighted Before Lighting Next Bulb
        if (RightLaneStage.isStaged == false)
        {
            RT();
        }
        else
        {
            fullTreeFire3();

        }
    }
    IEnumerator FiveTenthsFullTree3()
    {
        yield return new WaitForSeconds(.5f);
        StartCoroutine(WaitForPlayerToUnStage());
    }
    IEnumerator FourTenthsProTree()
    {
        yield return new WaitForSeconds(.4f);
        StartCoroutine(WaitForPlayerToUnStage());
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

        leftGreenBulb.GetComponent<Renderer>().material = unlit;
        leftGreenBulb.GetComponent<Renderer>().material = unlit;

        //Reset the RT to zero
        rightlanestage.resetRT();
        leftlanestage.resetRT();

    }

    IEnumerator endOfTreePause()
    {
     
        yield return new WaitForSeconds(1.5f);
        resetTree();
    }

    //Changes Right RollOut From Slider In Settings
    public void ChangeRollOut(float SliderValue)
    {
        // Sets RollOut To New Value
        rollOut = SliderValue;
        
        // Saves RollOut value To File 
        PlayerPrefs.SetFloat("RollOut", SliderValue);

        // RollOut Number To UI
        rollOutText.text = PlayerPrefs.GetFloat("RollOut", .3f).ToString();


    }
    //Changes Left RollOut From Slider In Settings
    public void LeftChangeRollOut(float SliderValue)
    {
        // Sets RollOut To New Value
        leftrollOut = SliderValue;

        // Saves RollOut value To File 
        PlayerPrefs.SetFloat("LeftLANRollOut", SliderValue);

        // RollOut Number To UI
        leftrollOutText.text = PlayerPrefs.GetFloat("LeftLANRollOut", .3f).ToString();


    }



}

