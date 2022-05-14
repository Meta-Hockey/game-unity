using System.Collections.Generic;
using Near.Models.Team.Team;
using Runtime;
using UI.ManageTeam.DragAndDrop;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ManageTeam
{
    public class ManageTeamView : MonoBehaviour
    {
        private ManageTeamController _controller;

        [SerializeField] private List<UISlot> fives; 
        [SerializeField] private List<UISlot> goalies;

        [SerializeField] private Transform canvasContent;

        [SerializeField] private Text line;
        [SerializeField] private Text iceTimePriority;

        private Team _team;

        private void Awake()
        {
            _controller = new ManageTeamController();
        }

        private async void Start()
        {
            _team = await _controller.LoadUserTeam();
            ShowFive("First");
        }

        private void ShowFive(string number)
        {
            Five firstFive = _team.Fives[number];
            
            line.text = firstFive.Number;
            iceTimePriority.text = firstFive.IceTimePriority;

            int fieldPlayerNumber = 0;
            foreach (var fieldPlayer in firstFive.FieldPlayers)
            {
                UISlot fieldPlayerSlot = fives[fieldPlayerNumber];
                UIPlayer player = Instantiate(Game.AssetRoot.manageTeamAsset.fieldPlayer, fieldPlayerSlot.transform, true);
                
                fieldPlayerSlot.uiPlayer = player;
            }
        }

        private void ShowGoalies()
        {
            
        }
    }
}