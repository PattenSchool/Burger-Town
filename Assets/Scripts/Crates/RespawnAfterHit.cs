using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IResettable))]
public class RespawnAfterHit : MonoBehaviour, IHitable
{
    #region Variables
    [Header("Reference Variables")]

    [Tooltip("The reference to the resettable script")]
    [SerializeField]
    private IResettable[] resettableReferences;
    #endregion

    #region Unity Methods
    private void Start()
    {
        resettableReferences = this.GetComponents<IResettable>();
    }
    #endregion

    public void IHit()
    {
        foreach(var resettable in resettableReferences)
        {
            resettable.IResetTransform();
        }
    }
}
