using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Dialog of the PNJ
/// </summary>
public class Dialog : MonoBehaviour
{
    public delegate void EventHandler(object sender, EventArgs e);
    public event EventHandler DisplayEnded;

    private TextMeshProUGUI _tmpDialog;

    // Write only variable
    private string DisplayedText {
        set => _tmpDialog.text = value;
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

    /// <summary>
    /// Called by a button
    /// </summary>
    public void DisplayNext() {
        StopAllCoroutines();
        Index++;
        DisplayedText = null;
        StartCoroutine(DisplayLetterByLetter(Index));
    }

    /// <summary>
    /// Display method
    /// </summary>
    /// <param name="index">The index of the string in the dialogContent list to display</param>
    /// <returns></returns>
    private IEnumerator DisplayLetterByLetter(int index) {
        for (int i = 0; i < dialogContent[index].Length; i++) {
            DisplayedText = dialogContent[index].Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
    }
}