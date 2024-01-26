using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public bool _playerIsGrounded = true; // Modified for animations

    public static Action OnPlayerGrounded;
    public static Action OnPlayerInAir;
    public static Action<bool> OnPlayerIsGrounded;

    public Animator animatorUpperBody;
    public Animator animatorBodyBody;

    private void Awake()
    {
        Instance = this;
    }

    public float sphereRadius = 0.5f;
    public Vector3 sphereOffset = new Vector3 (0, -0.2f, 0);
    public LayerMask groundLayer;

    private void FixedUpdate()
    {
        var prevGrounded = _playerIsGrounded;
        _playerIsGrounded = Physics.SphereCast(transform.position + sphereOffset, sphereRadius, Vector3.down, out RaycastHit hitInfo, 0.1f, groundLayer);

        if (prevGrounded == false && _playerIsGrounded == true)
        {
            OnPlayerGrounded?.Invoke();
            OnPlayerIsGrounded?.Invoke(true);
        }
        if (prevGrounded == true && _playerIsGrounded == false)
        {
            OnPlayerInAir?.Invoke();
            OnPlayerIsGrounded?.Invoke(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + sphereOffset - new Vector3(0, sphereRadius, 0), sphereRadius);
    }

}
