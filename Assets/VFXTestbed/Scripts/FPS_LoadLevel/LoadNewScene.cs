using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewScene : MonoBehaviour
{

    public void LoadByID(int levelID)
    {
        AutoFade.LoadLevel(levelID, 1, 1, Color.black);
    }
}
