using System;
using System.IO;
using System.Threading.Tasks;
using NearClientUnity;
using NearClientUnity.KeyStores;
using UnityEngine;


namespace Near
{
    public class NearPersistentManager : MonoBehaviour
    {
        public static NearPersistentManager Instance { get; private set; }
        public WalletAccount WalletAccount { get; private set; }
        private NearClientUnity.Near _near;
        
        private ContractNear _gameContract;
        private const string GameContactId = "uriyyuriy.testnet";

        private ContractNear _marketplaceContract;
        public readonly string MarketplaceContactId = "nft-marketplace.testnet";

        private ContractNear _nftContract;
        public readonly string nftContactId = "nft-0_0.testnet";
        
        private readonly string _dirName = "KeyStore";

        private async void Awake()
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
        }

        public async Task<ContractNear> GetGameContract()
        {
            if (_gameContract == null)
            {
                _gameContract = await CreateGameContract();
                return _gameContract;
            }   
        
            return _gameContract;
        }
    
        private async Task<ContractNear> CreateGameContract()
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
        
        public async Task<ContractNear> GetMarketplaceContract()
        {
            if (_marketplaceContract == null)
            {
                _marketplaceContract = await CreateMarketplaceContract();
                return _marketplaceContract;
            }   
        
            return _marketplaceContract;
        }
    
        private async Task<ContractNear> CreateMarketplaceContract()
        {
            Account account = await Instance.GetAccount();
        
            ContractOptions options = new ContractOptions()
            {
                viewMethods = new[] { "get_sales_by_owner_id", "get_sale",
                    "get_sales_by_nft_contract_id", "storage_paid", "storage_amount"},
                changeMethods = new[] { "update_price", "storage_deposit", "accept_offer", "offer"}
            };
        
            return new ContractNear(account, WalletAccount, MarketplaceContactId, options);
        }
        
        public async Task<ContractNear> GetNftContract()
        {
            if (_nftContract == null)
            {
                _nftContract = await CreateNftContract();
                return _nftContract;
            }   
        
            return _nftContract;
        }
        
        private async Task<ContractNear> CreateNftContract()
        {
            Account account = await Instance.GetAccount();
        
            ContractOptions options = new ContractOptions()
            {
                viewMethods = new[] { "nft_tokens_for_owner", "nft_tokens_batch",
                    "nft_token", "nft_tokens", "nft_total_supply", "get_owner_nft_team"},
                changeMethods = new[] { "nft_mint", "nft_approve"}
            };
        
            return new ContractNear(account, WalletAccount, nftContactId, options);
        }

        private async Task<Account> GetAccount()
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