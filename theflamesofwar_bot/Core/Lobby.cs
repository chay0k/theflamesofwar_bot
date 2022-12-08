using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theflamesofwar_bot.Models;
using theflamesofwar_bot.Repositories;

namespace theflamesofwar_bot.Core;
public static class Lobby
{
    private static List<Table> ActiveTables = new List<Table>();
    public static void CreateTable(User user)
    {
        var playerNumber = 1;
        var sqlTableRepository = new SqlTableRepository();
        var table = new Table();
        table.Name = "Table1";
        //table.Map = MapGenerator.map;
        table.Token = $"token_{table.Name}";
        table.Open = true;
        table.MapId = MapGenerator.map.Id;
        table.Id = Guid.NewGuid();

        sqlTableRepository.Create(table);

        var sqlTableCellRepository = new SqlTableCellRepository();
        foreach (Cell cell in MapGenerator.map.Cells)
        {
            var tableCell = new TableCell(cell, table.Id);
            sqlTableCellRepository.Create(tableCell);
            sqlTableCellRepository.Save();
        }
        sqlTableRepository.Save();
        var userSession = new UserSession();
        //userSession.Player = user;
        userSession.PlayerId = user.Id;
        userSession.Id = Guid.NewGuid();
        //userSession.Table = table;
        userSession.TableId = table.Id;
        userSession.DateTime = DateTime.Now;
        userSession.Location = 1;
        userSession.PlayerNumber = playerNumber;
        var sqlSessionRepository = new SqlUserSessionRepository();
        sqlSessionRepository.Create(userSession);
        sqlSessionRepository.Save();
        ActiveTables.Add(table);
    }
    public static void UpdateLobby(User user, int position)
    {
        var sqlUserSessionRepository = new SqlUserSessionRepository();

        var session = CurrentUserSession(user, sqlUserSessionRepository);
        session.Location = position; 
        sqlUserSessionRepository.Update(session);
        sqlUserSessionRepository.Save();
    }

    private static UserSession CurrentUserSession(User user, SqlUserSessionRepository sqlUserSessionRepository)
    {
        var session = sqlUserSessionRepository.SearchByUser(user);
        return session;
    }
    public static Table searchActiveTables(User user)
    {
        if (ActiveTables != null)
            foreach(Table table in ActiveTables)
                if (table.Users != null && table.Users.Contains(user))
                    return table;

        var sqlUserSessionRepository = new SqlUserSessionRepository();
        var userTables = sqlUserSessionRepository.SearchTablesByUser(user);

        foreach (var session in userTables)
            if (session != null && session.Open)
                return session;
        return new Table();
    }

    public static PlayerCondition retrievePlayerCondition(Models.User user, Table table)
    {
        if(user.IsEmpty() || table.IsEmpty())
            return new PlayerCondition();

        var conditionRep = new SqlPlayerConditionRepository();
        return conditionRep.Get(user, table);
    }
}
