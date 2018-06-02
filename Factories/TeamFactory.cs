using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Dapper;
using PlayersAndTeams.Models;

namespace PlayersAndTeams.Factories
{
    public class TeamFactory : IFactory<Team>
    {
        private readonly IOptions<MySqlOptions> MySqlConfig;
        public TeamFactory(IOptions<MySqlOptions> config)
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

        public void AddTeam(Team team)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO teams (name, location, information) VALUES (@name, @location, @information)";
                dbConnection.Open();
                dbConnection.Execute(query, team);
            }
        }
        public List<Team> AllTeams()
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = "SELECT * FROM teams";
                dbConnection.Open();
                return dbConnection.Query<Team>(query).ToList();
            }
        }
        public Team FindTeamByID(int id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var query = 
                @"
                SELECT * FROM teams WHERE id = @Id;
                SELECT # FROM players WHERE team_id = @Id;
                ";

                using(var multi = dbConnection.QueryMultiple(query, new {Id = id}))
                {
                    var team = multi.Read<Team>().Single();
                    team.players = multi.Read<Player>().ToList();
                    return team;
                }
            }
        }
    }
    
}