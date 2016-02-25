using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace SvnClientTests.ViewModels
{
    [POCOViewModel]
    public class EventLogViewModel
    {
        public EventLogViewModel()
        {
            if (this.IsInDesignMode())
            {
                InitializeDesignTimeData();
            }
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