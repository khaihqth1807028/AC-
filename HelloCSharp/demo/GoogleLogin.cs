using System;

namespace HelloCSharp.demo
{
    public class GoogleLogin: LoginInterface
    {
        public void doLogin()
        {
            Console.WriteLine("Vào trang google.com");
            Console.WriteLine("Yêu cầu nhập email, password");
            Console.WriteLine("Gửi mã otp về điện thoại");
            Console.WriteLine("Nhập mã otp");
            Console.WriteLine("Login thành công.");
        }
    }
}