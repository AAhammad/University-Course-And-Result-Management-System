using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class DatabaseGateway
    {
         private SqlConnection connectionObj;

        public SqlConnection ConnectionObj
        {
            get
            {
                return connectionObj;
            }
        }

        public SqlCommand CommandObj
        {
            get
            {
                commandObj.Connection = connectionObj;
                return commandObj;
            }
        }

        private SqlCommand commandObj;

        public DatabaseGateway()
        {
            connectionObj = new SqlConnection(WebConfigurationManager.ConnectionStrings["UniversityDBCon"].ConnectionString);
            commandObj=new SqlCommand();
        }
    }
}