using System;
using HelloCSharp.entity;
using MySql.Data.MySqlClient;

namespace HelloCSharp.model
{
    public class SHBAccountModel
    {
        // Bình thường không làm theo cách này,
        // phải mã hoá mật khẩu, kiểm tra tài khoản theo username sau đó so sánh mật khẩu sau khi mã hoá.
        public SHBAccount FindByUsernameAndPassword(string username, string password)
        {
            // Tạo connection đến db, lấy ra trong bảng shb account những tài khoản có username, password trùng.            
            var cmd = new MySqlCommand("select * from SHBAccount where Username = @Username And Password = @Password",
                ConnectionHelper.GetConnection());
                
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            // Tạo ra một đối tượng của lớp shbAccount.
            SHBAccount shbAccount = null;
            // Đóng Connection và trả về đối tượng này.          
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                shbAccount = new SHBAccount
                {
                    Username = reader.GetString("username"),
                    Password = reader.GetString("password"),
                    Balance = reader.GetDouble("balance")
                };
            }

            ConnectionHelper.CloseConnection();
            // Trong trường hợp không tìm thấy tài khoản thì trả về null.
            return shbAccount;
        }

        public bool UpdateBalance(SHBAccount currentLoggedInAccount, SHBTransaction transaction)
        {
            // 1. Kiểm tra số dư tài khoản hiện tại.
            // 2. Update số dư tài khoản hiện tại.
            // 3. Lưu thông tin giao dịch.
            // 4. Commit transaction.
            ConnectionHelper.GetConnection();
            var tran = ConnectionHelper.GetConnection().BeginTransaction(); // mở giao dịch.
            try
            {
                var cmd = new MySqlCommand("select * from SHBAccount where Username = @Username",
                    ConnectionHelper.GetConnection());
                cmd.Parameters.AddWithValue("@Username", currentLoggedInAccount.AccountNumber);
                SHBAccount shbAccount = null;
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
                    "update `SHBAccount` set `balance` = @balance where accountNumber = @accountNumber";
                var sqlCmd = new MySqlCommand(updateQuery, ConnectionHelper.GetConnection());
                sqlCmd.Parameters.AddWithValue("@balance", currentAccountBalance);
                sqlCmd.Parameters.AddWithValue("@accountNumber", currentLoggedInAccount.AccountNumber);
                var updateResult = sqlCmd.ExecuteNonQuery();
                var historyTransactionQuery =
                    "insert into `SHBTransaction` (transactionId, type, senderAccountNumber, receiverAccountNumber, amount, message) " +
                    "values (@id, @type, @senderAccountNumber, @receiverAccountNumber, @amount, @message)";
                var historyTransactionCmd =
                    new MySqlCommand(historyTransactionQuery, ConnectionHelper.GetConnection());
                historyTransactionCmd.Parameters.AddWithValue("@id", transaction.TransactionId);
                historyTransactionCmd.Parameters.AddWithValue("@amount", transaction.Amount);
                historyTransactionCmd.Parameters.AddWithValue("@type", transaction.Type);
                historyTransactionCmd.Parameters.AddWithValue("@message", transaction.Message);
                historyTransactionCmd.Parameters.AddWithValue("@senderAccountNumber",
                    transaction.SenderAccountNumber);
                historyTransactionCmd.Parameters.AddWithValue("@receiverAccountNumber",
                    transaction.ReceiverAccountNumber);
                var historyResult = historyTransactionCmd.ExecuteNonQuery();

                if (updateResult != 1 || historyResult != 1)
                {
                    throw new Exception("Không thể thêm giao dịch hoặc update tài khoản.");
                }

                tran.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                tran.Rollback(); // lưu giao dịch vào.                
                return false;
            }

            ConnectionHelper.CloseConnection();
            return true;
        }
    }
}