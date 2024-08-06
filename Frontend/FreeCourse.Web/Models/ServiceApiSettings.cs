﻿namespace FreeCourse.Web.Models
{
    public class ServiceApiSettings
    {
        public string IdentityBaseUri { get; set; }
        public string GatewayBaseUri { get; set; }
        public string PhotoStockUri { get; set; }
        public ServiceApi Catolog {  get; set; }
        public ServiceApi PhotoStock {  get; set; }
        public ServiceApi Basket {  get; set; }
    }

    public class ServiceApi
    {
        public string Path { get; set; }
    }
}