using UnityEngine;

public class Laser : MonoBehaviour
{
    const float GravitationalConstant = 0.667408f;

    [SerializeField] bool _manipulatorIsEnabled = false;
    [SerializeField] float _forceMultiplier = 10;

    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _manipulatorIsEnabled = true;
        }
        else if (Input.GetMouseButton(1))
        {
            return;
        }
        else
        {
            _manipulatorIsEnabled = false;
        }
    }

    private void Attract(Rigidbody rbToAttract)
    {
        Vector3 direction = transform.position - rbToAttract.position;
        float distance = direction.magnitude;
        if (distance == 0.0f) { return; }

        float forceMagnitude = GravitationalConstant * (750.0f * rbToAttract.mass) / distance * _forceMultiplier;
        Vector3 force = direction.normalized * forceMagnitude;
        rbToAttract.AddForce(force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_manipulatorIsEnabled) return;

        if (other.gameObject.CompareTag("Cube"))
        {
            Attract(other.gameObject.GetComponent<Rigidbody>());
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Attract(other.gameObject.GetComponent<Rigidbody>());
            other.gameObject.GetComponent<IHealthBehavior>().Damage(1);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!_manipulatorIsEnabled) return;

        if (other.gameObject.CompareTag("Cube"))
        {
            Attract(other.gameObject.GetComponent<Rigidbody>());
        }
        
    }
}
