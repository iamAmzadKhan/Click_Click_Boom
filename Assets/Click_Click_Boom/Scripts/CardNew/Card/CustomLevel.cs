using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomLevel : MonoBehaviour
{
    public TMP_InputField m_Rows;
    public TMP_InputField m_Columns;
    public Toggle m_IsFloor;
    private bool _IsFloor;
    private int _Rows;
    private int _Columns;

    private void Start()
    {
        SetLevel();
    }

    public void SetLevel()
    {
        if (string.IsNullOrEmpty(m_Rows.text))
        {
            m_Rows.text = "2";
            _Rows = System.Convert.ToInt32(m_Rows.text);
        }
        else
        {
            _Rows = System.Convert.ToInt32(m_Rows.text);
        }

        if (_Rows < 2)
        {
            _Rows = 2;
        }
        else if (_Rows >= 10)
        {
            _Rows = 10;
        }

        if (string.IsNullOrEmpty(m_Columns.text))
        {
            m_Columns.text = "2";
            _Columns = System.Convert.ToInt32(m_Columns.text);
        }
        else
        {
            _Columns = System.Convert.ToInt32(m_Columns.text);
        }

        if (_Columns < 2)
        {
            _Columns = 2;
        }
        else if (_Columns >= 10)
        {
            _Columns = 10;
        }
        _IsFloor = m_IsFloor.isOn;
    }

    public void PlayGame()
    {
        SetLevel();
        _Rows = Mathf.Clamp(_Rows, 2, 10);
        _Columns = Mathf.Clamp(_Columns, 2, 10);
        Deck_Manager.Instance.PlayGame(_Rows, _Columns,_IsFloor);
    }

    public void PlayDeck()
    {
        SetLevel();
        _Rows = Mathf.Clamp(_Rows,2, 3);
        _Columns = Mathf.Clamp(_Columns,2, 4);
        Deck_Manager.Instance.CreateDeck(_Rows, _Columns);
    }
}
