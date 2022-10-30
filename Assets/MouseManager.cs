using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hits = Physics2D.RaycastAll(
                Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero
            );

            foreach (var hit in hits)
            {
                var mouseInteractable = hit.collider.GetComponent<MouseInteractable>();
                if (mouseInteractable != null)
                {
                    mouseInteractable.OnMouseClick();
                }
            }
        }
    }
}

public interface MouseInteractable
{
    void OnMouseClick();
}
