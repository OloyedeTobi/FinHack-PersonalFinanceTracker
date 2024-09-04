using System.Data;
using Dapper;
using FinanceTracker.Data;
using FinanceTracker.Models;

namespace FinanceTracker.Helpers
{
    public class SqlQueries(IConfiguration config)
    {
        private readonly DataContext _dapper = new DataContext(config);

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

    public bool UpsertAccount(Account account)
    {
        const string sql = @"EXEC TutorialAPISchema.spAccount_Upsert
            @Id = @IdParameter,
            @UserId = @UserIdParameter, 
            @Name = @NameParameter, 
            @Balance = @BalanceParameter, 
            @AccountType = @AccountTypeParameter, 
            @CreatedDate = @CreatedDateParameter";

        var parameters = new DynamicParameters();
        parameters.Add("@IdParameter", account.Id, DbType.Int32);
        parameters.Add("@UserIdParameter", account.UserId, DbType.Int32);
        parameters.Add("@NameParameter", account.Name, DbType.String);
        parameters.Add("@BalanceParameter", account.Balance, DbType.Decimal);
        parameters.Add("@AccountTypeParameter", account.AccountType, DbType.String);
        parameters.Add("@CreatedDateParameter", account.CreatedDate, DbType.DateTime);

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


    public bool UpsertTransaction(Transaction transaction)
    {
        const string sql = @"EXEC TutorialAPISchema.spTransaction_Upsert
            @Id = @IdParameter,
            @UserId = @UserIdParameter, 
            @AccountId = @AccountIdParameter, 
            @Amount = @AmountParameter, 
            @Type = @TypeParameter, 
            @Description = @DescriptionParameter, 
            @Recurring = @RecurringParameter,
            @Frequency = @FrequencyParameter
            @Date = @DateParameter";

        var parameters = new DynamicParameters();
        parameters.Add("@IdParameter", transaction.Id, DbType.Int32);
        parameters.Add("@UserIdParameter", transaction.UserId, DbType.Int32);
        parameters.Add("@AccountIdParameter", transaction.AccountId, DbType.Int32);
        parameters.Add("@AmountParameter", transaction.Amount, DbType.Decimal);
        parameters.Add("@TypeParameter", transaction.Category, DbType.String);
        parameters.Add("@DescriptionParameter", transaction.Description, DbType.String);
        parameters.Add("@RecurringParameter", transaction.IsRecurring, DbType.Boolean);
        parameters.Add("@FrequencyParameter", transaction.Description, DbType.String);
        parameters.Add("@DateParameter", transaction.Date, DbType.DateTime);

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

    public bool UpsertBudget(Budget budget)
    {
        const string sql = @"EXEC TutorialAPISchema.spBudget_Upsert
            @Id = @IdParameter,
            @UserId = @UserIdParameter, 
            @Amount = @AmountParameter, 
            @Category = @CategoryParameter, 
            @StartDate = @StartDateParameter, 
            @EndDate = @EndDateParameter";

        var parameters = new DynamicParameters();
        parameters.Add("@IdParameter", budget.Id, DbType.Int32);
        parameters.Add("@UserIdParameter", budget.UserId, DbType.String);
        parameters.Add("@AmountParameter", budget.Amount, DbType.Decimal);
        parameters.Add("@CategoryParameter", budget.Category, DbType.String);
        parameters.Add("@StartDateParameter", budget.StartDate, DbType.DateTime);
        parameters.Add("@EndDateParameter", budget.EndDate, DbType.DateTime);

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

