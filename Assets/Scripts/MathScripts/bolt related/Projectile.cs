using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to define hitable objects
/// </summary>
public interface IHitable
{
    public abstract void IHit();
}

/// <summary>
/// The base class for all projectiles
/// </summary>
public class Projectile : MonoBehaviour, IHitable
{
    #region Interface Methods

    /// <summary>
    /// Empty intended for overload in inherited members
    /// </summary>
    public virtual void IHit()
    {
        //Needed for the interface to work with projectiles
    }
    #endregion
}
