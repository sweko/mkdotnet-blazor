using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClientApp
{
    public enum Operation
    {
        Add, 
        Subtract,
        Multiply,
        Divide
    }

    public struct Response
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Result { get; set; }
    }

    // public 

    public class CalcApi
    {
        const string baseUrl = "http://localhost:3000/api/calculator";

        public CalcApi(HttpClient http)
        {
            Http = http;
        }

        private readonly HttpClient Http;

        private readonly Dictionary<Operation, string> ops = new()
        {
            { Operation.Add, "add" },
            { Operation.Subtract, "subtract" },
            { Operation.Multiply, "multiply" },
            { Operation.Divide, "divide" }
        };

        private async Task<Response> GetResource(int first, int second, Operation operation)
        {
            var op = ops[operation];
            var url = $"{baseUrl}/{op}/{first}/{second}";
            var res = await Http.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadFromJsonAsync<Response>();
                return result;
            } 
            else
            {
                if (res.StatusCode == HttpStatusCode.UnprocessableEntity)
                {
                    throw new DivideByZeroException("The result is not defined if the divisor is zero");
                }
                throw new ApplicationException("Oops");
            }
        }
        public async Task<int> Add (int first, int second)
        {
            var result = await GetResource(first, second, Operation.Add);
            return result.Result;
        }

        public async Task<int> Subtract (int first, int second)
        {
            var result = await GetResource(first, second, Operation.Subtract);
            return result.Result;
        }

        public async Task<int> Multiply (int first, int second)
        {
            var result = await GetResource(first, second, Operation.Multiply);
            return result.Result;
        }

        public async Task<int> Divide (int first, int second)
        {
            var result = await GetResource(first, second, Operation.Divide);
            return result.Result;
        }
    }
}
