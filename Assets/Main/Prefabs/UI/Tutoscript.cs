using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tutoscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MakeTheMove();
    }

    private void MakeTheMove()
    {
        float _midRef = transform.position.x;
        float _midYref = transform.position.y;
        float _leftRef = transform.position.x - 200;
        float _rightRef = transform.position.x + 200;
        float _topRef = transform.position.y + 300;

        transform.DOMoveX(_leftRef, 0.75f).onComplete += () => transform.DOMoveX(_midRef, 0.75f).onComplete += () =>
            transform.DOMoveX(_rightRef, 0.75f).onComplete += () =>
                transform.DOMoveX(_midRef, 0.75f).onComplete += () => transform.DOMoveY(_topRef, 0.75f).onComplete += () => transform.DOMoveY(_midYref, 0.75f).onComplete += MakeTheMove;
    
    }
}
