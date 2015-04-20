using System;
using System.Collections.Generic;

namespace HowMetYourPattern.Core.Structural
{

    // Proxy Pattern Example Judith Bishop Aug 2007
    // Sets up a SpaceBook page with registration and authentication
    public class SpaceBookSystem
    {
        // The Subject
        private class SpaceBook
        {
            private static readonly SortedList<string, SpaceBook> Community = new SortedList<string, SpaceBook>(100);

            private string _pages;
            private readonly string _name;
            private const string Gap = "\n\t\t\t\t";

            public static bool IsUnique(string name)
            {
                return Community.ContainsKey(name);
            }

            internal SpaceBook(string n)
            {
                _name = n;
                Community[n] = this;
            }

            internal void Add(string s)
            {
                _pages += Gap + s;
                Console.Write(Gap + "======== " + _name + "'s SpaceBook =========");
                Console.Write(_pages);
                Console.WriteLine(Gap + "===================================");
            }

            internal void Add(string friend, string message)
            {
                Community[friend].Add(message);
            }

            internal void Poke(string who, string friend)
            {
                Community[who]._pages += Gap + friend + " poked you";
            }
        }

        // The Proxy
        public class MySpaceBook
        {
            // Combination of a virtual and authentication proxy
            private SpaceBook mySpaceBook;
            private string password;
            private string name;
            private bool loggedIn = false;

            private void Register()
            {
                Console.WriteLine("Let's register you for SpaceBook");
                do
                {
                    Console.WriteLine("All SpaceBook names must be unique");
                    Console.Write("Type in a user name: ");
                    name = Console.ReadLine();
                } while (SpaceBook.IsUnique(name));

                Console.Write("Type in a password: ");
                password = Console.ReadLine();
                Console.WriteLine("Thanks for registering with SpaceBook");
            }

            bool Authenticate()
            {
                Console.Write("Welcome " + name + ". Please type in your password: ");
                var supplied = Console.ReadLine();
                if (supplied == password)
                {
                    loggedIn = true;
                    Console.WriteLine("Logged into SpaceBook");
                    if (mySpaceBook == null)
                        mySpaceBook = new SpaceBook(name);
                    return true;
                }

                Console.WriteLine("Incorrect password");

                return false;
            }

            public void Add(string message)
            {
                Check();
                if (loggedIn) mySpaceBook.Add(message);
            }

            public void Add(string friend, string message)
            {
                Check();
                if (loggedIn)
                    mySpaceBook.Add(friend, name + " said: " + message);
            }

            public void Poke(string who)
            {
                Check();
                if (loggedIn)
                    mySpaceBook.Poke(who, name);
            }

            void Check()
            {
                if (!loggedIn)
                {
                    if (password == null)
                        Register();
                    if (mySpaceBook == null)
                        Authenticate();
                }
            }
        }

    }

    // The Client
    class ProxyPattern : SpaceBookSystem
    {
        private static void Main()
        {
            var me = new MySpaceBook();
            me.Add("Hello world");
            me.Add("Today I worked 18 hours");

            var tom = new MySpaceBook();
            tom.Poke("Judith");
            tom.Add("Judith", "Poor you");
            tom.Add("Off to see the Lion King tonight");
        }
    }

}