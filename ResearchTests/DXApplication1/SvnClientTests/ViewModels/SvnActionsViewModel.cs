using System;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpo.Logger;
using SharpSvn;

namespace SvnClientTests.ViewModels
{
    [POCOViewModel]
    public class SvnActionsViewModel
    {
        public SvnActionsViewModel()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            WorkdirPath = @"C:\Users\Andy\Documents\WorkDir";
        }

        public virtual string WorkdirPath { get; set; }

        public virtual void Checkout()
        {
            Debug.WriteLine("Checkout command");
            using (SvnClient client = new SvnClient())
            {
                client.Notify += Client_Notify;

                //client.CheckOut(WorkdirPath, StatusHandler);

                client.Notify -= Client_Notify;
            }
        }

        public virtual void Update()
        {
            Debug.WriteLine("Update command");
        }

        public virtual void Status()
        {
            Debug.WriteLine("Status command");
            using (SvnClient client = new SvnClient())
            {
                client.Notify += Client_Notify;

                client.Status(WorkdirPath, StatusHandler);

                client.Notify -= Client_Notify;
            }
        }

        private void StatusHandler(object sender, SvnStatusEventArgs svnStatusEventArgs)
        {
            Messenger.Default.Send(new SvnLogMessage(svnStatusEventArgs.FullPath));
        }

        private void Client_Notify(object sender, SvnNotifyEventArgs e)
        {
            Messenger.Default.Send(new SvnLogMessage(e.FullPath));
        }

        public virtual void Commit()
        {
            Debug.WriteLine("Commit command");
        }

        public virtual void ShowFileDialog()
        {
            using (var dialog = new FolderBrowserDialog { SelectedPath = WorkdirPath })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    WorkdirPath = dialog.SelectedPath;
                }
            }
        }
    }

    public class SvnLogMessage  
    {
        public string Text { get; set; }

        public SvnLogMessage(string text)
        {
            Text = text;
        }
    }
}