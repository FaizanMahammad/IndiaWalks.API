using IndiaWalks.API.Models.Domain;
using System.Net;

namespace IndiaWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
