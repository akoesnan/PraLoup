using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Interfaces;

namespace PraLoup.DataAccess
{
    public class TestSeedDataGenerator : IDataGenerator
    {
        #region ISeedDataGenerator Members

        public void Execute()
        {
            SetupEventData();
            SetupOffersData();
            SetupAccountsData();
            SetupActivitiesData();            
            SetupInvitationsData();
        }

        #endregion
        

        /// <summary>
        /// Create seed data for events 
        /// </summary>
        private void SetupEventData()
        {

        }

        /// <summary>
        /// Create seed data for user invite 
        /// </summary>
        private void SetupInvitationsData()
        {

        }

        /// <summary>
        /// Create seed data for users 
        /// </summary>
        private void SetupAccountsData()
        {

        }

        /// <summary>
        /// Create seed data for 
        /// </summary>
        private void SetupActivitiesData()
        {

        }

        private void SetupOffersData()
        {

        }
    }
}
