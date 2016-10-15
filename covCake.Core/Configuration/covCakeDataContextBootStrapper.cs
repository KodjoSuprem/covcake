using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace covCake.DataAccess
{
    /// <summary>
    /// Force la selection de connectionString "DBcovConnectionString"
    /// </summary>
    public partial class covCakeDataContext
    {
        
        public covCakeDataContext() 
            : base(ConfigurationManager.ConnectionStrings["DBcovConnectionString"].ToString(), mappingSource)
		{
		
		}
       

    }
}
