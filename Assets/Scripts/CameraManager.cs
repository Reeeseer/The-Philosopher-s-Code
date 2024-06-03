using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    List<CameraAngle> _angles;
    [SerializeField] CinemachineVirtualCamera _defaultCamera;
    [SerializeField] float _timeBetweenAngles;

    int _index;
    float _timeToNextAngle;

    public bool Stopped { get; private set; }

    private void OnEnable()
    {
        _angles = FindObjectsOfType<CameraAngle>().ToList();
        Stopped = false;
        _defaultCamera.Priority = 0;
    }
    private void Update()
    {
        if (Time.time >= _timeToNextAngle && !Stopped)
        {
            ChangeAngle();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !Stopped)
        {
            StopCameraMovement();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && Stopped)
        {
            Stopped = false;
            _defaultCamera.Priority = 0;
        }
    }

    private void StopCameraMovement()
    {
        Stopped = true;
        _defaultCamera.Priority = 10;
    }

    private void ChangeAngle()
    {
        _timeToNextAngle = Time.time + _timeBetweenAngles;
        _index++;
        if (_index == _angles.Count)
            _index = 0;

        foreach (var a in _angles)
        {
            if (_angles.IndexOf(a) != _index)
            {
                StartCoroutine(a.Disable());
            }
            else
            {
                a.Enable();
            }
        }
    }
}
