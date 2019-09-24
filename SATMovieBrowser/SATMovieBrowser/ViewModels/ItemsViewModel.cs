using SATMovieBrowser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SATMovieBrowser.ViewModels
{
    public class ItemsViewModel : INotifyPropertyChanged
    {
        public List<Items> _items { get; set; }
        public List<Items> items
        {
            get { return _items; }
            set { _items = value; }

        }
        public event PropertyChangedEventHandler PropertyChanged;

        public ItemsViewModel()
        {
            items = new List<Items>();
            items.Add(new Items { ID = 1, Name = "Raj", Author = "None", ImagePath = "Me.png" });
            items.Add(new Items { ID = 2, Name = "Amit", Author = "None", ImagePath = "Raz.jpg" });
            items.Add(new Items { ID = 3, Name = "Suresh", Author = "None", ImagePath = "raz1.jpg" });
            items.Add(new Items { ID = 4, Name = "Rahul", Author = "None", ImagePath = "w2.jpeg" });
            items.Add(new Items { ID = 5, Name = "Kabeer", Author = "None", ImagePath = "w3.jpeg" });
            items.Add(new Items { ID = 6, Name = "Manoj", Author = "None", ImagePath = "raz1.jpg" });

        }


    }
}
