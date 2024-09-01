﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Domain.Utilities.Interfaces
{
    public interface IDataResult<T> : IResult where T : class
    {
        public T? Data { get; }
    }
}
