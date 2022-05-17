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
        private List<UISlot> _benchPlayers;

        private List<NFTMetadata> _userNFTs;

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
            
            Five selectedFive = _team.Fives[number];
            
            lineText.text = selectedFive.Number + " line";
            iceTimePriority.text = selectedFive.IceTimePriority;
            iceTimePrioritySlider.value = Utils.Utils.GetSliderValueIceTimePriority(selectedFive.IceTimePriority);

            foreach (var fieldPlayer in selectedFive.FieldPlayers)
            {
                int positionId = Utils.Utils.GetFieldPlayerPositionId(fieldPlayer.Key);
                
                UISlot fieldPlayerSlot = fives[positionId];
                UIPlayer uiPlayer = Instantiate(Game.AssetRoot.manageTeamAsset.fieldPlayer, fieldPlayerSlot.transform,
                    true);

                uiPlayer.CardData = fieldPlayer.Value;
                uiPlayer.SetData(fieldPlayer.Value);
                
                uiPlayer.canvasContent = canvasContent;
                
                fives[positionId].uiPlayer = uiPlayer;
                uiPlayer.uiSlot = fives[positionId];

                uiPlayer.transform.localPosition = Vector3.zero;
                
                RectTransform rectTransformUIPlayer = uiPlayer.GetComponent<RectTransform>();
                rectTransformUIPlayer.localScale = Vector3.one;
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

                player.canvasContent = canvasContent;

                goalies[goalieNumber].uiPlayer = player;
                player.uiSlot = goalies[goalieNumber];

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

            int slotId = 0;
            
            foreach (NFTMetadata playerMetadata in benchPlayers)
            {
                UISlot benchSlot = Instantiate(Game.AssetRoot.manageTeamAsset.uiSlot, benchContent);
                benchSlot.slotPosition = SlotPositionEnum.Bench;
                
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

                benchSlot.manageTeamView = this;
                benchSlot.slotId = slotId;
                slotId++;
                
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

        private UISlot GetUISlot(SlotPositionEnum slotPositionEnum, int slotId)
        {
            UISlot uiSlot;

            switch (slotPositionEnum)
            {
                case SlotPositionEnum.Bench:
                    uiSlot = _benchPlayers.FirstOrDefault(x => x.slotId == slotId);
                    break;
                case SlotPositionEnum.MainGoalie or SlotPositionEnum.BackupGoalie:
                    uiSlot = goalies.FirstOrDefault(x => x.slotPosition == slotPositionEnum);
                    break;
                default:
                    uiSlot = fives.FirstOrDefault(x => x.slotPosition == slotPositionEnum);
                    break;
            }
            
            return uiSlot;
        }

        public void SwapCards(UIPlayer uiPlayer1, UIPlayer uiPlayer2)
        {
            UISlot uiSlot1 = GetUISlot(uiPlayer1.uiSlot.slotPosition, uiPlayer1.uiSlot.slotId);
            UISlot uiSlot2 = GetUISlot(uiPlayer2.uiSlot.slotPosition, uiPlayer2.uiSlot.slotId);

            uiSlot1.uiPlayer.uiSlot = uiSlot2;
            uiSlot2.uiPlayer.uiSlot = uiSlot1;
            
            (uiSlot1.uiPlayer, uiSlot2.uiPlayer) = (uiSlot2.uiPlayer, uiSlot1.uiPlayer);
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