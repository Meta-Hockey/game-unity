using System.Threading.Tasks;
using NearClientUnity;
using NearClientUnity.KeyStores;
using UnityEngine;

namespace Runtime
{
    public class NearPersistentManager : MonoBehaviour
    {
        public static NearPersistentManager Instance { get; private set; }
        public WalletAccount WalletAccount { get; set; }
        private Near _near;
    
        private ContractNear _gameContract;
        public readonly string _gameContactId = "uriyyuriy.testnet";


        void Start()
        {
            _near = new Near(config: new NearConfig()
            {
                NetworkId = "testnet",
                NodeUrl = "https://rpc.testnet.near.org",
                ProviderType = ProviderType.JsonRpc,
                SignerType = SignerType.InMemory,
                KeyStore = new InMemoryKeyStore(),
                ContractName = _gameContactId,
                WalletUrl = "https://wallet.testnet.near.org"
            });
            WalletAccount = new WalletAccount(
                _near,
                "",
                new AuthService(),
                new AuthStorage());

            Game.LoadMainMenu();
        }    

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
        public async Task<Account> GetAccount()
        {
            return await Instance._near.AccountAsync(WalletAccount.GetAccountId());
        }

        public async Task<ContractNear> GetContract()
        {
            if (_gameContract == null)
            {
                _gameContract = await CreateContract();
                return _gameContract;
            }   
        
            return _gameContract;
        }
    
        private async Task<ContractNear> CreateContract()
        {
            Account account = await Instance.GetAccount();
        
            ContractOptions options = new ContractOptions()
            {
                viewMethods = new[] { "get_available_players", "get_available_games",
                    "is_already_in_the_waiting_list", "get_game_config" },
                changeMethods = new[] { "make_available", "start_game", "generate_event",
                    "make_unavailable", "internal_stop_game", "get_owner_team" }
            };
        
            return new ContractNear(account, _gameContactId, options);
        }
    }

    public class AuthService : IExternalAuthService
    {
        public bool OpenUrl(string url)
        {
            try
            {
                Application.OpenURL(url);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class AuthStorage : IExternalAuthStorage
    {  
        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void Add(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        public string GetValue(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
        }
    }
}