using System;
using System.IO;
using System.Threading.Tasks;
using NearClientUnity;
using NearClientUnity.KeyStores;
using NearClientUnity.Utilities;
using Runtime;
using UnityEngine;

namespace Near
{
    public class NearPersistentManager : MonoBehaviour
    {
        public static NearPersistentManager Instance { get; private set; }
        public WalletAccount WalletAccount { get; private set; }
        private NearClientUnity.Near _near;

        private readonly string _dirName = "KeyStore";
    
        private ContractNear _gameContract;
        private const string GameContactId = "uriyyuriy.testnet";

        public readonly ulong GasMakeAvailable = 300_000_000_000_000;
        public readonly ulong GasMove = 50_000_000_000_000;
        private readonly UInt128 _nearNominationExp = UInt128.Parse("1000000000000000000000000");

        private async void Start()
        {
            if (!Directory.Exists(_dirName))
            {
                await UnencryptedFileSystemKeyStore.EnsureDir(_dirName);
            }
            
            _near = new NearClientUnity.Near(config: new NearConfig()
            {
                NetworkId = "testnet",
                NodeUrl = "https://rpc.testnet.near.org",
                ProviderType = ProviderType.JsonRpc,
                SignerType = SignerType.InMemory,
                KeyStore = new UnencryptedFileSystemKeyStore(_dirName),
                ContractName = GameContactId,
                WalletUrl = "https://wallet.testnet.near.org"
            });
            WalletAccount = new WalletAccount(
                _near,
                "",
                new AuthService(),
                new AuthStorage());

            Game.LoadMainMenu(); ;
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
        
                
        public UInt128 ParseNearAmount(string amount)
        {
            return UInt128.Parse(amount) * _nearNominationExp;
        }
        
        public UInt128 FormatNearAmount(UInt128 amount)
        {
            return amount / _nearNominationExp;
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
        
            return new ContractNear(account, WalletAccount, GameContactId, options);
        }

        public async Task<Account> GetAccount()
        {
            return await Instance._near.AccountAsync(WalletAccount.GetAccountId());
        }

        public string GetAccountId()
        {
            return WalletAccount.GetAccountId();
        }
        
        public async Task<bool> SignIn()
        {
            return await WalletAccount.RequestSignIn(
                GameContactId,
                "Nft hockey",
                new Uri("nearclientunity://testnet.near.org/success"),
                new Uri("nearclientunity://testnet.near.org/fail"),
                new Uri("nearclientios://testnet.near.org")
            );
        }

        public void SignOut()
        {
            WalletAccount.SignOut();
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