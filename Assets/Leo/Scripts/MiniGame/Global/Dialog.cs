using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

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
    
    [SerializeField] private LocalizedStringTable localizedStringTable = new LocalizedStringTable();
    private StringTable StringTable => localizedStringTable.GetTable();

    private int _index;
    private int Index {
        get => _index;
        set {
            if (_index >= StringTable.Count - 1) {
                DisplayEnded?.Invoke(this, EventArgs.Empty);
                Destroy(gameObject);
            }
            else _index = value;
        }
    }

    private string CurrentString => GetLocalizedString(StringTable, _index.ToString());
    
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
        for (int i = 0; i < CurrentString.Length; i++) {
            DisplayedText = CurrentString.Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
    }
    
    private string GetLocalizedString(StringTable table, string entryName) {
        return table.GetEntry(entryName).GetLocalizedString();
    }
}