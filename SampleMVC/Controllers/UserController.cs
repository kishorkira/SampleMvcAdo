using SampleMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleMVC.Repository;
using AutoMapper;
using SampleMVC.Dto;

namespace SampleMVC.Controllers
{
    public class UserController : Controller
    {
        IEnumerable<Company> _companies;
        IEnumerable<Masters> _masters;

        readonly IUserRepository _repository;

        public UserController()
        {
            _repository = new UserRepository();
            _companies = new List<Company>{
                new Company{ Id=1 ,Name="Company 1"},
                new Company{ Id=2 ,Name="Company 2"},
                new Company{ Id=3 ,Name="Company 3"}
            };
            _masters = new List<Masters>{
                new Masters{ Id=1 ,Name="Master 1"},
                new Masters{ Id=2 ,Name="Master 2"},
                new Masters{ Id=3 ,Name="Master 3"}
            };
        }

        [HttpGet]
        public ActionResult List()
        {
            var path = Server.MapPath("");
            var users = _repository.Get();
            var usersdto = Mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
            return View(usersdto);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(_companies, "Id", "Name");
            ViewBag.MasterId = new SelectList(_masters, "Id", "Name");

            return View();
        }


        [HttpPost]
        public ActionResult Create(UserDto userdto)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<UserDto, User>(userdto);
                var result = _repository.Add(user);
                return RedirectToAction("List");
            }
            ViewBag.CompanyId = new SelectList(_companies, "Id", "Name");
            ViewBag.MasterId = new SelectList(_masters, "Id", "Name");

            return View(userdto);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            ViewBag.Id = id;
            var user = _repository.Get(id);
            var userdto = Mapper.Map<User, UserDto>(user);
            return View(userdto);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;

            ViewBag.CompanyId = new SelectList(_companies, "Id", "Name");
            ViewBag.MasterId = new SelectList(_masters, "Id", "Name");

            var user = _repository.Get(id);
            var userdto = Mapper.Map<User, UserDto>(user);
            return View(userdto);
        }
        [HttpPost]
        public ActionResult Edit(UserDto userdto)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<UserDto, User>(userdto);
                var result = _repository.Update(user);
                return RedirectToAction("List");
            }
            ViewBag.CompanyId = new SelectList(_companies, "Id", "Name");
            ViewBag.MasterId = new SelectList(_masters, "Id", "Name");
            return View(userdto);
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            ViewBag.Id = id;
            var user = _repository.Get(id);
            var userdto = Mapper.Map<User, UserDto>(user);
            return View(userdto);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteUser(int id)
        {
            var result = _repository.Delete(id);
            return RedirectToAction("List");
        }
    }
}