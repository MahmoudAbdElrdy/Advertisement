using System.Collections.Generic;
using System.Threading.Tasks;
using AuthApplication.Auth.Commands;
using AuthApplication.Auth.Queries;
using AuthApplication.Regions.Commands;
using AuthApplication.Regions.Dto;
using AuthApplication.Regions.Queries;
using AuthApplication.UserManagment.Commands;
using AuthApplication.UserManagment.Queries;
using Common.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.UserManagement
{
    public partial class RegionManagementController
    {
        [AllowAnonymous]
        [HttpGet("cities")]
        public async Task<ActionResult<PageList<CityDto>>> Getcities([FromQuery] GetAllCitiesWithPaginationCommand query)
        {
            return Ok((await Mediator.Send(query)));
        }

        [AllowAnonymous]
        [HttpGet("cities/Get-cities-by-country-id")]
        public async Task<ActionResult<List<CityDto>>> Getcities([FromQuery] GetCitiesByCountryIdCommand query)
        {
            return Ok((await Mediator.Send(query)));
        }


        [AllowAnonymous]
        [HttpPost("cities/add-city")]
        public async Task<ActionResult<PageList<PageList<CityDto>>>> AddCity([FromBody] AddCityCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }
         
        [HttpPut("cities/edit-city/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<CityDto>>> EditCity(
          [FromRoute] string id,
          [FromBody] EditCityCommand command
        )
        {
            command.Id = id;
            return Ok((await Mediator.Send(command)));
        }

        [HttpDelete("cities/delete-city/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<CityDto>>> DeleteCity([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new DeleteCityCommand() { Id = id }));
        }




    }
}