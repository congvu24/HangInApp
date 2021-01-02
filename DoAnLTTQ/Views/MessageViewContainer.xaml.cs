using System.ComponentModel;
using System.Windows.Controls;
using DoAnLTTQ.Components;

namespace DoAnLTTQ.Views
{
    /// <summary>
    /// Interaction logic for MessageViewContainer.xaml
    /// </summary>
    public partial class MessageViewContainer : UserControl
    {
       
        public UserControl _ViewContext;
        public UserControl md = new MessageView_MessageDetails();
        public event PropertyChangedEventHandler PropertyChanged;

        public UserControl ViewContext
        {
            get { return this._ViewContext; }
            set
            {
                _ViewContext = value;
                OnPropertyChanged("ViewContext");
            }
        }
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        public MessageViewContainer()
        {
            InitializeComponent();
            this.ViewContext = new MessageView_MessageDetails();
        }
    }
}
