using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpperController : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float dampingFactor = 0.95f;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(hit.point);
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        }
    }


}
