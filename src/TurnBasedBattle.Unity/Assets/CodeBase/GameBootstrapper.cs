using System;
using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.EventBus;
using UnityEngine;

namespace CodeBase
{
    public class GameBootstrapper : MonoBehaviour
    {
        private void Start()
        {
            var bus = new EventBus<ICommand>();
            
        }
    }
}
