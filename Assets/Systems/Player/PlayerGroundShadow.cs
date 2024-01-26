using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundShadow : MonoBehaviour
{
    [SerializeField] MeshRenderer shadowMeshRenderer;
    void Update()
    {
        var pos = PlayerManager.Instance.transform.position;
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
    }
    public void ToggleShadow(bool toggle)
    {
        shadowMeshRenderer.enabled = toggle;
    }
}
