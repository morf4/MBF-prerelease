﻿// -------------------------------------------------------------------------------------
// <copyright file="TreeViewTooltip.xaml.cs" company="Microsoft">
//  Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// Treeview tooltip control would be used for building custom 
// tooltip for the Sequence and Consensus tree view.
// </summary>
// -------------------------------------------------------------------------------------
namespace SequenceAssembler
{
    #region -- Using Directives --

    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
    using MBF;
    #endregion

    /// <summary>
    /// Interaction logic for TreeViewTooltip.xaml, would have tooltip control 
    /// displaying various properties associated with the sequence. 
    /// </summary>
    public partial class TreeViewTooltip : UserControl
    {
        #region -- Private Members --

        /// <summary>
        /// Describes the Display Id of the sequence
        /// </summary>
        private string displayId;

        /// <summary>
        /// Describes the length of the sequence
        /// </summary>
        private int length;

        /// <summary>
        /// Describes the Molecule type of the sequence
        /// </summary>
        private MoleculeType type;

        /// <summary>
        /// Describes the description of the sequence
        /// </summary>
        private string description;

        /// <summary>
        /// Describes the sequence.
        /// </summary>
        private string sequence;

        /// <summary>
        /// Describes the FileName the sequence has been imported from.
        /// </summary>
        private string filename;
        #endregion

        #region -- Constructor --
        /// <summary>
        /// Initializes the Tree view Tooltip control
        /// </summary>
        public TreeViewTooltip()
        {
            InitializeComponent();
        }
        #endregion

        #region -- Public Properties --

        /// <summary>
        /// Gets or sets the Display Id of the sequence
        /// </summary>
        public string DisplayId
        {
            get
            {
                return this.displayId;
            }

            set
            {
                this.displayId = value;
                if (!string.IsNullOrEmpty(this.displayId))
                {
                    this.txtDisplayId.Text = this.displayId;
                    this.txtDispTitle.Visibility = Visibility.Visible;
                    this.txtDisplayId.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Gets or sets the length of the sequence
        /// </summary>
        public int Length
        {
            get
            {
                return this.length;
            }

            set
            {
                this.length = value;
                if (this.length != 0)
                {
                    this.txtLength.Text = this.length.ToString(CultureInfo.CurrentCulture);
                    this.txtLengthTitle.Visibility = Visibility.Visible;
                    this.txtLength.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Molecule type
        /// </summary>
        public MoleculeType MoleculeType
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
                if (this.type != MoleculeType.Invalid)
                {
                    this.txtmoleculeType.Text = this.type.ToString();
                    this.txtMoleculeTitle.Visibility = Visibility.Visible;
                    this.txtmoleculeType.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Gets or sets the description of the sequence
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                this.description = value;
                if (!string.IsNullOrEmpty(this.description))
                {
                    this.txtdescription.Text = this.description;
                    this.txtDescTitle.Visibility = Visibility.Visible;
                    this.txtdescription.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Sequence
        /// </summary>
        public string Sequence
        {
            get
            {
                return this.sequence;
            }

            set
            {
                this.sequence = value;
                if (!string.IsNullOrEmpty(this.sequence))
                {
                    this.txtSequence.Text = this.sequence;
                    this.txtSequenceTitle.Visibility = Visibility.Visible;
                    this.txtSequence.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Sequence
        /// </summary>
        public string FileName
        {
            get
            {
                return this.filename;
            }

            set
            {
                this.filename = value;
                if (!string.IsNullOrEmpty(this.filename))
                {
                    this.txtFileName.Text = this.filename;
                    this.txtFileNameTitle.Visibility = Visibility.Visible;
                    this.txtFileName.Visibility = Visibility.Visible;
                }
            }
        }
        #endregion
    }
}
