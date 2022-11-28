using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIngredientUI : MonoBehaviour
{
    public static bool topBunAcquired = false;
    public static bool cheeseAcquired = false;
    public static bool pattyAcquired = false;
    public static bool bottomBunAcquired = false;

    public GameObject topBun;
    public GameObject cheese;
    public GameObject patty;
    public GameObject bottomBun;

    public GameObject topBunGray;
    public GameObject cheeseGray;
    public GameObject pattyGray;
    public GameObject bottomBunGray;

    // Update is called once per frame
    void Update()
    {
        checkForIngredients();
    }

    void checkForIngredients()
    {
        //Checks to see if the top bun boolean is set to false.  
        //If so, then set the top bun sprite to inactive and the gray variant sprite to active.
        //Else, set the top bun sprite to active and the gray variant sprite to inactive. 
        if (topBunAcquired == false)
        {
            topBun.SetActive(false);
            topBunGray.SetActive(true);
        }
        else
        {
            topBun.SetActive(true);
            topBunGray.SetActive(false);
        }


        //Checks to see if the cheese boolean is set to false.  
        //If so, then set the cheese sprite to inactive and the gray variant sprite to active.
        //Else, set the cheese sprite to active and the gray variant sprite to inactive. 
        if (cheeseAcquired == false)
        {
            cheese.SetActive(false);
            cheeseGray.SetActive(true);
        }
        else
        {
            cheese.SetActive(true);
            cheeseGray.SetActive(false);
        }

        //Checks to see if the patty boolean is set to false.  
        //If so, then set the patty sprite to inactive and the gray variant sprite to active.
        //Else, set the patty sprite to active and the gray variant sprite to inactive. 
        if (pattyAcquired == false)
        {
            patty.SetActive(false);
            pattyGray.SetActive(true);
        }
        else
        {
            patty.SetActive(true);
            pattyGray.SetActive(false);
        }

        //Checks to see if the bottom bun boolean is set to false.  
        //If so, then set the bottom bun sprite to inactive and the gray variant sprite to active.
        //Else, set the bottom bun sprite to active and the gray variant sprite to inactive. 
        if (bottomBunAcquired == false)
        {
            bottomBun.SetActive(false);
            bottomBunGray.SetActive(true);
        }
        else
        {
            bottomBun.SetActive(true);
            bottomBunGray.SetActive(false);
        }
    }
}
