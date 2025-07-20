using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour, IFlipService, IMatchService
{
    public string CardId { get; private set; }
    public bool IsFaceUp { get; private set; }
    private bool _isMatched = false;

    [SerializeField] private TMP_Text CardNumber;
    [SerializeField] private SpriteRenderer cardImage;
    [SerializeField] private Sprite faceSprite, backSprite;
 
    private IAnimatorService animatorService;
    public void Init(string number)
    {
        CardId = number;
        faceSprite = backSprite;
        cardImage.sprite = backSprite;
        animatorService = GetComponent<CardFlipAnimator>();
        if (animatorService == null)
            animatorService = gameObject.AddComponent<CardFlipAnimator>();
    }

    public void Init(string cardId, Sprite face)
    {
        CardId = cardId;
        faceSprite = face;
        cardImage.sprite = backSprite;

        animatorService = GetComponent<CardFlipAnimator>();
        if (animatorService == null)
            animatorService = gameObject.AddComponent<CardFlipAnimator>();
    }

    void OnMouseDown()
    {
        Debug.Log($"Card clicked: {CardId}");

        if (!_isMatched && !IsFaceUp && !animatorService.IsAnimating)
        {
            Flip();
            Card_Manager.Instance.OnCardSelected(this);
            Sound_Manager.Instance.PlayClick();
        }
    }

    public void Flip()
    {
        animatorService.AnimateFlip(transform, () => {
            IsFaceUp = !IsFaceUp;
            cardImage.sprite = IsFaceUp ? faceSprite : backSprite;
            CardNumber.alpha = IsFaceUp ? 1.0f : 0.0f;
        }, null);
    }

    public void SetMatched(bool matched) => _isMatched = matched;
}
