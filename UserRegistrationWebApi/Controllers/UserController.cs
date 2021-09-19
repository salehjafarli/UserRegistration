using AutoMapper;
using DAL.Abstract;
using DAL.Concrete.Postgre;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserRegistrationWebApi.DTOs;

namespace UserRegistrationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUnitOfWork Work { get; set; }
        public IMapper Mapper { get; set; }
        public ILogger Logger { get; set; }
        public UserController(IUnitOfWork Work, IMapper Mapper, ILogger<UserController> Logger)
        {
            this.Work = Work;
            this.Mapper = Mapper;
            this.Logger = Logger;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Logger.LogInformation("Test 123456789");   
            var res = await Work.Users.GetByIdAsync(id);
            UserDto dto = Mapper.Map<UserDto>(res);
            return Ok(dto);
        }
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> Getall()
        {
            var res = await Work.Users.GetAllAsync();
            List<UserDto> dtos = res.Select(x => Mapper.Map<UserDto>(x)).ToList();
            return Ok(dtos);
            

        }


        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(UserDto dto)
        {
            User user = Mapper.Map<User>(dto);
            var x =await Work.Users.CreateAsync(user);
            return x ? Ok() : NotFound();
        }


        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var x = await Work.Users.DeleteAsync(id);
            return x ? Ok() : NotFound();
        }
    }
}
