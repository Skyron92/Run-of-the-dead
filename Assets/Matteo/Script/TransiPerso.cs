using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TransiPerso : MonoBehaviour
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

        Character.Current.MgStarted += (sender, args) =>
        {
            Vector3 position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            transform.position = position;
            _animator.SetBool("OnPNJ", true);
        };
        MiniGameManager.Victory += () =>
        {
            Vector3 position = new Vector3(transform.position.x, -10f, transform.position.z);
            _animator.SetBool("OnPNJ", false);
        };
    }

    public void Pause(float x)
    {
        Time.timeScale = x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Animator _animator;
}
