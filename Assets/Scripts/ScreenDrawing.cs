using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDrawing : MonoBehaviour
{
    public bool m_debugMode = true;
    public Transform m_boundGameObjectsParent;
    public float m_zOffset = .2f;

    public List<Transform> m_boundObjects;
    private Transform m_activeObject;
    private bool m_settingActiveObject = false;
    private bool m_donePlacing = false;
    private int m_activated = 0;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        GetBoundObjects();
    }

    void OnGUI()
    {
        if (m_donePlacing)
            return;

        Event e = Event.current;

        //Debug.Log("Current event detected: " + Event.current.type);
        Vector3 point = Vector3.zero;

        Vector2 mousePos = Vector2.zero;


        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = e.mousePosition.x;
        mousePos.y = cam.pixelHeight - e.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane + m_zOffset));

        if (e.type == EventType.MouseUp && m_settingActiveObject)
        {
            m_activeObject = null;
            m_settingActiveObject = false;
            Debug.Log("World position: " + point.ToString("F3"));
        }
        else if (!m_donePlacing && e.type == EventType.MouseDown)
        {

            if (m_activated >= m_boundObjects.Count)
            {
                m_donePlacing = true;
                Collider[] cols = m_boundGameObjectsParent.GetComponentsInChildren<Collider>();
                foreach (Collider c in cols)
                    c.enabled = true;


                Debug.Log("All the objects are placed at " + Time.time);
            }

            if (m_activeObject == null)
            {
                foreach (Transform g in m_boundObjects)
                {
                    if (!g.gameObject.activeSelf && !m_settingActiveObject)
                    {
                        g.gameObject.SetActive(true);
                        m_activeObject = g;
                        m_settingActiveObject = true;
                        m_activated++;
                    }

                }
            }
        }

        if (m_settingActiveObject && m_activeObject != null)
        {
            m_activeObject.position = point;
        }


        if (m_debugMode)
        {

            GUILayout.BeginArea(new Rect(20, 20, 450, 220));
            GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
            GUILayout.Label("Mouse position: " + mousePos.ToString("F3"));
            GUILayout.Label("World position: " + point.ToString("F3"));
            GUILayout.EndArea();
        }


    }


    public void GetBoundObjects()
    {
        m_donePlacing = false;
        m_activeObject = null;
        m_activated = 0;
        m_settingActiveObject = false;

        m_boundObjects = new List<Transform>();

        Transform[] temp = m_boundGameObjectsParent.GetComponentsInChildren<Transform>(true);

        foreach (Transform t in temp)
        {
            if (t.parent == m_boundGameObjectsParent)
            {
                m_boundObjects.Add(t);
                t.gameObject.SetActive(false);
            }
        }
    }
}
