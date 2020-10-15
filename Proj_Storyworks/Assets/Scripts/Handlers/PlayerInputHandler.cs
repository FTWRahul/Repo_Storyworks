using System;
using System.Linq;
using TDC.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace TDC.Handlers
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public PlayerInput playerInput;
        private PlayerMover _mover;

        private void Awake()
        {
            var index = playerInput.playerIndex;
            PlayerMover[] movers = FindObjectsOfType<PlayerMover>();
            _mover = movers.FirstOrDefault(x => x.PlayerIndex == index);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if(_mover != null)
                _mover.SetInputVector(context.ReadValue<Vector2>());
        }
    }
}
