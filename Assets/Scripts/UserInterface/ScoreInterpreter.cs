using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Arkanoid
{
    public class ScoreInterpreter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_InputField _inputField;
        private long score;
        private void Start()
        {
            _inputField.onValueChanged.AddListener(Interpret);
        }
        private void Interpret(string value)
        {
            if (Int64.TryParse(value, out var number))
            {
                _text.text = RoundNumbers(number);
            }

        }
        private string RoundNumbers(long number)
        {
            if (number / 1000000000 > 0)
                return (number / 1000000000).ToString() + "B " + RoundNumbers(number - number / 1000000000 * 1000000000);
            if (number / 1000000 > 0)
                return (number / 1000000).ToString() + "M " + RoundNumbers(number - number / 1000000 * 1000000);
            if (number / 1000 > 0)
                return (number / 1000).ToString() + "K ";
            else
                return number.ToString();
        }

        private void Update()
        {
            
        }
    }
}
