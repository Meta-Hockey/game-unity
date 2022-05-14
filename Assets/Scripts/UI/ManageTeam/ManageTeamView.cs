using System;
using System.Collections.Generic;
using System.Linq;
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

        private List<NFTMetadata> _userNFTs;
        private List<UIPlayer> _benchPlayers;

        [SerializeField] private Transform canvasContent;
        [SerializeField] private Transform benchContent;

        [SerializeField] private Text lineText;
        [SerializeField] private Text iceTimePriority;

        private Team _team;

        private async void Awake()
        {
            _controller = new ManageTeamController();
        }

        private async void Start()
        {
            _team = await _controller.LoadUserTeam();
            _userNFTs = await _controller.LoadUserNFTs();

            ShowFive("First");
            ShowBench("First");
        }
        
        public void Cancel()
        {
            Start();
        }

        private void ShowFive(string number)
        {
            Five firstFive = _team.Fives[number];
            
            lineText.text = firstFive.Number + " line";
            iceTimePriority.text = firstFive.IceTimePriority;

            int fieldPlayerNumber = 0;
            foreach (var fieldPlayer in firstFive.FieldPlayers)
            {
                UISlot fieldPlayerSlot = fives[fieldPlayerNumber];
                UIPlayer uiPlayer = Instantiate(Game.AssetRoot.manageTeamAsset.fieldPlayer, fieldPlayerSlot.transform,
                    true);

                uiPlayer.SetData(fieldPlayer.Value.Metadata);
                
                uiPlayer.currentParent = fieldPlayerSlot.transform;
                uiPlayer.canvasContent = canvasContent;
                
                fieldPlayerSlot.uiPlayer = uiPlayer;

                uiPlayer.transform.localPosition = Vector3.zero;
                
                RectTransform rectTransformUIPlayer = uiPlayer.GetComponent<RectTransform>();
                rectTransformUIPlayer.localScale = Vector3.one;

                
                fieldPlayerNumber++;
            }
        }
        
        private void ShowGoalies()
        {
            lineText.text = "Goalies";

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

        private void ShowBench(string line)
        {
            if (_benchPlayers == null)
            {
                _benchPlayers = new List<UIPlayer>();
            }
            else
            {
                foreach (UIPlayer uiPlayer in _benchPlayers)
                {
                    Destroy(uiPlayer);
                }
            }
            
            string type = line switch
            {
                "Goalies" => "FieldPlayer",
                _ => "GoaliePos"
            };

            List<NFTMetadata> benchPlayers = type switch
            {
                "GoaliePos" => _userNFTs.Where(x => x.Metadata.extra.Type != type && x.Metadata.extra.Type != "Goalie")
                    .ToList(),
                _ => _userNFTs.Where(x => x.Metadata.extra.Type != type).ToList()
            };

            foreach (NFTMetadata playerMetadata in benchPlayers)
            {
                UIPlayer uiPlayer = playerMetadata.Metadata.extra.Type switch
                {
                    "FieldPlayer" => Instantiate(Game.AssetRoot.manageTeamAsset.fieldPlayer, benchContent),
                    "Goalie" => Instantiate(Game.AssetRoot.manageTeamAsset.goalie, benchContent),
                    "GoaliePos" => Instantiate(Game.AssetRoot.manageTeamAsset.goalie, benchContent),
                    _ => throw new Exception("Extra type not found")
                };
                
                uiPlayer.SetData(playerMetadata.Metadata);
                uiPlayer.transform.localPosition = Vector3.zero;
            }
        }

        public void Back()
        {
            Game.LoadMainMenu();
        }
    }
}