using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class LaunchArcRenderer : MonoBehaviour { 

    [SerializeField] private int _resolution = 10;

    private LineRenderer _lr;
    private float _velocity = 5;
    private float _angle = 45;
    private float _height;
    private float _time = 0;
    private float _distance = 0;
    private float _g;
    private float _radianAngle;  
    private int _total = 0;
    private Vector2 [] _wayPoints;


 
    private void Awake () {
        _lr = GetComponent<LineRenderer>();
        _g = Mathf.Abs(Physics2D.gravity.y);
    }


    private void DrawArc() {
        _lr.positionCount = _resolution + 1;
        _lr.SetPositions(CalculateArcArray());
    }

    public Vector2[] TakePoints(float distance, float height, float angle) {
        if (_lr != null) {
            this._distance = distance;
            this._height = height;
            this._angle = angle;

            DrawArc();

            Vector3 [] temp = new Vector3[15];
            _total = _lr.GetPositions(temp);
            _wayPoints = new Vector2[_total];

            for(int i = 0; i< _total; i++) {
                _wayPoints[i] = transform.TransformPoint(temp[i]);
            }  

            return _wayPoints;
        }
        return null;
    }

  

    private Vector3[] CalculateArcArray() {
        Vector3[] arcArray = new Vector3[_resolution + 1];

        _radianAngle = Mathf.Deg2Rad * _angle;

        float a = (0.5f * _g * (Mathf.Pow(_distance / Mathf.Cos(_radianAngle), 2f)));
        float b = _height + (Mathf.Sin(_radianAngle) * (_distance / Mathf.Cos(_radianAngle)));
        _velocity = Mathf.Sqrt(a / b);

        _time = _distance / (_velocity * Mathf.Cos(_radianAngle));

        for(int i = 0; i <= _resolution; i++){
            float t = (float) (i * _time) / (float) _resolution;
            arcArray[i] = CalculateArcPoint(t);
        }
 
        return arcArray; 
    }
 
    private Vector3 CalculateArcPoint(float t){
        float x = t * (_velocity * Mathf.Cos(_radianAngle));
        float y = _velocity * Mathf.Sin(_radianAngle) * t - ((_g * 0.5f) * t * t);
        return new Vector3(x, y);
    }
}