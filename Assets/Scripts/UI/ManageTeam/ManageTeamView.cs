using UnityEngine;

namespace UI.ManageTeam
{
    public class ManageTeamView : MonoBehaviour
    {
        private ManageTeamController _controller;

        private void Awake()
        {
            _controller = new ManageTeamController();
            _controller.LoadUserTeam();
        }
    }
}