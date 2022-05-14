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
                UIPlayer player = Instantiate(Game.AssetRoot.manageTeamAsset.fieldPlayer, fieldPlayerSlot.transform,
                    true);

                player.SetData(fieldPlayer.Value.Metadata);
                
                player.currentParent = fieldPlayerSlot.transform;
                player.canvasContent = canvasContent;
                
                fieldPlayerSlot.uiPlayer = player;

                fieldPlayerNumber++;
            }
        }
        
        private void ShowGoalies()
        {
            line.text = "Goalies";

            int goalieNumber = 0;
            foreach (var goalieNftMetadata in  _team.Goalies)
            {
                UISlot goalieSlot = goalies[goalieNumber];
                UIPlayer player = Instantiate(Game.AssetRoot.manageTeamAsset.goalie, goalieSlot.transform);
                
                player.SetData(goalieNftMetadata.Value.Metadata);

                player.currentParent = goalieSlot.transform;
                player.canvasContent = canvasContent;

                goalieSlot.uiPlayer = player;

                goalieNumber++;
            }
        }

        public async void Cancel()
        {
            _team = await _controller.LoadUserTeam();
            ShowFive("First");
        }
    }
}