using fikatime_api.Models;
using fikatime_api.Repositories;

namespace fikatime_api.Services
{
    public class FikatimeService
    {
        private readonly FikatimeDocumentRepository _fikatimeRepository;

        public FikatimeService(FikatimeDocumentRepository fikatimeRepository)
        {
            _fikatimeRepository = fikatimeRepository;
        }

        internal async Task<IList<FikaModelDTO>> GetAllFikatimesForSpecificMonth(int month)
        {
            var fikaModels = await _fikatimeRepository.GetAllFikatimesForSpecificMonth(month);

            var fikaDtos = MapModelsToDTOs(fikaModels);

            return fikaDtos;
        }

        internal static IList<FikaModelDTO> MapModelsToDTOs(IReadOnlyCollection<FikaModel> fikaModels)
            => fikaModels
            .Select(
                fikaModel => new FikaModelDTO
                {
                    Date = GetDateForFikaItem(fikaModel),
                    Description = fikaModel.Description,
                    Name = fikaModel.Name,
                    WikiUrl = fikaModel.WikiUrl
                }
                ).ToList();

        private static DateTime GetDateForFikaItem(FikaModel fikaModel)
        {
            var dateAsUtc = new DateTime(
                DateTime.UtcNow.Year,
                fikaModel.Month,
                fikaModel.DayOfMonth,
                DateTime.UtcNow.Hour,
                DateTime.UtcNow.Minute,
                DateTime.UtcNow.Second,
                DateTimeKind.Utc);

            return dateAsUtc;
        }
    }
}
