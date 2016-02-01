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
                Console.ReadLine();
            }
        }
    }
}
