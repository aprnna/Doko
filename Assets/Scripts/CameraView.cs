using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class CameraView : MonoBehaviour {

    RawImage rawImage;
    AspectRatioFitter fitter;
    WebCamTexture webcamTexture;
    bool ratioSet;

    void Start() {
        rawImage = GetComponent<RawImage>();
        fitter = GetComponent<AspectRatioFitter>();
        InitWebCam();
    }

    void Update() {

        if (!ratioSet) {
            ratioSet = true;
            SetAspectRatio();
        }
    }

    void SetAspectRatio()
    {
        fitter.aspectRatio = ((float)webcamTexture.width / (float)webcamTexture.height);
        transform.localScale = new Vector3(fitter.aspectRatio, fitter.aspectRatio, fitter.aspectRatio);
    }

    void InitWebCam() {
        string camName = WebCamTexture.devices[0].name;
        webcamTexture = new WebCamTexture(camName, Screen.width, Screen.height, 120);
        rawImage.texture = webcamTexture;
        webcamTexture.Play();
    }

    public WebCamTexture GetCamImage() {
        return webcamTexture;
    }

    public void StopCamera()
    {
        webcamTexture.Stop();
    }
}