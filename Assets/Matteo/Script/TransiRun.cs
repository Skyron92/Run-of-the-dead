using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TransiRun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        Character.Current.Dead += () =>
        {
            transform.DOMoveY(0f, 0.5f).onComplete += () => transform.DOMoveY(2.2f, 0.5f).onComplete += () => transform.DOMoveY(0f, 0.5f);
            _animator.SetBool("Hurt", true);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Animator _animator;
}
