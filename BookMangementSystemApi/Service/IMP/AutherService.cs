using AutoMapper;
using BookMangementSystemApi.Dtos.Request;
using BookMangementSystemApi.Dtos.Response;
using BookMangementSystemApi.Exceptions;
using BookMangementSystemApi.Models;
using BookMangementSystemApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace BookMangementSystemApi.Service.IMP
{
    public class AutherService : IAutherService
    {
        private readonly  IAutherRepository _repository;
        private readonly  IMapper _mapper;
        public AutherService(IAutherRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AutherResponse> AddAuther(AutherRequest autherRequest)
        {
            if (autherRequest.Name.IsNullOrEmpty())
            {
                throw new ApiException("Auther Name Is Empty",(int)HttpStatusCode.BadRequest);
            }

            var auther = _mapper.Map<Auther>(autherRequest);
            await _repository.AddAsync(auther);
            await _repository.SaveAsync();
            return _mapper.Map<AutherResponse>(auther);  
        }

        public async Task<IEnumerable<AutherResponse>> GetAllAuthers()
        {
            var authers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AutherResponse>>(authers);
        }


        public async Task<AutherResponse> GetAutherById(int id)
        {
            var auther = await _repository.GetByIdAsync(id);
            if (auther == null)
            {
                throw new ApiException("Auther Is Not Found", (int)HttpStatusCode.NotFound);
            }
            return  _mapper.Map<AutherResponse>(auther);
        }

        public async Task<AutherResponse> UpdateAuther(int id,[FromBody] AutherRequest autherRequest)
        {
            var auther = await _repository.GetByIdAsync(id);
            if(auther == null)
            {
                throw new ApiException("Auther Is Not Found", (int)HttpStatusCode.NotFound);
            }

            var  updatedAuther =_mapper.Map(autherRequest,auther);
            await _repository.UpdateAsync(updatedAuther);
            await _repository.SaveAsync();

            return _mapper.Map<AutherResponse>(updatedAuther);
        }


        public async Task DeleteAuther(int id)
        {
            var auther = await _repository.GetByIdAsync(id);
            if(auther == null)
            {
                throw new ApiException("Auther Is Not Found", (int)HttpStatusCode.BadRequest);
            }
            await _repository.DeleteAsync(auther);
            await _repository.SaveAsync();
        }

        

       

        
    }
}
