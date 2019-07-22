using System;
using HelloCSharp.entity;
using HelloCSharp.model;

namespace HelloCSharp.demo
{
    public class GiaoDichSHB : GiaoDich
    {
        private static SHBAccountModel shbAccountModel;

        public GiaoDichSHB()
        {
            shbAccountModel = new SHBAccountModel();
        }

        public void Login()
        {
            Program.currentLoggedInAccount = null;
            Console.Clear();
            Console.WriteLine("Tiến hành đăng nhập hệ thống SHB.");
            // Yêu cầu nhập username, password.
            Console.WriteLine("Vui lòng nhập usename: ");
            var username = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập mật khẩu: ");
            var password = Console.ReadLine();
            // gọi đến model kiểm, nếu model trả về null thì báo đăng nhập sai.
            var shbAccount = shbAccountModel.FindByUsernameAndPassword(username, password);
            if (shbAccount == null)
            {
                Console.WriteLine("Sai thông tin tài khoản, vui lòng đăng nhập lại.");
                Console.WriteLine("Ấn phím bất kỳ để tiếp tục.");
                Console.Read();
                return;
            }

            // trong trường hợp trả về khác null.
            // set giá trị vào biến currentLoggedInAccount.
            Program.currentLoggedInAccount = shbAccount;
        }

        public void RutTien()
        {
            if (Program.currentLoggedInAccount != null)
            {
                Console.Clear();
                Console.WriteLine("Tiến hành rút tiền tại hệ thống SHB.");
                Console.WriteLine("Vui lòng nhập số tiền cần rút.");
                var amount = double.Parse(Console.ReadLine());
                if (amount <= 0)
                {
                    Console.WriteLine("Số lượng không hợp lệ, vui lòng thử lại.");
                    return;
                }

                var transaction = new SHBTransaction
                {
                    TransactionId = Guid.NewGuid().ToString(),
                    SenderAccountNumber = Program.currentLoggedInAccount.AccountNumber,
                    ReceiverAccountNumber = Program.currentLoggedInAccount.AccountNumber,
                    Type = 1,
                    Message = "Tiến hành rút tiền tại ATM với số tiền: " + amount,
                    Amount = amount,
                    CreatedAtMLS = DateTime.Now.Ticks,
                    UpdatedAtMLS = DateTime.Now.Ticks,
                    Status = 1
                };
                bool result = shbAccountModel.UpdateBalance(Program.currentLoggedInAccount, transaction);
            }
            else
            {
                Console.WriteLine("Vui lòng đăng nhập để sử dụng chức năng này.");
            }
        }

        public void GuiTien()
        {
            if (Program.currentLoggedInAccount != null)
            {
                Console.Clear();
                Console.WriteLine("Tiến hành gửi tiền tại hệ thống SHB.");
                Console.WriteLine("Vui lòng nhập số tiền cần gửi.");
                var amount = double.Parse(Console.ReadLine());
                if (amount <= 50000)
                {
                    Console.WriteLine("Số tiền phải trên 50000vnđ, vui lòng thử lại.");
                    return;
                }

                var transaction = new SHBTransaction
                {
                    TransactionId = Guid.NewGuid().ToString(),
                    SenderAccountNumber = Program.currentLoggedInAccount.AccountNumber,
                    ReceiverAccountNumber = Program.currentLoggedInAccount.AccountNumber,
                    Type = 1,
                    Message = "Tiến hành rút tiền tại ATM với số tiền: " + amount,
                    Amount = amount,
                    CreatedAtMLS = DateTime.Now.Ticks,
                    UpdatedAtMLS = DateTime.Now.Ticks,
                    Status = 1
                };
                bool result = shbAccountModel.UpdateBalance(Program.currentLoggedInAccount, transaction);
            }
            else
            {
                Console.WriteLine("Vui lòng đăng nhập để sử dụng chức năng này.");
            }
        }
        public void ChuyenKhoan()
        {
            throw new System.NotImplementedException();
        }
    }
}