﻿// Copyright (c) 2023 Files Community
// Licensed under the MIT License. See the LICENSE.

using Files.App.Dialogs;
using Microsoft.UI.Xaml.Controls;

namespace Files.App.Actions
{
	internal sealed class CompressIntoArchiveAction : BaseCompressArchiveAction
	{
		public override string Label
			=> "CreateArchive".GetLocalizedResource();

		public override string Description
			=> "CompressIntoArchiveDescription".GetLocalizedResource();

		public CompressIntoArchiveAction()
		{
		}

		public override async Task ExecuteAsync()
		{
			var (sources, directory, fileName) = CompressHelper.GetCompressDestination(context.ShellPage);

			var dialog = new CreateArchiveDialog
			{
				FileName = fileName,
			};

			var result = await dialog.TryShowAsync();

			if (!dialog.CanCreate || result != ContentDialogResult.Primary)
				return;

			ICompressArchiveModel creator = new CompressArchiveModel(
				sources,
				directory,
				dialog.FileName,
				dialog.Password,
				dialog.FileFormat,
				dialog.CompressionLevel,
				dialog.SplittingSize);

			await CompressHelper.CompressArchiveAsync(creator);
		}
	}
}
