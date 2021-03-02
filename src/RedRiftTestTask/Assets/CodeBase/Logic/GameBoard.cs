using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private Transform _battlePanel;
        [SerializeField] private List<CardHand> _hands;

        public List<CardHand> Hands => _hands;
    }
}