using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using PaySys.ModelAndBindLib.Entities;

namespace PaySys.UI.UC
{
    /// <summary>
    ///     Interaction logic for UcMissionFormulaMng.xaml
    /// </summary>
    public partial class UcMissionFormulaMng : UserControl
    {
        public static readonly DependencyProperty CurrentSubGroupProperty =
            DependencyProperty.Register("CurrentSubGroup", typeof(SubGroup), typeof(UcMissionFormulaMng),
                new PropertyMetadata(default(SubGroup)));

        public static readonly DependencyProperty ReadOnlyOfFieldsProperty =
            DependencyProperty.Register("ReadOnlyOfFields", typeof(bool), typeof(UcMissionFormulaMng),
                new PropertyMetadata(default(bool)));

        public UcMissionFormulaMng()
        {
            InitializeComponent();
            ListViewMissionFormulaInvolvedContractFields.Items.SortDescriptions.Add(
                new SortDescription("Key.Title", ListSortDirection.Ascending));
        }

        public SubGroup CurrentSubGroup
        {
            get => (SubGroup) GetValue(CurrentSubGroupProperty);
            set => SetValue(CurrentSubGroupProperty, value);
        }

        public bool ReadOnlyOfFields
        {
            get => (bool) GetValue(ReadOnlyOfFieldsProperty);
            set => SetValue(ReadOnlyOfFieldsProperty, value);
        }

        private void ContractField_Checked(object sender, RoutedEventArgs e)
        {
            CurrentSubGroup.CurrentOrNewMissionFormula.TempMissionFormulaInvolvedContractFieldsLeftJoined[
                (e.Source as Control)?.Tag as ContractField] = true;
        }

        private void ContractField_UnChecked(object sender, RoutedEventArgs e)
        {
            CurrentSubGroup.CurrentOrNewMissionFormula.TempMissionFormulaInvolvedContractFieldsLeftJoined[
                (e.Source as Control)?.Tag as ContractField] = false;
        }
    }
}