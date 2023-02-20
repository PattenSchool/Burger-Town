using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChangeManager : MonoBehaviour
{
    #region Platforms
    [Header("Platform Types")]

    [Tooltip("Platforms that appear if the default bolt is switched to")]
    [SerializeField]
    protected GameObject[] defaultBoltPlatforms;

    [Tooltip("Platforms that change if not the default bolt is switched to")]
    [SerializeField]
    protected GameObject[] altBoltPlatforms;

    [Tooltip("If on default or not")]
    [SerializeField]
    protected bool isDefault = true;
    #endregion

    #region Unity Methods
    protected void Start()
    {
        if (!IsListsNull())
            SetAppropriatePlatformsNoSafegaurd();
    }

    protected void Update()
    {
        //Set the platform bool
        SetDefaultPlatformBool();

        //Set the platform
        SetAppropriatePlatforms();
    }
    #endregion

    #region Platform Methods
    /// <summary>
    /// Set the default boolean check
    /// </summary>
    protected virtual void SetDefaultPlatformBool()
    {
        isDefault = (PlayerStatic.BoltSelected.name == PlayerStatic.DefaultBolt.name);
    }

    protected virtual void SetAppropriatePlatformsNoSafegaurd()
    {
        //Set the propper platform for default platforms
        foreach (var defaultPlatform in defaultBoltPlatforms)
        {
            defaultPlatform.SetActive(isDefault);
        }

        //Set the propper platform for alt platforms
        foreach (var altPlatform in altBoltPlatforms)
        {
            altPlatform.SetActive(!isDefault);
        }
    }
    
    /// <summary>
    /// Sets the propper platforms to the 
    /// </summary>
    protected virtual void SetAppropriatePlatforms()
    {
        //Safegaurd against null
        if (IsListsNull())
        {
            return;
        }

        //Checks if propper platforms were already enabled
        if (IsPropperPlatformsAlreadyEnabled())
        {
            return;
        }

        SetAppropriatePlatformsNoSafegaurd();

        print("Bolt thing switched");
    }
    #endregion

    #region Safegaurd Methods
    /// <summary>
    /// Checks if either platform lists is null or empty
    /// </summary>
    /// <returns></returns>
    protected virtual bool IsListsNull()
    {
        //Safegaurd against length
        if ((defaultBoltPlatforms.Length * altBoltPlatforms.Length) == 0)
        {
            return true;
        }

        //Safegaurd against element nullity
        if (defaultBoltPlatforms[0] == null)
        {
            return true;
        }
        else if (altBoltPlatforms[0] == null)
        {
            return true;
        }

        //Return not null
        return false;
    }

    /// <summary>
    /// Returns true if propper platforms were already enabled
    /// </summary>
    /// <returns></returns>
    protected virtual bool IsPropperPlatformsAlreadyEnabled()
    {
        //If default and enabled, then return true
        if (defaultBoltPlatforms[0].activeInHierarchy && isDefault)
            return true;

        //If alt is enabled and default not changed, then returned true
        if (altBoltPlatforms[0].activeInHierarchy && !isDefault)
            return true;

        //else return false
        return false;
    }
    #endregion
}
