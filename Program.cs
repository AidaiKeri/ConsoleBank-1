using ConsoleBank;
using ConsoleBank_1.Enum;
using ConsoleBank_1.Models;
using Microsoft.EntityFrameworkCore;
using Migration49;
using System;
using System.Linq;
using System.Threading;

namespace ConsoleBank_1
{
    class Program
    {
        /// <summary>
        /// Точка входа в приложение
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            HomePage();
        }
        /// <summary>
        /// Метод отвечающий за главную страницу приложения
        /// </summary>
        public static void HomePage()
        {
            Console.Clear();
            Console.Title = "Алим лучший ментор :)";
            Console.WriteLine("=============================");
            Console.WriteLine("Добро пожаловать в терминал!");
            Console.WriteLine("=============================");

            int top = Console.CursorTop;
            int y = top;

            Console.WriteLine("→ Войти");
            Console.WriteLine("→ Регистрация");
            Console.WriteLine("→ Выйти");

            int down = Console.CursorTop;

            Console.CursorTop = top;

            ConsoleKey key;
            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
            {
                if (key == ConsoleKey.UpArrow)
                {
                    if (y > top)
                    {
                        y--;
                        Console.CursorTop = y;
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (y < down - 1)
                    {
                        y++;
                        Console.CursorTop = y;
                    }
                }
            }

            Console.CursorTop = down;

            if (y == top)
                Login();
            else if (y == top + 1)
                Registration();
            else if (y == top + 2)
                return;
        }

        /// <summary>
        /// Метод отвечающий за вход пользователя 
        /// </summary>
        public static void Login()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();

                Console.WriteLine("Введите свой логин ↓");
                var login = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Введите свой пароль ↓");
                var password = Console.ReadLine();

                var person = db.Persons.FirstOrDefault(x => x.Login == login && x.Password == password);
                if (person is Client client)
                {
                    Console.WriteLine();
                    Console.WriteLine("Пользователь найден!");
                    Thread.Sleep(1500);
                    Console.Clear();
                    ClientMenu(client);
                }
                else if (person is Admin admin)
                {
                    Console.WriteLine();
                    Console.WriteLine("Админ найден!");
                    Thread.Sleep(1000);
                    Console.Clear();
                    AdminMenu(admin);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Не существует такого пользователя.\n Или данные не правильно введены.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("==========================");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    int top = Console.CursorTop;
                    int y = top;

                    Console.WriteLine("→ Пробовать заново");
                    Console.WriteLine("→ Выйти");

                    int down = Console.CursorTop;

                    Console.CursorTop = top;

                    ConsoleKey key;
                    while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                    {
                        if (key == ConsoleKey.UpArrow)
                        {
                            if (y > top)
                            {
                                y--;
                                Console.CursorTop = y;
                            }
                        }

                        else if (key == ConsoleKey.DownArrow)
                        {
                            if (y < down - 1)
                            {
                                y++;
                                Console.CursorTop = y;
                            }
                        }
                    }

                    Console.CursorTop = down;
                    if (y == top)
                        Login();
                    else if (y == top + 1)
                        HomePage();
                }
            }
        }

        /// <summary>
        /// Метод отвечающий за добавление клиента
        /// </summary>
        public static void AddClient()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();

                string login;
                Console.WriteLine("Введите логин пользователя ↓");
                login = Console.ReadLine();
                bool loginOkay = false;
                while (!loginOkay)
                {
                    var client = db.Persons.FirstOrDefault(x => x.Login == login);
                    if (login.Length < 8)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Логин не может быть короче 4х символов!");
                        Console.WriteLine("Введите логин заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        login = Console.ReadLine();
                    }
                    else if (login.Length > 20)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Логин не должен быть длиннее 20 символов!");
                        Console.WriteLine("Введите логин заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        login = Console.ReadLine();
                    }
                    else if (client != null)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Данный логин занят!");
                        Console.WriteLine("Введите другой логин ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        login = Console.ReadLine();
                    }
                    else if (login.Any(x => Char.IsWhiteSpace(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("С пробелами не прокатит!");
                        Console.WriteLine("Введите логин заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        login = Console.ReadLine();
                    }
                    else
                    {
                        loginOkay = true;
                    }
                }

                Console.WriteLine();
                string password = "";
                Console.WriteLine("Введите пароль ↓");
                password = Console.ReadLine();
                bool passwordOkay = false;
                while (!passwordOkay)
                {
                    if (password.Length < 4)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Пароль не должен быть короче 4х символов!");
                        Console.WriteLine("Введите пароль заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        password = Console.ReadLine();
                    }
                    else if (password.Length > 15)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Пароль не должен быть длиннее 15 символов!");
                        Console.WriteLine("Введите пароль заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        password = Console.ReadLine();
                    }
                    else if (password.Any(x => Char.IsWhiteSpace(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("С пробелами не прокатит!");
                        Console.WriteLine("Введите пароль заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        password = Console.ReadLine();
                    }
                    else
                    {
                        passwordOkay = true;
                    }
                }
                string confirmPassword;
                Console.WriteLine();
                Console.WriteLine("Повторите пароль ↓");
                confirmPassword = Console.ReadLine();
                bool confirmPassOkay = false;
                while (!confirmPassOkay)
                {
                    if (password != confirmPassword)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Пароли не совпадают");
                        Console.WriteLine("Введите пароль заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        confirmPassword = Console.ReadLine();
                    }
                    else
                    {
                        confirmPassOkay = true;
                    }
                }

                string name1;
                Console.WriteLine();
                Console.WriteLine("Введите ваше имя ↓");
                name1 = Console.ReadLine();
                Console.WriteLine();
                bool isNameOkay = false;
                while (!isNameOkay)
                {
                    if (name1.Length < 2)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Имя не может быть короче 2-х букв!");
                        Console.WriteLine("Повторно введите ваше имя ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name1 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else if (name1.Length > 15)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Имя не может быть длиннее 15 букв!");
                        Console.WriteLine("Повторно введите ваше имя ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name1 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else if (name1.Any(x => Char.IsWhiteSpace(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("С пробелами не прокатит!");
                        Console.WriteLine("Повторно введите ваше имя ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name1 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else if (name1.Any(x => Char.IsDigit(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Имя не может содержать в себе цифры!");
                        Console.WriteLine("Повторно введите ваше имя ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name1 = Console.ReadLine();
                        Console.WriteLine();
                    }

                    else
                    {
                        isNameOkay = true;
                    }
                }
                string name2 = "";
                Console.WriteLine("Введите вашу фамилию ↓");
                name2 = Console.ReadLine();
                Console.WriteLine();
                bool name2Okay = false;
                while (!name2Okay)
                {
                    if (name2.Length < 2)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Фамилия не может быть короче 2-х букв.");
                        Console.WriteLine("Повторно введите вашу фамилию ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name2 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else if (name2.Length > 20)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Фамилия не может быть длиннее 20 букв!");
                        Console.WriteLine("Повторно введите ваше имя ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name2 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else if (name2.Any(x => Char.IsDigit(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Фамилия не может содержать в себе цифры!");
                        Console.WriteLine("Повторно введите вашу фамилию ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name2 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else if (name2.Any(x => Char.IsWhiteSpace(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Не прокатит с пробелами!");
                        Console.WriteLine("Повторно введите вашу фамилию ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name2 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else
                    {
                        name2Okay = true;
                    }
                }

                string phoneNum = "";
                Console.WriteLine("Введите номер телефона ↓");
                Console.Write("+996 ");
                phoneNum = Console.ReadLine();

                bool phoneIsOkay = false;
                var cl = db.Clients.Where(x => x.PhoneNumber == phoneNum);
                while (phoneIsOkay == false)
                {
                    if (phoneNum.Any(x => String.IsNullOrEmpty(phoneNum)) == true)
                    {
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Нельзя ввести пустоту!");
                        Console.WriteLine("Повторно введите номер ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("+996 ");
                        phoneNum = Console.ReadLine();
                    }
                    else if (phoneNum.Any(x => Char.IsNumber(x)) == false)
                    {
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Нельзя ввести буквы или пустую строку!");
                        Console.WriteLine("Повторно введите номер ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("+996 ");
                        phoneNum = Console.ReadLine();
                    }
                    else if (phoneNum.Length < 9)
                    {
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Номер телефона не может быть короче 9 цифр!");
                        Console.WriteLine("Повторно введите номер ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("+996 ");
                        phoneNum = Console.ReadLine();
                    }
                    else if (phoneNum.Length > 9)
                    {
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Номер телефона не может быть длиннее 9 цифр!");
                        Console.WriteLine("Повторно введите номер ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("+996 ");
                        phoneNum = Console.ReadLine();
                    }
                    else if (cl.Count() >= 1)
                    {
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Аккаунт с данным номером уже существует!");
                        Console.WriteLine("Введите другой номер ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("+996 ");
                        phoneNum = Console.ReadLine();
                    }
                    else
                    {
                        phoneIsOkay = true;
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Укажите свой банк ↓");
                Console.WriteLine();
                int top = Console.CursorTop;
                int y = top;

                Console.WriteLine($"→ {Bank.DemirBank}");
                Console.WriteLine($"→ {Bank.Mbank}");
                Console.WriteLine($"→ {Bank.OptimaBank}");
                Console.WriteLine($"→ {Bank.BakaiBank}");
                Console.WriteLine($"→ {Bank.AiylBank}");

                var bank = Bank.DefaultVolume;

                int down = Console.CursorTop;

                Console.CursorTop = top;

                ConsoleKey key;

                while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                {
                    if (key == ConsoleKey.UpArrow)
                    {
                        if (y > top)
                        {
                            y--;
                            Console.CursorTop = y;
                        }
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        if (y < down - 1)
                        {
                            y++;
                            Console.CursorTop = y;
                        }
                    }
                }

                Console.CursorTop = down;

                if (y == top)
                    bank = Bank.DemirBank;
                else if (y == top + 1)
                    bank = Bank.Mbank;
                else if (y == top + 2)
                    bank = Bank.OptimaBank;
                else if (y == top + 3)
                    bank = Bank.BakaiBank;
                else if (y == top + 4)
                    bank = Bank.AiylBank;

                var user1 = new Client()
                {
                    Login = login,
                    Password = password,
                    ConfirmPassword = confirmPassword,
                    FirstName = name1,
                    LastName = name2,
                    DateOfCreate = DateTime.Now,
                    PhoneNumber = phoneNum,
                    Bank = bank
                };

                db.Clients.Add(user1);
                db.SaveChanges();
                ClientMenu(user1);
            }
        }

        /// <summary>
        /// Метод отвечающий за добавление админа
        /// </summary>
        public static void AddAdmin()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();

                string login;
                Console.WriteLine("Введите логин ↓");
                login = Console.ReadLine();
                bool loginOkay = false;
                while (!loginOkay)
                {
                    var cl = db.Persons.FirstOrDefault(x => x.Login == login);
                    if (login.Length < 8)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Логин не может быть короче 8 символов!");
                        Console.WriteLine("Введите логин заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        login = Console.ReadLine();
                    }
                    else if (login.Length > 20)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Логин не должен быть длиннее 20 символов!");
                        Console.WriteLine("Введите логин заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        login = Console.ReadLine();
                    }
                    else if (cl != null)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Данный логин занят!");
                        Console.WriteLine("Введите другой логин ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        login = Console.ReadLine();
                    }

                    else if (login.Any(x => Char.IsWhiteSpace(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("С пробелами не прокатит!");
                        Console.WriteLine("Введите логин заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        login = Console.ReadLine();
                    }
                    else
                    {
                        loginOkay = true;
                    }
                }

                Console.WriteLine();
                string password = "";
                Console.WriteLine("Введите пароль ↓");
                password = Console.ReadLine();
                bool passwordOkay = false;
                while (!passwordOkay)
                {
                    if (password.Length < 4)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Пароль не может быть короче 4х символов!");
                        Console.WriteLine("Введите пароль заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        password = Console.ReadLine();
                    }
                    else if (password.Length > 15)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Пароль не должен быть длиннее 15 символов!");
                        Console.WriteLine("Введите пароль заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        password = Console.ReadLine();
                    }
                    else if (password.Any(x => Char.IsWhiteSpace(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("С пробелами не прокатит!");
                        Console.WriteLine("Введите пароль заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        password = Console.ReadLine();
                    }
                    else
                    {
                        passwordOkay = true;
                    }
                }

                Console.WriteLine();
                string confirmPassword;
                Console.WriteLine("Повторите пароль ↓");
                confirmPassword = Console.ReadLine();
                bool confirmPassOkay = false;
                while (!confirmPassOkay)
                {
                    if (password != confirmPassword)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Пароли не совпадают");
                        Console.WriteLine("Введите пароль заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        confirmPassword = Console.ReadLine();
                    }
                    else
                    {
                        confirmPassOkay = true;
                    }
                }

                Console.WriteLine();
                string name1;
                Console.WriteLine("Введите ваше имя ↓");
                name1 = Console.ReadLine();
                Console.WriteLine();
                bool isNameOkay = false;
                while (!isNameOkay)
                {
                    if (name1.Length < 2)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Не правильно ввели имя.");
                        Console.WriteLine("Повторно введите ваше имя ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.WriteLine();
                        name1 = Console.ReadLine();
                    }
                    else if (name1.Length > 15)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Имя не может быть длиннее 15 букв!");
                        Console.WriteLine("Повторно введите ваше имя ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name1 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else if (name1.Any(x => Char.IsWhiteSpace(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Не правильно ввели имя.");
                        Console.WriteLine("Повторно введите ваше имя ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.WriteLine();
                        name1 = Console.ReadLine();
                    }
                    else if (name1.Any(x => Char.IsDigit(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Не правильно ввели имя.");
                        Console.WriteLine("Повторно введите ваше имя ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.WriteLine();
                        name1 = Console.ReadLine();
                    }
                    else
                    {
                        isNameOkay = true;
                    }
                }
                string name2;
                Console.WriteLine("Введите вашу фамилию ↓");
                name2 = Console.ReadLine();
                Console.WriteLine();
                bool name2Okay = false;
                while (!name2Okay)
                {
                    if (name2.Length < 2)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Имя не может иметь меньше 2-х букв!");
                        Console.WriteLine("Повторно введите вашу фамилию ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.WriteLine();
                        name2 = Console.ReadLine();
                    }
                    else if (name2.Length > 20)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Фамилия не может быть длиннее 20 букв!");
                        Console.WriteLine("Повторно введите ваше имя ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name2 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else if (name2.Any(x => Char.IsDigit(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Имя не может содержать в себе цифры!");
                        Console.WriteLine("Повторно введите вашу фамилию ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.WriteLine();
                        name2 = Console.ReadLine();
                    }
                    else if (name2.Any(x => Char.IsWhiteSpace(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("с пробелами не прокатит !");
                        Console.WriteLine("Повторно введите вашу фамилию ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.WriteLine();
                        name2 = Console.ReadLine();
                    }
                    else
                    {
                        name2Okay = true;
                    }

                    var admin1 = new Admin()
                    {
                        Login = login,
                        Password = password,
                        ConfirmPassword = confirmPassword,
                        FirstName = name1,
                        LastName = name2,
                        DateOfCreate = DateTime.Now
                    };

                    db.Admins.Add(admin1);
                    db.SaveChanges();
                    AdminMenu(admin1);
                }
            }
        }

        /// <summary>
        /// Метод отвечающий за меню клиента
        /// </summary>
        /// <param name="client"></param>
        public static void ClientMenu(Client client)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();

                Console.WriteLine("==============================");
                Console.WriteLine($"Добро пожаловать {client.FirstName} {client.LastName}!");
                Console.WriteLine("==============================");

                int top = Console.CursorTop;
                int y = top;

                Console.WriteLine("→ Информация о пользователе");
                Console.WriteLine("→ Список услуг");
                Console.WriteLine("→ Перевод денег пользователю");
                Console.WriteLine("→ Пополнение баланса");
                Console.WriteLine("→ Выйти в главное меню");

                int down = Console.CursorTop;

                Console.CursorTop = top;

                ConsoleKey key;
                while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                {
                    if (key == ConsoleKey.UpArrow)
                    {
                        if (y > top)
                        {
                            y--;
                            Console.CursorTop = y;
                        }
                    }

                    else if (key == ConsoleKey.DownArrow)
                    {
                        if (y < down - 1)
                        {
                            y++;
                            Console.CursorTop = y;
                        }
                    }
                }

                Console.CursorTop = down;

                if (y == top)
                    ClientPrintInfo(client);
                else if (y == top + 1)
                    ListServiceToClient(client);
                else if (y == top + 2)
                    MoneyTransfer(client);
                else if (y == top + 3)
                    Balance(client);
                else if (y == top + 4)
                    HomePage();
            }
        }

        /// <summary>
        /// Метод отвечающий за меню администратора
        /// </summary>
        /// <param name="admin"></param>
        public static void AdminMenu(Admin admin)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();

                Console.WriteLine("==============================");
                Console.WriteLine($"Добро пожаловать {admin.Role}, {admin.FirstName} {admin.LastName}!");
                Console.WriteLine("==============================");

                int top = Console.CursorTop;
                int y = top;

                Console.WriteLine("→ Информация об админе");
                Console.WriteLine("→ Добавить услугу");
                Console.WriteLine("→ Список пользователей");
                Console.WriteLine("→ Список транзакций");
                Console.WriteLine("→ Список услуг");
                Console.WriteLine("→ Выйти");

                int down = Console.CursorTop;

                Console.CursorTop = top;

                ConsoleKey key;
                while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                {
                    if (key == ConsoleKey.UpArrow)
                    {
                        if (y > top)
                        {
                            y--;
                            Console.CursorTop = y;
                        }
                    }

                    else if (key == ConsoleKey.DownArrow)
                    {
                        if (y < down - 1)
                        {
                            y++;
                            Console.CursorTop = y;
                        }
                    }
                }

                Console.CursorTop = down;

                if (y == top)
                    AdminPrintInfo(admin);
                else if (y == top + 1)
                    AddCheckForClient(admin);
                else if (y == top + 2)
                    ListOfClients(admin);
                else if (y == top + 3)
                    TransactionList(admin);
                else if (y == top + 4)
                    ListOfServices(admin);
                else if (y == top + 5)
                    HomePage();
            }
        }


        /// <summary>
        /// Метод отвечающий за вывод информации о клиенте 
        /// </summary>
        /// <param name="admin"></param>
        public static void ListOfClients(Admin admin)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();

                var clients = db.Clients.Where(x => x.Id >= 1);
                var data = clients.ToList();
                var count = clients.Count();


                if (count >= 1)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Количество пользователей: {count}");
                    Console.WriteLine();
                    Console.WriteLine("Список пользователей ↓");
                    foreach (var item in data)
                    {
                        var dateOnly = item.DateOfCreate.ToString("dd.MM.yyyy");

                        char[] array = item.PhoneNumber.ToArray();

                        char one = array[0];
                        char two = array[1];
                        char three = array[2];
                        char four = array[3];
                        char five = array[4];
                        char six = array[5];
                        char seven = array[6];
                        char eigth = array[7];
                        char nine = array[8];

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine();
                        Console.WriteLine("==========================");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Идентификатор: {item.Id}");
                        Console.WriteLine($"Имя: {item.FirstName}");
                        Console.WriteLine($"Фамилия: {item.LastName}");
                        Console.WriteLine($"Номер телефона: +996 {one}{two}{three}-{four}{five}{six}-{seven}{eigth}{nine}");
                        Console.WriteLine($"Дата создания аккаунта: {dateOnly}");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("==========================");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    int top = Console.CursorTop;
                    int y = top;
                    Console.WriteLine("→ Поиск истории пользователя по ID");
                    Console.WriteLine("→ Вернуться назад");

                    int down = Console.CursorTop;

                    Console.CursorTop = top;

                    ConsoleKey key;
                    while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                    {
                        if (key == ConsoleKey.UpArrow)
                        {
                            if (y > top)
                            {
                                y--;
                                Console.CursorTop = y;
                            }
                        }
                        else if (key == ConsoleKey.DownArrow)
                        {
                            if (y < down - 1)
                            {
                                y++;
                                Console.CursorTop = y;
                            }
                        }
                    }
                    Console.CursorTop = down;

                    if (y == top)
                        IdTransactionList(admin);
                    else if (y == top + 1)
                        AdminMenu(admin);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("В базе нету ни одного пользователя.");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Thread.Sleep(1100);
                    AdminMenu(admin);
                }
            }
        }

        /// <summary>
        /// Метод отвечающий за историю транзакций
        /// </summary>
        /// <param name="admin"></param>
        public static void TransactionList(Admin admin)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var list = db.HistoryOfTransactions.Where(x => x.Id >= 1);
                var data = list.ToList();

                var count = list.Count();
                if (count >= 1)
                {
                    Console.Clear();

                    Console.WriteLine($"Количество транзакций: {count}");
                    Console.WriteLine();
                    Console.WriteLine("Список транзакций ↓");
                    foreach (var item in data)
                    {
                        var dateOnly = item.TransDateTime.ToString("dd.MM.yyyy");

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine();
                        Console.WriteLine("==========================");
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        Console.WriteLine($"Название транзакции: {item.NameOfTransaction}");
                        Console.WriteLine($"Отправитель: {item.Sender}");
                        Console.WriteLine($"Банк отправителя: {item.SenderBank}");
                        Console.WriteLine($"Получатель: {item.Recipient}");
                        Console.WriteLine($"Банк получателя: {item.RecipientBank}");
                        Console.WriteLine($"Сумма: {item.Sum} сом");
                        Console.WriteLine($"Комиссия: {item.Commission} сом");
                        Console.WriteLine($"Дата транзакции: {dateOnly}");

                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("==========================");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine();
                    int top = Console.CursorTop;
                    int y = top;
                    Console.WriteLine("→ Очистить историю транзакций");
                    Console.WriteLine("→ Вернуться назад");

                    int down = Console.CursorTop;

                    Console.CursorTop = top;

                    ConsoleKey key;

                    while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                    {
                        if (key == ConsoleKey.UpArrow)
                        {
                            if (y > top)
                            {
                                y--;
                                Console.CursorTop = y;
                            }
                        }
                        else if (key == ConsoleKey.DownArrow)
                        {
                            if (y < down - 1)
                            {
                                y++;
                                Console.CursorTop = y;
                            }
                        }
                    }

                    Console.CursorTop = down;

                    if (y == top)
                        ClearTransList(admin);
                    else if (y == top + 1)
                        AdminMenu(admin);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("В базе нет транзакций.");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Thread.Sleep(1500);
                    AdminMenu(admin);
                }
            }
        }

        /// <summary>
        /// Метод отвечающий за вывод информации о транзакции по id клиента
        /// </summary>
        /// <param name="admin"></param>
        public static void IdTransactionList(Admin admin)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();

                Console.WriteLine("Введите Id клиента для просмотра\nего истории транзакций ↓");
                int idClient = int.Parse(Console.ReadLine());
                var srv = db.HistoryOfTransactions.Where(x => x.ClientId == idClient);
                var data = srv.ToList();
                var count = srv.Count();

                if (count >= 1)
                {
                    Console.Clear();

                    Console.WriteLine($"Количество транзакций: {count}");
                    Console.WriteLine();
                    Console.WriteLine("Список транзакций ↓");
                    foreach (var item in data)
                    {
                        var dateOnly = item.TransDateTime.ToString("dd.MM.yyyy");

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine();
                        Console.WriteLine("==========================");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Название транзакции: {item.NameOfTransaction}");
                        Console.WriteLine($"Отправитель: {item.Sender}");
                        Console.WriteLine($"Банк отправителя: {item.SenderBank}");
                        Console.WriteLine($"Получатель: {item.Recipient}");
                        Console.WriteLine($"Банк получателя: {item.RecipientBank}");
                        Console.WriteLine($"Сумма: {item.Sum} сом");
                        Console.WriteLine($"Дата транзакции: {dateOnly}");
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("==========================");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    int top = Console.CursorTop;
                    int y = top;

                    Console.WriteLine("→ Вернуться назад");

                    int down = Console.CursorTop;

                    Console.CursorTop = top;

                    ConsoleKey key;

                    while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                    {
                        if (key == ConsoleKey.UpArrow)
                        {
                            if (y > top)
                            {
                                y--;
                                Console.CursorTop = y;
                            }
                        }
                        else if (key == ConsoleKey.DownArrow)
                        {
                            if (y < down - 1)
                            {
                                y++;
                                Console.CursorTop = y;
                            }
                        }
                    }

                    Console.CursorTop = down;

                    if (y == top)
                        ListOfClients(admin);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("В базе нет транзакций.");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Thread.Sleep(1500);
                    ListOfClients(admin);
                }
            }
        }

        /// <summary>
        /// Метод отвечающий за удаление администратора
        /// </summary>
        /// <param name="admin"></param>
        public static void AdminDelete(Admin admin)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();
                Console.WriteLine("Введите свой пароль ↓");
                var password = Console.ReadLine();

                Console.WriteLine();

                if (password.Length >= 4)
                {
                    bool confirm = false;
                    var ad = db.Admins.Where
                        (x => x.Password == password)
                        .FirstOrDefault();
                    while (!confirm)
                    {
                        confirm = false;
                        if (ad != null && ad.Password == admin.Password)
                        {
                            while (confirm == false)
                            {
                                Console.Clear();
                                Console.WriteLine("Вы уверены что хотите удалить свой аккаунт?");
                                Console.WriteLine();

                                int top = Console.CursorTop;
                                int y = top;

                                Console.WriteLine("→ Да");
                                Console.WriteLine("→ Нет");
                                Console.WriteLine("→ Вернуться назад");

                                int down = Console.CursorTop;

                                Console.CursorTop = top;

                                ConsoleKey key;

                                while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                                {
                                    if (key == ConsoleKey.UpArrow)
                                    {
                                        if (y > top)
                                        {
                                            y--;
                                            Console.CursorTop = y;
                                        }
                                    }
                                    else if (key == ConsoleKey.DownArrow)
                                    {
                                        if (y < down - 1)
                                        {
                                            y++;
                                            Console.CursorTop = y;
                                        }
                                    }
                                }

                                Console.CursorTop = down;

                                if (y == top)
                                    confirm = true;
                                else if (y == top + 1)
                                    AdminDelete(admin);
                                else if (y == top + 2)
                                    AdminPrintInfo(admin);

                                db.Persons.Remove(ad);
                                db.SaveChanges();

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine();
                                Console.WriteLine($"Админ {ad.Login} успешно удален.");
                                Console.ForegroundColor = ConsoleColor.Cyan;

                                Thread.Sleep(1500);
                                HomePage();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Пароль не правильный!");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Thread.Sleep(1500);
                            AdminDelete(admin);
                        }
                    }
                }

                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Пароль слишком короткий!");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Thread.Sleep(1200);
                    AdminDelete(admin);
                }
            }
        }

        /// <summary>
        /// Метод отвечающий за удаление клиента
        /// </summary>
        /// <param name="client"></param>
        public static void ClientDelete(Client client)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();
                Console.WriteLine("Введите ваш пароль ↓");
                var password = Console.ReadLine();

                if (password.Length >= 4)
                {
                    bool confirm = false;

                    var cl = db.Clients.Where
                        (x => x.Password == password)
                        .FirstOrDefault();
                    while (confirm == false)
                    {
                        if (cl != null && password == client.Password)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Вы уверены что хотите удалить?");
                            Console.WriteLine();

                            int top = Console.CursorTop;
                            int y = top;

                            Console.WriteLine("→ Да");
                            Console.WriteLine("→ Нет");
                            Console.WriteLine("→ Вернуться назад");

                            int down = Console.CursorTop;

                            Console.CursorTop = top;

                            ConsoleKey key;

                            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                            {
                                if (key == ConsoleKey.UpArrow)
                                {
                                    if (y > top)
                                    {
                                        y--;
                                        Console.CursorTop = y;
                                    }
                                }
                                else if (key == ConsoleKey.DownArrow)
                                {
                                    if (y < down - 1)
                                    {
                                        y++;
                                        Console.CursorTop = y;
                                    }
                                }
                            }

                            Console.CursorTop = down;

                            if (y == top)
                                confirm = true;
                            else if (y == top + 1)
                                ClientDelete(client);
                            else if (y == top + 2)
                                ClientPrintInfo(client);

                            db.Persons.Remove(cl);
                            db.SaveChanges();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine($"Пользователь {cl.Login} успешно удален.");
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            Thread.Sleep(1500);
                            HomePage();
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Вы не правильно ввели свой пароль!");
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            Thread.Sleep(1500);
                            ClientDelete(client);
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Пароль слишком короткий!");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Thread.Sleep(1500);
                    ClientDelete(client);
                }
            }
        }

        /// <summary>
        /// Метод отвечающий за регистрацию пользователей
        /// </summary>
        public static void Registration()
        {
            Console.Clear();

            Console.WriteLine("==============================");
            Console.WriteLine("Укажите тип пользователя ↓");
            Console.WriteLine("==============================");

            int top = Console.CursorTop;
            int y = top;

            Console.WriteLine("→ Пользователь");
            Console.WriteLine("→ Админ");
            Console.WriteLine("→ Вернуться назад");

            int down = Console.CursorTop;

            Console.CursorTop = top;

            ConsoleKey key;

            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
            {
                if (key == ConsoleKey.UpArrow)
                {
                    if (y > top)
                    {
                        y--;
                        Console.CursorTop = y;
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (y < down - 1)
                    {
                        y++;
                        Console.CursorTop = y;
                    }
                }
            }

            Console.CursorTop = down;

            if (y == top)
                AddClient();
            else if (y == top + 1)
                AddAdmin();

            else if (y == top + 2)
                HomePage();
        }

        /// <summary>
        /// Метод отвечающий за перевод денег
        /// </summary>
        /// <param name="us"></param>
        /// <returns></returns>
        public static decimal MoneyTransfer(Client us)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();
                string transName = "Перевод денег";

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("=============================");
                Console.WriteLine("          Внимание!\nЕсли у получателя другой банк, взимается %." +
                    "\n (3 % - 50 - 5.000 сом)" +
                    "\n (5 % - 5.000 - 10.000 сом)" +
                    "\n (7 % - 10.000 > сом)");
                Console.WriteLine("=============================");
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("Введите номер телефона пользователя ↓");
                Console.Write("+996 ");

                var ph = Console.ReadLine();
                Console.WriteLine();

                if (ph.Length == 9 && ph != us.PhoneNumber)
                {
                    var client = db.Clients.FirstOrDefault(x => x.PhoneNumber == ph);
                    if (client != null)
                    {
                        Console.WriteLine($"Абонент найден, имя абонента: {client.FirstName}");
                        Console.WriteLine();
                        Console.WriteLine("Введите сумму перевода ↓");
                        string stSum = Console.ReadLine();
                        decimal sum;
                        bool SumOk = decimal.TryParse(stSum, out sum);
                        Console.WriteLine();

                        if (us.Money > 0 && ph != us.PhoneNumber)
                        {
                            while (!SumOk)
                            {
                                Console.Clear();

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Введите сумму перевода ↓");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                stSum = Console.ReadLine();
                                sum = 0;
                                SumOk = decimal.TryParse(stSum, out sum);
                                Console.WriteLine();
                            }
                            if (stSum.Any(x => Char.IsWhiteSpace(x)) == false)
                            {
                                while (!SumOk)
                                {
                                    bool sumOk = false;
                                    if (!sumOk)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Не корректная сумма!");
                                        Console.WriteLine("Введите сумму перевода ↓");
                                        Console.ForegroundColor = ConsoleColor.Cyan;

                                        stSum = Console.ReadLine();
                                        sum = 0;
                                        SumOk = decimal.TryParse(stSum, out sum);
                                        Console.WriteLine();
                                    }
                                }

                                if (sum > 20)
                                {
                                    if (us.Money >= sum)
                                    {
                                        if (us.Bank != client.Bank)
                                        {
                                            if (sum >= 2 && sum < 50)
                                            {
                                                us.Money -= sum;
                                                client.Money += sum;

                                                Console.WriteLine("Идёт процесс...");
                                                Thread.Sleep(1100);
                                                Console.WriteLine("");
                                                Console.WriteLine($"Сумма успешно переведена абоненту: {client.FirstName} {client.LastName}.");
                                                Console.WriteLine();
                                                Console.WriteLine($"Сумма перевода: {sum} сом");
                                                Console.WriteLine($"Комиссия: 0 cом (0 % - от суммы: {sum} сом)");
                                                db.Entry<Client>(client).State = EntityState.Modified;
                                                db.Entry<Client>(us).State = EntityState.Modified;
                                                db.SaveChanges();

                                                Thread.Sleep(2100);

                                                HistoryOfTransaction historyOfTransaction = new HistoryOfTransaction()
                                                {
                                                    ClientId = us.Id,
                                                    NameOfTransaction = transName,
                                                    Sender = us.Login,
                                                    SenderBank = us.Bank.ToString(),
                                                    Recipient = client.Login,
                                                    RecipientBank = client.Bank.ToString(),
                                                    Sum = sum,
                                                    Commission = 0.0,
                                                    TransDateTime = DateTime.Now.Date
                                                };

                                                db.HistoryOfTransactions.Add(historyOfTransaction);
                                                db.SaveChanges();
                                                ClientMenu(us);
                                            }
                                            else if (sum > 50 && sum <= 5000)
                                            {
                                                var comission = (double)sum * 0.03;
                                                var sum2 = (double)sum - comission;
                                                us.Money -= sum;
                                                client.Money += (decimal)sum2;

                                                Console.WriteLine("Идёт процесс...");
                                                Thread.Sleep(1100);
                                                Console.WriteLine("");
                                                Console.WriteLine($"Сумма успешно переведена абоненту: {client.FirstName} {client.LastName}.");
                                                Console.WriteLine();
                                                Console.WriteLine($"Сумма перевода: {sum2} сом");
                                                Console.WriteLine($"Комиссия: {Math.Round(comission, 2)} сом (3 % - от суммы: {sum} сом)");
                                                db.Entry<Client>(client).State = EntityState.Modified;
                                                db.Entry<Client>(us).State = EntityState.Modified;
                                                db.SaveChanges();

                                                Thread.Sleep(2100);

                                                HistoryOfTransaction historyOfTransaction = new HistoryOfTransaction()
                                                {
                                                    ClientId = us.Id,
                                                    NameOfTransaction = transName,
                                                    Sender = us.Login,
                                                    SenderBank = us.Bank.ToString(),
                                                    Recipient = client.Login,
                                                    RecipientBank = client.Bank.ToString(),
                                                    Sum = (decimal)sum2,
                                                    Commission = comission,
                                                    TransDateTime = DateTime.Now.Date
                                                };

                                                db.HistoryOfTransactions.Add(historyOfTransaction);
                                                db.SaveChanges();
                                                ClientMenu(us);
                                            }
                                            else if (sum > 5000 && sum <= 10000)
                                            {
                                                var comission = (double)sum * 0.05;
                                                var sum2 = (double)sum - comission;
                                                us.Money -= sum;
                                                client.Money += (decimal)sum2;

                                                Console.WriteLine("Идёт процесс...");
                                                Thread.Sleep(1100);
                                                Console.WriteLine("");
                                                Console.WriteLine($"Сумма успешно переведена абоненту: {client.FirstName} {client.LastName}.");
                                                Console.WriteLine();
                                                Console.WriteLine($"Сумма перевода: {sum2} сом");
                                                Console.WriteLine($"Комиссия: {Math.Round(comission, 2)} cом (5 % - от суммы: {sum} сом)");
                                                db.Entry<Client>(client).State = EntityState.Modified;
                                                db.Entry<Client>(us).State = EntityState.Modified;
                                                db.SaveChanges();

                                                Thread.Sleep(2500);

                                                HistoryOfTransaction historyOfTransaction = new HistoryOfTransaction()
                                                {
                                                    ClientId = us.Id,
                                                    NameOfTransaction = transName,
                                                    Sender = us.Login,
                                                    SenderBank = us.Bank.ToString(),
                                                    Recipient = client.Login,
                                                    RecipientBank = client.Bank.ToString(),
                                                    Sum = (decimal)sum2,
                                                    Commission = comission,
                                                    TransDateTime = DateTime.Now.Date
                                                };

                                                db.HistoryOfTransactions.Add(historyOfTransaction);
                                                db.SaveChanges();
                                                ClientMenu(us);
                                            }
                                            else if (sum > 10000)
                                            {
                                                var comission = (double)sum * 0.07;
                                                var sum2 = (double)sum - comission;
                                                us.Money -= sum;
                                                client.Money += (decimal)sum2;

                                                Console.WriteLine("Идёт процесс...");
                                                Thread.Sleep(1100);
                                                Console.WriteLine("");
                                                Console.WriteLine($"Сумма успешно переведена абоненту: {client.FirstName} {client.LastName}.");
                                                Console.WriteLine();
                                                Console.WriteLine($"Сумма перевода: {sum2} сом");
                                                Console.WriteLine($"Комиссия: {Math.Round(comission, 2)} cом (7 % - от суммы: {sum} сом)");
                                                db.Entry<Client>(client).State = EntityState.Modified;
                                                db.Entry<Client>(us).State = EntityState.Modified;
                                                db.SaveChanges();

                                                Thread.Sleep(2500);

                                                HistoryOfTransaction historyOfTransaction = new HistoryOfTransaction()
                                                {
                                                    ClientId = us.Id,
                                                    NameOfTransaction = transName,
                                                    Sender = us.Login,
                                                    SenderBank = us.Bank.ToString(),
                                                    Recipient = client.Login,
                                                    RecipientBank = client.Bank.ToString(),
                                                    Sum = (decimal)sum2,
                                                    Commission = comission,
                                                    TransDateTime = DateTime.Now.Date
                                                };

                                                db.HistoryOfTransactions.Add(historyOfTransaction);
                                                db.SaveChanges();
                                                ClientMenu(us);
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Недостаточно средств на балансе");
                                                Console.WriteLine($"Ваш баланc: {us.Money} сом");
                                                Console.ForegroundColor = ConsoleColor.Cyan;

                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine("==========================");
                                                Console.ForegroundColor = ConsoleColor.Cyan;

                                                int top = Console.CursorTop;
                                                int y = top;
                                                Console.WriteLine("→ Пополнить баланс");
                                                Console.WriteLine("→ Вернуться назад");

                                                int down = Console.CursorTop;

                                                Console.CursorTop = top;

                                                ConsoleKey key;

                                                while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                                                {
                                                    if (key == ConsoleKey.UpArrow)
                                                    {
                                                        if (y > top)
                                                        {
                                                            y--;
                                                            Console.CursorTop = y;
                                                        }
                                                    }
                                                    else if (key == ConsoleKey.DownArrow)
                                                    {
                                                        if (y < down - 1)
                                                        {
                                                            y++;
                                                            Console.CursorTop = y;
                                                        }
                                                    }
                                                }

                                                Console.CursorTop = down;

                                                if (y == top)
                                                    BalanceAdd(client);
                                                else if (y == top + 1)
                                                    ClientMenu(client);
                                            }
                                        }
                                        else if (us.Bank == client.Bank)
                                        {
                                            us.Money -= sum;
                                            client.Money += sum;

                                            Console.WriteLine("Идёт процесс...");
                                            Thread.Sleep(1100);
                                            Console.WriteLine("");
                                            Console.WriteLine($"Сумма успешно переведена абоненту: {client.FirstName} {client.LastName}.");
                                            Console.WriteLine();
                                            Console.WriteLine($"Сумма перевода: {sum} сом");
                                            Console.WriteLine($"Комиссия: 0 cом (0 % - от суммы перевода: {sum} сом)");
                                            db.Entry<Client>(client).State = EntityState.Modified;
                                            db.Entry<Client>(us).State = EntityState.Modified;
                                            db.SaveChanges();

                                            Thread.Sleep(2100);

                                            HistoryOfTransaction historyOfTransaction = new HistoryOfTransaction()
                                            {
                                                ClientId = us.Id,
                                                NameOfTransaction = transName,
                                                Sender = us.Login,
                                                SenderBank = us.Bank.ToString(),
                                                Recipient = client.Login,
                                                RecipientBank = client.Bank.ToString(),
                                                Sum = sum,
                                                Commission = 0.0,
                                                TransDateTime = DateTime.Now.Date
                                            };

                                            db.HistoryOfTransactions.Add(historyOfTransaction);
                                            db.SaveChanges();
                                            ClientMenu(us);
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Недостаточно средств на балансе.");
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine($"Ваш баланc: {us.Money} сом");

                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine("==========================");
                                        Console.ForegroundColor = ConsoleColor.Cyan;

                                        int top = Console.CursorTop;
                                        int y = top;

                                        Console.WriteLine("→ Пополнить баланс");
                                        Console.WriteLine("→ Вернуться назад");

                                        int down = Console.CursorTop;

                                        Console.CursorTop = top;

                                        ConsoleKey key;

                                        while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                                        {
                                            if (key == ConsoleKey.UpArrow)
                                            {
                                                if (y > top)
                                                {
                                                    y--;
                                                    Console.CursorTop = y;
                                                }
                                            }
                                            else if (key == ConsoleKey.DownArrow)
                                            {
                                                if (y < down - 1)
                                                {
                                                    y++;
                                                    Console.CursorTop = y;
                                                }
                                            }
                                        }

                                        Console.CursorTop = down;

                                        if (y == top)
                                            Balance(us);
                                        else if (y == top + 1)
                                            ClientMenu(us);
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Нельзя ввести сумму меньше 20!");
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                    Thread.Sleep(1100);
                                    MoneyTransfer(us);
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Нельзя ввести с пробелами или буквами!");
                                Console.ForegroundColor = ConsoleColor.Cyan;

                                Thread.Sleep(1100);
                                MoneyTransfer(us);
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Недостаточно средств на балансе.");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"Ваш баланc: {us.Money} сом");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("==========================");
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            int top = Console.CursorTop;
                            int y = top;

                            Console.WriteLine("→ Пополнить баланс");
                            Console.WriteLine("→ Вернуться назад");

                            int down = Console.CursorTop;

                            Console.CursorTop = top;

                            ConsoleKey key;

                            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                            {
                                if (key == ConsoleKey.UpArrow)
                                {
                                    if (y > top)
                                    {
                                        y--;
                                        Console.CursorTop = y;
                                    }
                                }
                                else if (key == ConsoleKey.DownArrow)
                                {
                                    if (y < down - 1)
                                    {
                                        y++;
                                        Console.CursorTop = y;
                                    }
                                }
                            }

                            Console.CursorTop = down;

                            if (y == top)
                                Balance(us);
                            else if (y == top + 1)
                                ClientMenu(us);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Абонент не найден.\nИли номер не правильно введено!");
                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("==========================");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        int top = Console.CursorTop;
                        int y = top;
                        Console.WriteLine("→ Ввести номер заново");
                        Console.WriteLine("→ Вернуться назад");

                        int down = Console.CursorTop;

                        Console.CursorTop = top;

                        ConsoleKey key;

                        while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                        {
                            if (key == ConsoleKey.UpArrow)
                            {
                                if (y > top)
                                {
                                    y--;
                                    Console.CursorTop = y;
                                }
                            }
                            else if (key == ConsoleKey.DownArrow)
                            {
                                if (y < down - 1)
                                {
                                    y++;
                                    Console.CursorTop = y;
                                }
                            }
                        }

                        Console.CursorTop = down;

                        if (y == top)
                            MoneyTransfer(us);
                        else if (y == top + 1)
                            ClientMenu(us);
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Номер телефона не корректно введено!\nИли вы ввели свой номер!");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("==========================");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    int top = Console.CursorTop;
                    int y = top;

                    Console.WriteLine("→ Номер заново");
                    Console.WriteLine("→ Вернуться назад");

                    int down = Console.CursorTop;

                    Console.CursorTop = top;

                    ConsoleKey key;

                    while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                    {
                        if (key == ConsoleKey.UpArrow)
                        {
                            if (y > top)
                            {
                                y--;
                                Console.CursorTop = y;
                            }
                        }
                        else if (key == ConsoleKey.DownArrow)
                        {
                            if (y < down - 1)
                            {
                                y++;
                                Console.CursorTop = y;
                            }
                        }
                    }

                    Console.CursorTop = down;

                    if (y == top)
                        MoneyTransfer(us);
                    else if (y == top + 1)
                        ClientMenu(us);
                }
            }
            return us.Money;
        }

        /// <summary>
        /// Метод отвечающий за меню пополнения баланса
        /// </summary>
        /// <param name="client"></param>
        public static void Balance(Client client)
        {
            Console.Clear();
            Console.WriteLine();
            BalanceAdd(client);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================");
            Console.ForegroundColor = ConsoleColor.Cyan;

            int top = Console.CursorTop;
            int y = top;

            Console.WriteLine("→ Пополнить баланс");
            Console.WriteLine("→ Вернуться назад");

            int down = Console.CursorTop;

            Console.CursorTop = top;

            ConsoleKey key;

            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
            {
                if (key == ConsoleKey.UpArrow)
                {
                    if (y > top)
                    {
                        y--;
                        Console.CursorTop = y;
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (y < down - 1)
                    {
                        y++;
                        Console.CursorTop = y;
                    }
                }
            }

            Console.CursorTop = down;

            if (y == top)
                Balance(client);
            else if (y == top + 1)
                ClientMenu(client);
        }

        /// <summary>
        /// Метод отвечающий за пополнение баланса
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static decimal BalanceAdd(Client client)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                try
                {
                    string transName = "Пополнение баланса";
                    string sender = "Терминал";
                    Console.Clear();
                    Console.WriteLine("Введите сумму ↓");
                    decimal money = decimal.Parse(Console.ReadLine());
                    if (money >= 20)
                    {
                        var commission = 5;
                        client.Money += money - commission;
                        db.Entry<Client>(client).State = EntityState.Modified;
                        db.SaveChanges();

                        Console.WriteLine();
                        Console.WriteLine("Идёт процесс...\n");
                        Thread.Sleep(1800);
                        Console.WriteLine($"Комиссия: {commission} сом");
                        Console.WriteLine($"Сумма пополнения: {money - commission}");
                        Console.WriteLine();
                        Console.WriteLine($"Ваш баланс успешно пополнен!\nВаш баланс: {client.Money} сом");
                        Console.WriteLine();

                        HistoryOfTransaction historyOfTransaction = new HistoryOfTransaction()
                        {
                            ClientId = client.Id,
                            NameOfTransaction = transName,
                            Sender = sender,
                            Recipient = client.Login,
                            RecipientBank = client.Bank.ToString(),
                            Sum = money,
                            Commission = commission,
                            TransDateTime = DateTime.Now.Date
                        };
                        db.HistoryOfTransactions.Add(historyOfTransaction);
                        db.SaveChanges();
                        Thread.Sleep(2000);
                        ClientMenu(client);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Нельзя ввести сумму меньше 20 сом!");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Thread.Sleep(1100);
                        BalanceAdd(client);
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Сумма введена не корректно.\nИли содержит в себе буквы!");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Thread.Sleep(1100);
                    BalanceAdd(client);
                }
            }
            return client.Money;
        }

        /// <summary>
        /// Метод отвечающий за вывод информации об администраторе
        /// </summary>
        /// <param name="admin"></param>
        public static void AdminPrintInfo(Admin admin)
        {
            Console.Clear();

            var dateOnly = admin.DateOfCreate.ToString("dd.MM.yyyy");

            Console.WriteLine("==============================");
            Console.WriteLine("Информация о пользователе ↓");
            Console.WriteLine("==============================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine($"Логин: {admin.Login}");
            Console.WriteLine($"Тип пользователя: {admin.Role}");
            Console.WriteLine($"Полное имя: {admin.FirstName} {admin.LastName}");
            Console.WriteLine($"Дата создания аккаунта: {dateOnly}");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================");
            Console.ForegroundColor = ConsoleColor.Cyan;

            int top = Console.CursorTop;
            int y = top;

            Console.WriteLine("→ Удалить аккаунт");
            Console.WriteLine($"→ Вернуться в меню {admin.Role}");


            int down = Console.CursorTop;

            Console.CursorTop = top;

            ConsoleKey key;
            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
            {
                if (key == ConsoleKey.UpArrow)
                {
                    if (y > top)
                    {
                        y--;
                        Console.CursorTop = y;
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (y < down - 1)
                    {
                        y++;
                        Console.CursorTop = y;
                    }
                }
            }

            Console.CursorTop = down;

            if (y == top)
                AdminDelete(admin);
            else if (y == top + 1)
                AdminMenu(admin);
        }

        /// <summary>
        /// Метод отвечающий за вывод информации о пользователе
        /// </summary>
        /// <param name="client"></param>
        public static void ClientPrintInfo(Client client)
        {
            Console.Clear();

            Console.WriteLine("==============================");
            Console.WriteLine("Информация о пользователе ↓");
            Console.WriteLine("==============================");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;

            char[] array = client.PhoneNumber.ToArray();

            char one = array[0];
            char two = array[1];
            char three = array[2];
            char four = array[3];
            char five = array[4];
            char six = array[5];
            char seven = array[6];
            char eigth = array[7];
            char nine = array[8];

            var dateOnly = client.DateOfCreate.ToString("dd.MM.yyyy");

            Console.WriteLine($"Логин: {client.Login}");
            Console.WriteLine($"Тип пользователя: {client.Role}");
            Console.WriteLine($"Полное имя: {client.FirstName} {client.LastName}");
            Console.WriteLine($"Номер телефона: +996 {one}{two}{three}-{four}{five}{six}-{seven}{eigth}{nine}");
            Console.WriteLine($"Банк пользователя: {client.Bank}");
            Console.WriteLine($"Баланс: {client.Money} сом");
            Console.WriteLine($"Дата создания аккаунта: {dateOnly}");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==========================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            int top = Console.CursorTop;
            int y = top;

            Console.WriteLine("→ Изменить личную информацию");
            Console.WriteLine("→ Удалить аккаунт");
            Console.WriteLine($"→ Вернуться в меню {client.Role}");

            int down = Console.CursorTop;

            Console.CursorTop = top;

            ConsoleKey key;
            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
            {
                if (key == ConsoleKey.UpArrow)
                {
                    if (y > top)
                    {
                        y--;
                        Console.CursorTop = y;
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (y < down - 1)
                    {
                        y++;
                        Console.CursorTop = y;
                    }
                }
            }
            Console.CursorTop = down;

            if (y == top)
                ChangeClient(client);
            else if (y == top + 1)
                ClientDelete(client);
            else if (y == top + 2)
                ClientMenu(client);
        }

        /// <summary>
        /// Метод отвечающий за подтверждение пароля
        /// </summary>
        /// <param name="client"></param>
        public static void ConfirmPass(Client client)
        {
            Console.Clear();
            Console.WriteLine("Введите свой текущий пароль, чтобы провести изменения ↓");
            string pass = "";
            pass = Console.ReadLine();
            bool passwordOkay = false;
            while (!passwordOkay)
            {
                if (pass != client.Password)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Пароли не совпадают!");
                    Console.WriteLine("Введите пароль заново ↓");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    pass = Console.ReadLine();
                }
                else
                {
                    passwordOkay = true;
                }
            }
        }

        /// <summary>
        /// Метод отвечающий за смену пароля клиента
        /// </summary>
        /// <param name="client"></param>
        public static void ChangeClientPass(Client client)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ConfirmPass(client);
                Console.WriteLine();
                string password = "";
                Console.WriteLine("Введите новый пароль ↓");
                password = Console.ReadLine();
                bool passwordOkay = false;
                while (!passwordOkay)
                {
                    if (password.Length < 4)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Пароль слишком короткий!");
                        Console.WriteLine("Введите пароль заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        password = Console.ReadLine();
                    }
                    else if (password.Any(x => Char.IsWhiteSpace(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("С пробелами не прокатит!");
                        Console.WriteLine("Введите пароль заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        password = Console.ReadLine();
                    }
                    else
                    {
                        passwordOkay = true;
                        client.Password = password;
                        db.Entry<Client>(client).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                string confirmPassword;
                Console.WriteLine();
                Console.WriteLine("Повторите новый пароль ↓");
                confirmPassword = Console.ReadLine();
                bool confirmPassOkay = false;
                while (!confirmPassOkay)
                {
                    if (password != confirmPassword)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Новый пароль не совпадает");
                        Console.WriteLine("Введите пароль заново ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        confirmPassword = Console.ReadLine();
                    }
                    else
                    {
                        confirmPassOkay = true;
                        client.ConfirmPassword = confirmPassword;
                        db.Entry<Client>(client).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                db.SaveChanges();
                Thread.Sleep(1000);
                Console.WriteLine("Вы успешно сменили пароль!");
                Thread.Sleep(1000);
                ClientPrintInfo(client);
            }
        }

        /// <summary>
        /// Метод отвечающий за изменение банка у пользователя
        /// </summary>
        /// <param name="client"></param>
        public static void ChangeClientBank(Client client)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();
                ConfirmPass(client);
                Console.Clear();

                Console.WriteLine("Выберите новый банк ↓");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("======================");
                Console.ForegroundColor = ConsoleColor.Cyan;

                var bank = client.Bank;
                int top = Console.CursorTop;
                int y = top;

                Console.WriteLine($"→ {Bank.Mbank}");
                Console.WriteLine($"→ {Bank.OptimaBank}");
                Console.WriteLine($"→ {Bank.BakaiBank}");
                Console.WriteLine($"→ {Bank.DemirBank}");
                Console.WriteLine($"→ {Bank.AiylBank}");

                int down = Console.CursorTop;

                Console.CursorTop = top;

                ConsoleKey key;
                while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                {
                    if (key == ConsoleKey.UpArrow)
                    {
                        if (y > top)
                        {
                            y--;
                            Console.CursorTop = y;
                        }
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        if (y < down - 1)
                        {
                            y++;
                            Console.CursorTop = y;
                        }
                    }
                }

                Console.CursorTop = down;

                if (y == top)
                    bank = Bank.Mbank;
                else if (y == top + 1)
                    bank = Bank.OptimaBank;
                else if (y == top + 2)
                    bank = Bank.BakaiBank;
                else if (y == top + 3)
                    bank = Bank.DemirBank;
                else if (y == top + 4)
                    bank = Bank.AiylBank;

                client.Bank = bank;
                db.Entry<Client>(client).State = EntityState.Modified;
                db.SaveChanges();
                Thread.Sleep(1000);
                Console.WriteLine();
                Console.WriteLine("Вы успешно сменили банк!");
                Thread.Sleep(1000);
                ClientPrintInfo(client);
            }
        }


        /// <summary>
        /// Метод отвечающий за изменение имени пользователя
        /// </summary>
        /// <param name="client"></param>
        public static void ChangeClienFirstName(Client client)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();
                ConfirmPass(client);
                Console.Clear();
                string name1;
                Console.WriteLine();
                Console.WriteLine("Введите новое имя ↓");
                name1 = Console.ReadLine();
                bool isNameOkay = false;
                while (!isNameOkay)
                {
                    if (name1.Length < 2)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Имя не может быть короче 2-х букв.");
                        Console.WriteLine("Повторно введите новое имя ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name1 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else if (name1.Any(x => Char.IsWhiteSpace(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("С пробелами не прокатит!");
                        Console.WriteLine("Повторно введите ваше имя ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name1 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else if (name1.Any(x => Char.IsDigit(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Имя не может содержать в себе цифры!");
                        Console.WriteLine("Повторно введите ваше имя ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name1 = Console.ReadLine();
                        Console.WriteLine();
                    }

                    else
                    {
                        isNameOkay = true;
                        client.FirstName = name1;
                        db.Entry<Client>(client).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                db.SaveChanges();
                Thread.Sleep(1000);
                Console.WriteLine("Вы успешно сменили имя!");
                Thread.Sleep(1000);
                ClientPrintInfo(client);
            }
        }

        /// <summary>
        /// Метод отвечающий за изменение фамилии пользователя
        /// </summary>
        /// <param name="client"></param>
        public static void ChangeClienLastName(Client client)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();
                ConfirmPass(client);
                Console.Clear();
                string name2 = "";
                Console.WriteLine("Введите вашу фамилию ↓");
                name2 = Console.ReadLine();
                bool name2Okay = false;
                while (!name2Okay)
                {
                    if (name2.Length < 2)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Фамилия не может быть короче 2-х букв.");
                        Console.WriteLine("Повторно введите вашу фамилию ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name2 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else if (name2.Any(x => Char.IsDigit(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Фамилия не может содержать в себе цифры!");
                        Console.WriteLine("Повторно введите вашу фамилию ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name2 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else if (name2.Any(x => Char.IsWhiteSpace(x)))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("Не прокатит с пробелами!");
                        Console.WriteLine("Повторно введите вашу фамилию ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        name2 = Console.ReadLine();
                        Console.WriteLine();
                    }
                    else
                    {
                        name2Okay = true;
                        client.LastName = name2;
                        db.Entry<Client>(client).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                db.SaveChanges();

                Thread.Sleep(1000);
                Console.WriteLine();
                Console.WriteLine("Вы успешно сменили фамилию!");
                Thread.Sleep(1000);
                ClientPrintInfo(client);
            }
        }

        /// <summary>
        /// Метод отвечающий за изменение личной информации клиента
        /// </summary>
        /// <param name="client"></param>
        public static void ChangeClient(Client client)
        {
            Console.Clear();
            Console.WriteLine("==============================");
            Console.WriteLine($"Меню изменения\nличной информации {client.Login} ↓");
            Console.WriteLine("==============================");
            Console.WriteLine();
            int top = Console.CursorTop;
            int y = top;

            Console.WriteLine("→ Сменить пароль");
            Console.WriteLine("→ Сменить имя");
            Console.WriteLine("→ Сменить фамилию");
            Console.WriteLine("→ Сменить банк");
            Console.WriteLine("→ Вернуться назад");

            int down = Console.CursorTop;

            Console.CursorTop = top;

            ConsoleKey key;
            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
            {
                if (key == ConsoleKey.UpArrow)
                {
                    if (y > top)
                    {
                        y--;
                        Console.CursorTop = y;
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (y < down - 1)
                    {
                        y++;
                        Console.CursorTop = y;
                    }
                }
            }
            Console.CursorTop = down;

            if (y == top)
                ChangeClientPass(client);
            else if (y == top + 1)
                ChangeClienFirstName(client);
            else if (y == top + 2)
                ChangeClienLastName(client);
            else if (y == top + 3)
                ChangeClientBank(client);
            else if (y == top + 4)
                ClientPrintInfo(client);
        }

        /// <summary>
        /// Метод отвечающий за оплату услуг пользователя
        /// </summary>
        /// <param name="admin"></param>
        public static void AddCheckForClient(Admin admin)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();

                Console.WriteLine("Введите логин пользователя, для добавления услуги ↓");
                var login = Console.ReadLine();
                var cl = db.Clients.FirstOrDefault(x => x.Login == login);
                if (cl != null)
                {
                    Console.Clear();
                    Console.WriteLine($"Пользователь найден, имя пользователя: {cl.FirstName}");
                    Console.WriteLine();
                    var ser = Services.DefaultValue;
                    Console.WriteLine("Выберите услугу ↓");
                    Console.WriteLine();

                    int top = Console.CursorTop;
                    int y = top;

                    Console.WriteLine("→ Оплата за свет");
                    Console.WriteLine("→ Оплата за воду");
                    Console.WriteLine("→ Оплата за газ");
                    Console.WriteLine("→ Оплата за интернет и ТВ");

                    int down = Console.CursorTop;

                    Console.CursorTop = top;

                    ConsoleKey key;
                    while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                    {
                        if (key == ConsoleKey.UpArrow)
                        {
                            if (y > top)
                            {
                                y--;
                                Console.CursorTop = y;
                            }
                        }
                        else if (key == ConsoleKey.DownArrow)
                        {
                            if (y < down - 1)
                            {
                                y++;
                                Console.CursorTop = y;
                            }
                        }
                    }
                    Console.CursorTop = down;

                    if (y == top)
                        ser = Services.ForLight;
                    else if (y == top + 1)
                        ser = Services.ForWater;
                    else if (y == top + 2)
                        ser = Services.ForGas;
                    else if (y == top + 3)
                        ser = Services.ForInthernetTv;

                    Console.WriteLine();
                    Console.WriteLine("Введите сумму ↓");
                    decimal sum;
                    string s = Console.ReadLine();
                    bool sumOkay = decimal.TryParse(s, out sum);
                    while (sum < 20)
                    {
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Нельзя ввести сумму меньше 20 сом!");
                        Console.WriteLine("повторно введите сумму ↓");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        s = Console.ReadLine();
                        sumOkay = decimal.TryParse(s, out sum);
                        while (!sumOkay)
                        {
                            if (sum > 20)
                            {
                                if (sumOkay == false)
                                {
                                    Console.Clear();

                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Нельзя ввести буквы или пустую строку!");
                                    Console.WriteLine($"Введите сумму заново ↓");
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                    s = Console.ReadLine();
                                    sumOkay = decimal.TryParse(s, out sum);
                                }
                                else if (s.Any(x => String.IsNullOrWhiteSpace(s)))
                                {
                                    Console.Clear();

                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Нельзя ввести пустую строку!");
                                    Console.WriteLine($"Введите сумму заново ↓");
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                    s = Console.ReadLine();
                                    sumOkay = decimal.TryParse(s, out sum);
                                }
                                else
                                {
                                    sumOkay = true;
                                }
                            }
                            else
                            {
                                Console.Clear();

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Не правильный ввод суммы!");
                                Console.WriteLine($"Введите сумму заново ↓");
                                Console.ForegroundColor = ConsoleColor.Cyan;

                                s = Console.ReadLine();
                                sumOkay = decimal.TryParse(s, out sum);
                            }
                        }
                    }

                    var service = new Service()
                    {
                        ClientId = cl.Id,
                        ClientLogin = cl.Login,
                        Services = ser,
                        ServiceAddDate = DateTime.Now.Date,
                        PaidDate = DateTime.Now.AddDays(31),
                        Sum = sum,
                        IsPaid = false
                    };

                    db.Services.Add(service);
                    db.SaveChanges();

                    Console.WriteLine();
                    Console.WriteLine($"Вы успешно добавили услугу клиенту: {cl.Login}");

                    Thread.Sleep(1500);
                    AdminMenu(admin);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Такого пользователя не существует.\nИли не правильные данные!");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("==========================");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    int top = Console.CursorTop;
                    int y = top;

                    Console.WriteLine("→ Заново ввести логин");
                    Console.WriteLine("→ Вернуться назад");

                    int down = Console.CursorTop;

                    Console.CursorTop = top;

                    ConsoleKey key;
                    while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                    {
                        if (key == ConsoleKey.UpArrow)
                        {
                            if (y > top)
                            {
                                y--;
                                Console.CursorTop = y;
                            }
                        }
                        else if (key == ConsoleKey.DownArrow)
                        {
                            if (y < down - 1)
                            {
                                y++;
                                Console.CursorTop = y;
                            }
                        }
                    }
                    Console.CursorTop = down;

                    if (y == top)
                        AddCheckForClient(admin);
                    else if (y == top + 1)
                        AdminMenu(admin);
                }
            }
        }

        /// <summary>
        /// Метод отвечающий о выводе информации об услугах пользователя
        /// </summary>
        /// <param name="admin"></param>
        public static void ListOfServices(Admin admin)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();
                var ser = db.Services.Where(x => x.Id >= 1);
                var data = ser.ToList();
                var count = ser.Count();
                if (count >= 1)
                {
                    Console.WriteLine($"Количество услуг: {count}");
                    Console.WriteLine();
                    Console.WriteLine("Список услуг ↓");

                    foreach (var item in data)
                    {
                        var dateOnly1 = item.PaidDate.ToString("dd.MM.yyyy");
                        var dateOnly2 = item.ServiceAddDate.ToString("dd.MM.yyyy");

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine();
                        Console.WriteLine("==========================");
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        Console.WriteLine($"Идентификатор услуги: {item.Id}");
                        Console.WriteLine($"Логин клиента: {item.ClientLogin}");
                        Console.WriteLine($"Тип услуги: {item.Services}");
                        Console.WriteLine($"Сумма: {item.Sum} сом");
                        Console.WriteLine($"Дата создания услуги: {dateOnly2}");
                        Console.WriteLine($"Срок оплаты: {dateOnly1}");
                        Console.WriteLine($"Статус оплаты: {item.IsPaid}");
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("==========================");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    int top = Console.CursorTop;
                    int y = top;
                    Console.WriteLine("→ Удалить услугу");
                    Console.WriteLine("→ Вернуться назад");

                    int down = Console.CursorTop;

                    Console.CursorTop = top;

                    ConsoleKey key;
                    while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                    {
                        if (key == ConsoleKey.UpArrow)
                        {
                            if (y > top)
                            {
                                y--;
                                Console.CursorTop = y;
                            }
                        }
                        else if (key == ConsoleKey.DownArrow)
                        {
                            if (y < down - 1)
                            {
                                y++;
                                Console.CursorTop = y;
                            }
                        }
                    }
                    Console.CursorTop = down;

                    if (y == top)
                        DeleteServices(admin);
                    else if (y == top + 1)
                        AdminMenu(admin);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("В базе нет Услуг.");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine();

                    Thread.Sleep(1000);
                    AdminMenu(admin);
                }
            }
        }

        /// <summary>
        /// Метод отвечающий за вывод информации о неоплаченных услугах
        /// </summary>
        /// <param name="client"></param>
        public static void ListServiceToClient(Client client)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();

                var srv = db.Services.Where(x => x.ClientId == client.Id);
                var data = srv.ToList();
                int count = srv.Count();
                if (count >= 1)
                {
                    Console.WriteLine($"Количество уcлуг: {count}");
                    Console.WriteLine();
                    Console.WriteLine("Список услуг ↓");

                    foreach (var item in data)
                    {
                        var dateOnly1 = item.PaidDate.ToString("dd.MM.yyyy");
                        var dateOnly2 = item.ServiceAddDate.ToString("dd.MM.yyyy");

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine();
                        Console.WriteLine("==========================");
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        Console.WriteLine($"Тип услуги: {item.Services}");
                        Console.WriteLine($"Идентификатор услуги: {item.Id}");
                        Console.WriteLine($"Сумма: {item.Sum}");
                        Console.WriteLine($"Дата создания услуги: {dateOnly2}");
                        Console.WriteLine($"Срок оплаты, до: {dateOnly1}");
                        Console.WriteLine($"Статус оплаты: {item.IsPaid}");
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("==========================");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    int top = Console.CursorTop;
                    int y = top;
                    Console.WriteLine("→ Погасить счета");
                    Console.WriteLine("→ Вернуться назад");

                    int down = Console.CursorTop;

                    Console.CursorTop = top;

                    ConsoleKey key;

                    while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                    {
                        if (key == ConsoleKey.UpArrow)
                        {
                            if (y > top)
                            {
                                y--;
                                Console.CursorTop = y;
                            }
                        }
                        else if (key == ConsoleKey.DownArrow)
                        {
                            if (y < down - 1)
                            {
                                y++;
                                Console.CursorTop = y;
                            }
                        }
                    }

                    Console.CursorTop = down;

                    if (y == top)
                        PaymentServices(client);
                    else if (y == top + 1)
                        ClientMenu(client);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("У вас нет не оплаченных услуг.");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Thread.Sleep(1500);
                    ClientMenu(client);
                }
            }
        }

        /// <summary>
        /// Метод отвечающий за оплату услуг
        /// </summary>
        /// <param name="client"></param>
        public static void PaymentServices(Client client)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();
                string TransName = "Оплата счетов";
                Console.WriteLine("Введите идентификатор услуги ↓");
                int Id;
                string Ids = Console.ReadLine();
                bool IdOkay = int.TryParse(Ids, out Id);
                if (IdOkay == true)
                {
                    if (Id >= 1)
                    {
                        var srv = db.Services.FirstOrDefault(x => x.Id == Id);
                        if (srv != null)
                        {
                            if (srv.ClientId == client.Id && srv.Id == Id)
                            {
                                if (client.Money >= srv.Sum)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine($"Сумма услуги: {srv.Sum} сом.");
                                    Console.WriteLine();
                                    Console.WriteLine("Введите сумму для погашения ↓");
                                    string stSum = Console.ReadLine();
                                    decimal sum = 0;
                                    bool SumOk = decimal.TryParse(stSum, out sum);
                                    while (!SumOk)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine($"Сумма услуги: {srv.Sum} сом.");
                                        Console.WriteLine();
                                        Console.WriteLine("Введите сумму для погашения ↓");
                                        stSum = Console.ReadLine();
                                        SumOk = decimal.TryParse(stSum, out sum);
                                    }
                                    if (sum >= srv.Sum)
                                    {
                                        Console.WriteLine();

                                        client.Money -= srv.Sum;
                                        db.Services.Remove(srv);

                                        db.Entry<Client>(client).State = EntityState.Modified;
                                        db.Services.Remove(srv);
                                        db.SaveChanges();

                                        HistoryOfTransaction history = new HistoryOfTransaction()
                                        {
                                            NameOfTransaction = TransName,
                                            Sender = client.Login,
                                            Recipient = "Terminal Services",
                                            Sum = srv.Sum,
                                            TransDateTime = DateTime.Now,
                                            SenderBank = client.Bank.ToString(),
                                            ClientId = client.Id,
                                            Commission = 0
                                        };
                                        db.HistoryOfTransactions.Add(history);
                                        db.SaveChanges();

                                        Console.WriteLine($"Вы успешно погасили сумму за услугу: {srv.Services}");
                                        Console.WriteLine();
                                        Console.WriteLine($"Остаток вашего баланса: {client.Money} сом.");
                                        Thread.Sleep(1800);
                                        ClientMenu(client);
                                    }
                                    else
                                    {
                                        Console.Clear();

                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Не корректный ввод!");
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Thread.Sleep(1100);
                                        PaymentServices(client);
                                    }
                                }
                                else
                                {
                                    Console.Clear();

                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Недостаточно средств на балансе");
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("==========================");
                                    Console.ForegroundColor = ConsoleColor.Cyan;

                                    int top = Console.CursorTop;
                                    int y = top;
                                    Console.WriteLine("→ Пополнить баланс");
                                    Console.WriteLine("→ Вернуться назад");

                                    int down = Console.CursorTop;

                                    Console.CursorTop = top;

                                    ConsoleKey key;

                                    while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                                    {
                                        if (key == ConsoleKey.UpArrow)
                                        {
                                            if (y > top)
                                            {
                                                y--;
                                                Console.CursorTop = y;
                                            }
                                        }
                                        else if (key == ConsoleKey.DownArrow)
                                        {
                                            if (y < down - 1)
                                            {
                                                y++;
                                                Console.CursorTop = y;
                                            }
                                        }
                                    }

                                    Console.CursorTop = down;

                                    if (y == top)
                                        BalanceAdd(client);
                                    else if (y == top + 1)
                                        ClientMenu(client);
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Не правильный ввод.");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine();

                                Thread.Sleep(1000);
                                int top = Console.CursorTop;
                                int y = top;

                                Console.WriteLine("→ Ввести идентификатор заново");
                                Console.WriteLine("→ Вернуться назад");

                                int down = Console.CursorTop;

                                Console.CursorTop = top;

                                ConsoleKey key;
                                while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                                {
                                    if (key == ConsoleKey.UpArrow)
                                    {
                                        if (y > top)
                                        {
                                            y--;
                                            Console.CursorTop = y;
                                        }
                                    }
                                    else if (key == ConsoleKey.DownArrow)
                                    {
                                        if (y < down - 1)
                                        {
                                            y++;
                                            Console.CursorTop = y;
                                        }
                                    }
                                }
                                Console.CursorTop = down;

                                if (y == top)
                                    PaymentServices(client);
                                else if (y == top + 1)
                                    ClientMenu(client);
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Не правильный ввод идентификатора.");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine();

                            Thread.Sleep(1000);
                            int top = Console.CursorTop;
                            int y = top;

                            Console.WriteLine("→ Ввести идентификатор заново");
                            Console.WriteLine("→ Вернуться назад");

                            int down = Console.CursorTop;

                            Console.CursorTop = top;

                            ConsoleKey key;
                            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                            {
                                if (key == ConsoleKey.UpArrow)
                                {
                                    if (y > top)
                                    {
                                        y--;
                                        Console.CursorTop = y;
                                    }
                                }
                                else if (key == ConsoleKey.DownArrow)
                                {
                                    if (y < down - 1)
                                    {
                                        y++;
                                        Console.CursorTop = y;
                                    }
                                }
                            }
                            Console.CursorTop = down;

                            if (y == top)
                                PaymentServices(client);
                            else if (y == top + 1)
                                ClientMenu(client);
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Не правильный ввод идентификатора.");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine();

                        Thread.Sleep(1000);
                        int top = Console.CursorTop;
                        int y = top;

                        Console.WriteLine("→ Ввести идентификатор заново");
                        Console.WriteLine("→ Вернуться назад");

                        int down = Console.CursorTop;

                        Console.CursorTop = top;

                        ConsoleKey key;
                        while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                        {
                            if (key == ConsoleKey.UpArrow)
                            {
                                if (y > top)
                                {
                                    y--;
                                    Console.CursorTop = y;
                                }
                            }
                            else if (key == ConsoleKey.DownArrow)
                            {
                                if (y < down - 1)
                                {
                                    y++;
                                    Console.CursorTop = y;
                                }
                            }
                        }
                        Console.CursorTop = down;

                        if (y == top)
                            PaymentServices(client);
                        else if (y == top + 1)
                            ClientMenu(client);
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Не правильный ввод идентификатора.");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Thread.Sleep(1100);
                    PaymentServices(client);
                }
            }
        }

        /// <summary>
        /// Метод отвечающий за удаление услуги у пользователя
        /// </summary>
        /// <param name="admin"></param>
        public static void DeleteServices(Admin admin)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();
                Console.WriteLine("Введите идентификатор услуги ↓");
                int Id;
                string Ids = Console.ReadLine();
                bool IdOkay = int.TryParse(Ids, out Id);
                if (IdOkay == true)
                {
                    if (Id >= 1)
                    {
                        var srv = db.Services.FirstOrDefault(x => x.Id == Id);
                        if (srv != null)
                        {
                            if (srv.Id == Id)
                            {
                                db.Services.Remove(srv);
                                db.SaveChanges();

                                Console.WriteLine();
                                Console.WriteLine("Идет процесс...");
                                Console.WriteLine();
                                Thread.Sleep(1200);
                                Console.WriteLine($"Вы успешно удалили услугу с Id: {srv.Id}");
                                Thread.Sleep(1300);
                                AdminMenu(admin);
                            }
                            else
                            {
                                Console.Clear();

                                int top = Console.CursorTop;
                                int y = top;

                                Console.WriteLine("→ Ввести идентификатор заново");
                                Console.WriteLine("→ Вернуться назад");

                                int down = Console.CursorTop;

                                Console.CursorTop = top;

                                ConsoleKey key;
                                while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                                {
                                    if (key == ConsoleKey.UpArrow)
                                    {
                                        if (y > top)
                                        {
                                            y--;
                                            Console.CursorTop = y;
                                        }
                                    }
                                    else if (key == ConsoleKey.DownArrow)
                                    {
                                        if (y < down - 1)
                                        {
                                            y++;
                                            Console.CursorTop = y;
                                        }
                                    }
                                }
                                Console.CursorTop = down;

                                if (y == top)
                                    DeleteServices(admin);
                                else if (y == top + 1)
                                    AdminMenu(admin);
                            }
                        }
                        else
                        {
                            Console.Clear();

                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("В базе нет услуги с таким идентификатором.");
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            int top = Console.CursorTop;
                            int y = top;

                            Console.WriteLine("→ Ввести идентификатор заново");
                            Console.WriteLine("→ Вернуться назад");

                            int down = Console.CursorTop;

                            Console.CursorTop = top;

                            ConsoleKey key;
                            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                            {
                                if (key == ConsoleKey.UpArrow)
                                {
                                    if (y > top)
                                    {
                                        y--;
                                        Console.CursorTop = y;
                                    }
                                }
                                else if (key == ConsoleKey.DownArrow)
                                {
                                    if (y < down - 1)
                                    {
                                        y++;
                                        Console.CursorTop = y;
                                    }
                                }
                            }

                            Console.CursorTop = down;

                            if (y == top)
                                DeleteServices(admin);
                            else if (y == top + 1)
                                AdminMenu(admin);
                        }
                    }
                    else
                    {
                        Console.Clear();

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Не правильный ввод идентификатора.");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Не правильный ввод идентификатора.");
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        int top = Console.CursorTop;
                        int y = top;

                        Console.WriteLine("→ Ввести идентификатор заново");
                        Console.WriteLine("→ Вернуться назад");

                        int down = Console.CursorTop;

                        Console.CursorTop = top;

                        ConsoleKey key;
                        while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                        {
                            if (key == ConsoleKey.UpArrow)
                            {
                                if (y > top)
                                {
                                    y--;
                                    Console.CursorTop = y;
                                }
                            }
                            else if (key == ConsoleKey.DownArrow)
                            {
                                if (y < down - 1)
                                {
                                    y++;
                                    Console.CursorTop = y;
                                }
                            }
                        }

                        Console.CursorTop = down;

                        if (y == top)
                            DeleteServices(admin);
                        else if (y == top + 1)
                            AdminMenu(admin);
                    }
                }
                else
                {
                    Console.Clear();

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Не правильный ввод идентификатора.");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    int top = Console.CursorTop;
                    int y = top;

                    Console.WriteLine("→ Ввести идентификатор заново");
                    Console.WriteLine("→ Вернуться назад");

                    int down = Console.CursorTop;

                    Console.CursorTop = top;

                    ConsoleKey key;
                    while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                    {
                        if (key == ConsoleKey.UpArrow)
                        {
                            if (y > top)
                            {
                                y--;
                                Console.CursorTop = y;
                            }
                        }
                        else if (key == ConsoleKey.DownArrow)
                        {
                            if (y < down - 1)
                            {
                                y++;
                                Console.CursorTop = y;
                            }
                        }
                    }
                    Console.CursorTop = down;

                    if (y == top)
                        DeleteServices(admin);
                    else if (y == top + 1)
                        AdminMenu(admin);
                }
            }
        }

        /// <summary>
        /// Метод отвечающий за удаление истории транзакций
        /// </summary>
        /// <param name="admin"></param>
        public static void ClearTransList(Admin admin)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Console.Clear();
                IQueryable<HistoryOfTransaction> transactions = db.HistoryOfTransactions;

                transactions = transactions.Where(x => x.Id >= 1);

                Console.WriteLine("Введите ваш пароль\nдля подтверждения личности ↓");
                var password = Console.ReadLine();

                if (password.Length >= 4)
                {
                    bool confirm = false;

                    var cl = db.Admins.Where
                        (x => x.Password == password)
                        .FirstOrDefault();
                    var his = db.HistoryOfTransactions
                        .Where(x => x.Id >= 1);

                    while (confirm == false)
                    {
                        if (cl != null && password == admin.Password)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Вы уверены что хотите очистить список транзакций?");
                            Console.WriteLine();

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("==========================");
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            int top = Console.CursorTop;
                            int y = top;

                            Console.WriteLine("→ Да");
                            Console.WriteLine("→ Нет");
                            Console.WriteLine("→ Вернуться назад");

                            int down = Console.CursorTop;

                            Console.CursorTop = top;

                            ConsoleKey key;

                            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
                            {
                                if (key == ConsoleKey.UpArrow)
                                {
                                    if (y > top)
                                    {
                                        y--;
                                        Console.CursorTop = y;
                                    }
                                }
                                else if (key == ConsoleKey.DownArrow)
                                {
                                    if (y < down - 1)
                                    {
                                        y++;
                                        Console.CursorTop = y;
                                    }
                                }
                            }


                            Console.CursorTop = down;

                            if (y == top)
                                confirm = true;
                            else if (y == top + 1)
                                ClearTransList(admin);
                            else if (y == top + 2)
                                TransactionList(admin);

                            foreach (var item in his)
                            {
                                db.HistoryOfTransactions.Remove(item);
                            }
                            db.SaveChanges();

                            Thread.Sleep(1000);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine($"Список успешно очищен.");
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            Thread.Sleep(1500);
                            AdminMenu(admin);
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Вы не правильно ввели свой пароль!");
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            Thread.Sleep(1500);
                            ClearTransList(admin);
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Пароль слишком короткий!");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Thread.Sleep(1500);
                    ClearTransList(admin);
                }
            }
        }
    }
}