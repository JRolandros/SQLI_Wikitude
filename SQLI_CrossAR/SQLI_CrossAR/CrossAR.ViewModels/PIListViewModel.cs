using System;
using System.Collections.Generic;
using System.Windows.Input;


using SQLI_CrossAR.CrossAR.DataAccess.DB;
using SQLI_CrossAR.CrossAR.Models;
using System.ComponentModel;
using SQLI_CrossAR.CrossAR.DataAccess.DAO;

namespace SQLI_CrossAR.CrossAR.ViewModels
{
    public class PIListViewModel : RootViewModel
    {
       
        public List<PointInteret> piList { get; set; }
        DAOPointInteret _daoPI = DAOPointInteret.GetDAOImplInstance();

        public PIListViewModel()
        {
            if (_daoPI.GetTablePI() != null)
                piList = _daoPI.GetTablePI();
        }      
    }
}
