using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public delegate void EventHandler(object sender, EventArgs e);

    public event EventHandler DisplayEnded;

    private TextMeshProUGUI _tmpDialog;

    private string DisplayedText {
        get {
            return _tmpDialog.text;
        }
        set {
            _tmpDialog.text = value;
        }
    }

    [SerializeField] private List<string> dialogContent;

    private int _index;
    private int Index {
        get => _index;
        set {
            if (_index >= dialogContent.Count - 1) {
                _isEnded = true;
                DisplayEnded?.Invoke(this, EventArgs.Empty);
                Destroy(gameObject);
            }
            else _index = value;
        }
    }

    private bool _isEnded;
    
    [SerializeField, Range(0.01f, .2f)] private float delay;
    
    private void Awake() {
        _tmpDialog = GetComponent<TextMeshProUGUI>();
        DisplayedText = null;
        StartCoroutine(DisplayLetterByLetter(Index));
    }

    public void DisplayNext() {
        StopAllCoroutines();
        Index++;
        DisplayedText = null;
        StartCoroutine(DisplayLetterByLetter(Index));
    }

    private IEnumerator DisplayLetterByLetter(int index) {
        for (int i = 0; i < dialogContent[index].Length; i++) {
            DisplayedText = dialogContent[index].Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
    }
}