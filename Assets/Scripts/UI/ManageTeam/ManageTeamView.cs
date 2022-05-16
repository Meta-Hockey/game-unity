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
        private List<UISlot> _benchPlayers;

        [SerializeField] private Transform canvasContent;
        [SerializeField] private Transform benchContent;

        [SerializeField] private Text lineText;
        [SerializeField] private Text iceTimePriority;
        [SerializeField] private Slider iceTimePrioritySlider;

        [SerializeField] private Transform fiveView;
        [SerializeField] private Transform goaliesView;

        private Team _team;
        private string lineNumber;
        
        private void Awake()
        {
            _controller = new ManageTeamController();
        }

        private async void Start()
        {
            _team = await _controller.LoadUserTeam();
            _userNFTs = await _controller.LoadUserNFTs();

            lineNumber = "First";
            
            ShowFive(lineNumber);
            ShowBench(lineNumber);
        }

        private void ShowFive(string number)
        {
            foreach (UISlot fieldPlayer in fives)
            {
                if (fieldPlayer.uiPlayer != null)
                {
                    Destroy(fieldPlayer.uiPlayer.gameObject);
                }
            }
            
            Five firstFive = _team.Fives[number];
            
            lineText.text = firstFive.Number + " line";
            iceTimePriority.text = firstFive.IceTimePriority;
            iceTimePrioritySlider.value = Utils.Utils.GetSliderValueIceTimePriority(firstFive.IceTimePriority);

            int fieldPlayerNumber = 0;
            foreach (var fieldPlayer in firstFive.FieldPlayers)
            {
                UISlot fieldPlayerSlot = fives[fieldPlayerNumber];
                UIPlayer uiPlayer = Instantiate(Game.AssetRoot.manageTeamAsset.fieldPlayer, fieldPlayerSlot.transform,
                    true);

                uiPlayer.CardData = fieldPlayer.Value;
                uiPlayer.SetData(fieldPlayer.Value);
                
                uiPlayer.currentParent = fieldPlayerSlot.transform;
                uiPlayer.canvasContent = canvasContent;
                
                fives[fieldPlayerNumber].uiPlayer = uiPlayer;

                uiPlayer.transform.localPosition = Vector3.zero;
                
                RectTransform rectTransformUIPlayer = uiPlayer.GetComponent<RectTransform>();
                rectTransformUIPlayer.localScale = Vector3.one;
                
                fieldPlayerNumber++;
            }
        }
        
        private void ShowGoalies()
        {
            foreach (UISlot goalie in goalies)
            {
                if (goalie.uiPlayer != null)
                {
                    Destroy(goalie.uiPlayer.gameObject);
                }
            }
            
            lineText.text = "Goalies";

            int goalieNumber = 0;
            foreach (var goalieNftMetadata in  _team.Goalies)
            {
                UISlot goalieSlot = goalies[goalieNumber];
                UIPlayer player = Instantiate(Game.AssetRoot.manageTeamAsset.goalie, goalieSlot.transform);
                
                player.SetData(goalieNftMetadata.Value);

                player.currentParent = goalieSlot.transform;
                player.canvasContent = canvasContent;

                goalies[goalieNumber].uiPlayer = player;

                goalieNumber++;
            }
        }

        private void ShowBench(string line)
        {
            if (_benchPlayers != null)
            {
                foreach (UISlot uiPlayerSlot in _benchPlayers)
                {
                    Destroy(uiPlayerSlot.gameObject);
                }
            }
            _benchPlayers = new List<UISlot>();
            
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
                UISlot benchSlot = Instantiate(Game.AssetRoot.manageTeamAsset.uiSlot, benchContent);
                
                UIPlayer uiPlayer = playerMetadata.Metadata.extra.Type switch
                {
                    "FieldPlayer" => Instantiate(Game.AssetRoot.manageTeamAsset.fieldPlayer, benchSlot.transform),
                    "Goalie" => Instantiate(Game.AssetRoot.manageTeamAsset.goalie, benchSlot.transform),
                    "GoaliePos" => Instantiate(Game.AssetRoot.manageTeamAsset.goalie, benchSlot.transform),
                    _ => throw new Exception("Extra type not found")
                };

                benchSlot.uiPlayer = uiPlayer;
                uiPlayer.uiSlot = benchSlot;
                
                uiPlayer.SetData(playerMetadata);
                uiPlayer.transform.localPosition = Vector3.zero;
                uiPlayer.canvasContent = canvasContent;
                
                _benchPlayers.Add(benchSlot);
            }
        }

        private void SetLineDataToTeam(string line)
        {
            if (line == "Goalies")
            {
                if (goalies[0].uiPlayer == null || goalies[1] == null)
                {
                    return;
                }
                
                _team.Goalies["MainGoalkeeper"] = goalies[0].uiPlayer.CardData ;
                _team.Goalies["SubstituteGoalkeeper"] = goalies[1].uiPlayer.CardData;
            }
            else
            {
                _team.Fives[line].FieldPlayers["LeftWing"] = fives[0].uiPlayer.CardData;
                _team.Fives[line].FieldPlayers["Center"] = fives[1].uiPlayer.CardData;
                _team.Fives[line].FieldPlayers["RightWing"] = fives[2].uiPlayer.CardData;
                _team.Fives[line].FieldPlayers["LeftDefender"] = fives[3].uiPlayer.CardData;
                _team.Fives[line].FieldPlayers["RightDefender"] = fives[4].uiPlayer.CardData;
            }
        }
        
        public void SwitchLine(string line)
        {
            SetLineDataToTeam(line);
            
            if (line == "Goalies")
            {
                if (lineNumber == "Goalies")
                {
                    return;
                }
                
                fiveView.gameObject.SetActive(false);
                goaliesView.gameObject.SetActive(true);
                
                ShowGoalies();
                ShowBench(line);
            }
            else
            {
                fiveView.gameObject.SetActive(true);
                goaliesView.gameObject.SetActive(false);
                
                ShowFive(line);
                
                if (lineNumber == "Goalies")
                {
                    ShowBench(line);
                }
            }
            
            lineNumber = line;
        }

        public void ChangeIceTimePriority()
        {
            iceTimePriority.text = Utils.Utils.GetIceTimePriority((int)iceTimePrioritySlider.value);
        }
        
        public void Apply()
        {
            SetLineDataToTeam(lineNumber);
            
            _controller.ChangeLineups(_team);
        }
        
        public void Cancel()
        {
            Start();
        }
        
        public void Back()
        {
            Game.LoadMainMenu();
        }
    }
}