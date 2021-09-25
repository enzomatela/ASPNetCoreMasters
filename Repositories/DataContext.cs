using DomainModel;
using System.Collections.Generic;

namespace Repositories
{
    public class DataContext
    {
        private List<Item> DummyData = new List<Item>() { new Item {  ItemId = 0, Text ="Sample1" },
                                                          new Item {  ItemId = 1, Text ="Sample2" },
                                                          new Item {  ItemId = 2, Text ="Sample3" }
                                                        };
        public List<Item> Items { get { return DummyData; }
                                  set { 
                DummyData = value; 
            }
        }
    }
}
