using System;
using System.Collections.Generic;
using HelloCSharp.controller;
using HelloCSharp.demo;
using HelloCSharp.entity;
using HelloCSharp.model;
using MySql.Data.MySqlClient;

namespace HelloCSharp
{
    class Program
    {
        public static SHBAccount currentLoggedInAccount;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                GiaoDich giaoDich = null;
                Console.WriteLine("Vui lòng lựa chọn phương thức giao dịch: ");
                Console.WriteLine("============================================");
                Console.WriteLine("1. Giao dịch trên ngân hàng SHB - Spring Hero Bank.");
                Console.WriteLine("2. Giao dịch blockchain.");
                Console.WriteLine("============================================");
                Console.WriteLine("Nhập lựa chọn của bạn: ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        giaoDich = new GiaoDichSHB();
                        break;
                    case 2:
                        giaoDich = new GiaoDichBlockchain();
                        break;
                    default:
                        Console.WriteLine("Sai phương thức đăng nhập.");
                        break;
                }

                // yêu cầu người dùng đăng nhập.
                giaoDich.Login();
                if (currentLoggedInAccount != null)
                {
                    Console.WriteLine("Đăng nhập thành công với tài khoản.");
                    Console.WriteLine($"Tài khoản: {currentLoggedInAccount.Username}");
                    Console.WriteLine($"Số dư: {currentLoggedInAccount.Balance}");
                    Console.WriteLine("Ấn phím bất kỳ để tiếp tục giao dịch.");
                    Console.Read();
                    GenerateTransactionMenu(giaoDich);
                }
            }
        }

        private static void GenerateTransactionMenu(GiaoDich giaoDich)
        {
            while (true)
            {
                Console.Clear();
                // show menu dành cho người dùng đã đăng nhập.
                Console.WriteLine("Vui lòng lựa chọn kiểu giao dịch: ");
                Console.WriteLine("============================================");
                Console.WriteLine("1. Rút tiền.");
                Console.WriteLine("2. Gửi tiền.");
                Console.WriteLine("3. Chuyển khoản.");
                Console.WriteLine("4. Thoát.");
                Console.WriteLine("============================================");
                Console.WriteLine("Nhập lựa chọn của bạn: ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        giaoDich.RutTien();
                        break;
                    case 2:
                        giaoDich.GuiTien();
                        break;
                    case 3:
                        giaoDich.ChuyenKhoan();
                        break;
                    case 4:
                        Console.WriteLine("Thoát giao diện giao dịch.");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn sai.");
                        break;
                }

                if (choice == 4)
                {
                    break;
                }
            }
        }
    }

    internal interface GiaoDich
    {
        void RutTien();
        void GuiTien();
        void ChuyenKhoan();
        void Login();
    }
}