using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Options;
using Dapper;
using MySql.Data.MySqlClient;
using PlayersAndTeams.Models;

namespace PlayersAndTeams.Factories
{
    public class PlayerFactory : IFactory<Player>
    {
        private readonly IOptions<MySqlOptions> MySqlConfig;
        public PlayerFactory(IOptions<MySqlOptions> config)
        {
            MySqlConfig = config;
        }
        internal IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(MySqlConfig.Value.ConnectionString);
            }
        }
        
        public void AddPlayer(Player player)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO players (name, level, description, team) VALUES ( @name, @level, @description, @team)";
                dbConnection.Open();
                dbConnection.Execute(query, player);
            }
        }
        public List<Player> AllPlayers()
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = "SELECT * FROM players";
                dbConnection.Open();
                return dbConnection.Query<Player>(query).ToList();
            }
        }
        public Player FindPlayerByID(int id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Player>("SELECT * FROM players WHERE id = @Id", new {Id = id}).FirstOrDefault();
            }
        }
        public IEnumerable<Player> UpdatePlayerByID(int playerId, int teamId)
        {
            using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                IEnumerable<Team> team = dbConnection.Query<Team>("SELECT * FROM teams WHERE id = @Id", new {Id = teamId});
                return dbConnection.Query<Player>("UPDATE players SET team = @Team WHERE id = @Id", new {Id = playerId, Team = team});
            }
        }
        public void DeletePlayer(int id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM players WHERE id = @Id", new {Id = id});
            }
        }
    }
}