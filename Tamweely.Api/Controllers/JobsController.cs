using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tamweely.Application.DTOs;
using Tamweely.Application.Interfaces;
using Tamweely.Domain.Entities;

namespace Tamweely.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController(IGenericRepository<JobTitle> _repo, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _repo.GetAllAsync();
            var jobsDtos = _mapper.Map<List<JobTitleDto>>(jobs);
            return Ok(jobsDtos);
        }
    }
}
