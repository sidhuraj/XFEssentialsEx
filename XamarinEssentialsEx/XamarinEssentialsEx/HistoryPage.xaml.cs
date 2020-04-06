using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinEssentialsEx
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage(List<History> objHistoryList)
        {
            InitializeComponent();
            lbxHistory.ItemsSource = objHistoryList;
        }
    }
}