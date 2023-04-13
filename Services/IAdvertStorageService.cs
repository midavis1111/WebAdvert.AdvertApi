﻿using WebAdvert.AdvertApi.Models;

namespace WebAdvert.AdvertApi.Services
{
    public interface IAdvertStorageService
    {
        Task<string> Add(AdvertModel model);
        Task Confirm(ConfirmAdvertModel model);
    }
}
