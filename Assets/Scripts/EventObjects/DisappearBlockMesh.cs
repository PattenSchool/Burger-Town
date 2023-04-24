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
        blockMesh = this.gameObject.GetComponentInChildren<MeshRenderer>();
        meshVisible = false;
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

            // enabled box collider
            // was going to disable gameobject but since this script is attached
            // that won't be possible
            this.GetComponent<MeshCollider>().enabled = true;
        }
        else
        {
            blockMesh.enabled = false;

            // disabled box collider
            this.GetComponent<MeshCollider>().enabled = false;
        }
    }
}
