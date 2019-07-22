using System;

namespace HelloCSharp.demo
{
    public class MemberLogin: LoginInterface
    {
        public void doLogin()
        {
            Console.WriteLine("Nhập username");
            Console.WriteLine("Nhập password");
            Console.WriteLine("Login success!");
        }
    }
}