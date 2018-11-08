using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        float[] spectrum = new float[256];
        float[] outputData = new float[1024];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hanning);
        AudioListener.GetOutputData(outputData, 0);
        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }

        float avg = 0;
        foreach (float f in outputData)
        {
            avg += Mathf.Abs(f);
        }
        avg /= outputData.Length;
        

        float newScale = 1.0f + (avg * 5);
        float oldScale = transform.localScale.y;
        float scale = Mathf.Lerp(oldScale, newScale, Time.deltaTime * 10);
        transform.localScale = new Vector3(scale, scale, scale);
        GetComponent<Renderer>().material.color = Color.HSVToRGB(scale / 2, 1, 1);
    }
}
