using System;
using System.Collections.Generic;
using System.Globalization;
using Near.Models;
using Near.Models.Game.Bid;
using NearClientUnity.Utilities;
using Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Main_menu.UIPopups
{
    public class UIPopupOpponentsBets : UIPopup
    {
        [SerializeField] private Transform content;
        [SerializeField] private UIPopupOpponent uiPopupOpponent;

        [SerializeField] private MainMenuController controller;

        private List<OpponentBet> _opponentBets;
        private OpponentBet _selectedOpponentBet;
        
        private void Awake()
        {
            controller = new MainMenuController();
        }

        public void ChooseOpponent()
        {
            if (_selectedOpponentBet)
            {
                uiPopupOpponent.Show();
                uiPopupOpponent.LoadData(_selectedOpponentBet);
            }
        }
        
        public async void LoadOpponentBeds()
        {
            if (_opponentBets != null)
            {
                foreach (OpponentBet opponentBet in _opponentBets)
                {
                    Destroy(opponentBet.gameObject); 
                }    
            }
            
            _opponentBets = new List<OpponentBet>();
            IEnumerable<Opponent> opponents = await controller.GetOpponents();
            
            foreach (Opponent opponent in opponents)
            {
                OpponentBet opponentBet = Instantiate(Game.AssetRoot.mainMenuAsset.opponentBet, content);
                opponentBet.OpponentId.text = opponent.Name;
                opponentBet.Price.text = Near.NearUtils.FormatNearAmount(UInt128.Parse(opponent.GameConfig.Deposit)).ToString();
                opponentBet.Attack.text = Random.Range(60, 86).ToString();
                opponentBet.Defensive.text = Random.Range(60, 86).ToString();
                opponentBet.Goalie.text = Random.Range(60, 86).ToString();

                opponentBet.SelectButton.onClick.AddListener(delegate
                {
                    OpponentBet param=opponentBet;
                    OnClickBet(param);
                });

                _opponentBets.Add(opponentBet);
            }
        }

        private void OnClickBet(OpponentBet opponentBet)
        {
            ChangeButtonColor(opponentBet);
            _selectedOpponentBet = opponentBet;
        }

        private void ChangeButtonColor(OpponentBet activeOpponentBet)
        {
            foreach (OpponentBet opponentBet in _opponentBets)
            {
                opponentBet.Image.color = Color.white;
            }

            activeOpponentBet.Image.color = activeOpponentBet.ActiveColor;
        }
    }
}