using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using ExtendedXmlSerializer.Configuration;
using ExtendedXmlSerializer.ExtensionModel.Xml;
using ReactiveUI;

namespace App6
{
	public class MainViewModel : ReactiveObject
	{
		public TextModel TextModel { get; } = new TextModel();

		public MainViewModel()
		{
			SaveCommand = ReactiveCommand.CreateFromTask(GetValue);
		}

		private async Task GetValue()
		{
			var saveFilePicker = new FileSavePicker()
			{
				CommitButtonText = "Save",
				FileTypeChoices = { { "XML", new List<string>() { ".xml" } } },
			};

			var file = await saveFilePicker.PickSaveFileAsync();

			using (var output = await file.OpenStreamForWriteAsync())
			{
				var serializer = new ConfigurationContainer().Create();
				serializer.Serialize(output, TextModel);
			}
		}

		public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
	}

	public class TextModel : ReactiveObject
	{
		private string text;

		public string Text
		{
			get => text;
			set => this.RaiseAndSetIfChanged(ref text, value);
		}
	}
}