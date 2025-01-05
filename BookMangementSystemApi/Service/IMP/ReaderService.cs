using AutoMapper;
using BookMangementSystemApi.Dtos.Request;
using BookMangementSystemApi.Dtos.Response;
using BookMangementSystemApi.Exceptions;
using BookMangementSystemApi.Models;
using BookMangementSystemApi.Repository;
using BookMangementSystemApi.Validation;
using System.Net;

namespace BookMangementSystemApi.Service.IMP
{
    public class ReaderService : IReaderService
    {
        private readonly IGenericRepository<Reader> _repository;
        private readonly IMapper _mapper;
        private readonly IReaderValidator _readerValidate;


        public ReaderService(IGenericRepository<Reader> repository, IMapper mapper, IReaderValidator readerValidate)
        {
            _repository = repository;
            _mapper = mapper;
            _readerValidate = readerValidate;
        }

        public async Task<ReaderResponse> AddReader(ReaderRequest readerRequest)
        {
            await _readerValidate.ValidateReaderNameIsUnique(readerRequest.Name);
            var reader = _mapper.Map<Reader>(readerRequest);
            await _repository.AddAsync(reader);
            await _repository.SaveAsync();

            return _mapper.Map<ReaderResponse>(reader);
        }

        public async Task<IEnumerable<ReaderResponse>> GetAllReaders()
        {
            var readers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReaderResponse>>(readers);
        }



        public async Task<ReaderResponse> GetReaderById(int id)
        {
            var reader = await _repository.GetByIdAsync(id);
            return _mapper.Map<ReaderResponse>(reader);
        }

        public async Task<ReaderResponse> UpdateReader(int id, ReaderRequest readerRequest)
        {
            var reader = await _repository.GetByIdAsync(id);
            if (reader != null)
            {
                throw new ApiException("Reader Is Not Found", (int)HttpStatusCode.NotFound);
            }
            _mapper.Map(readerRequest, reader);

            _repository.Update(reader);
            await _repository.SaveAsync();

            return _mapper.Map<ReaderResponse>(reader);
        }

        public async Task DeleteReader(int id)
        {
            var reader = await _repository.GetByIdAsync(id);

            _repository.Delete(reader);
            await _repository.SaveAsync();
        }

    }
}
