using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class MoveOnMouseDown : MonoBehaviour
{
    private Collider m_collider;
    private bool m_isGrabbed = false;
    private Camera cam;
    private float m_zOffset = 1f;

    private void Awake()
    {
        m_collider = this.GetComponent<Collider>();
        cam = Camera.main;


        ScreenDrawing s = FindObjectOfType<ScreenDrawing>();
        m_zOffset = s.m_zOffset;
    }


    void OnDisable()
    {
        m_collider.enabled = false;
    }


    void Update()
    {
        if (m_isGrabbed)
        {

            Vector2 mousePos = new Vector2();

            mousePos.x = Input.mousePosition.x;
            mousePos.y = Input.mousePosition.y;

            this.transform.position = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane + m_zOffset));
        }
    }

    void OnMouseDown()
    {
        m_isGrabbed = true;
    }

    private void OnMouseUp()
    {
        m_isGrabbed = false;
    }
}
