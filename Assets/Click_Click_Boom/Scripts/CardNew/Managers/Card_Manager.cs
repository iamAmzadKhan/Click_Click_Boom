using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Manager : MonoBehaviour
{
    public static Card_Manager Instance { get; private set; }
    private Card _first, _second;
    private bool _accepting = true;
    public float delay = 1f;

    void Awake() => Instance = this;

    public void OnCardSelected(Card c)
    {
        if (!_accepting) return;
        if (_first == null)
        {
            _first = c;
        }
        else if (_second == null && c != _first)
        {
            _second = c;
            CheckMatch();
        }
    }

    private void CheckMatch()
    {
        _accepting = false;
        if (_first.CardId == _second.CardId)
        {
            _first.SetMatched(true); _second.SetMatched(true);
            Sound_Manager.Instance.PlayMatch();
            Game_Manager.Instance.ScoreService.AddPoint();
            Game_Manager.Instance.ScoreService.AddTries();
            StartCoroutine(ClearCards());
        }
        else
        {
            Game_Manager.Instance.ScoreService.AddTries();
            Sound_Manager.Instance.PlayFail();
            StartCoroutine(FlipBack());
        }
    }

    private IEnumerator ClearCards()
    {
        yield return new WaitForSeconds(delay);
        Destroy(_first.gameObject);
        Destroy(_second.gameObject);
        ResetState();
        Game_Manager.Instance.OnPairCleared();
    }

    private IEnumerator FlipBack()
    {
        yield return new WaitForSeconds(delay);
        _first.Flip(); _second.Flip();
        ResetState();
    }

    private void ResetState()
    {
        _first = _second = null;
        _accepting = true;
    }
}
