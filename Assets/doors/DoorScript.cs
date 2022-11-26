using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Material physicalWorldMaterial;
    public Animator animator;
    public GameObject item;
    bool used = false;
    bool isOpen = false;

    void Update() {
        if (!used && isOpen && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0))) {
            used = true;
            Renderer[] renderers = item.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers) {
                r.material = physicalWorldMaterial;
            }
        }
    }

    public void openDoor() {
        isOpen = true;
        animator.SetBool("open", true);
    }

    public void closeDoor() {
        isOpen = false;
        animator.SetBool("open", false);
    }
}
