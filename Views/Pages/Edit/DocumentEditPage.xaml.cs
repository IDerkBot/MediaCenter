using MediaCenter.Models;
using MediaCenter.Models.Entity;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaCenter.Views.Pages.Edit
{
	/// <summary>
	/// Логика взаимодействия для DocumentEditPage.xaml
	/// </summary>
	public partial class DocumentEditPage : Page
	{
		private readonly Document _currentDocument;
		public DocumentEditPage(Document selectedDocument = null)
		{
			InitializeComponent();
			_currentDocument = selectedDocument ?? new Document();
			DataContext = _currentDocument;
		}

		private void UIElement_OnDrop(object sender, DragEventArgs e)
		{
			var filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (filePath == null) return;
			TbFileName.Text = $"Файл: {filePath[0].Split('\\').ToList().Last()}";
			_currentDocument.File = File.ReadAllBytes(filePath[0]);
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(_currentDocument.Title))
			{
				MessageBox.Show("Введите название документа");
				return;
			}

			if (_currentDocument.ID == 0) MediaCenterEntities.GetContext().Documents.Add(_currentDocument);
			MediaCenterEntities.GetContext().SaveChanges();
			MessageBox.Show("Данные сохранены!");
			PageManager.GoBack();
		}
	}
}
