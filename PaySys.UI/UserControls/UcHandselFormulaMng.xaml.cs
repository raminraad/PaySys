using System.Windows;
using System.Windows.Controls;
using PaySys.Model.Entities;

namespace PaySys.UI.UserControls
{
	/// <summary>
	/// Interaction logic for UcHandselFormulaMng.xaml
	/// </summary>
	public partial class UcHandselFormulaMng : UserControl
	{
		public UcHandselFormulaMng()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty CurrentSubGroupProperty = DependencyProperty.Register("CurrentSubGroup", typeof(SubGroup), typeof(UcHandselFormulaMng), new PropertyMetadata(default(SubGroup)));
	    public static readonly DependencyProperty ReadOnlyOfFieldsProperty = DependencyProperty.Register("ReadOnlyOfFields", typeof(bool), typeof(UcHandselFormulaMng), new PropertyMetadata(default(bool)));

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
	}
}