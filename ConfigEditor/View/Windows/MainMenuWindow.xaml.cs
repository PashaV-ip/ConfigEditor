using ConfigEditor.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace ConfigEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            (DataContext as MainMenuViewModel).StackPanelConfigsList = StackPanelConfigsList;
            (DataContext as MainMenuViewModel).StackPanelSelectedPropertiesList = StackPanelSelectedProperties;
            (DataContext as MainMenuViewModel).StackPanelFilesPropertiesList = StackPanelFilesProperties;
            (DataContext as MainMenuViewModel).StackPanelParametersConfigList = StackPanelParameterList;
            (DataContext as MainMenuViewModel).StackPanelSlideMenu = StackPanelSlideMenu;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
