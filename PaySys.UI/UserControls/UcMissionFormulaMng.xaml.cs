using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PaySys.CalcLib.Delegates;
using PaySys.ModelAndBindLib;
using PaySys.ModelAndBindLib.Model;

namespace PaySys.UI.UC
{
    /// <summary>
    /// Interaction logic for UcMissionFormulaMng.xaml
    /// </summary>
    public partial class UcMissionFormulaMng : UserControl
    {
        public UcMissionFormulaMng()
        {
            InitializeComponent();
            ListViewMissionFormulaInvolvedContractFields.Items.SortDescriptions.Add(
                new SortDescription("Key.Title", ListSortDirection.Ascending));
        }

        public static readonly DependencyProperty CurrentSubGroupProperty =
            DependencyProperty.Register("CurrentSubGroup", typeof(SubGroup), typeof(UcMissionFormulaMng),
                new PropertyMetadata(default(SubGroup)));

        public static readonly DependencyProperty ReadOnlyOfFieldsProperty =
            DependencyProperty.Register("ReadOnlyOfFields", typeof(bool), typeof(UcMissionFormulaMng),
                new PropertyMetadata(default(bool)));

        public SubGroup CurrentSubGroup
        {
            get => (SubGroup) GetValue(CurrentSubGroupProperty);
            set => SetValue(CurrentSubGroupProperty, value);
        }

        public bool ReadOnlyOfFields
        {
            get { return (bool) GetValue(ReadOnlyOfFieldsProperty); }
            set { SetValue(ReadOnlyOfFieldsProperty, value); }
        }

        private void ContractField_Checked(object sender, RoutedEventArgs e)
        {
            CurrentSubGroup.CurrentOrNewMissionFormula.TempMissionFormulaInvolvedContractFieldsLeftJoined[
                (e.Source as System.Windows.Controls.Control)?.Tag as ContractField] = true;
        }

        private void ContractField_UnChecked(object sender, RoutedEventArgs e)
        {
            CurrentSubGroup.CurrentOrNewMissionFormula.TempMissionFormulaInvolvedContractFieldsLeftJoined[
                (e.Source as System.Windows.Controls.Control)?.Tag as ContractField] = false;
        }
    }
}