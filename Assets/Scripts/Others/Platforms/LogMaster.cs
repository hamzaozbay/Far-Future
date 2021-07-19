using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMaster : MonoBehaviour {

    [SerializeField] private GameObject _logPrefab;
    [SerializeField] private int _logCount;
    [SerializeField] private float _logFallStartTime, _nextLogFallTime;

    private List<GameObject> _logs = new List<GameObject>();
    private int _index = 0;
    private bool _canDrop = false;



    private void Start() {
        CreateLogs();
    }


    public void CreateLogs() {
        for (int i = 0; i < _logCount; i++) {
            GameObject log = Instantiate(_logPrefab, this.transform);
            log.transform.localPosition = new Vector3(i * 0.725f, 0f);
            _logs.Add(log);
        }
    }


    private void DropLog() {
        if (_index >= _logs.Count) return;

        if (_canDrop) {
            Rigidbody2D rb = _logs[_index].GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            rb.gravityScale = 3f;
            _index++;
            Invoke("DropLog", _nextLogFallTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            _canDrop = true;
            Invoke("DropLog", _logFallStartTime);
        }
    }

}
