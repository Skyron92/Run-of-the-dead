using System;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private IMiniGame _miniGameReference;

    private void BindEvent() {
        if(_miniGameReference == null) return;
        _miniGameReference.MiniGameWon += MyMethod();

    }

    private IMiniGame.MiniGameWonEvent MyMethod()
    {
        throw new NotImplementedException();
    }
}