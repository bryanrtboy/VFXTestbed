using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

[RequireComponent(typeof(Text))]
public class VFXParticleCount : MonoBehaviour
{
    private VisualEffect m_VFX;
    private Text m_UI;

    // Start is called before the first frame update
    void Start()
    {
        m_UI = this.GetComponent<Text>();

        InvokeRepeating("GetParticleCount", 0, .25f);
    }



    void GetParticleCount()
    {

        if (m_VFX != null)
        {
            m_UI.text = "Active Particles: " + m_VFX.aliveParticleCount.ToString("N0");
        }
        else
        {
            m_UI.text = "No Active Particle Systems";

            m_VFX = FindObjectOfType<VisualEffect>();
        }
    }


}
