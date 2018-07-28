using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PaySys.UI.UC
{
	/// <summary>
	///  SelectedValue="{Binding Path=[property of type string]}"
	///  ItemsSource="{Binding Path=[list of type string]}"
	/// </summary>
	public partial class UcSuggesterTextBox : UserControl
	{
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="UcSuggesterTextBox"/> class.
		/// </summary>
		public UcSuggesterTextBox()
		{
			InitializeComponent();

			// Attach events to the controls
			autoTextBox.TextChanged += autoTextBox_TextChanged;
			autoTextBox.PreviewKeyDown += autoTextBox_PreviewKeyDown;
			suggestionListBox.SelectionChanged += suggestionListBox_SelectionChanged;
			this.PreviewKeyUp += Uc_PreviewKeyUp;
		}


		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the items source.
		/// </summary>
		/// <value>The items source.</value>
		public IEnumerable<string> ItemsSource
		{
			get
			{
				return (IEnumerable<string>) GetValue(ItemsSourceProperty);
			}
			set
			{
				SetValue(ItemsSourceProperty, value);
			}
		}

		// Using a DependencyProperty as the backing store for ItemsSource.  
		// This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable<string>), typeof(UcSuggesterTextBox), new UIPropertyMetadata(null));

		/// <summary>
		/// Gets or sets the selected value.
		/// </summary>
		/// <value>The selected value.</value>
		public string SelectedValue
		{
			get
			{
				return (string) GetValue(SelectedValueProperty);
			}
			set
			{
				SetValue(SelectedValueProperty, value);
			}
		}

		// Using a DependencyProperty as the backing store for SelectedValue.  
		// This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(string), typeof(UcSuggesterTextBox), new UIPropertyMetadata(string.Empty));

		public ItemCollection Items => suggestionListBox.Items;

		#endregion

		#region Methods
		private void Uc_PreviewKeyUp(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Escape)
			{
				// DiscardChanges the selection
				suggestionListBox.ItemsSource = null;
				suggestionListBox.Visibility = Visibility.Collapsed;
			}
			else if(e.Key == Key.Enter || e.Key == Key.Tab)
			{
				// Commit the selection
				suggestionListBox.Visibility = Visibility.Collapsed;
				e.Handled = (e.Key == Key.Enter);
			}
		}

		/// <summary>
		/// Handles the TextChanged event of the autoTextBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The instance containing the event data.</param>
		void autoTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			// Only autocomplete when there is text
			if(autoTextBox.Text.Length > 0)
			{
				// Use Linq to Query ItemsSource for resultdata
				string condition = string.Format("{0}%", autoTextBox.Text);
				IEnumerable<string> results = ItemsSource.Where(s => s.ToLower().Contains(autoTextBox.Text.Trim().ToLower()));
				if(results.Count() > 0)
				{
					suggestionListBox.ItemsSource = results;
					suggestionListBox.Visibility = Visibility.Visible;
					suggestionListBox.SelectedIndex = -1;
				}
				else
				{
					suggestionListBox.Visibility = Visibility.Collapsed;
					suggestionListBox.ItemsSource = null;
				}
			}
			else
			{
				suggestionListBox.Visibility = Visibility.Collapsed;
				suggestionListBox.ItemsSource = null;
			}
		}

		/// <summary>
		/// Handles the PreviewKeyDown event of the autoTextBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The instance containing the event data.</param>
		void autoTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Down)
			{
				if(suggestionListBox.Visibility == Visibility.Visible && suggestionListBox.SelectedIndex < suggestionListBox.Items.Count)
				{
					suggestionListBox.SelectedIndex = suggestionListBox.SelectedIndex + 1;
				}
				else if(suggestionListBox.Visibility != Visibility.Visible)
				{
					if(string.IsNullOrEmpty(autoTextBox.Text))
					{
						suggestionListBox.ItemsSource = ItemsSource;
						suggestionListBox.Visibility = Visibility.Visible;
					}
					else
					{
						autoTextBox_TextChanged(autoTextBox, null);
					}
				}
			}
			else if(e.Key == Key.Up)
			{
				if(suggestionListBox.Visibility == Visibility.Visible && suggestionListBox.SelectedIndex > 0)
				{
					suggestionListBox.SelectedIndex = suggestionListBox.SelectedIndex - 1;
				}
			}
			/*if(e.Key!=Key.Back &&  suggestionListBox.Items.Count == 0 && !string.IsNullOrEmpty(autoTextBox.Text))
				e.Handled = true;*/ else if(string.IsNullOrEmpty(autoTextBox.Text) && e.Key == Key.Space)
				e.Handled = true;
		}

		/// <summary>
		/// Handles the SelectionChanged event of the suggestionListBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void suggestionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(suggestionListBox.ItemsSource != null)
			{
				autoTextBox.TextChanged -= autoTextBox_TextChanged;
				if(suggestionListBox.SelectedIndex != -1)
				{
					autoTextBox.Text = suggestionListBox.SelectedItem.ToString();
				}
				autoTextBox.TextChanged += autoTextBox_TextChanged;
			}
		}

		#endregion
	}
}