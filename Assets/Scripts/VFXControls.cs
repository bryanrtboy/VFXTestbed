using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


[RequireComponent(typeof(VisualEffect))]
public class VFXControls : MonoBehaviour
{

    VisualEffect m_vfx;

    // Start is called before the first frame update
    void Start()
    {
        m_vfx = this.GetComponent<VisualEffect>();
    }

    public void SetVelocity(float velocity)
    {
        m_vfx.SetFloat("VelocityBlend", velocity);
    }
}
