using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tamweely.Application.DTOs;
using Tamweely.Application.Interfaces;
using Tamweely.Domain.Entities;

namespace Tamweely.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController(IGenericRepository<Department> _repo, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _repo.GetAllAsync();
            var deptsDtos = _mapper.Map<List<DepartmentDto>>(departments);
            return Ok(deptsDtos);
        }
    }
}
