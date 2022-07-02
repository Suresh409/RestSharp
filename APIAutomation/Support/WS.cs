
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.Support
{
    public class WS
    {
        private static readonly JsonReader config = new();
        static readonly Stopwatch stopWatch = new();

        public WS()
        {
            WSHelpers.response = new RestResponse();
        }

        public static void SetUrl(string baseUrlKeyName)
        {
         
            string urlInConfigFile = JsonReader.GetApiConfigValue(baseUrlKeyName);
            String finalUrl = String.Format(urlInConfigFile, JsonReader.GetApiConfigValue("environment"), JsonReader.GetApiConfigValue("version"));
            WSHelpers.
                        // Console.WriteLine(finalUrl);
                        client = new RestClient(finalUrl);
        }
        public static void SetEndPoint(string endPoint)
        {
            string endPointValue = JsonReader.GetApiConfigValue(endPoint);
            WSHelpers.request = new RestRequest(endPointValue);
            SetDefaultHeaders();
        }
        public static void SetPathParam(string paramName, string value)
        {
            WSHelpers.request.AddUrlSegment(paramName, value);

        }
        public static void SetDefaultHeaders()
        {
            WSHelpers.request.AddHeaders(new Dictionary<string, string>
            {
                { "Accept", "application/json" },
                { "Content-Type", "application/json" }

            });
            //request.AddFile("Test file", @"C:\Users\viswa\Downloads\Test.txt", "multipart/form-data");
        }

        public static void SetHeader(string keyName,string value)
        {
            WSHelpers.request.AddHeader(keyName, value);
        }
        public static void ChangeHeaderValue(string keyName, string value)
        {
            WSHelpers.request.AddOrUpdateHeader(keyName, value);
        }
        public static void AddRequestBody(object requestBody)
        {
            WSHelpers.request.AddJsonBody(JsonConvert.SerializeObject(requestBody));
        }

        public static RestResponse Get()
        {
            try
            {
                stopWatch.Start();
                WSHelpers.response = WSHelpers.client.Get(WSHelpers.request);
                stopWatch.Stop();
                //Console.WriteLine("RESPONSE>>>>>>>>>>>>>>>>>>> " + response.Content);
                return WSHelpers.response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {

               //Console.WriteLine(LogRequest());
                LogRequest();
            }
        }
        public static RestResponse Post()
        {
            try
            {
                stopWatch.Start();
                WSHelpers.response = WSHelpers.client.Post(WSHelpers.request);
                stopWatch.Stop();
                //Console.WriteLine("RESPONSE>>>>>>>>>>>>>>>>>>> " + response.Content);
                return WSHelpers.response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                LogRequest();
            }

        }
        public static RestResponse Put()
        {
            try
            {
                stopWatch.Start();
                WSHelpers.response = WSHelpers.client.Put(WSHelpers.request);
                stopWatch.Stop();
                //Console.WriteLine("RESPONSE>>>>>>>>>>>>>>>>>>> " + response.Content);
                return WSHelpers.response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                LogRequest();
            }

        }
        public static RestResponse Patch()
        {
            try
            {
                stopWatch.Start();
                WSHelpers.response = WSHelpers.client.Patch(WSHelpers.request);
                stopWatch.Stop();
                //Console.WriteLine("RESPONSE>>>>>>>>>>>>>>>>>>> " + response.Content);
                return WSHelpers.response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {

                LogRequest();
            }
        }
        public static RestResponse Delete()
        {
            try
            {
                stopWatch.Start();
                WSHelpers.response = WSHelpers.client.Delete(WSHelpers.request);
                stopWatch.Stop();
                //Console.WriteLine("RESPONSE>>>>>>>>>>>>>>>>>>> " + response.Content);
                return WSHelpers.response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                LogRequest();
            }
        }
         public static void VerifyStatusCode(string expectedCode)
        {
            Assert.AreEqual(System.Convert.ToInt32(expectedCode), ((int)WSHelpers.response.StatusCode));

        }

        public static string GetResponse()
        {

#pragma warning disable CS8603 // Possible null reference return.
            return WSHelpers.response.Content;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public static string LogRequest()
        {
           
            {
                var requestToLog = new
                {
                    resource = WSHelpers.request.Resource,
                    // Parameters are custom anonymous objects in order to have the parameter type as a nice string
                    // otherwise it will just show the enum value
                    parameters = WSHelpers.request.Parameters.Select(parameter => new
                    {
                        name = parameter.Name,
                        value = parameter.Value,
                        type = parameter.Type.ToString()
                    }),
                    // ToString() here to have the method as a nice string otherwise it will just show the enum value
                    method = WSHelpers.request.Method.ToString(),
                    // This will generate the actual Uri used in the request
                    uri = WSHelpers.client.BuildUri(WSHelpers.request),
                };

                var responseToLog = new
                {
                    statusCode = WSHelpers.response.StatusCode,
                    content = WSHelpers.response.Content,
                    headers = WSHelpers.response.Headers,
                    // The Uri that actually responded (could be different from the requestUri if a redirection occurred)
                    responseUri = WSHelpers.response.ResponseUri,
                    errorMessage = WSHelpers.response.ErrorMessage,
                };

                return string.Format("Request completed in {0} ms, Request: {1}, Response: {2}",
                    stopWatch.ElapsedMilliseconds, JsonConvert.SerializeObject(requestToLog),
                    JsonConvert.SerializeObject(responseToLog));
            }
            
       
        }

    }
}
