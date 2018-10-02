﻿using System;
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
using PaySys.ModelAndBindLib.Entities;
using PaySys.UI.ExtensionMethods;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace PaySys.UI.UC.Tab
{
	/// <summary>
	/// Interaction logic for UcCityMng.xaml
	/// </summary>
	public partial class UcCityMng : UserControl
	{
		public PaySysContext Context { get; set; }

		public ObservableCollection<City> Cities { get; set; }

		public UcCityMng()
		{
			InitializeComponent();
			BtnReload_OnClick(null, null);
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Add;
			var newItem = new City
			{
				Title = ResourceAccessor.Labels.GetString("New")
			};
			Cities.Add(newItem);
			DataGridCities.SelectedItem = newItem;
			DataGridCities.ScrollIntoView(newItem);
		    TextBoxTitle.Focus();
		}

		private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
		{
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Edit;
		    TextBoxTitle.Focus();
        }

        private void BtnReload_OnClick(object sender, RoutedEventArgs e)
		{
			var selectedId = (DataGridCities.SelectedItem as City)?.Id;
			Context = new PaySysContext();
			Context.Cities.Load();
			Cities = Context.Cities.Local;
			DataGridCities.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
			DataGridCities.GetBindingExpression(DataContextProperty)?.UpdateTarget();
			if(selectedId.HasValue)
				DataGridCities.SelectedItem = Cities.FirstOrDefault(city => city.Id == selectedId.Value);
		}

		private void BtnSave_OnClick(object sender, RoutedEventArgs e)
		{
			foreach(var textBox in this.FindVisualChildren<TextBox>())
				textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

			Context.SaveChanges();
			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
            CollectionViewSource.GetDefaultView(DataGridCities.ItemsSource)?.Refresh();
		}

		private void BtnDiscardChanges_OnClick(object sender, RoutedEventArgs e)
		{
			if (SmpUcFormStateLabel.CurrentState==FormCurrentState.Add)
			Cities.Remove((City) DataGridCities.SelectedItem);
			foreach(var textBox in this.FindVisualChildren<TextBox>())
				textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();

			SmpUcFormStateLabel.CurrentState = FormCurrentState.Select;
		}
	}
}