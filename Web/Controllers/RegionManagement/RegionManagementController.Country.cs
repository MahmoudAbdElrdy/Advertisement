using System.Collections.Generic;
using System.Threading.Tasks;
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
        [HttpGet("Countries/get-ddl")]
        public async Task<ActionResult<List<CountryDto>>> GetCountries([FromQuery] GetAllCountriesCommand query)
        {
            return Ok((await Mediator.Send(query)));
        }

        [AllowAnonymous]
        [HttpGet("Countries")]
        public async Task<ActionResult<PageList<CountryDto>>> GetCountries([FromQuery] GetAllCountriesWithPaginationCommand query)
        {
            return Ok((await Mediator.Send(query)));
        }

        [AllowAnonymous]
        [HttpPost("Countries/add-Country")]
        public async Task<ActionResult<PageList<CountryDto>>> AddCountry([FromBody] CreateCountryCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

        [HttpPut("Countries/edit-Country/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<CountryDto>>> EditCountry(
       [FromRoute] string id,
       [FromBody] EditCountryCommand command
     )
        {
            command.Id = id;
            return Ok((await Mediator.Send(command)));
        }

        [HttpDelete("Countries/delete-country/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PageList<CountryDto>>> DeleteCountry([FromRoute] string id)
        {
            return Ok(await Mediator.Send(new DeleteCountryCommand() { Id = id }));
        }





    }
}