﻿using System.Threading.Tasks;
using UpsideDownKitten.BL.Clients;
using UpsideDownKitten.BL.Utils;

namespace UpsideDownKitten.BL.Services
{
    public class CatsService : ICatsService
    {
        private readonly ICatsClient _catsClient;

        public CatsService(ICatsClient catsClient)
        {
            _catsClient = catsClient;
        }

        public async Task<byte[]> GetRotated()
        {
            var result = await _catsClient.GetCatAsync().ConfigureAwait(false);
            return ImagesProcessor.Rotate(result);
        }

        public async Task<byte[]> GetBlurred()
        {
            var result = await _catsClient.GetCatAsync().ConfigureAwait(false);
            return ImagesProcessor.Blur(result);
        }

        public async Task<byte[]> GetBlackWhite()
        {
            var result = await _catsClient.GetCatAsync().ConfigureAwait(false);
            return ImagesProcessor.BlackWhite(result);
        }
    }
}