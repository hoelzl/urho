﻿using System;
using System.Linq;
using System.Text;

namespace Urho
{
	/// <summary>
	/// Application options, see full description at:
	/// http://urho3d.github.io/documentation/1.5/_running.html 
	/// </summary>
	public class ApplicationOptions
	{
		/// <param name="assetsFolder">usually it's "Data". Can be null if built-in assets are enough for you</param>
		public ApplicationOptions(string assetsFolder)
		{
			if (assetsFolder != null)
			{
				ResourcePaths = new[] { assetsFolder };
			}
		}

		public ApplicationOptions(string[] assetsFolders)
		{
			ResourcePaths = assetsFolders;
		}

		/// <summary>
		/// Desktop only
		/// </summary>
		public int Width { get; set; } = 0;

		/// <summary>
		/// Desktop only
		/// </summary>
		public int Height { get; set; } = 0;
		
		/// <summary>
		/// Desktop only
		/// </summary>
		public bool WindowedMode { get; set; } = true;

		/// <summary>
		/// Desktop only
		/// </summary>
		public bool ResizableWindow { get; set; } = false;

		/// <summary>
		/// With limit enabled: 200 fps for Desktop (and always 60 fps for mobile despite of the flag)
		/// </summary>
		public bool LimitFps { get; set; } = true;

		/// <summary>
		/// iOS only
		/// </summary>
		public OrientationType Orientation { get; set; } = OrientationType.Landscape;

		/// <summary>
		/// Resource path(s) to use (default: Data, CoreData)
		/// </summary>
		public string[] ResourcePaths { get; set; } = null;

		/// <summary>
		/// Resource prefix path, default to URHO3D_PREFIX_PATH env-var or executable path
		/// </summary>
		public string ResourcePrefixPath { get; set; } = null;

		/// <summary>
		/// Resource package files to use (default: empty)
		/// </summary>
		public string[] ResourcePackagesPaths { get; set; } = null;

		/// <summary>
		/// Touch emulation on desktop platform
		/// </summary>
		public bool TouchEmulation { get; set; } = false;

		/// <summary>
		/// Add any flag listed here: http://urho3d.github.io/documentation/1.5/_running.html 
		/// </summary>
		public string AdditionalFlags { get; set; } = string.Empty;

		/// <summary>
		/// Windows: external window handle (WinForms Panel.Handle) to use in order to display Urho game
		/// You can use it in WPF via WindowsFormsHost (and a WF panel inside it)
		/// </summary>
		public IntPtr ExternalWindow { get; set; }

		public enum OrientationType
		{
			Landscape,
			Portrait
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("args");//it will be skipped by Urho;

			if (WindowedMode)
				builder.Append(" -w");

			if (!LimitFps)
				builder.Append(" -nolimit");

			if (Width > 0)
				builder.AppendFormat(" -x {0}", Width);

			if (Height > 0)
				builder.AppendFormat(" -y {0}", Height);

			if (ResizableWindow)
				builder.Append(" -s");

			var resourcePathes = new[] {"CoreData"}.Concat(ResourcePaths ?? new string[0]);
			builder.AppendFormat(" -p \"{0}\"", string.Join(";", resourcePathes.Distinct()));

			if (ResourcePackagesPaths?.Length > 0)
				builder.AppendFormat(" -pf \"{0}\"", string.Join(";", ResourcePackagesPaths));

			if (ResourcePrefixPath != null)
				builder.AppendFormat(" -pp \"{0}\"", ResourcePrefixPath);

			if (TouchEmulation)
				builder.Append(" -touch");

			builder.AppendFormat(" -{0}", Orientation.ToString().ToLower());

			return builder + " " + AdditionalFlags;
		}
	}
}