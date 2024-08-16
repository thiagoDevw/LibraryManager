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

        public static ResultViewModel Success (string message = "")
            => new ResultViewModel(true, message);
        public static ResultViewModel Error(string message)
            => new ResultViewModel(false, message);
        public static ResultViewModel NotFound(string message)
            => Error(message);
    }

    public class ResultViewModel<T> : ResultViewModel
    {
        public ResultViewModel(T? data, bool isSuccess = true , string message = "")
            : base(isSuccess, message)
        {
            Data = data;
        }
        public T? Data { get; private set; }
        public static ResultViewModel<T> Success(T data, string message = "")
            => new ResultViewModel<T>(data, true, message);
        public static ResultViewModel<T> Error(string message)
            => new(default, false, message);
        public static ResultViewModel<T> NotFound(string message)
            => Error(message);
    }
}
