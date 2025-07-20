using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck_Manager : MonoBehaviour
{
    public static Deck_Manager Instance { get; private set; }
    public Card cardPrefab;
    public Card card2Prefab;
    public List<Sprite> cardFaces;
    public int columns, rows;
    public float spacing = 2f;

    public Vector3 _InstaPosition;
    public int _Width, _Height;
    public bool _Floor = false;
    public int randomNumber = 0;
    public int Row = 0;
    public int Column = 0;
    public List<int> GridNumbers = new List<int>();
    public List<int> RandomNumbers = new List<int>();
    public int PairCounter = 1;
    public int UniqueIndex = 0;
    public GameObject _InstantiatedTile;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UniqueIndex = 1;
        _InstaPosition = Vector3.zero;
        //CreateDeck(2, 2);

        //PlayGame(6, 6, true);
    }

    public void CreateDeck(int rows, int columns)
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
            var card = Instantiate(cardPrefab, new Vector3(transform.position.x - 5 + x, transform.position.y + 2.5f + y), Quaternion.identity, transform);
            card.Init(info.id, info.f);
        }
    }

    public void SetDeck(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
    }

    public void PlayGame(int width, int height, bool IsFloor)
    {
        _Width = width;
        _Height = height;
        _Floor = IsFloor;
        CheckValidInput();
        GenerateList();
        GenerateGrid();
    }

    void GenerateList()
    {
        CheckValidInput();
        for (int i = 0; i < _Width * _Height; i++)
        {
            randomNumber = UnityEngine.Random.Range(0, (_Width * _Height));
            Debug.Log(randomNumber);

            while (RandomNumbers.Contains(randomNumber))
            {
                randomNumber = UnityEngine.Random.Range(0, (_Width * _Height));
            }
            RandomNumbers.Add(randomNumber);
            if (UniqueIndex > 0 && UniqueIndex < (((_Width * _Height) / 2) + 1))
            {
                GridNumbers.Add(UniqueIndex);
                UniqueIndex++;
            }
            else
            {
                UniqueIndex = 1;
                GridNumbers.Add(UniqueIndex);
                UniqueIndex++;
            }
        }
    }

    void GenerateGrid()
    {
        CheckValidInput();
        if (card2Prefab.gameObject == null)
        {
            return;
        }

        _InstaPosition = Vector3.zero;
        for (int Row = 0; Row < _Height; Row++)
        {
            for (int Column = 0; Column < _Width; Column++)
            {
                _InstaPosition = new Vector3(Column * 3f, Row*3f);
                var card = Instantiate(card2Prefab, _InstaPosition, Quaternion.identity, transform);
                _InstantiatedTile = card.gameObject;
                _InstantiatedTile.transform.name = GridNumbers[RandomNumbers[Row * _Width + Column]].ToString();
                _InstantiatedTile.GetComponent<Tile>()?.SetText(GridNumbers[RandomNumbers[Row * _Width + Column]].ToString());
                _InstantiatedTile.GetComponent<Tile>()._TextMesh.alpha = 0.0f;
                _InstantiatedTile.transform.SetParent(gameObject.transform);
                card.Init(_InstantiatedTile.transform.name);
            }
        }
    }

    void CheckValidInput()
    {
        if (_Width < 2)
        {
            _Width = 2;
        }
        else if (_Width >= 10)
        {
            _Width = 10;
        }

        if (_Height < 2)
        {
            _Height = 2;
        }
        else if (_Height >= 10)
        {
            _Height = 10;
        }

        bool IsGreater = false;
        while (_Width * _Height % 2 != 0)
        {
            if (_Floor)
            {
                IsGreater = (_Width > _Height);

                if (!IsGreater && _Height % 2 != 0)
                {
                    if (_Height > 2)
                    {
                        _Height--;
                    }
                    else if (_Height < 2)
                    {
                        _Height++;
                    }
                    else
                    {
                        _Height = 2;
                    }
                }
                else if (IsGreater && _Width % 2 != 0)
                {
                    if (_Width > 2)
                    {
                        _Width--;
                    }
                    else if (_Width < 2)
                    {
                        _Width++;
                    }
                    else
                    {
                        _Width = 2;
                    }
                }
            }
            else
            {
                IsGreater = (_Width > _Height);
                if (IsGreater && _Height % 2 != 0)
                {
                    if (_Height > 2)
                    {
                        _Height++;
                    }
                    else if (_Height < 2)
                    {
                        _Height++;
                    }
                    else
                    {
                        _Height = 2;
                    }
                }
                else if (!IsGreater && _Width % 2 != 0)
                {
                    if (_Width > 2)
                    {
                        _Width++;
                    }
                    else if (_Width < 2)
                    {
                        _Width++;
                    }
                    else
                    {
                        _Width = 2;
                    }
                }
            }

        }
        SetCameraPosition(_Width, _Height);
    }

    void SetCameraPosition(float width, float height)
    {
        float up = (width > height) ? width : height;
        Camera.main.transform.position = new Vector3((float)(width), (float)(height),- up * 4f);
    }

    public void ClearGame()
    {
        if (gameObject.transform.childCount > 0)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }
        GridNumbers.Clear();
        RandomNumbers.Clear();
        UniqueIndex = 1;
        _InstaPosition = Vector3.zero;
    }
}
