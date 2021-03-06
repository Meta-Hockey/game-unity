using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Near.MarketplaceContract;
using Near.Models;
using Near.Models.Game.Bid;
using Near.Models.ManageTeam.Team;
using Near.Models.Team.Team;
using Near.Models.Team.TeamIds;
using NearClientUnity;
using Newtonsoft.Json;

namespace Near.GameContract.ContractMethods
{
    public static class Views
    {
        public static async Task<List<AvailableGame>> GetAvailableGames()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();

            dynamic args = new ExpandoObject();
            args.from_index = 0;
            args.limit = 50;

            dynamic results = await gameContract.View("get_available_games", args);
            List<dynamic> availableGamesResults = JsonConvert.DeserializeObject<List<dynamic>>(
                results.result.ToString()
                );

            List<AvailableGame> availableGames = new List<AvailableGame>(); 
            foreach (dynamic availableGamesResult in availableGamesResults)
            {
                int gameId = availableGamesResult[0];
                string playerId1 = availableGamesResult[1][0].ToString();
                string playerId2 = availableGamesResult[1][1].ToString();
                
                availableGames.Add(new AvailableGame()
                {
                    GameId = gameId, 
                    PlayerIds = new Tuple<string, string>(playerId1, playerId2)
                });
            }

            return availableGames;
        }

        public static async Task<AvailableGame> GetUserGame()
        {
            string accountId = NearPersistentManager.Instance.WalletAccount.GetAccountId();
            AvailableGame userGame = (await GetAvailableGames())
                .FirstOrDefault(x => x.PlayerIds.Item1 == accountId || x.PlayerIds.Item2 == accountId);

            return userGame;
        }
        
        /// <summary>
        /// If user is not in the game gameId = -1
        /// </summary>
        /// <returns></returns>
        public static async Task<int> GetGameId()
        {
            AvailableGame userGame = await GetUserGame();

            if (userGame == null)
            {
                return -1;
            }
            
            return userGame.GameId;
        }
        
        public static async Task<IEnumerable<Opponent>> GetAvailablePlayers()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();

            dynamic args = new ExpandoObject();
            args.from_index = 0;
            args.limit = 50;

            dynamic opponents = await gameContract.View("get_available_players", args);

            List<List<object>> objects = JsonConvert
                .DeserializeObject<List<List<object>>>(opponents.result.ToString());
            
            var result = objects.Select(o => new Opponent
            {
                Name = (string) o[0],
                GameConfig = JsonConvert.DeserializeObject<GameConfig>(o[1].ToString())
            }); 
            
            return result;
        }
        
        public static async Task<bool> IsAlreadyInTheList()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();
                
            dynamic args = new ExpandoObject();
            args.account_id = NearPersistentManager.Instance.WalletAccount.GetAccountId();

            dynamic isInTheList = await gameContract.View("is_already_in_the_waiting_list", args);
            
            return bool.Parse(isInTheList.result);
        }

        public static async Task<GameConfig> GetGameConfig()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();
                
            dynamic args = new ExpandoObject();
            args.account_id = NearPersistentManager.Instance.WalletAccount.GetAccountId();

            dynamic gameConfig = await gameContract.View("get_game_config", args);
            
            return JsonConvert.DeserializeObject<GameConfig>(gameConfig.result);
        }
        
        private static Metadata ParseMetadata(dynamic card)
        {
            return new Metadata()
            {
                title = card["title"].ToString(),
                description = card["description"].ToString(),
                media = card["media"].ToString(),
                media_hash = card["media_hash"].ToString(),
                issued_at = card["issued_at"].ToString(),
                expires_at = card["expires_at"].ToString(),
                starts_at = card["starts_at"].ToString(),
                updated_at = card["updated_at"].ToString(),
                extra = JsonConvert.DeserializeObject<Extra>(card["extra"].ToString(), new ExtraConverter())
            };
        }

        public static async Task<Team> LoadUserTeam()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();
            ContractNear nftContract = await NearPersistentManager.Instance.GetNftContract();
            
            dynamic args = new ExpandoObject();
            args.account_id = NearPersistentManager.Instance.GetAccountId();
            
            dynamic ownerTeamResult = await gameContract.Change("get_owner_team", args, NearUtils.Gas);

            if (ownerTeamResult == null)
            {
                return new Team();
            }
            
            TeamIds teamIds = new TeamIds();
            
            dynamic ownerTeamIdsResult = await nftContract.View("get_owner_nft_team", args);
            teamIds = JsonConvert.DeserializeObject<TeamIds>(ownerTeamIdsResult.result);
            
            Dictionary<string, Five> fives = new Dictionary<string, Five>();

            foreach (dynamic fiveResult in ownerTeamResult["fives"])
            {
                Five five = new Five();
                
                dynamic fiveChild = fiveResult.Children();
                
                five.Number = fiveResult.Name;
                
                foreach (dynamic iceTimePriority in fiveChild["ice_time_priority"])
                {
                    five.IceTimePriority = iceTimePriority.ToString();
                }

                Dictionary<string, NFTMetadata> fieldPlayers = new Dictionary<string, NFTMetadata>();
                
                foreach (dynamic fieldPlayersResults in fiveChild["field_players"])
                {
                    foreach (dynamic fieldPlayerResults in fieldPlayersResults)
                    {
                        
                        string playerPosition = fieldPlayerResults.Name;
                        foreach (dynamic fieldPlayer in fieldPlayerResults)
                        {
                            Metadata fieldPlayerMetadata = ParseMetadata(fieldPlayer);

                            string nftId = "-1"; 
                            if (teamIds.fives.ContainsKey(five.Number) &&
                                teamIds.fives[five.Number].field_players.ContainsKey(playerPosition))
                            {
                                nftId = teamIds.fives[five.Number].field_players[playerPosition];
                            }
                            
                            NFTMetadata nftMetadata = new NFTMetadata()
                            {
                                Id = nftId,
                                Metadata = fieldPlayerMetadata
                            };
                            
                            fieldPlayers.Add(playerPosition, nftMetadata);
                        }
                    }
                }

                five.FieldPlayers = fieldPlayers;
                fives.Add(five.Number, five);
            }

            Dictionary<string, NFTMetadata> goalies = new Dictionary<string, NFTMetadata>();
            
            foreach (dynamic goalieResults in ownerTeamResult["goalies"])
            {
                string number = goalieResults.Name;
                dynamic goalieChildResults = goalieResults.Children();

                foreach (dynamic goalieResult in goalieChildResults)
                {
                    Metadata goalie = ParseMetadata(goalieResult);

                    string nftId = "-1"; 
                    if (teamIds.goalies.ContainsKey(number))
                    {
                        nftId = teamIds.goalies[number];
                    }
                            
                    NFTMetadata nftMetadata = new NFTMetadata()
                    {
                        Id = nftId,
                        Metadata = goalie
                    };
                            
                    goalies.Add(number, nftMetadata);
                }
            }

            return new Team()
            {
                Fives = fives,
                Goalies = goalies
            };
        }
    }
}