using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
	public partial class MainForm : Form
	{
		private DialogProcessor dialogProcessor = new DialogProcessor();

		public MainForm()
		{
			InitializeComponent();
		}
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}


		void DrawRectangleShapeSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddDumbRectangleM();

			statusBar.Items[0].Text = "Last action: Draw a rectangle";

			viewPort.Invalidate();
		}

	
		void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pickUpSpeedButton.Checked)
			{
				ShapeM selec = dialogProcessor.ContainsPoint(e.Location);
				if (selec != null)
				{
					if (dialogProcessor.Selection.Contains(selec))
					{
						selec.FillColor = dialogProcessor.currentFillColor;
						dialogProcessor.Selection.Remove(selec);

					}
					else
					{
						dialogProcessor.Selection.Add(selec);
					}
				}

				statusBar.Items[0].Text = "Last action: Selection of primitive";
				dialogProcessor.IsDragging = true;
				dialogProcessor.LastLocation = e.Location;
				viewPort.Invalidate();

			}
		}

		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging)
			{
				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Last action: Dragging";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}

		private void speedMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}
		
		public Button enterBtn;
		public TextBox widthTextBox;
		public Label text;
		public Form borderForm;
		private void changeBorderWidth_Click(object sender, EventArgs e)
		{


			// Show testDialog as a modal dialog and determine if DialogResult = OK.
			borderForm = new Form();

			borderForm.Text = "Enter border width";
			enterBtn = new Button();
			Button cancelBtn = new Button();
			widthTextBox = new TextBox();
			text = new Label();
			text.Text = "Width(1-20): ";
			enterBtn.Text = "Set Border Width";
			cancelBtn.Text = "Cancel";
			text.Location = new Point(90, 80);
			widthTextBox.Location = new Point(text.Left, text.Height + text.Top + 10);
			borderForm.Controls.Add(text);
			borderForm.Controls.Add(widthTextBox);
			enterBtn.Location = new Point(widthTextBox.Left, widthTextBox.Height + widthTextBox.Top + 10);
			cancelBtn.Location = new Point(enterBtn.Left, enterBtn.Height + enterBtn.Top + 10);
			// Set the accept button of the form to button1.
			borderForm.AcceptButton = enterBtn;

			// Set the cancel button of the form to button2.
			borderForm.CancelButton = cancelBtn;
			// Add enterBtn to the form.
			borderForm.Controls.Add(enterBtn);
			enterBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			// Add cancelBtn to the form.
			borderForm.Controls.Add(cancelBtn);
			borderForm.StartPosition = FormStartPosition.CenterScreen;
			borderForm.ShowDialog();

			enterBtn_Click(sender, e);


		}

		private void enterBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (widthTextBox.Text == "")
				{
					borderForm.Close();
				}
				else if ((float.Parse(widthTextBox.Text) < 0) || (float.Parse(widthTextBox.Text) > 20))
				{
					string message = "Enter appropriate value(1-20)!";
					string caption = "Error Detected in Input";
					MessageBoxButtons button = MessageBoxButtons.OK;
					DialogResult result;

					// Displays the MessageBox.
					result = MessageBox.Show(message, caption, button);
					if (result == System.Windows.Forms.DialogResult.OK)
					{

					}
				}
				else
				{

					dialogProcessor.currentWidth = float.Parse(widthTextBox.Text);
					dialogProcessor.ChangeGroupStrokeWidth(float.Parse(widthTextBox.Text));
					statusBar.Items[0].Text = "Последно действие: Задаване на дебелина на контура около фигурата.";
					viewPort.Invalidate();
				}
			}
			catch
			{
				borderForm.Close();
			}

		}

	
		public Button enterRotateBtn;
		public TextBox rotateTextBox;
		public Label textRotate;
		public Form rotateForm;

		public void RotateFormMethod(object sender, EventArgs e)
		{
			rotateForm = new Form();

			rotateForm.Text = "Enter rotate degree: ";
			enterRotateBtn = new Button();
			Button cancelRotateBtn = new Button();
			rotateTextBox = new TextBox();
			textRotate = new Label();
			textRotate.Text = "Degree(1-1000): ";
			enterRotateBtn.Text = "Set Rotate Radius";
			cancelRotateBtn.Text = "Cancel";
			textRotate.Location = new Point(90, 80);
			rotateTextBox.Location = new Point(textRotate.Left, textRotate.Height + textRotate.Top + 10);
			rotateForm.Controls.Add(textRotate);
			rotateForm.Controls.Add(rotateTextBox);
			enterRotateBtn.Location = new Point(rotateTextBox.Left, rotateTextBox.Height + rotateTextBox.Top + 10);
			cancelRotateBtn.Location = new Point(enterRotateBtn.Left, enterRotateBtn.Height + enterRotateBtn.Top + 10);
			// Set the accept button of the form to button1.
			rotateForm.AcceptButton = enterRotateBtn;

			// Set the cancel button of the form to button2.
			rotateForm.CancelButton = cancelRotateBtn;
			// Add enterBtn to the form.
			rotateForm.Controls.Add(enterRotateBtn);
			enterRotateBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			// Add cancelBtn to the form.
			rotateForm.Controls.Add(cancelRotateBtn);
			rotateForm.StartPosition = FormStartPosition.CenterScreen;
			rotateForm.ShowDialog();

			enterRotateBtn_Click(sender, e);
		}
		
		private void enterRotateBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (rotateTextBox.Text == "")
				{
					rotateForm.Close();
				}
				else if ((float.Parse(rotateTextBox.Text) < 1) || (float.Parse(rotateTextBox.Text) > 1000))
				{
					string message = "Enter appropriate value(1-1000)!";
					string caption = "Error Detected in Input";
					MessageBoxButtons button = MessageBoxButtons.OK;
					DialogResult result;

					// Displays the MessageBox.
					result = MessageBox.Show(message, caption, button);
					if (result == System.Windows.Forms.DialogResult.OK)
					{

					}
				}
				else
				{

					dialogProcessor.RotateShapeM(float.Parse(rotateTextBox.Text));
					statusBar.Items[0].Text = "Последно действие: Завъртане на фигура/фигури.";
					viewPort.Invalidate();
				}
			}
			catch
			{
				rotateForm.Close();
			}

		}
		public Button enterResizeBtn;
		public TextBox widthBox;
		public Label widthLabel;
		public TextBox heightBox;
		public Label heightLabel;
		public Form resizeForm;
		private void ResizeShapeMethod(object sender, EventArgs e)
		{
			resizeForm = new Form();

			resizeForm.Text = "Enter width and height: ";
			enterResizeBtn = new Button();
			Button cancelResizeBtn = new Button();
			widthBox = new TextBox();
			heightBox = new TextBox();
			widthLabel = new Label();
			heightLabel = new Label();
			widthLabel.Text = "Width(5-800): ";
			heightLabel.Text = "Height(5-800): ";

			enterResizeBtn.Text = "Resize Shapes";
			cancelResizeBtn.Text = "Cancel";
			widthLabel.Location = new Point(25, 80);
			heightLabel.Location = new Point(widthLabel.Left, widthLabel.Height + widthLabel.Top + 10);
			widthBox.Location = new Point(widthLabel.Left + widthLabel.Width + 10, widthLabel.Top);
			heightBox.Location = new Point(heightLabel.Left + heightLabel.Width + 10, heightLabel.Top);
			resizeForm.Controls.Add(widthLabel);
			resizeForm.Controls.Add(heightLabel);
			resizeForm.Controls.Add(widthBox);
			resizeForm.Controls.Add(heightBox);

			enterResizeBtn.Location = new Point(heightLabel.Left + 80, heightBox.Height + heightBox.Top + 10);
			cancelResizeBtn.Location = new Point(enterResizeBtn.Left, enterResizeBtn.Height + enterResizeBtn.Top + 10);
			// Set the accept button of the form to button1.
			resizeForm.AcceptButton = enterResizeBtn;

			// Set the cancel button of the form to button2.
			resizeForm.CancelButton = cancelResizeBtn;
			// Add enterBtn to the form.
			resizeForm.Controls.Add(enterResizeBtn);
			enterResizeBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			// Add cancelBtn to the form.
			resizeForm.Controls.Add(cancelResizeBtn);
			resizeForm.StartPosition = FormStartPosition.CenterScreen;
			resizeForm.ShowDialog();

			enterResizeBtn_Click(sender, e);
		}

		private void enterResizeBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (widthBox.Text == "" || heightBox.Text == "")
				{
					resizeForm.Close();
				}
				else if ((float.Parse(widthBox.Text) < 5) || (float.Parse(widthBox.Text) > 800) || (float.Parse(heightBox.Text) < 5) || (float.Parse(heightBox.Text) > 800))
				{
					string message = "Enter appropriate values for width and height(5-800)!";
					string caption = "Error Detected in Input";
					MessageBoxButtons button = MessageBoxButtons.OK;
					DialogResult result;

					// Displays the MessageBox.
					result = MessageBox.Show(message, caption, button);
					if (result == System.Windows.Forms.DialogResult.OK)
					{

					}
				}
				else
				{

					dialogProcessor.ResizeShapeM(float.Parse(widthBox.Text), float.Parse(heightBox.Text));
					statusBar.Items[0].Text = "Последно действие: Преоразмеряване на фигура/фигури.";
					viewPort.Invalidate();
				}
			}
			catch
			{
				resizeForm.Close();
			}

		}

        private void drawEllipse_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddDumbEllipseM();

				statusBar.Items[0].Text = "Последно действие: Рисуване на елипса.";

			viewPort.Invalidate();
		}

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddDumbPointM();

			statusBar.Items[0].Text = "Последно действие: Рисуване на точка.";

			viewPort.Invalidate();
		}

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

			dialogProcessor.AddDumbLineM();

			statusBar.Items[0].Text = "Последно действие: Рисуване на отсечка.";

			viewPort.Invalidate();
		}

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddDumbPolygonM();

			statusBar.Items[0].Text = "Последно действие: Рисуване на петоъгълник.";

			viewPort.Invalidate();
		}

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddDumbCircleM();

			statusBar.Items[0].Text = "Последно действие: Рисуване на кръг.";

			viewPort.Invalidate();
		}

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
			ColorDialog colorSelectDialog = new ColorDialog();
			if (colorSelectDialog.ShowDialog() == DialogResult.OK)
			{

				foreach (ShapeM item in dialogProcessor.Selection)
				{
					item.StrokeColor = colorSelectDialog.Color;
					viewPort.Invalidate();
				}
				dialogProcessor.currentStrokeColor = colorSelectDialog.Color;
				//dialogProcessor.ChangeGroupStrokeColor(colorSelectDialog.Color);
				statusBar.Items[0].Text = "Последно действие: Смяна на цвета на контура.";

			}
		}

       private void toolStripButton6_Click(object sender, EventArgs e)
        {
			ColorDialog fillColorSelectDialog = new ColorDialog();
			if (fillColorSelectDialog.ShowDialog() == DialogResult.OK)
			{

				foreach (ShapeM item in dialogProcessor.Selection)
				{
					item.FillColor = fillColorSelectDialog.Color;
					viewPort.Invalidate();
				}
				dialogProcessor.currentFillColor = fillColorSelectDialog.Color;
				dialogProcessor.ChangeGroupFillColor(fillColorSelectDialog.Color);
				statusBar.Items[0].Text = "Последно действие: Смяна на цвета на запълване.";

			}
		}

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
			ColorDialog selectedShapesColorDialog = new ColorDialog();
			if (selectedShapesColorDialog.ShowDialog() == DialogResult.OK)
			{

				foreach (ShapeM item in dialogProcessor.Selection)
				{
					item.FillColor = selectedShapesColorDialog.Color;
					viewPort.Invalidate();
				}
				dialogProcessor.currentSelectedColor = selectedShapesColorDialog.Color;

				statusBar.Items[0].Text = "Последно действие: Смяна на цвета на запълване при селекция.";

			}
		}

     
        

       /* private void OpacityPicker_Click(object sender, EventArgs e)
        {

        }*/

        private void toolStripButton8_Click(object sender, EventArgs e)
        {


			// Show testDialog as a modal dialog and determine if DialogResult = OK.
			borderForm = new Form();

			borderForm.Text = "Enter border width";
			enterBtn = new Button();
			Button cancelBtn = new Button();
			widthTextBox = new TextBox();
			text = new Label();
			text.Text = "Width(1-20): ";
			enterBtn.Text = "Set Border Width";
			cancelBtn.Text = "Cancel";
			text.Location = new Point(90, 80);
			widthTextBox.Location = new Point(text.Left, text.Height + text.Top + 10);
			borderForm.Controls.Add(text);
			borderForm.Controls.Add(widthTextBox);
			enterBtn.Location = new Point(widthTextBox.Left, widthTextBox.Height + widthTextBox.Top + 10);
			cancelBtn.Location = new Point(enterBtn.Left, enterBtn.Height + enterBtn.Top + 10);
			// Set the accept button of the form to button1.
			borderForm.AcceptButton = enterBtn;

			// Set the cancel button of the form to button2.
			borderForm.CancelButton = cancelBtn;
			// Add enterBtn to the form.
			borderForm.Controls.Add(enterBtn);
			enterBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			// Add cancelBtn to the form.
			borderForm.Controls.Add(cancelBtn);
			borderForm.StartPosition = FormStartPosition.CenterScreen;
			borderForm.ShowDialog();

			enterBtn_Click(sender, e);

		}

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
			/*SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				dialogProcessor.SerializeFile(dialogProcessor.ShapeMList, saveFileDialog1.FileName);
			}
			statusBar.Items[0].Text = "Последно действие: Записване на файл.";*/
			if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				dialogProcessor.WriteShapeListToFile((List<ShapeM>)dialogProcessor.ShapeMList, saveFileDialog1.FileName);
			}
			statusBar.Items[0].Text = "Последно действие: Записване на файл.";
		}

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
			/*OpenFileDialog openFileDialog1 = new OpenFileDialog();
			if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				dialogProcessor.ShapeMList = (List<ShapeM>)dialogProcessor.DeSerializeFile(openFileDialog1.FileName);
				viewPort.Invalidate();
			}
			statusBar.Items[0].Text = "Последно действие: Отваряне на файл.";*/
			if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				dialogProcessor.ShapeMList = (List<ShapeM>)dialogProcessor.LoadShapeListFromFile(openFileDialog1.FileName);
				viewPort.Invalidate();
			}
			statusBar.Items[0].Text = "Последно действие: Отваряне на файл.";
		}

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Close();
        }

		private void deleteCtrlDMToolStripMenuItem_Click(object sender, EventArgs e)
        {
			
			dialogProcessor.DeleteSelectedShapeMs();
			statusBar.Items[0].Text = "Последно действие: Изтриване на селектираните фигури.";
			viewPort.Invalidate();
		}

        private void copyCtrlCMToolStripMenuItem_Click(object sender, EventArgs e)
        {
			dialogProcessor.CopySelectedShapeMs();
			statusBar.Items[0].Text = "Последно действие: Копиране на селектираните фигури.";
			viewPort.Invalidate();
		}

        private void pasteCtrlPMToolStripMenuItem_Click(object sender, EventArgs e)
        {
			dialogProcessor.PasteSelectedShapeMs();
			statusBar.Items[0].Text = "Последно действие: Поставяне на копирани фигури.";
			viewPort.Invalidate();
		}

        private void rotateCtrlRToolStripMenuItem_Click(object sender, EventArgs e)
        {
			RotateFormMethod(sender, e);
		}

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
			ResizeShapeMethod(sender, e);
		}

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rectangleShiftRToolStripMenuItem_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddDumbRectangleM();

			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник.";

			viewPort.Invalidate();
		}

        private void elipseShiftEToolStripMenuItem_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddDumbEllipseM();

			statusBar.Items[0].Text = "Последно действие: Рисуване на елипса.";

			viewPort.Invalidate();
		}

        private void circleShiftCToolStripMenuItem_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddDumbCircleM();

			statusBar.Items[0].Text = "Последно действие: Рисуване на кръг.";

			viewPort.Invalidate();
		}

        private void lineShiftLToolStripMenuItem_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddDumbLineM();

			statusBar.Items[0].Text = "Последно действие: Рисуване на отсечка.";

			viewPort.Invalidate();
		}

        private void pointShiftPToolStripMenuItem_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddDumbPointM();

			statusBar.Items[0].Text = "Последно действие: Рисуване на точка.";

			viewPort.Invalidate();
	}
		

        private void viewPort_Load(object sender, EventArgs e)
        {

			dialogProcessor.RotateShapeM(0);
			statusBar.Items[0].Text = "Последно действие: Завъртане";
			viewPort.Invalidate();
		}

        private void toolStripButton9_Click(object sender, EventArgs e)
        {

			dialogProcessor.DeleteSelectedShapeMs();
			statusBar.Items[0].Text = "Последно действие: Изтриване на селектираните фигури.";
			viewPort.Invalidate();
		}

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
			dialogProcessor.GroupSelectedShapeMs();
			statusBar.Items[0].Text = "Последно действие: Групиране на избраните примитиви";
			viewPort.Invalidate();
		}
		//UnGroup Shapes Button - forgot to rename it
		private void toolStripButton11_Click(object sender, EventArgs e)
        {

			dialogProcessor.UnGroupSelectedShapeMs();
			statusBar.Items[0].Text = "Последно действие: Отмяна на групиране на избраните примитиви";
			viewPort.Invalidate();
		}

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
			RotateFormMethod(sender, e);
		}

     
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

			dialogProcessor.RotateShapeM(45);
			statusBar.Items[0].Text = "Последно действие: Завъртане";
			viewPort.Invalidate();
		}

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

			dialogProcessor.RotateShapeM(90);
			statusBar.Items[0].Text = "Последно действие: Завъртане";
			viewPort.Invalidate();
		}

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomFigNine();

			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

			viewPort.Invalidate();
		}

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
			dialogProcessor.RotateShapeM(10);
			statusBar.Items[0].Text = "Последно действие: Завъртане";
			viewPort.Invalidate();
		}

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
			dialogProcessor.RotateShapeM(20);
			statusBar.Items[0].Text = "Последно действие: Завъртане";
			viewPort.Invalidate();
		}

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
			dialogProcessor.RotateShapeM(30);
			statusBar.Items[0].Text = "Последно действие: Завъртане";
			viewPort.Invalidate();
		}

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
			dialogProcessor.RotateShapeM(60);
			statusBar.Items[0].Text = "Последно действие: Завъртане";
			viewPort.Invalidate();
		}

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
			dialogProcessor.RotateShapeM(80);
			statusBar.Items[0].Text = "Последно действие: Завъртане";
			viewPort.Invalidate();
		}

        private void figNineShiftFNToolStripMenuItem_Click(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomFigNine();

			statusBar.Items[0].Text = "Последно действие: Рисуване на 9.";

			viewPort.Invalidate();
		}
    }
}
