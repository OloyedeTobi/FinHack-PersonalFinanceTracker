using System.Data;
using Dapper;
using FinanceTracker.Data;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Http.HttpResults;



namespace FinanceTracker.Helpers
{
    public class SqlQueries
    {
        private readonly DataContext _dapper;

        public SqlQueries(IConfiguration config)
        {
            _dapper = new DataContext(config);
        }

        public bool UpsertUser(UserComplete user)
        {
            const string sql = @"EXEC TutorialAPISchema.spUser_Upsert
                @FirstName = @FirstNameParameter, 
                @LastName = @LastNameParameter, 
                @Email = @EmailParameter, 
                @Gender = @GenderParameter, 
                @Active = @ActiveParameter, 
                @JobTitle = @JobTitleParameter,  
                @UserId = NULL";

            var parameters = new DynamicParameters();
            parameters.Add("@FirstNameParameter", user.FirstName, DbType.String);
            parameters.Add("@LastNameParameter", user.LastName, DbType.String);
            parameters.Add("@EmailParameter", user.Email, DbType.String);
            parameters.Add("@GenderParameter", user.Gender, DbType.String);
            parameters.Add("@ActiveParameter", user.Active, DbType.Boolean);
            parameters.Add("@JobTitleParameter", user.JobTitle, DbType.String);
            parameters.Add("@UserIdParameter", user.UserId, DbType.Int32);

            try
            {
                _dapper.ExecuteCommand(sql, parameters);
                return true;
            }

            catch
            {
                return false;
            }
        }

    }
}

