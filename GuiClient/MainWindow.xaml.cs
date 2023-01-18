using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using DataAccess;
using DataAccess.Models;

namespace GuiClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRepository<Human> _peopleManager;
        public MainWindow(IRepository<Human> peopleManager)
        {
            InitializeComponent();
            _peopleManager = peopleManager;
            UpdatePeopleView();
        }
        
        private void People_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (People.SelectedItem is Human person)
            {
                FirstName.Text = person.FirstName;
                LastName.Text = person.LastName;
                Age.Text = $"{person.Age}";
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var person = new Human()
            {
                FirstName = FirstName.Text, 
                LastName = LastName.Text,
                Age = int.Parse(Age.Text)
            };

            _peopleManager.Add(person);
            UpdatePeopleView();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (People.SelectedItem is Human person)
            {
                if (!string.IsNullOrEmpty(FirstName.Text) && !string.IsNullOrEmpty(LastName.Text) && !string.IsNullOrEmpty(Age.Text))
                {
                    var newPerson = new Human()
                    {
                        FirstName = FirstName.Text,
                        LastName = LastName.Text,
                        Age = int.Parse(Age.Text)
                    };
                    _peopleManager.Replace(person.Id, newPerson);
                }

                UpdatePeopleView();
            }
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (People.SelectedItem is Human person)
            {
                _peopleManager.Delete(person.Id);
            }

            UpdatePeopleView();
        }

        private void UpdatePeopleView()
        {
            var people = _peopleManager.GetAll();
            People.Items.Clear();
            foreach (var person in people)
            {
                People.Items.Add(person);
            }

            ClearFields();
        }

        private void ClearFields()
        {
            FirstName.Text = string.Empty;
            LastName.Text = string.Empty;
            Age.Text = string.Empty;
        }
    }
}
