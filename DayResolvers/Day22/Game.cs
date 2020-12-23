using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020_1.DayResolvers.Day22
{
    public class Game
    {
        public List<Player> Players = new List<Player>();
        public Tuple<List<string>, List<string>> Configurations = new Tuple<List<string>, List<string>>(new List<string>(), new List<string>());

        public Game(StreamReader input)
        {
            string line = input.ReadLine();
            List<string> lines = new List<string>();
            while (line != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    Players.Add(new Player(lines));
                    lines.Clear();
                }
                else
                {
                    lines.Add(line);
                }

                line = input.ReadLine();
            }

            Players.Add(new Player(lines));
        }

        public long Play()
        {
            while (Players[0].Queue.Count != 0 && Players[1].Queue.Count != 0)
            {
                Round();
            }

            return Players[0].Queue.Count != 0 ? CountScore(Players[0]) : CountScore(Players[1]);
        }

        public long Play2() =>  Play2Internal(Players[0].Queue, Players[1].Queue);

        private long Play2Internal(Queue<int> queue1, Queue<int> queue2)
        {
            while (queue1.Count != 0 && queue2.Count != 0)
            {
                Round2(queue1, queue2);
            }

            return queue1.Count != 0 ? CountScore2(queue1) : CountScore2(queue2);
        }


        private long CountScore(Player player)
        {
            long sum = 0;
            int count = player.Queue.Count;

            while (count != 0)
            {
                sum += count * player.Queue.Dequeue();
                count--;
            }

            return sum;
        }

        private long CountScore2(Queue<int> queue)
        {
            long sum = 0;
            int count = queue.Count;

            while (count != 0)
            {
                sum += count * queue.Dequeue();
                count--;
            }

            return sum;
        }

        private void Round()
        {
            int player1 = Players[0].Queue.Dequeue();
            int player2 = Players[1].Queue.Dequeue();

            if (player1 > player2)
            {
                Players[0].Queue.Enqueue(player1);
                Players[0].Queue.Enqueue(player2);
            }
            else
            {
                Players[1].Queue.Enqueue(player2);
                Players[1].Queue.Enqueue(player1);
            }
        }

        private bool Round2(Queue<int> queue1, Queue<int> queue2)
        {
            if (ConfigurationExists(queue1, queue2))
            {
                return false;
            }
            int player1 = queue1.Dequeue();
            int player2 = queue2.Dequeue();

            if (player1 <= queue1.Count && player2 <= queue2.Count)
            {
                //subgame
                var queue1Clone = new Queue<int>(queue1);
                var queue2Clone = new Queue<int>(queue1);
                Play2();

            }
            else
            {
                //normal game
                if (player1 > player2)
                {
                    queue1.Enqueue(player1);
                    queue1.Enqueue(player2);
                }
                else
                {
                    queue2.Enqueue(player2);
                    queue2.Enqueue(player1);
                }
            }

            return true;
        }

        private bool ConfigurationExists(Queue<int> queue1, Queue<int> queue2)
        {
            return false;
        }
    }
}
