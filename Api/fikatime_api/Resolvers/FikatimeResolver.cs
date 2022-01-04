using fikatime_api.Models;
using fikatime_api.Repositories;
using fikatime_api.Services;

namespace fikatime_api.Resolvers
{
    public class FikatimeResolver
    {
        private readonly FikatimeDocumentRepository _fikatimeRepo;

        public FikatimeResolver(FikatimeDocumentRepository fikatimeRepo)
        {
            _fikatimeRepo = fikatimeRepo;
        }

        internal async Task<IList<FikaModelDTO>> ResolveFikatimesForSpecificMonth(int month)
        {
            var fikaModels = await _fikatimeRepo.GetAllFikatimesForSpecificMonth(month);

            var fikaDtos = FikatimeService.MapModelsToDTOs(fikaModels);

            return fikaDtos;
        }
    }
}
