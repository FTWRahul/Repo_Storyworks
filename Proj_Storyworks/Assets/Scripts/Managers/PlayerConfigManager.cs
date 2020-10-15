using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace TDC.Managers
{
    public class PlayerConfigManager : MonoBehaviour
    {
        public List<PlayerConfig> playerConfigs = new List<PlayerConfig>();

        private int _maxPlayers = 4;

        private void Awake()
        {
            GameSettings.configManager = this;
            DontDestroyOnLoad(this);
        }

        public void OnPlayerJoin(PlayerInput input)
        {
            PlayerConfig config = ScriptableObject.CreateInstance<PlayerConfig>();
            config.Initialize(input);
            input.transform.SetParent(transform);
            playerConfigs.Add(config);
        }

        public void OnPlayerLeft(PlayerInput input)
        {
            PlayerConfig config = playerConfigs.First(x => x.input == input);
            playerConfigs.Remove(config);
            Destroy(config);
        }

        public void SetPlayerColor(int index, Color c)
        {
            playerConfigs[index].playerColor = c;
        }

        public void OnPlayerReady(int index)
        {
            playerConfigs[index].isReady = true;
        }

        private void CheckPlayers()
        {
            if (playerConfigs.Count == _maxPlayers && playerConfigs.All(x => x.isReady))
            {
                SceneManager.LoadScene("PrototypeScene");
            }
        }
    }

    public class PlayerConfig : ScriptableObject
    {
        public Color playerColor;
        [NonSerialized] public bool isReady;
        [NonSerialized] public int playerIndex;
        [NonSerialized] public PlayerInput input;

        public void Initialize(PlayerInput i)
        {
            input = i;
            playerIndex = i.playerIndex;
        }
    }
}