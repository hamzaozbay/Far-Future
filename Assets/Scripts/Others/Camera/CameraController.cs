using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float smooth = 7f;

    [SerializeField] private Vector2 _offset = new Vector2(2.85f, 0f);
    [SerializeField] private Vector2 _mapBoundaries;



    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void LateUpdate() {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + _offset.x, transform.position.y, transform.position.z), smooth * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _mapBoundaries.x, _mapBoundaries.y), _offset.y, -10f);
    }

}
