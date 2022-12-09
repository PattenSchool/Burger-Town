using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearBlockMesh : MonoBehaviour
{
    public static bool meshVisible = false;
    public MeshRenderer blockMesh;
    // Start is called before the first frame update
    void Start()
    {
        blockMesh = this.gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        checkForMeshVisibility();
    }

    void checkForMeshVisibility()
    {
        if (meshVisible == true)
        {
            blockMesh.enabled = true;
        }
        else
        {
            blockMesh.enabled = false;
        }
    }
}
