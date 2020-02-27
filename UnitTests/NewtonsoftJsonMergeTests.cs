using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

// https://stackoverflow.com/questions/21160337/how-can-i-merge-two-jobject

namespace UnitTests
{
    /// <summary>
    /// The needs for JSON merge is for override general path setting with specific one
    /// </summary>
    public class NewtonsoftJsonMergeTests
    {
        private readonly ITestOutputHelper _outputHelper;

        #region Ctor

        public NewtonsoftJsonMergeTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        #endregion // Ctor

        [Fact]
        public void JsonMerge_Test()
        {
            #region Json

            var dataObject1 = JObject.Parse(@"{
                ""data"": [{
                    ""field"": ""field1""
                }],
                ""paging"": {
                    ""prev"": ""link1""
                }
            }");
            var dataObject2 = JObject.Parse(@"{
                ""data"": [{
                    ""id"": ""id2"",
                    ""field"": ""field2""
                }],
                ""paging"": {
                    ""prev"": ""link2""
                }
            }");

            #endregion // Jsons

            var mergeSettings = new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            };

            dataObject1.Merge(dataObject2, mergeSettings);

            _outputHelper.WriteLine(dataObject1.ToString());

            string expected = @"
{
  ""data"": [
    {
      ""field"": ""field1""
    },
    {
      ""id"": ""id2"",
      ""field"": ""field2""
    }
  ],
  ""paging"": {
    ""prev"": ""link2""
  }
}";


            string expectedTrimed = Regex.Replace(expected, @"\s", "");
            string mergedTrimed = Regex.Replace(dataObject1.ToString(), @"\s", "");
            Assert.Equal(expectedTrimed, mergedTrimed);
        }
    }
}
