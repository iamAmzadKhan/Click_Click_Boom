using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck_Manager : MonoBehaviour
{
    public Card cardPrefab;
    public List<Sprite> cardFaces;
    public int columns, rows;
    public float spacing = 2f;

    void Start()
    {
        var total = columns * rows;
        if (total % 2 != 0) throw new Exception("Even number of cards needed");

        var pairs = total / 2;
        var faces = cardFaces.Take(pairs).ToList();
        var ids = faces.Select((f, i) => new { f, id = $"card_{i}" }).ToList();

        var cards = ids.Concat(ids)
                      .OrderBy(x => UnityEngine.Random.value)
                      .ToList();

        for (int i = 0; i < total; i++)
        {
            var x = (i % columns) * (spacing);
            var y = (i / columns) * -(spacing);
            var info = cards[i];
            var card = Instantiate(cardPrefab, new Vector3(transform.position.x-5+x, transform.position.y +2.5f+y), Quaternion.identity, transform);
            card.Init(info.id, info.f);
        }
    }
}
