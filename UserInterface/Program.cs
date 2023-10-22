using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using dotNET_Module_6_practice.Bankomat.Accounts;
using dotNET_Module_6_practice.Bankomat.MyBank;
using dotNET_Module_6_practice.Bankomat.Clients;
using Personality;

namespace UserInterface
{
    class Program
    {
        static void Main(string[] args)
        {

            Person person = new Person("Olzhas", 1000);

            Console.WriteLine($"Здравствуйте, {person.Name}");
            Console.WriteLine("Для открытия счета в банке, введите некоторую информацию");
            Bank bank = new Bank();

            
            string pass = "";
            
            Console.WriteLine("Введите паооль:");
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    Console.Write('\n');
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass = pass.Remove(pass.Length - 1);
                    Console.Write("\b \b");
                }
                else if (i.Key == ConsoleKey.Backspace && pass.Length < 1)
                {
                    Console.WriteLine("Итак пусто");
                }
                else
                {
                    pass += i.KeyChar;
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            // Создаем клиента и открываем счет
            Client client = new Client(person.Name, pass);
           

            
            Account account = bank.openAccount(client, person.InitialBalance);
            client.LinkAccount(account);

            int attempts = 3;
            string inputPassword = "";
            while (attempts > 0)
            {
                Console.Write("Введите пароль: ");
                while (true)
                {
                    ConsoleKeyInfo i = Console.ReadKey(true);
                    if (i.Key == ConsoleKey.Enter)
                    {
                        Console.Write('\n');
                        break;
                    }
                    else if (i.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        inputPassword = inputPassword.Remove(inputPassword.Length - 1);
                        Console.Write("\b \b");
                    }
                    else if (i.Key == ConsoleKey.Backspace && pass.Length < 1)
                    {
                        Console.WriteLine("Итак пусто");
                    }
                    else
                    {
                        inputPassword += i.KeyChar;
                        Console.Write("*");
                    }
                }

                if (inputPassword == client.Password)
                {
                    ShowMenu(client);
                    break;
                }
                else
                {
                    attempts--;
                    if (attempts > 0)
                    {
                        Console.WriteLine($"Неверный пароль. У вас осталось {attempts} попыток.");
                    }
                    else
                    {

                        Console.WriteLine("Исчерпаны все попытки. Попробуйте позже.");
                        Thread.Sleep(2500);
                    }
                }
            }
        }

        static void ShowMenu(Client client)
        {
            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("a. Вывод баланса на экран");
                Console.WriteLine("b. Пополнение счета");
                Console.WriteLine("c. Снять деньги со счета");
                Console.WriteLine("d. Выход");
                Console.WriteLine("e. Очистить консоль");

                Console.Write("Выберите действие: ");
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case 'a':
                        Console.WriteLine($"Баланс: {client.Account.Balance}");
                        break;
                    case 'b':
                        Console.Write("Введите сумму для пополнения: ");
                        decimal depositAmount = decimal.Parse(Console.ReadLine());
                        client.Account.Deposit(depositAmount);
                        break;
                    case 'c':

                        Console.Write("Введите сумму для снятия: ");
                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                        if (client.Account.Withdraw(withdrawAmount))
                        {
                            Console.WriteLine($"Сумма {withdrawAmount} снята со счета.");
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно средств на счете или сумма снятия меньше нуля.");
                        }
                        break;
                    case 'd':
                        Console.WriteLine("Выход.");
                        return;
                    case 'e':
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Повторите попытку.");
                        break;
                }
            }
        }
    }
}
