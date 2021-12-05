using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using api.conevento.ActionFilter;
using api.conevento.Models;
using biz.conevento.Entities;
using biz.conevento.Repository;
using biz.conevento.Servicies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using api.conevento.Models.User;
using System.Text;

namespace api.conevento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IUserRepository _userRepository;

        public UserController(
             IMapper mapper,
             ILoggerManager logger,
             IUserRepository userRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<UserDto>>> GetAll()
        {
            var response = new ApiResponse<List<UserDto>>();

            try
            {
                response.Result = _mapper.Map<List<UserDto>>(_userRepository.GetAll());
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost("Login", Name = "Login")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Login(string email, string password)
        {
            var response = new ApiResponse<UserDto>();

            try
            {
                var _user = _mapper.Map<User>(_userRepository.Find(c => c.Correo == email));
                //var _userType = _mapper.Map<List<User>>(_userRepository.GetAllIncluding(c => c.UserType, r => r.Role, y => y.ProfileUsers, x => x.AssigneeInformations));
                //var _assignee = _mapper.Map<List<User>>(_userRepository.GetAllIncluding(c => c.AssigneeInformations));
                if (_user != null)
                {
                    if (_user.Pass == password)
                    {

                        var userData = _mapper.Map<UserDto>(_user);

                        response.Result = userData;
                        response.Success = true;
                        response.Message = "Acceso correcto";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Usuario y/o contraseña incorrecta";

                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Usuario y/o contraseña incorrecta";

                }

            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost("ResetPass", Name = "ResetPass")]
        public async Task<ActionResult<ApiResponse<UserDto>>> ResetPass(string email)
        {
            var response = new ApiResponse<UserDto>();

            try
            {
                var _user = _mapper.Map<User>(_userRepository.Find(c => c.Correo == email)); if (_user != null)
                {
                    string _pass = "";
                    _pass = _userRepository.CreatePassword(5);
                    _user.Pass = _pass;
                    _userRepository.Update(_mapper.Map<User>(_user), _user.Id);
                    StreamReader reader = new StreamReader(Path.GetFullPath("TemplateMail/Email.html"));
                    string body = string.Empty;
                    body = reader.ReadToEnd();
                    body = body.Replace("{user}", _user.Nombres);
                    body = body.Replace("{username}", $"{_user.Correo}");
                    body = body.Replace("{pass}", _pass);

                    // _userRepository.SendMail(_user.Correo, body, "Recovery password");

                    response.Result = _mapper.Map<UserDto>(_user);
                    response.Success = true;
                    response.Message = "Contraseña actualizada";

                }
                else
                {
                    response.Success = false;
                    response.Message = "Usuario y/o contraseña incorrecta";

                }

            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }

            return Ok(response);
        }


        [HttpPost("AddUser", Name = "AddUser")]
        public async Task<ActionResult<ApiResponse<UserDto>>> AddUser(UserDto _user)
        {
            var response = new ApiResponse<UserDto>();

            try
            {
                //var _user = _mapper.Map<User>(_userRepository.Find(c => c.Correo == email)); if (_user != null)
                //{

                _userRepository.Add(_mapper.Map<User>(_user));
                StreamReader reader = new StreamReader(Path.GetFullPath("TemplateMail/Email.html"));
                string body = string.Empty;
                body = reader.ReadToEnd();
                body = body.Replace("{user}", _user.nombres);
                body = body.Replace("{username}", $"{_user.correo}");
                body = body.Replace("{pass}", _user.pass);

                //  _userRepository.SendMail(_user.correo, body, "Bienvenido a Conevento");

                response.Result = _user;
                response.Success = true;
                response.Message = "Usuario creado con exíto";

                //

            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost("UpdateUser", Name = "UpdateUser")]
        public async Task<ActionResult<ApiResponse<UserDto>>> UpdateUser(UserDto user)
        {
            var response = new ApiResponse<UserDto>();

            try
            {
                var _user = _mapper.Map<User>(_userRepository.Find(c => c.Id == user.Id));

                if (_user != null)
                {
                    _mapper.Map<User>(user);
                    _userRepository.Save();

                    response.Result = _mapper.Map<UserDto>(_user);
                    response.Success = true;
                    response.Message = "Usuario actualizado con éxito.";

                }
                else
                {
                    response.Success = false;
                    response.Message = "Usuario y/o contraseña incorrecta";

                }

            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }

            return Ok(response);
        }


    }
}
