using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Utility
{
    public class ServiceResult
    {
        public ServiceResult()
        {

        }
        public ServiceResult(bool issucess)
        {
            IsSuccess = issucess;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public static ServiceResult Okay()
        {
            return new ServiceResult
            {
                IsSuccess = true,
                Message = "عملیات با موفقیت انجام شد"
            };
        }
        public static ServiceResult Error()
        {
            return new ServiceResult
            {
                IsSuccess = false,
                Message = "در انجام عملیات خطایی رخ داد"
            };
        }
        public List<string> Errors { get; set; } = new List<string>();
        public void AddError(string errorMessage)
        {
            IsSuccess = false;
            Errors.Add(errorMessage);
        }
        public ServiceResult<T> AddData<T>(T data)
        {
            return new ServiceResult<T>() { Data = data, Errors = this.Errors, IsSuccess = this.IsSuccess, Message = this.Message };
        }     
    }
    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult()
        {

        }
        public ServiceResult(bool issucess)
        {
            IsSuccess = issucess;
        }
        public ServiceResult(bool issucess, T data)
        {
            IsSuccess = issucess;
            Data = data;
        }
        public T Data { get; set; }
        public static ServiceResult<T> Okay(T data)
        {
            return new ServiceResult<T>
            {
                IsSuccess = true,
                Message = "عملیات با موفقیت انجام شد",
                Data = data
            };
        }

        public static ServiceResult<T> Error(T data)
        {
            return new ServiceResult<T>
            {
                IsSuccess = false,
                Message = "در انجام عملیات خطایی رخ داد",
                Data = data
            };
        }
    }
}
