using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Near.Models.Team.Team;
using NearClientUnity;
using Newtonsoft.Json;

namespace Near.GameContract.ContractMethods
{
    public static class Actions
    {
        public static async Task MakeAvailable(string bid)
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();
                
            dynamic args = new ExpandoObject();
            args.config = new Object();
            await gameContract.Change("make_available", args,
                NearUtils.GasMakeAvailable,
                NearUtils.ParseNearAmount(bid));
        }

        public static async Task MakeUnavailable()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();
                
            dynamic args = new ExpandoObject();

            await gameContract.Change("make_unavailable", args);
        }

        public static async Task<dynamic> GenerateEvent(int numberOfRenderedEvents, int gameId)
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();
                
            dynamic args = new ExpandoObject();
            args.number_of_rendered_events = numberOfRenderedEvents;
            args.game_id = gameId;
            

            var result = await gameContract.Change("generate_event", args, NearUtils.GasMove);

            return result;
        }

        public static async void ChangeLineups(Team team)
        {
            ContractNear nftContract = await NearPersistentManager.Instance.GetNftContract();
            
            List<dynamic> fives = new List<dynamic>();

            foreach (var five in team.Fives)
            {
                List<List<string>> fiveArgs = new List<List<string>>();
                
                string fiveNumber = five.Key;
                
                foreach (var fieldPlayer in five.Value.FieldPlayers)
                {
                    if (fieldPlayer.Value.Id != "-1")
                    {
                        fiveArgs.Add(new List<string> { fieldPlayer.Key, fieldPlayer.Value.Id });
                    }
                }

                if (fiveArgs.Count != 0)
                {
                    List<dynamic> fiveTuple = new List<dynamic>() { fiveNumber, fiveArgs }; 
                    fives.Add(fiveTuple);
                }
            }

            if (fives.Count != 0)
            {
                dynamic args = new ExpandoObject();
                args.fives = fives;
                
                await nftContract.Change("insert_nft_field_players", args, NearUtils.GasMakeAvailable);
            }

            List<List<string>> goalies = new List<List<string>>();

            foreach (var goalie in team.Goalies)
            {
                if (goalie.Value.Id != "-1")
                {
                    goalies.Add(new List<string> { goalie.Key, goalie.Value.Id });
                }
            }

            if (goalies.Count != 0)
            {
                dynamic args = new ExpandoObject();
                args.goalies = goalies;
                
                await nftContract.Change("insert_nft_goalies", args, NearUtils.GasMakeAvailable);
            }
        }
    }
}