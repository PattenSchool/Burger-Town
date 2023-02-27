using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If an object is hit, then switch selected platforms
/// </summary>
public class CollisionManagerScript : PlatformChangeManager
{
    #region Targets
    [SerializeField]
    protected GameObject defaultTarget;

    [SerializeField]
    protected GameObject altTarget;
    #endregion

    #region Unity Methods
    private void Start()
    {
        if (!IsListsNull())
            SetAppropriatePlatformsNoSafegaurd();

        altTarget.SetActive(false);
    }
    #endregion

    protected override void SetDefaultPlatformBool()
    {
        isDefault = (defaultTarget.activeInHierarchy && !altTarget.activeInHierarchy);
    }

    protected override void SetAppropriatePlatformsNoSafegaurd()
    {
        //Set platforms original uses
        base.SetAppropriatePlatformsNoSafegaurd();

        //Set the targets to their state if one or the other is changed
        altTarget.SetActive(!isDefault);
        defaultTarget.SetActive(isDefault);
    }
}
