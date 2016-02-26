using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpo.Logger;

namespace SvnClientTests.ViewModels
{
    [POCOViewModel]
    public class EventLogViewModel
    {
        public EventLogViewModel()
        {
            Messenger.Default.Register<SvnLogMessage>(this, OnSvnLogMessage);
            if (this.IsInDesignMode())
            {
                InitializeDesignTimeData();
            }
        }

        private void OnSvnLogMessage(SvnLogMessage svnLogMessage)
        {
            AddText(svnLogMessage.Text);
        }


        private void InitializeDesignTimeData()
        {
            LogText = string.Empty;
            AddText("Text1");
            AddText("Text2");
            AddText("Text3");
        }

        public virtual string LogText { get; set; }

        public void AddText(string newText)
        {
            LogText += newText + Environment.NewLine;
        }
    }
}