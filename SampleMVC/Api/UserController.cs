using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SampleMVC.Repository;
using SampleMVC.Models;
using AutoMapper;
using SampleMVC.Dto;

namespace SampleMVC.Api
{
    public class UserController : ApiController
    {
        readonly IUserRepository _repository;
        public UserController()
        {
            _repository = new UserRepository();

        }

        [HttpGet]
        [Route("api/user")]
        public HttpResponseMessage Get()
        {
            var users = _repository.Get();
            var usersdto = Mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);

            if (usersdto == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Users not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, usersdto);
        }

        [HttpGet]
        [Route("api/user/{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var user = _repository.Get(id);
            var userdto = Mapper.Map<User, UserDto>(user);

            if (userdto == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"User with id {id} not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, userdto);
        }

        [HttpGet]
        [Route("api/user/GetCount")]
        public int GetCount(string username)
        {
            return _repository.UsersCountByName(username);
        }

        [HttpPost]
        [Route("api/user")]
        public HttpResponseMessage PostUser(UserDto userdto)
        {
            var user = Mapper.Map<UserDto, User>(userdto);

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (_repository.Add(user))
            {
                userdto = Mapper.Map<User, UserDto>(user);
                return Request.CreateResponse(HttpStatusCode.Created, userdto);
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Please try later");
        }


        [HttpDelete]
        [Route("api/user/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            if (_repository.Get(id) == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"User with id {id} not found");
            }
            if (_repository.Delete(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"User with id {id} deleted");
            }

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"User with id {id} not deleted");
        }


        [HttpPut]
        [Route("api/user/{id:int}")]
        public HttpResponseMessage Put(int id, UserDto userdto)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (_repository.Get(id) == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"User with {id} not found");
            }
            var user = Mapper.Map<UserDto, User>(userdto);
            if (_repository.Update(user))
            {
                userdto = Mapper.Map<User, UserDto>(user);
                return Request.CreateResponse(HttpStatusCode.OK, userdto);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, $"User with {id} not Modified");
        }
    }
}
