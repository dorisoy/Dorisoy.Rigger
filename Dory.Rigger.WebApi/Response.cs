using System;
using System.Collections.Generic;
using System.Text;

namespace Dory.Rigger.WebApi
{
    public class Response
    {
        /// <summary>
        /// 操作消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 0 成功  -1 失败
        /// </summary>
        public int Code { get; set; }


        public Response()
        {
            Code = 0;
            Message = "操作成功";
        }

        public Response(int code, string message)
        {
            Code = code;
            Message = message;
        }  
    }

    public class Response<T> : Response
    {
        /// <summary>
        /// 结果
        /// </summary>
        public T Result { get; set; }
        public Response<T> Success()
        {
            Code = 0;
            return this;
        }
        public Response<T> Success(T t)
        {
            Code = 0;
            Message = "OK";
            Result = t;
            return this;
        }
        public Response<T> Success(string msg, T t)
        {
            Code = 0;
            Message = msg;
            Result = t;
            return this;
        }

        public Response<T> Error500(string msg, T t)
        {
            Code = -1;
            Message = msg;
            Result = t;
            return this;
        }
    }
}
