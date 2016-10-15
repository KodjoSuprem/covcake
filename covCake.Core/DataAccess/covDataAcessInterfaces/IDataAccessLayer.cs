﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace covCake.DataAccess
{
    public interface IDataAccessLayer
    {
        ICovEntity Create();
        void SubmitChanges();
        void Insert(ICovEntity entityObject);
    }
}
