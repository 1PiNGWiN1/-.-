using System;
using System.Collections.Generic;

namespace CSharpLight
{
    class Program
    {
        static void Main(string[] args)
        {
            const string TakeCardCommand = "1";
            const string ShowCardsCommand = "2";
            const string StopGameCommand = "3";

            Pack pack = new Pack();
            Player player = new Player();

            bool isWorking = true;

            pack.CreateCards();
            pack.ShuffleCards();

            while (isWorking)
            {
                Console.WriteLine("=== Игра ===");
                Console.WriteLine($"Приветствуем вас! Давайте сыграем в игру вытяни карту и покажи\n" +
                $"\n{TakeCardCommand}:Взять карту.\n" +
                $"{ShowCardsCommand}: Показать карты в руке.\n" +
                $"{StopGameCommand}: Выход.\n");

                switch (Console.ReadLine())
                {
                    case TakeCardCommand:
                        player.TakeCard(pack.GiveCard());
                        break;

                    case ShowCardsCommand:
                        player.ShowCards();
                        break;

                    case StopGameCommand:
                        isWorking = false;
                        break;

                    default:
                        Console.WriteLine("Ошибка ввода!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Card
    {
        public string Suit { get; private set; }
        public string Name { get; private set; }

        public Card(string suit, string name)
        {
            Suit = suit;
            Name = name;
        }
    }

    class Pack
    {
        private List<Card> _cards = new List<Card>();

        public void CreateCards()
        {
            string[] suits = { "♣", "♠", "♥", "♦" };
            string[] names = { "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < names.Length; j++)
                {
                    Card card = new Card(suits[i], names[j]);
                    _cards.Add(card);
                }
            }
        }

        public void ShuffleCards()
        {
            Random random = new Random();
            int index = 0;
            Card temporaryCard;

            for (int i = 0; i < _cards.Count; i++)
            {
                index = random.Next(_cards.Count);
                temporaryCard = _cards[index];

                _cards[index] = _cards[i];
                _cards[i] = temporaryCard;
            }
        }

        public Card GiveCard()
        {
            Card card = null;

            if (_cards.Count > 0)
            {
                card = _cards[0];
                _cards.Remove(card);
            }
            else
            {
                Console.WriteLine("Карт в колоде нет!");
            }

            return card;
        }
    }

    class Player
    {
        private List<Card> _cardsInHand = new List<Card>();

        public void TakeCard(Card card)
        {
            _cardsInHand.Add(card);
        }

        public void ShowCards()
        {
            foreach (Card card in _cardsInHand)
            {
                Console.WriteLine($"{card.Suit}{card.Name} ");
            }
        }
    }
}