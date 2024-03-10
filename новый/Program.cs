using System;
using System.Collections.Generic;

namespace новый
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddPlayers = "1";
            const string CommandDeletePlayers = "2";
            const string CommandBanPlayers = "3";
            const string CommandUnbanPlayers = "4";
            const string CommandShowPlayers = "5";
            const string CommandExit = "6";

            //ConsoleUtils.ReadInt("Введите крокодилов: ");

            DataBaseController controller = new DataBaseController();



            bool isWorking = true;

            while (isWorking)
            {


                Console.WriteLine($"Добавить игрока: {CommandAddPlayers}\n" +
                    $"Удалить игрока: {CommandDeletePlayers}\n" +
                    $"Забанить игрока: {CommandBanPlayers}\n" +
                    $"разбанить игрока: {CommandUnbanPlayers}\n" +
                    $"Посмотреть базу данных: {CommandShowPlayers}\n" +
                    $"Выйти из прогрыммы: {CommandExit}");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddPlayers:

                        break;
                    case CommandDeletePlayers:
                        controller.DeletePlayer();
                        break;
                    case CommandBanPlayers:
                        break;

                    case CommandUnbanPlayers:
                        break;

                    case CommandShowPlayers:
                        break;

                    case CommandExit:
                        isWorking = false;
                        break;
                }

            }
        }
    }

    class DataBase
    {
        private Dictionary<int, Player> _players = new Dictionary<int, Player> { };

        private int _indexCounter = 0;

        public IReadOnlyDictionary<int, Player> Players { get { return _players; } }

        public void AddPlayer(Player player)
        {
            _indexCounter++;
            _players.Add(_indexCounter, player);
        }

        public bool TryDeletePlayer(int playerId)
        {
            if (_players.ContainsKey(playerId))
            {
                _players.Remove(playerId);
                return true;
            }

            return false;
        }

        public bool TryBan(int playerId)
        {
            if (_players.ContainsKey(playerId))
            {
                _players[playerId].Ban();
                return true;
            }

            return false;
        }

        public bool TryUnban(int playerId)
        {
            if (_players.ContainsKey(playerId))
            {
                _players[playerId].Unban();
                return true;
            }

            return false;
        }
    }

    class DataBaseController
    {
        private DataBase _dataBase = new DataBase();

        public void ShowInfo(Player player)
        {
            for (int i = 0; i < _dataBase.Players.Count; i++)
            {
                Console.WriteLine($"Имя : {player.Name} " +
              $"Уровень: {player.LevelPlayer}" +
              $"Номер: {player.Number}" +
              $"Статус: {player.IsBanned}");

                Console.WriteLine();
            }
        }

        public void AddPlayer()
        {
            string name;
            int number;
            int levelPlayer;
            bool isBanned = true;

            Console.WriteLine("Введите имя игрока: ");
            name = Console.ReadLine();

            Console.WriteLine("Введите номер игрока: ");
            number = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите уровень игрока: ");
            levelPlayer = int.Parse(Console.ReadLine());

            Player players = new Player(name, number, levelPlayer, isBanned);

            _dataBase.AddPlayer(players);
        }

        public void DeletePlayer()
        {
            int userInput;
            int indexPlayer = 0;

            Console.WriteLine("Укажите индекс игрока для удаления: ");

            do
            {
                userInput = int.Parse(Console.ReadLine());
                indexPlayer++;

            } while (indexPlayer != userInput);

            if (_dataBase.TryDeletePlayer(userInput))
            {
                Console.WriteLine("Игрок удален");

            }
            else
            {
                Console.WriteLine("Игрок не удален");
            }


        }

        public void BanPlayer(Player player)
        {
            int playerIndex;

            Console.WriteLine("Укажите индекс игрока для бана: ");

            bool specifyStatus = int.TryParse(Console.ReadLine(), out playerIndex);

            if (specifyStatus)
            {
                Console.WriteLine("Игрок забанен");
                _dataBase.TryBan(playerIndex);
            }
            else
            {
                Console.WriteLine("Игрок все еще разбанен");
            }
        }

        public void UnbanPlayer(Player player)
        {
            int playerIndex;

            Console.WriteLine("Укажите индекс игрока для разбана: ");

            bool specifyStatus = int.TryParse(Console.ReadLine(), out playerIndex);

            if (specifyStatus)
            {
                Console.WriteLine("Игрок разбанен");
                _dataBase.TryUnban(playerIndex);
            }
            else
            {
                Console.WriteLine("Игрок все еще забанен");

            }
        }
    }
}

public class Player
{
    private string _name;
    private int _levelPlayer;
    private bool _isBanned;

    public Player(string name, int number, int levelPlayer, bool status)
    {
        Number = number;
        _isBanned = status;
        _name = name;
        _levelPlayer = levelPlayer;
    }

    public string Name { get { return _name; } }

    public int LevelPlayer { get => _levelPlayer; }

    public int Number { get; }

    public bool IsBanned => _isBanned;

    public void Ban()
    {
        _isBanned = true;
    }

    public void Unban()
    {
        _isBanned = false;
    }
}

public static class ConsoleUtils
{
    public static int ReadInt(string message = "Введите целое число: ")
    {
        int result;
        bool success;

        do
        {
            Console.WriteLine(message);
            string userInput = Console.ReadLine();

            success = int.TryParse(userInput, out result);

            if (success == false)
            {
                Console.WriteLine("Неудача. Ожидалось целое число, попробуйте ещё раз");
            }

        } while (success == false);

        return result;
    }
}
