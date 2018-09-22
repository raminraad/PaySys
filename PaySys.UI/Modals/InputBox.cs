using System;
using System.Windows.Forms;
using System.Drawing;
using PaySys.Globalization;

namespace PaySys.UI
{
	public static class InputBox
	{
		public static DialogResult Show(string title, string promptText, ref string value)
		{
			var form = new Form();
			var label = new Label();
			var textBox = new TextBox();
			var buttonOk = new Button();
			var buttonCancel = new Button();

			form.Text = title;
			form.RightToLeftLayout = true;
			form.RightToLeft=RightToLeft.Yes;
			label.Text = promptText;
			textBox.Text = value;

			buttonOk.Text = ResourceAccessor.Labels.GetString("Ok"); ;
			buttonCancel.Text = ResourceAccessor.Labels.GetString("DiscardChanges"); ;
			buttonOk.DialogResult = DialogResult.OK;
			buttonCancel.DialogResult = DialogResult.Cancel;

			label.SetBounds(9, 20, 372, 13);
			textBox.SetBounds(12, 42, 372, 20);
			buttonOk.SetBounds(228, 85, 75, 30);
			buttonCancel.SetBounds(309, 85, 75, 30);

			label.AutoSize = true;
			textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
			buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

			form.ClientSize = new Size(396, 120);
			form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
			form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
			form.FormBorderStyle = FormBorderStyle.FixedDialog;
			form.StartPosition = FormStartPosition.CenterScreen;
			form.MinimizeBox = false;
			form.MaximizeBox = false;
			form.AcceptButton = buttonOk;
			form.CancelButton = buttonCancel;

			var dialogResult = form.ShowDialog();
			value = textBox.Text;
			return dialogResult;
		}

		public static DialogResult Show(string promptText, ref string value)
		{
			return Show(string.Empty, promptText, ref value);
		}
	}
}