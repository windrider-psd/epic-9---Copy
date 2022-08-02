using Assets.source.DataManager;
using Assets.Source.Heroes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.DataManager
{
    internal class LocalDataManager : IDataManager
    {
        public static readonly string gameDataDirectory = Application.persistentDataPath + "/" + gameDataPath + "/";
        private static readonly string gameDataPath = "savegames";

        private FileWriter fileWriter = new BinaryFileWriter();

        public LocalDataManager()
        {
            if (!Directory.Exists(Application.persistentDataPath + gameDataPath))
            {
                Directory.CreateDirectory(gameDataDirectory);
            }
        }

        private static string PlayerDataSaveFile
        {
            get
            {
                return gameDataDirectory + ".playerdata";
            }
        }

        private static string PlayerHeroesSaveFile
        {
            get
            {
                return gameDataDirectory + ".playerheores";
            }
        }

        public async Task SetUp()
        {
            PlayerData playerData = await GetPlayerData();
            List<Hero> playerHeroes = await GetPlayerHeroes();

            if (playerData == null)
            {
                await CreatePlayerData(null);
            }
            if (playerHeroes == null)
            {
                await CreatePlayersHeores();
            }
        }


        #region playerHeroes

        public async Task AddPlayerHero(Hero hero)
        {

            var heroes = await GetPlayerHeroes();
            heroes.Add(hero);

            await SavePlayerHeores(heroes);
        }

        public async Task CreatePlayersHeores()
        {
            List<Hero> heroes = new();
            await SavePlayerHeores(heroes);
        }

        public async Task<List<Hero>> GetPlayerHeroes()
        {
            return await fileWriter.LoadFile<List<Hero>>(PlayerHeroesSaveFile);
        }

        public async Task RemovePlayerHero(Hero hero)
        {
            var heroes = await GetPlayerHeroes();
            heroes.Remove(hero);

            await SavePlayerHeores(heroes);
        }

        private Task SavePlayerHeores(List<Hero> heroes)
        {
            return fileWriter.CreateFile(PlayerHeroesSaveFile, heroes);
        }

        #endregion



        #region playerdata

        public async Task CreatePlayerData(string playerName)
        {
            string id = Guid.NewGuid().ToString();

            PlayerData playerData = new(id) { Name = playerName };

            await SavePlayerData(playerData);
        }

        public Task DeletePlayerData()
        {
            return Task.Factory.StartNew(() =>
            {
                File.Delete(PlayerDataSaveFile);
            });
        }

        public async Task<PlayerData> GetPlayerData()
        {
            return await fileWriter.LoadFile<PlayerData>(PlayerDataSaveFile);
        }

        public Task UpdatePlayerData(PlayerData playerData)
        {
            throw new NotImplementedException();
        }

        private Task SavePlayerData(PlayerData playerData)
        {
            return fileWriter.CreateFile(PlayerDataSaveFile, playerData);
        }

        #endregion playerdata
    }
}