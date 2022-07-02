
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
        public static RestClient client;
        public static RestRequest request;
        public static RestResponse response;
        private static JsonReader config = new JsonReader();
        static Stopwatch stopWatch = new Stopwatch();

        public WS()
        { 
            response = new RestResponse();
        }

        public static void setUrl(string baseUrlKeyName)
        {
         
            string urlInConfigFile = config.getApiConfigValue(baseUrlKeyName);
            String finalUrl = String.Format(urlInConfigFile, config.getApiConfigValue("environment"), config.getApiConfigValue("version"));
            Console.WriteLine(finalUrl);
            client = new RestClient(finalUrl);
        }
        public static void setEndPoint(string endPoint)
        {
            string endPointValue = config.getApiConfigValue(endPoint);
            request = new RestRequest(endPointValue);
            setDefaultHeaders();
        }
        public static void setPathParam(string paramName, string value)
        {
            request.AddUrlSegment(paramName, value);

        }
        public static void setDefaultHeaders()
        { 
            //request.AddHeader("Accept", "application/json");
   
            request.AddHeaders(new Dictionary<string, string>
            {
                { "Accept", "application/json" },
                { "Content-Type", "application/json" }

            });
            //request.AddFile("Test file", @"C:\Users\viswa\Downloads\Test.txt", "multipart/form-data");
        }

        public static void setHeader(string keyName,string value)
        {
            request.AddHeader(keyName, value);
        }
        public static void changeHeaderValue(string keyName, string value)
        {
            request.AddOrUpdateHeader(keyName, value);
        }
        public static void addRequestBody(object requestBody)
        {
            request.AddJsonBody(JsonConvert.SerializeObject(requestBody));
        }

        public static RestResponse get()
        {
            try
            {
                stopWatch.Start();
                response = client.Get(request);
                stopWatch.Stop();
                Console.WriteLine("RESPONSE>>>>>>>>>>>>>>>>>>> " + response.Content);
                return response;
            }
            catch (Exception e)
            {
                // Handle exceptions in your CUSTOM CODE (restSharp will never throw itself)
            }
            finally
            {

               Console.WriteLine(LogRequest());
                //LogRequest();
            }

            return null;
        }
        public static RestResponse post()
        {
            try
            {
                stopWatch.Start();
                response = client.Post(request);
                stopWatch.Stop();
                Console.WriteLine("RESPONSE>>>>>>>>>>>>>>>>>>> " + response.Content);
                return response;
            }
            catch (Exception e)
            {
                // Handle exceptions in your CUSTOM CODE (restSharp will never throw itself)
            }
            finally
            {
                LogRequest();
            }

            return null;

        }
        public static RestResponse put()
        {
            try
            {
                stopWatch.Start();
                response = client.Put(request);
                stopWatch.Stop();
                Console.WriteLine("RESPONSE>>>>>>>>>>>>>>>>>>> " + response.Content);
                return response;
            }
            catch (Exception e)
            {
                // Handle exceptions in your CUSTOM CODE (restSharp will never throw itself)
            }
            finally
            {
                LogRequest();
            }

            return null;

        }
        public static RestResponse patch()
        {
            try
            {
                stopWatch.Start();
                response = client.Patch(request);
                stopWatch.Stop();
                Console.WriteLine("RESPONSE>>>>>>>>>>>>>>>>>>> " + response.Content);
                return response;
            }
            catch (Exception e)
            {
                // Handle exceptions in your CUSTOM CODE (restSharp will never throw itself)
            }
            finally
            {

                LogRequest();
            }

            return null;
        }
        public static RestResponse delete()
        {
            try
            {
                stopWatch.Start();
                response = client.Delete(request);
                stopWatch.Stop();
                Console.WriteLine("RESPONSE>>>>>>>>>>>>>>>>>>> " + response.Content);
                return response;
            }
            catch (Exception e)
            {
                // Handle exceptions in your CUSTOM CODE (restSharp will never throw itself)
            }
            finally
            {
                LogRequest();
            }

            return null;
        }
         public static void verifyStatusCode(int expectedCode)
        {
            Assert.AreEqual(expectedCode, (int)response.StatusCode);

        }

        public static string getResponse()
        {

            return response.Content;
        }

        public static string LogRequest()
        {
           
            {
                var requestToLog = new
                {
                    resource = request.Resource,
                    // Parameters are custom anonymous objects in order to have the parameter type as a nice string
                    // otherwise it will just show the enum value
                    parameters = request.Parameters.Select(parameter => new
                    {
                        name = parameter.Name,
                        value = parameter.Value,
                        type = parameter.Type.ToString()
                    }),
                    // ToString() here to have the method as a nice string otherwise it will just show the enum value
                    method = request.Method.ToString(),
                    // This will generate the actual Uri used in the request
                    uri = client.BuildUri(request),
                };

                var responseToLog = new
                {
                    statusCode = response.StatusCode,
                    content = response.Content,
                    headers = response.Headers,
                    // The Uri that actually responded (could be different from the requestUri if a redirection occurred)
                    responseUri = response.ResponseUri,
                    errorMessage = response.ErrorMessage,
                };

                return string.Format("Request completed in {0} ms, Request: {1}, Response: {2}",
                    stopWatch.ElapsedMilliseconds, JsonConvert.SerializeObject(requestToLog),
                    JsonConvert.SerializeObject(responseToLog));
            }
            
       
        }

    }
}
