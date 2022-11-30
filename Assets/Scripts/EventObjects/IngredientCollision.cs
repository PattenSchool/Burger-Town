using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCollision : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        // Gameobject variable for another gameobject's collision
        GameObject otherObject = collision.gameObject;

        //Checks to see if the other gameobject has the "Player" tag.
        if (otherObject.tag == "Player")
        {
            //Checks to see if this gameobject is the Bun_Top ingredient.
           if(this.gameObject.ToString() == "Bun_Top (UnityEngine.GameObject)")
            {
                //Switches the top bun static boolean to true in the TestIngredientUI script.
                TestIngredientUI.topBunAcquired = true;

                Debug.Log("Top Bun Acquired!");
            }

            //Checks to see if this gameobject is the Cheese ingredient.
            if (this.gameObject.ToString() == "Cheese (UnityEngine.GameObject)")
            {
                //Switches the cheese static boolean to true in the TestIngredientUI script.
                TestIngredientUI.cheeseAcquired = true;

                Debug.Log("Cheese Acquired!");
            }

            //Checks to see if this gameobject is the Patty ingredient.
            if (this.gameObject.ToString() == "Patty (UnityEngine.GameObject)")
            {
                //Switches the patty static boolean to true in the TestIngredientUI script.
                TestIngredientUI.pattyAcquired = true;

                Debug.Log("Patty Acquired!");
            }

            //Checks to see if this gameobject is the Bun_Bottom ingredient.
            if (this.gameObject.ToString() == "Bun_Bottom (UnityEngine.GameObject)")
            {
                //Switches the bottom bun static boolean to true in the TestIngredientUI script.
                TestIngredientUI.bottomBunAcquired = true;

                Debug.Log("Bottom Bun Acquired!");
            }
            Destroy(this.gameObject);
        }
    }
}
