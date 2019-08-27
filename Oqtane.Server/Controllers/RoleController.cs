﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Oqtane.Repository;
using Oqtane.Models;

namespace Oqtane.Controllers
{
    [Route("{site}/api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleRepository Roles;

        public RoleController(IRoleRepository Roles)
        {
            this.Roles = Roles;
        }

        // GET: api/<controller>?siteid=x
        [HttpGet]
        public IEnumerable<Role> Get(string siteid)
        {
            if (siteid == "")
            {
                return Roles.GetRoles();
            }
            else
            {
                return Roles.GetRoles(int.Parse(siteid));
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Role Get(int id)
        {
            return Roles.GetRole(id);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = "Administrators")]
        public Role Post([FromBody] Role Role)
        {
            if (ModelState.IsValid)
            {
                Role = Roles.AddRole(Role);
            }
            return Role;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrators")]
        public Role Put(int id, [FromBody] Role Role)
        {
            if (ModelState.IsValid)
            {
                Role = Roles.UpdateRole(Role);
            }
            return Role;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrators")]
        public void Delete(int id)
        {
            Roles.DeleteRole(id);
        }
    }
}