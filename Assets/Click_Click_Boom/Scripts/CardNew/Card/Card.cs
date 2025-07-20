using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, IFlipService, IMatchService
{
    public string CardId { get; private set; }
    public bool IsFaceUp { get; private set; }
    private bool _isMatched = false;

    [SerializeField] private SpriteRenderer cardImage;
    [SerializeField] private Sprite faceSprite, backSprite;

    private IAnimatorService animatorService;

    public void Init(string cardId, Sprite face)
    {
        CardId = cardId;
        faceSprite = face;
        cardImage.sprite = backSprite;

        animatorService = GetComponent<CardFlipAnimatorService>();
        if (animatorService == null)
            animatorService = gameObject.AddComponent<CardFlipAnimatorService>();
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
       
        Debug.Log("Flipping card...");
        animatorService.AnimateFlip(transform, () => {
            IsFaceUp = !IsFaceUp;
            cardImage.sprite = IsFaceUp ? faceSprite : backSprite;
        }, null);
    }

    public void SetMatched(bool matched) => _isMatched = matched;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider2D>().bounds.size);
    }
}
