using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private Transform _battlePanel;
        [SerializeField] private List<CardHand> _handPoints;

        public List<CardHand> Hands => _handPoints;
    }
}