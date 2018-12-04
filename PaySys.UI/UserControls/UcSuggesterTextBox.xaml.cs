using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PaySys.UI.UserControls
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
			AutoTextBox.TextChanged += autoTextBox_TextChanged;
			AutoTextBox.PreviewKeyDown += autoTextBox_PreviewKeyDown;
			SuggestionListBox.SelectionChanged += suggestionListBox_SelectionChanged;
			PreviewKeyUp += Uc_PreviewKeyUp;
		}


		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the items source.
		/// </summary>
		/// <value>The items source.</value>
		public IEnumerable<string> ItemsSource
		{
			get => (IEnumerable<string>) GetValue(ItemsSourceProperty);
		    set => SetValue(ItemsSourceProperty, value);
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
			get => (string) GetValue(SelectedValueProperty);
		    set => SetValue(SelectedValueProperty, value);
		}

		// Using a DependencyProperty as the backing store for SelectedValue.  
		// This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(string), typeof(UcSuggesterTextBox), new UIPropertyMetadata(string.Empty));

		public ItemCollection Items => SuggestionListBox.Items;

		#endregion

		#region Methods
		private void Uc_PreviewKeyUp(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Escape)
            {
                // DiscardChanges the selection
                SuggestionListBox.ItemsSource = null;
                CollapseList();
            }
            else if(e.Key == Key.Enter || e.Key == Key.Tab)
			{
				// Commit the selection
				CollapseList();
				e.Handled = e.Key == Key.Enter;
			}
		}

        public void CollapseList()
        {
            SuggestionListBox.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Handles the TextChanged event of the autoTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void autoTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			// Only autocomplete when there is text
			if(AutoTextBox.Text.Length > 0)
			{
				// Use Linq to Query ItemsSource for resultdata
				var condition = string.Format("{0}%", AutoTextBox.Text);
				var results = ItemsSource?.Where(s => s.ToLower().Contains(AutoTextBox.Text.Trim().ToLower()));
				if( results == null )
					return;

				var itemsSource = results.ToList();
				if(itemsSource.Any())
                {
                    SuggestionListBox.ItemsSource = itemsSource;
                    ExpandList();
                    SuggestionListBox.SelectedIndex = -1;
                }
                else
				{
					CollapseList();
					SuggestionListBox.ItemsSource = null;
				}
			}
			else
			{
				CollapseList();
				SuggestionListBox.ItemsSource = null;
			}
		    
        }

        public void ExpandList()
        {
            SuggestionListBox.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the autoTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void autoTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Down)
			{
				if(SuggestionListBox.Visibility == Visibility.Visible && SuggestionListBox.SelectedIndex < SuggestionListBox.Items.Count)
				{
					SuggestionListBox.SelectedIndex = SuggestionListBox.SelectedIndex + 1;
				}
				else if(SuggestionListBox.Visibility != Visibility.Visible)
				{
					if(string.IsNullOrEmpty(AutoTextBox.Text))
					{
						SuggestionListBox.ItemsSource = ItemsSource;
						ExpandList();
					}
					else
					{
						autoTextBox_TextChanged(AutoTextBox, null);
					}
				}
			}
			else if(e.Key == Key.Up)
			{
				if(SuggestionListBox.Visibility == Visibility.Visible && SuggestionListBox.SelectedIndex > 0) SuggestionListBox.SelectedIndex = SuggestionListBox.SelectedIndex - 1;
			}
			/*if(e.Key!=Key.Back &&  suggestionListBox.Items.Count == 0 && !string.IsNullOrEmpty(autoTextBox.Text))
				e.Handled = true;*/ else if(string.IsNullOrEmpty(AutoTextBox.Text) && e.Key == Key.Space)
			{
			    e.Handled = true;
			}

        }

		/// <summary>
		/// Handles the SelectionChanged event of the suggestionListBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void suggestionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(SuggestionListBox.ItemsSource != null)
			{
				AutoTextBox.TextChanged -= autoTextBox_TextChanged;
				if(SuggestionListBox.SelectedIndex != -1) AutoTextBox.Text = SuggestionListBox.SelectedItem.ToString();
			    AutoTextBox.TextChanged += autoTextBox_TextChanged;
			}
		}

		#endregion


	    private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
	    {
	        CollapseList();
        }


	}
}