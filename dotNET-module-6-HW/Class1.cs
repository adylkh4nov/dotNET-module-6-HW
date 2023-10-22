using dotNET_Module_6_practice.Bankomat.Accounts;
using dotNET_Module_6_practice.Bankomat.MyBank;
using dotNET_Module_6_practice.Bankomat.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET_Module_6_practice
{
    namespace Bankomat
    {


        namespace MyBank
        {

            public class Bank
            {

                private string accountNumber;
                private decimal initialBalance;

                public Account openAccount(Client client, decimal initialBalance)
                {
                    string accountNumber = Guid.NewGuid().ToString();

                    Account newAccount = new Account(accountNumber, initialBalance);

                    client.Account = newAccount;

                    Console.WriteLine($"Счет {accountNumber} успешно открыт для клиента {client.Name} с начальным балансом {initialBalance}.");

                    return newAccount;
                }
            }
        }

        namespace Clients
        {

            public class Client
            {
                public string Name { get; set; }
                public byte[] CardNumber { get; set; }
                public string Password { get; set; }
                public Account Account { get; set; }

                public Client(string name, string password)
                {
                    Name = name;
                    CardNumber = Guid.NewGuid().ToByteArray();
                    Password = password;
                }

                public Client()
                {
                }


                public void LinkAccount(Account account)
                {
                    Account = account;
                }
            }
        }

        namespace Accounts
        {

            public class Account
            {
                public decimal Balance { get; private set; }

                public Account(string accountNumber, decimal initialBalance)
                {
                    Balance = initialBalance;
                }

                public void Deposit(decimal amount)
                {
                    if (amount > 0)
                    {
                        Balance += amount;
                        Console.WriteLine($"Пополнено {amount} на счет. Новый баланс: {Balance}");
                    }
                    else
                    {
                        Console.WriteLine("Сумма пополнения должна быть больше нуля.");
                    }
                }

                public bool Withdraw(decimal amount)
                {
                    if (amount > 0 && Balance >= amount)
                    {
                        Balance -= amount;
                        Console.WriteLine($"Сумма {amount} снята со счета. Новый баланс: {Balance}");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Недостаточно средств на счете или сумма снятия меньше нуля.");
                        return false;
                    }
                }
            }
        }
    }
}
