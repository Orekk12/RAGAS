using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
    public LineRenderer[] rends;

	private void Start ()
    {
        
    }

	private void Update ()
    {
        float[] spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        SetRendsActivity(spectrum[0] > 0.1f);
    }

    private void SetRendsActivity (bool flag)
    {
        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].enabled = flag;
        }
    }
}
