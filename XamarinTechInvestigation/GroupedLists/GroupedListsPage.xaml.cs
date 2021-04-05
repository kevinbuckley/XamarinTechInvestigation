using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinTechInvestigation.GroupedLists
{
    public partial class GroupedListsPage : ContentPage
    {
        public List<AnimalGroup> Animals { get; private set; } = new List<AnimalGroup>();

        public GroupedListsPage()
        {
            InitializeComponent();
            this.BindingContext = new GroupedAnimalsViewModel(false);
        }
        
    }
}

