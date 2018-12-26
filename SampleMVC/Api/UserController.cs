using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SampleMVC.Repository;
using SampleMVC.Models;

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
            if (users == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Users with not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        [HttpGet]
        [Route("api/user/{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var user = _repository.Get(id);
            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"User with {id} not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        [HttpGet]
        [Route("api/user/GetCount")]
        public int GetCount(string username)
        {
            return _repository.UsersCountByName(username);
        }

        [HttpPost]
        [Route("api/user")]
        public HttpResponseMessage PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (_repository.Add(user))
            {
                return Request.CreateResponse(HttpStatusCode.Created, user);
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
        public HttpResponseMessage Put(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (_repository.Get(id) == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"User with {id} not found");
            }

            if (_repository.Update(user))
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);

            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, $"User with {id} not Modified");
        }
    }
}
