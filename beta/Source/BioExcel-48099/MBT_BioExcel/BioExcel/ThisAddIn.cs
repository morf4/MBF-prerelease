﻿// -------------------------------------------------------------------------------------
// <copyright file="ThisAddIn.cs" company="Microsoft">
//  Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// ThisAddIn represents the entire add-in.
// </summary>
// -------------------------------------------------------------------------------------
namespace BioExcel
{
    /// <summary>
    /// ThisAddIn represents the entire add-in.
    /// </summary>
    public partial class ThisAddIn
    {
        /// <summary>
        /// Fired when addin starts up.
        /// </summary>
        /// <param name="sender">ThisAddIn instance.</param>
        /// <param name="e">Event data</param>
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {            
        }

        /// <summary>
        /// Fired when addin shut down.
        /// </summary>
        /// <param name="sender">ThisAddIn instance.</param>
        /// <param name="e">Event data.</param>
        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
