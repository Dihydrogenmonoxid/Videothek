using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Videothek.Logic.Ui.ViewModel;

namespace Videothek.Ui.Desktop
{
    /// <summary>
    /// Interaction logic for DeleteYesOrNo.xaml
    /// </summary>
    public partial class DeleteYesOrNo : Window
    {
        public DeleteYesOrNo()
        {
            InitializeComponent();
            Messenger.Default.Register<PropertyChangedMessage<EditResult>>(this, (PropertyChangedMessage<EditResult> e) =>
            {
                if (e.PropertyName.Equals("Delete"))
                {
                   
                }
            });
        }
    }
}
