using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bogus;
using PaySys.Globalization;
using PaySys.ModelAndBindLib.Engine;
using PaySys.ModelAndBindLib.Model;
using PaySys.UI.UC;
using ListView = System.Windows.Controls.ListView;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC
{
	/// <summary>
	/// Interaction logic for UcGroupMng.xaml
	/// </summary>
	public partial class UcGroupMng : UserControl
	{
		private enum GroupType
		{
			None,
			MainGroup,
			SubGroup
		}

		private GroupType LastFocusedList = GroupType.None;
		private PaySysContext _context = new PaySysContext();
		public UcGroupMng()
		{
			InitializeComponent();
			ListViewGroupMain.DataContext = _context.MainGroups.ToList();

			SmpUcContractFieldTitlesMng.SaveContext += () => _context.SaveChanges();
			SmpUcMiscMng.SaveContext += () => _context.SaveChanges();
			SmpUcParameterMng.SaveContext += () => _context.SaveChanges();
			SmpUcTaxTableMng.SaveContext += () => _context.SaveChanges();
			SmpUcHandselFormula.SaveContext += () => _context.SaveChanges();
			SmpUcMissionFormulaMng.SaveContext += () => _context.SaveChanges();

			SmpUcContractFieldTitlesMng.ExpenseArticlesAll = _context.ExpenseArticles.ToList();
			SmpUcMiscMng.ExpenseArticlesAll = _context.ExpenseArticles.ToList();
		}

		private void BtnAddMainGroup_OnClick(object sender, RoutedEventArgs e)
		{
			var title = string.Empty;
			var randomColor = new Faker().PickRandom(Enum.GetValues(typeof(ColorPallet)).Cast<ColorPallet>()
				.Where(color => color != ColorPallet.Unknown));
			if (InputBox.Show(ResourceAccessor.Messages.GetString("EnterMainGroupName"), ref title) == DialogResult.OK)
			{
				_context.MainGroups.Add(new MainGroup
				{
					Title = title,
					ItemColor = randomColor,
					SubGroups = new List<SubGroup>()
				});
				_context.SaveChanges();
				ListViewGroupMain.DataContext = _context.MainGroups.ToList();
			}
		}

		private void ListViewGroupMain_OnGotFocus(object sender, RoutedEventArgs e)
		{
			LastFocusedList = GroupType.MainGroup;
		}

		private void ListViewSubGroup_OnGotFocus(object sender, RoutedEventArgs e)
		{
			LastFocusedList = GroupType.SubGroup;
		}

		private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
		{
			var title = string.Empty;
			
			switch (LastFocusedList)
			{
				case GroupType.MainGroup:
					var selectedMainGroup = (MainGroup)ListViewGroupMain.SelectedItem;
					title = selectedMainGroup.Title;
					if (InputBox.Show(ResourceAccessor.Messages.GetString("EnterMainGroupName"), ref title) == DialogResult.OK)
					{
						selectedMainGroup.Title = title;
						_context.SaveChanges();
						ListViewGroupMain.DataContext = _context.MainGroups.ToList();
					}
					break;
				case GroupType.SubGroup:
					var selectedSubGroup = (SubGroup)ListViewSubGroup.SelectedItem;
					title = selectedSubGroup.Title;
					if (InputBox.Show(ResourceAccessor.Messages.GetString("EnterSubGroupName"), ref title) == DialogResult.OK)
					{
						selectedSubGroup.Title = title;
						_context.SaveChanges();
						CollectionViewSource.GetDefaultView(ListViewSubGroup.ItemsSource).Refresh();
					}
					break;
			}
		}

		private void BtnRefresh_OnClick(object sender, RoutedEventArgs e)
		{
						_context = new PaySysContext();
						ListViewGroupMain.DataContext = _context.MainGroups.ToList();
		}

		private void BtnAddSubGroup_OnClick(object sender, RoutedEventArgs e)
		{
			var title = string.Empty;
			if (InputBox.Show(ResourceAccessor.Messages.GetString("EnterSubGroupName"), ref title) == DialogResult.OK)
			{
				var selectedMainGroup = (MainGroup)ListViewGroupMain.SelectedItem;
				selectedMainGroup.SubGroups.Add(new SubGroup
				{
					Title = title,
					ItemColor = selectedMainGroup.ItemColor
				});
				_context.SaveChanges();
				CollectionViewSource.GetDefaultView(ListViewSubGroup.ItemsSource).Refresh();
			}
		}
	}
}