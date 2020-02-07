//You don't need to do anything with this script, just place it somewhere in your project. It creates automatically a GameObject when you use the LoadLevel function.
//
//To load a new level just write in any of your scripts:
//
// AutoFade.LoadLevel(1 ,3,1,Color.black);
// // or
// AutoFade.LoadLevel("MyLevelName" ,3,1,Color.black);
// 
//The 4 parameters are:
//
//LevelName or LevelIndex
//
//FadeOutTime - time in seconds until the level actually starts loading
//
//FadeInTime - time in seconds until the fade-in process is completed.
//
//FadeColor - this is the color the screen fades to.
//
//Keep in mind that after calling AutoFade.LoadLevel the game continues "FadeOutTime" seconds. You might want to block Input or something while the fade-process is active. Just check if AutoFade.Fading is true.

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AutoFade : MonoBehaviour
{
	private static AutoFade m_Instance = null;
	public Material m_Material = null;
	private string m_LevelName = "";
	private int m_LevelIndex = 0;
	private bool m_Fading = false;

	private static AutoFade Instance {
		get {
			if (m_Instance == null) {
				m_Instance = (new GameObject ("AutoFade")).AddComponent<AutoFade> ();

			}
			return m_Instance;
		}
	}
	public static bool Fading {
		get { return Instance.m_Fading; }
	}

	private void Awake ()
	{
		DontDestroyOnLoad (this);
		m_Instance = this;
		m_Material = (Material)Resources.Load ("fadeMaterial", typeof (Material));

		//m_Material = new Material("Shader \"Plane/No zTest\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha ZWrite Off Cull Off Fog { Mode Off } BindChannels { Bind \"Color\",color } } } }");
		gameObject.layer = 5;

	}

	private void DrawQuad (Color aColor, float aAlpha)
	{
		aColor.a = aAlpha;
		m_Material.SetPass (0);
		GL.PushMatrix ();
		GL.LoadOrtho ();
		GL.Begin (GL.QUADS);
		GL.Color (aColor);   // moved here, needs to be inside begin/end
		GL.Vertex3 (0, 0, -1);
		GL.Vertex3 (0, 1, -1);
		GL.Vertex3 (1, 1, -1);
		GL.Vertex3 (1, 0, -1);
		GL.End ();
		GL.PopMatrix ();
	}


	private IEnumerator Fade (float aFadeOutTime, float aFadeInTime, Color aColor)
	{
		float t = 0.0f;
		while (t < 1.0f) {
			yield return new WaitForEndOfFrame ();
			t = Mathf.Clamp01 (t + Time.deltaTime / aFadeOutTime);
			DrawQuad (aColor, t);
		}
		if (m_LevelName != "")
			SceneManager.LoadScene (m_LevelName);
		else
			SceneManager.LoadScene (m_LevelIndex);
		while (t > 0.0f) {
			yield return new WaitForEndOfFrame ();
			t = Mathf.Clamp01 (t - Time.deltaTime / aFadeInTime);
			DrawQuad (aColor, t);
		}
		m_Fading = false;
	}
	private void StartFade (float aFadeOutTime, float aFadeInTime, Color aColor)
	{
		m_Fading = true;
		StartCoroutine (Fade (aFadeOutTime, aFadeInTime, aColor));
	}

	public static void LoadLevel (string aLevelName, float aFadeOutTime, float aFadeInTime, Color aColor)
	{
		if (Fading)
			return;
		Instance.m_LevelName = aLevelName;
		Instance.StartFade (aFadeOutTime, aFadeInTime, aColor);
	}
	public static void LoadLevel (int aLevelIndex, float aFadeOutTime, float aFadeInTime, Color aColor)
	{
		if (Fading)
			return;
		Instance.m_LevelName = "";
		Instance.m_LevelIndex = aLevelIndex;
		Instance.StartFade (aFadeOutTime, aFadeInTime, aColor);
	}
}
