using System;
using HelloCSharp.entity;
using MySql.Data.MySqlClient;

namespace HelloCSharp.model
{
    public class BlockchainAddressModel
    {
      public bool UpdateBalance(BlockchainAddress currentLoggedInAccount, BlockchainTransaction transaction)
        {
            ConnectionHelper.GetConnection();
            var tranB = ConnectionHelper.GetConnection().BeginTransaction(); 
            try
            {
                var cmd = new MySqlCommand("select * from Blockchain where Address ,@address",
                    ConnectionHelper.GetConnection());
                cmd.Parameters.AddWithValue("@senderAddress", currentLoggedInAccount.PrivateKey);
                BlockchainAddress blockchainAddress = null;
                var reader = cmd.ExecuteReader();
                double currentAccountBalance = 0;

                if (reader.Read())
                {
                    currentAccountBalance = reader.GetDouble("balance");
                }

                reader.Close();
                if (currentAccountBalance < 0)
                {
                    Console.WriteLine("Không đủ tiền trong tài khoản.");
                    return false;
                }

                if (transaction.Type == 1)
                {
                    if (currentAccountBalance < transaction.Amount)
                    {
                        Console.WriteLine("Khong du tien thuc hien giao dich");
                        return false;
                    }
                    currentAccountBalance -= transaction.Amount;
                }
                else if (transaction.Type == 2)
                {
                    currentAccountBalance += transaction.Amount;
                }

                var updateQuery =
                    "update `BlockChain` set `balance` = @balance where accountNumber = @accountNumber";
                var sqlCmd = new MySqlCommand(updateQuery, ConnectionHelper.GetConnection());
                sqlCmd.Parameters.AddWithValue("@balance", currentAccountBalance);
                sqlCmd.Parameters.AddWithValue("@accountNumber", currentLoggedInAccount.PrivateKey);
                var updateResult = sqlCmd.ExecuteNonQuery();
                var historyTransactionQuery =
                    "insert into `BlockTransaction` (transactionId, type, senderAccountNumber, receiverAccountNumber, amount, message) " +
                    "values (@id, @type, @senderAccountNumber, @receiverAccountNumber, @amount, @message)";
                var historyTransactionCmd = 
                    new MySqlCommand(historyTransactionQuery, ConnectionHelper.GetConnection());
       
                historyTransactionCmd.Parameters.AddWithValue("@amount", transaction.Amount);
                historyTransactionCmd.Parameters.AddWithValue("@type", transaction.Type);
                historyTransactionCmd.Parameters.AddWithValue("@receiver", transaction.ReceiverAddress);
                historyTransactionCmd.Parameters.AddWithValue("@created",
                    transaction.CreatedAtMLS);
                historyTransactionCmd.Parameters.AddWithValue("@update",
                    transaction.UpdatedAtMLS);
                var historyResult = historyTransactionCmd.ExecuteNonQuery();

                if (updateResult != 1 || historyResult != 1)
                {
                }

                tranB.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                tranB.Rollback();               
                return false;
            }

            ConnectionHelper.CloseConnection();
            return true;
        }
    }
}