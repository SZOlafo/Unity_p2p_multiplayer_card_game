using MultiplayerModule.P2P;
using UnityEngine;

namespace Assets.Scripts.Handlers
{
    public class MultiplayerHandler : MonoBehaviour
    {
        private string _playerName;
        private PlayerNode _playerNode = new();



        public void Start()
        {
            _playerName = PlayerPrefs.GetString("PlayerName");
            var serverAddress = PlayerPrefs.GetString("Address");
            if (serverAddress == "none")
            {
                _playerNode.HostGame(_playerName);
            }
            else
            {
                _playerNode.JoinGame(serverAddress, _playerName);
            }
        }

        public void Update()
        {

        }
    }


}