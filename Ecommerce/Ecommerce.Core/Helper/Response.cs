﻿using System.Net;

namespace Ecommerce.Core.Helper
{
    public class Response<T>
    {
        public T value { get; set; }
        public bool isSuccess { get; set; } = false;
        public string message { get; set; }
        public int statusCode { get; set; }
        public string returnUrl { get; set; }
    }
}
