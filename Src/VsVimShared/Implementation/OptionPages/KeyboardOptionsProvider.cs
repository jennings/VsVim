﻿using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Vim;
using Vim.UI.Wpf;
using Vim.Extensions;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio;

namespace Vim.VisualStudio.Implementation.OptionPages
{
    [Export(typeof(IKeyboardOptionsProvider))]
    internal sealed class KeyboardOptionsProvider : IKeyboardOptionsProvider
    {
        private readonly IVsShell _vsShell;

        [ImportingConstructor]
        internal KeyboardOptionsProvider(SVsServiceProvider serviceProvider)
        {
            _vsShell = serviceProvider.GetService<SVsShell, IVsShell>();
        }

        private void ShowOptionsPage()
        {
            Guid packageGuid = VsVimConstants.PackageGuid;
            if (ErrorHandler.Succeeded(_vsShell.LoadPackage(ref packageGuid, out IVsPackage vsPackage)))
            {
                if (vsPackage is Package package)
                {
                    package.ShowOptionPage(typeof(KeyboardOptionPage));
                }
            }
        }

        #region IKeyboardOptionsProvider

        void IKeyboardOptionsProvider.ShowOptionsPage()
        {
            ShowOptionsPage();
        }

        #endregion
    }
}
