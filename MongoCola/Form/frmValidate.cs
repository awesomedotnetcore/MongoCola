﻿using System;
using System.Windows.Forms;
using MongoCola.Module;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoUtility.Basic;

namespace MongoCola
{
    public partial class frmValidate : Form
    {
        private BsonDocument Result;

        public frmValidate()
        {
            InitializeComponent();
            if (!SystemManager.IsUseDefaultLanguage)
            {
                cmdSave.Text = SystemManager.guiConfig.MStringResource.GetText(StringResource.TextType.Common_Save);
                cmdClose.Text = SystemManager.guiConfig.MStringResource.GetText(StringResource.TextType.Common_Close);
                Text = SystemManager.guiConfig.MStringResource.GetText(StringResource.TextType.Common_Validate);
                cmdValidate.Text = SystemManager.guiConfig.MStringResource.GetText(StringResource.TextType.Common_Validate);
            }
            cmdSave.Enabled = false;
        }

        /// <summary>
        ///     Run Validate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdValidate_Click(object sender, EventArgs e)
        {
            BsonDocument TextSearchOption = new BsonDocument().Add(new BsonElement("full", chkFull.Checked.ToString()));
            CommandResult SearchResult = CommandHelper.ExecuteMongoColCommand("validate",
                MongoUtility.Core.RuntimeMongoDBContext.GetCurrentCollection(), TextSearchOption);
            Result = SearchResult.Response;
            MongoGUICtl.UIHelper.FillDataToTreeView("Validate Result", trvResult, Result);
            cmdSave.Enabled = true;
        }

        /// <summary>
        ///     Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
        	SaveFileDialog dialog = new SaveFileDialog();
        	dialog.Filter = Common.Utility.JsFilter;
        	if (dialog.ShowDialog() == DialogResult.OK){
	            MongoUtility.Basic.Utility.SaveResultToJSonFile(Result,dialog.FileName);
        	}
        }
    }
}