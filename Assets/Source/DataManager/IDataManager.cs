using Assets.source.DataManager;
using Assets.Source.Heroes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.Source.DataManager
{
    public interface IDataManager
    {
        public Task SetUp();

        #region PlayerData

        public Task CreatePlayerData(string playerName);

        public Task DeletePlayerData();

        public Task<PlayerData> GetPlayerData();

        public Task UpdatePlayerData(PlayerData playerData);

        #endregion PlayerData

        #region PlayerHeroes

        public Task AddPlayerHero(Hero hero);

        public Task<List<Hero>> GetPlayerHeroes();

        public Task RemovePlayerHero(Hero hero);

        public Task CreatePlayersHeores();

        #endregion PlayerHeroes
    }
}