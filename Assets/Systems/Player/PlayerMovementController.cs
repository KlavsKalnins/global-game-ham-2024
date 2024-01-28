using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] KeyCode moveLeft = KeyCode.A;
    [SerializeField] KeyCode moveRight = KeyCode.D;
    [SerializeField] KeyCode moveUp = KeyCode.W;
    [SerializeField] KeyCode moveDown = KeyCode.S;

    [SerializeField] Vector3 _playerMoveInput = Vector3.zero;
    [SerializeField] float speed = 5f;
    [SerializeField] float damping = 2f;

    [SerializeField] Transform lowerBodyTransform;
    public float rotationTime = 0.3f;

    private void Awake()
    {
        _playerMoveInput = Vector3.zero;
    }

    void Update()
    {
        var prevPlayerInput = _playerMoveInput;
        _playerMoveInput = Vector3.zero;
        bool didPressAnyKey = false;
        if (Input.GetKey(moveUp))
        {
            _playerMoveInput += transform.forward;
            didPressAnyKey = true;
        }
        if (Input.GetKey(moveDown))
        {
            _playerMoveInput -= transform.forward;
            didPressAnyKey = true;
        }
        if (Input.GetKey(moveRight))
        {
            _playerMoveInput += transform.right;
            didPressAnyKey = true;
        }
        if (Input.GetKey(moveLeft))
        {
            _playerMoveInput -= transform.right;
            didPressAnyKey = true;
        }
        if (_playerMoveInput == Vector3.zero)
        {
            PlayerManager.Instance.animatorBodyBody.SetBool("IsWalking", false);
        } 
        else
        {
            PlayerManager.Instance.animatorBodyBody.SetBool("IsWalking", true);
        }

        _playerMoveInput.Normalize();
        var movement = _playerMoveInput.normalized * speed * Time.deltaTime;

        _rigidbody.AddForce(movement, ForceMode.VelocityChange);

        // Optional: Apply damping to make the movement snappier
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, damping * Time.deltaTime);

        /*        _rigidbody.AddForce(-_rigidbody.velocity * damping, ForceMode.VelocityChange);
                _rigidbody.AddRelativeForce(_playerMoveInput * speed, ForceMode.Force);*/
        //_rigidbody.AddRelativeForce(_playerMoveInput * speed, ForceMode.Force);

        if (didPressAnyKey)
        {
            float rotationAngle = Mathf.Atan2(_playerMoveInput.x, _playerMoveInput.z) * Mathf.Rad2Deg;
            LeanTween.rotateY(lowerBodyTransform.gameObject, rotationAngle, rotationTime).setEase(LeanTweenType.easeInOutQuad);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemyy") && PlayerHive.Instance.isJumpSmashInvulnerability)
        {
            // PlayerHive.Instance.isJumpSmashInvulnerability = false;
            Destroy(collision.gameObject);
            JumpSmashController.Instance.Smash();
        }
    }
}
