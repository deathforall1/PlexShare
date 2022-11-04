using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using PlexShareDashboard.Dashboard.Server.Summary;

namespace PlexShareTests.DashboardTests.Summary
{
    internal class SummaryTest
    {
    }
    public class SummaryTest
    {
        private ISummarizer _summarizer;

        /// <summary>
        ///     Setup the summarizer from the Summarizer factory
        /// </summary>
        [Fact]
        public void Setup()
        {
            _summarizer = SummarizerFactory.GetSummarizer();
        }

        /// <summary>
        ///     Null chat should give empty string
        /// </summary>
        [Fact]
        public void GetSummary_NullChatContext_EmptyString()
        {
            var chats = Utils.GetChatContext("Null Context");
            Assert.AreEqual(_summarizer.GetSummary(chats), "");
        }
        
    }
}
