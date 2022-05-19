using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GyroManager : MonoBehaviour
{
    
    public Gyroscope Gyro { get { return gyro; } }

    private Gyroscope gyro;

    [SerializeField] TMP_Text text;

    public void EnableGyro() {
        gyro = Input.gyro;
        Input.gyro.enabled = true;
	}

	private void Start() {
        EnableGyro();
        
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public Quaternion GetGyroRotation() {
        return gyro.attitude;
	}

	private void Update() {
        Quaternion q = GetGyroRotation();
        Quaternion unityQuaternion = new Quaternion(q.x, q.y, -q.z, -q.w); ;
        text.text = unityQuaternion.eulerAngles.ToString() + "\n" + gyro.userAcceleration;
	}
}
