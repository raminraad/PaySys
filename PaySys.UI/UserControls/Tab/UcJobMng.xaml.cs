using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;
using PaySys.UI.ExtensionMethods;

namespace PaySys.UI.UC
{
    /// <summary>
    /// Interaction logic for UcJobMng.xaml
    /// </summary>
    public partial class UcJobMng : UserControl
    {
		public PaySysContext Context { get; set; }

	    public ObservableCollection<Job> Jobs { get; set; }

	    public UcJobMng()
	    {
		    InitializeComponent();
		    BtnRefresh_OnClick(null, null);
		    SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
	    }

	    private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
	    {
		    SmpUcFormStateLabel.CurrentState = FormCurrentState.Add;
		    var newItem = new Job
		    {
			    Title = ResourceAccessor.Labels.GetString("New")
		    };
		    Jobs.Add(newItem);
		    ListViewJobs.SelectedItem = newItem;
		    ListViewJobs.ScrollIntoView(newItem);
	    }

	    private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
	    {
		    SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
	    }

	    private void BtnRefresh_OnClick(object sender, RoutedEventArgs e)
	    {
		    var selectedId = (ListViewJobs.SelectedItem as Job)?.JobId;
		    Context = new PaySysContext();
		    Context.Jobs.Load();
		    Jobs = Context.Jobs.Local;
		    ListViewJobs.GetBindingExpression(ItemsControl.ItemsSourceProperty).UpdateTarget();
		    if (selectedId.HasValue)
			    ListViewJobs.SelectedItem = Jobs.FirstOrDefault(Job => Job.JobId == selectedId.Value);
	    }

	    private void BtnSave_OnClick(object sender, RoutedEventArgs e)
	    {
		    foreach (var textBox in this.FindVisualChildren<TextBox>())
			    textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

		    Context.SaveChanges();
		    SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
	    }

	    private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
	    {
		    Jobs.Remove((Job)ListViewJobs.SelectedItem);
		    foreach (var textBox in this.FindVisualChildren<TextBox>())
			    textBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();

		    SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
	    }
	}
}
