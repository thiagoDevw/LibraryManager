using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Manager.Application.Models
{
    public class ResultViewModel
    {
        public ResultViewModel(bool isSuccess = true, string message = "")
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
    }

    public class ResultViewModel<T> : ResultViewModel
    {
        public ResultViewModel(T? data, bool isSuccess = true , string message = "")
            : base(isSuccess, message)
        {
            Data = data;
        }
        public T? Data { get; private set; }
    }
}
