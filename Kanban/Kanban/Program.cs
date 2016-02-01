using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new KanbanEntities())
            {
                foreach (var list in db.Lists)
                {
                    Console.WriteLine(list.Name);
                    foreach (var card in db.Cards)
                    {
                        if (card.ListID == list.ListID)
                        {
                            Console.WriteLine("\t" + card.Text);
                        }
                    }
                }

                try
                {
                    while (true) {

                        Console.WriteLine("Do you want to create a New List, a New Card, Delete a Card, or Delete a List?");
                        string choice = Console.ReadLine();
                        if (choice.ToLower() == "new list")
                        {
                            Console.WriteLine("Please enter the name of the new list.");
                            string listname = Console.ReadLine();
                            var newie = db.Set<List>();
                            newie.Add(new List { Name = listname, CreatedDate = DateTime.Now });
                            db.SaveChanges();
                            Console.WriteLine(listname + " added!");
                            break;
                        }

                        else if (choice.ToLower() == "new card")
                        {
                            int listIDadd = 0;

                            Console.WriteLine("Please enter the name of the List you would like to add your card to.");
                            string listnameadd = Console.ReadLine();

                            var listAdd = db.Lists.Where(u => u.Name == listnameadd);
                            foreach (var u in listAdd)
                            {
                                listIDadd = u.ListID;
                            }


                            Console.WriteLine("Please enter the card information to be added to this list.");
                            string cardinfo = Console.ReadLine();
                            var newie = db.Set<Card>();
                            newie.Add(new Card { ListID = listIDadd, CreatedDate = DateTime.Now, Text = cardinfo });
                            db.SaveChanges();
                            Console.WriteLine(cardinfo + " added to " + listnameadd);
                            break;
                        }

                        else if (choice.ToLower() == "delete a card")
                        {
                            Console.WriteLine("Please enter the name of the Card you would like to delete.");
                            string cardDeleteInfo = Console.ReadLine();

                            var cardDelete = db.Cards.Where(u => u.Text == cardDeleteInfo);

                            foreach (var u in cardDelete)
                            {
                                db.Cards.Remove(u);
                            }

                            db.SaveChanges();
                            break;
                        }

                        else if (choice.ToLower() == "delete a list")
                        {
                            Console.WriteLine("Please enter the name of the List you would like to delete.");
                            string listDeleteInfo = Console.ReadLine();

                            var listDelete = db.Lists.Where(u => u.Name == listDeleteInfo);

                            foreach (var u in listDelete)
                            {
                                db.Lists.Remove(u);
                            }

                            db.SaveChanges();
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Entry not understood.");
                        }
                    }



                    Console.ReadLine();

                }
                catch
                {
                    Console.WriteLine("Please enter the correct name of the list or card.");
                    Console.ReadLine();
                }

            }
        }
    }
}
