using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundShadow : MonoBehaviour
{
    public static PlayerGroundShadow Instance;
    [SerializeField] MeshRenderer shadowMeshRenderer;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        PlayerManager.OnPlayerIsGrounded += PlayerManagerJumped;
    }
    private void OnDisable()
    {
        PlayerManager.OnPlayerIsGrounded -= PlayerManagerJumped;
    }

    void Update()
    {
        if (PlayerManager.Instance == null)
        {
            return;
        }
        // if (!shadowMeshRenderer.enabled) return;
        var pos = PlayerManager.Instance.transform.position;
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
    }

    void PlayerManagerJumped(bool isInAir)
    {
        if (PlayerHive.Instance.playerInSaw)
        {
            return;
        }
        ToggleShadow(!isInAir);
    }

    public void ToggleShadow(bool toggle)
    {
        shadowMeshRenderer.enabled = toggle;
    }
}
