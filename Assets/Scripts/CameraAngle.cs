using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraAngle : MonoBehaviour
{
    [SerializeField] Transform _start;
    [SerializeField] Transform _end;
    [SerializeField] float _timeForSway;

    CinemachineVirtualCamera _cine;

    private void OnEnable()
    {
        _cine = GetComponent<CinemachineVirtualCamera>();
    }
    internal IEnumerator Disable()
    {
        _cine.Priority = 0;
        yield return new WaitForSeconds(5f);
        transform.SetPositionAndRotation(_start.position, _start.rotation);
    }

    internal void Enable()
    {
        StartCoroutine(CameraSway());
        _cine.Priority = 1;
    }

    private IEnumerator CameraSway()
    {
        float a = 0;
        while (a < 0.99f)
        {
            Vector3 newPosition = new Vector3(Mathf.Lerp(_start.position.x, _end.position.x, a),
                Mathf.Lerp(_start.position.y, _end.position.y, a),
                Mathf.Lerp(_start.position.z, _end.position.z, a));

            //Vector3 newRotation = new Vector3(Mathf.Lerp(_start.rotation.x, _end.rotation.x, a),
            //    Mathf.Lerp(_start.rotation.y, _end.rotation.y, a),
            //    Mathf.Lerp(_start.rotation.z, _end.rotation.z, a));

            transform.position = newPosition;
            a += Time.deltaTime / _timeForSway;
            yield return null;
        }

        a = 1;
        transform.position = _end.position;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(_start.position, Vector3.one * 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_end.position, Vector3.one * 0.5f);
    }
#endif 
}
