using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public PlayerScript.GRANT grantType = PlayerScript.GRANT.SPRAY;
    public Material physicalWorldMaterial;
    public Animator animator;
    public GameObject item;
    bool used = false;
    bool isOpen = false;
    Vector3 initItemPos;

    void Update() {
        if (!used && isOpen && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0))) {
            acquire();
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

    void acquire() {
        used = true;
        Renderer[] renderers = item.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers) {
            r.material = physicalWorldMaterial;
        }
        StartCoroutine(animateAcquire());
    }

    IEnumerator animateAcquire() {
        PlayerScript player = FindObjectOfType<PlayerScript>();
        initItemPos = item.transform.position;
        player.requestGrant(initItemPos);
        float t = 0;
        while (t < 1.0f) {
            t += Time.deltaTime * 1.8f;
            item.transform.position = Vector3.Lerp(initItemPos, player.transform.position + Vector3.up, t * t * t * t);
            yield return null;
        }
        Destroy(item);
        player.grant(grantType);
    }
}
