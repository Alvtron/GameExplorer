﻿using GameExplorer.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace GameExplorer.Uwp.DataSource
{
    /// <summary>
    /// 
    /// </summary>
    public static class ImagesDataSource
    {
        /// <summary>
        /// The controller
        /// </summary>
        private const string Controller = "Images";
        /// <summary>
        /// The base URI
        /// </summary>
        private const string BaseUri = @"http://localhost:49952/api/";
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        private static HttpClient Client { get; } = new HttpClient { BaseAddress = new Uri(BaseUri) };

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public static async Task<Image[]> Get()
        {
            var json = await Client.GetStringAsync(Controller).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Image[]>(json);
        }

        /// <summary>
        /// Gets the specified uid.
        /// </summary>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public static async Task<Image> Get(Guid uid)
        {
            var json = await Client.GetStringAsync($@"{Controller}\{uid}").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Image>(json);
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static async Task<bool> Delete(Image item)
        {
            var response = await Client.DeleteAsync($@"{Controller}\{item.Uid}").ConfigureAwait(false);
            return response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NotFound;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static async Task<bool> Add(Image item)
        {
            var postBody = JsonConvert.SerializeObject(item, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            var response = await Client.PostAsync(Controller, new StringContent(postBody, Encoding.UTF8, "application/json")).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static async Task<bool> Update(Image item)
        {
            var postBody = JsonConvert.SerializeObject(item, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            var response = await Client.PutAsync($@"{Controller}\{item.Uid}", new StringContent(postBody, Encoding.UTF8, "application/json")).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}