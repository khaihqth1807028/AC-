using System;

namespace HelloCSharp.demo
{
    public class FacebookLogin: LoginInterface
    {
        public void doLogin()
        {
            Console.WriteLine("Nhập số điện thoại");
            Console.WriteLine("Nhập mật khẩu");
            Console.WriteLine("Login thành công");
        }
    }
}