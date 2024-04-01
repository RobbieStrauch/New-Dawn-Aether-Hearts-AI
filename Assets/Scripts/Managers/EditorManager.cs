using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System.Text;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    public Camera mainCamera;
    public LayerMask ground;
    public GameObject groundObject;
    public Material transparentMaterial;
    public Material normalMaterial;
    public bool instantiated = false;
    public GameObject item;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Drop()
    {
        item.GetComponent<Collider>().enabled = true;
        item.GetComponent<NavMeshAgent>().enabled = true;
        instantiated = false;
    }

    private void DropItem()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); 

        groundObject.layer = 13;

        if (instantiated && Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Drop();
            }
        }
    }

    void Update()
    {
        if (instantiated)
        {
            groundObject.layer = 0;

            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                item.transform.position = hit.point + Vector3.up * 2;
            }
        }

        DropItem();
    }
}
