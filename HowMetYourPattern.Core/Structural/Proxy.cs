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
            private SpaceBook _mySpaceBook;
            private string _password;
            private string _name;
            private bool _loggedIn;

            private void Register()
            {
                Console.WriteLine("Let's register you for SpaceBook");
                do
                {
                    Console.WriteLine("All SpaceBook names must be unique");
                    Console.Write("Type in a user name: ");
                    _name = Console.ReadLine();
                } while (SpaceBook.IsUnique(_name));

                Console.Write("Type in a password: ");
                _password = Console.ReadLine();
                Console.WriteLine("Thanks for registering with SpaceBook");
            }

            void Authenticate()
            {
                Console.Write("Welcome " + _name + ". Please type in your password: ");
                var supplied = Console.ReadLine();
                if (supplied == _password)
                {
                    _loggedIn = true;
                    Console.WriteLine("Logged into SpaceBook");
                    if (_mySpaceBook == null)
                        _mySpaceBook = new SpaceBook(_name);
                    return;
                }

                Console.WriteLine("Incorrect password");
            }

            public void Add(string message)
            {
                Check();
                if (_loggedIn) _mySpaceBook.Add(message);
            }

            public void Add(string friend, string message)
            {
                Check();
                if (_loggedIn)
                    _mySpaceBook.Add(friend, _name + " said: " + message);
            }

            public void Poke(string who)
            {
                Check();
                if (_loggedIn)
                    _mySpaceBook.Poke(who, _name);
            }

            void Check()
            {
                if (!_loggedIn)
                {
                    if (_password == null)
                        Register();
                    if (_mySpaceBook == null)
                        Authenticate();
                }
            }
        }

    }

    // The Client
    public class ProxyPattern : SpaceBookSystem
    {
        public void Main()
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