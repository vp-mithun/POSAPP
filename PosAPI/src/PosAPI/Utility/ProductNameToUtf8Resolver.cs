using AutoMapper;
using PosAPI.DTO;
using System.Text;
using System;

namespace PosAPI.Utility
{
    public class ProductNameToUtf8Resolver : IValueResolver<Products, ProductsDTO, string>
    {
        public string Resolve(Products source, ProductsDTO destination, string destMember, ResolutionContext context)
        {
            return Encoding.UTF8.GetString(Encoding.GetEncoding("latin1").GetBytes(source.ProductName));
        }
    }

    public class ProductNameToSalesDTOUtf8Resolver : IValueResolver<Sales, SalesDTO, string>
    {
        public string Resolve(Sales source, SalesDTO destination, string destMember, ResolutionContext context)
        {
            return Encoding.UTF8.GetString(Encoding.GetEncoding("latin1").GetBytes(source.ProductName));
        }
    }

    public class ProductNameToLatin1Resolver : IValueResolver<SalesDTO, Sales, string>
    {
        public string Resolve(SalesDTO source, Sales destination, string destMember, ResolutionContext context)
        {
            return Encoding.UTF7.GetString(Encoding.GetEncoding("utf-8").GetBytes(source.ProductName));
        }
    }
}
