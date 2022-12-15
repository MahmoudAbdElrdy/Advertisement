using System.Collections.Generic;
using System.Threading.Tasks;
using AuthApplication.Advertisement.Command;
using AuthApplication.Advertisement.Commands;
using AuthApplication.Advertisement.Dto;
using AuthApplication.Advertisement.Queries;
using AuthApplication.Regions.Commands;
using AuthApplication.Regions.Dto;
using AuthApplication.Regions.Queries;
using AuthApplication.UserManagment.Commands;
using AuthApplication.UserManagment.Queries;
using Common.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Advertisement
{
    
    [Route("api/[controller]")]
    public partial class AdvertisementController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("apply-for-advertisement")]
        public async Task<ActionResult<bool>> ApplyForAdvertisement([FromBody] ApplyForAdvertisementCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

        [AllowAnonymous]
        [HttpPost("add-advertisement-without-space")]
        public async Task<ActionResult<AdvertisementDto>> AddAdvertisementWithoutSpace([FromBody] CreateAdvertisementWithoutSPaceCommand command)
        {
            return Ok((await Mediator.Send(command)));
        } 
        [AllowAnonymous]
        [HttpPost("create-reservation-by-client")]
        public async Task<ActionResult<bool>> CreateReservationBClient([FromBody] CreateReservationByClientCommand command)
        {
            return Ok((await Mediator.Send(command)));
        } 
        [AllowAnonymous]
        [HttpPost("stop-ad")]
        public async Task<ActionResult<bool>> StopAd([FromBody] StopAdvertisementCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

        [AllowAnonymous]
        [HttpPost("add-commercial-record")]
        public async Task<ActionResult<bool>> AddCommercialRecord([FromBody] AddCommercialRecordCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

        [AllowAnonymous]
        [HttpPost("add-paid-service")]
        public async Task<ActionResult<bool>> AddPaidService([FromBody] AddPaidServicesCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }
        [AllowAnonymous]
        [HttpPost("edit-paid-service")]
        public async Task<ActionResult<bool>> EditPaidService([FromBody] EditPaidServicesCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

        [AllowAnonymous]
        [HttpPost("get-my-service")]
        public async Task<ActionResult<List<GetPaidServicesDto>>> GetMyService([FromBody] GetMyServiceCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }
        
        [AllowAnonymous]
        [HttpPost("get-ad-services")]
        public async Task<ActionResult<List<PaidServicesOfAdDto>>> GetMyService([FromBody] GetAdServicesCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

        [AllowAnonymous]
        [HttpPost("edit-Advertisement")]
        public async Task<ActionResult<AdvertisementDto>> EditAdvertisement([FromBody] EditAdvertisementCommand command)
        {
            return Ok((await Mediator.Send(command)));
        } 
        
        [AllowAnonymous]
        [HttpPost("add-advertisement")]
        public async Task<ActionResult<AdvertisementDto>> AddAdvertisement([FromBody] CreateAdvertisementCommand command)
        {
            return Ok((await Mediator.Send(command)));
        } 
     
         
        
        [AllowAnonymous]
        [HttpPut("disable-Advertisement")]
        public async Task<ActionResult<AdvertisementDto>> ChangeStateAdvertisement([FromBody]string id,bool enable)
        {
            return Ok((await Mediator.Send(new DisableAdvertisementCommand() { Id = id ,Enable= enable })));
        } 
        
        [AllowAnonymous]
        [HttpGet("get-all-advertisement")]
        public async Task<ActionResult<PageList<AdvertisementDto>>> GetAllAdvertisement([FromQuery] GetAllAdvertisementCommand command)
        {   
            return Ok((await Mediator.Send(command)));
        }
        [AllowAnonymous]
        [HttpGet("get-all-advertisement-dashboard")]
        public async Task<ActionResult<PageList<AdvertisementDto>>> GetAllAdvertisementDashboard([FromQuery] GetAllAdvertisementCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

        [AllowAnonymous]
        [HttpPost("Search-Advertisement")]
        public async Task<ActionResult<PageList<AdvertisementDto>>> SearchAdvertisement([FromBody] SearchAdvertisementCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

        [AllowAnonymous]
        [HttpGet("get-advertisement-detail")]
        public async Task<ActionResult<SpaceInfoDto>>  GetSingleAdvertisement([FromQuery] GetSpaceAdvertisementCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

        [AllowAnonymous]
        [HttpGet("get-advertisement-ById")]
        public async Task<ActionResult<AdsDto>> GetAdvertisementById([FromQuery] GetAdvertisementByIdCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

        [AllowAnonymous]
        [HttpGet("get-top-ads")]
        public async Task<ActionResult<List<AdsDto>>> GetTopAds(
            [FromQuery]GetTopAdsQuery query
            ) {
            
            return Ok(await Mediator.Send(query));
        }
        
        [AllowAnonymous]
        [HttpPost("get-my-ads")]
        public async Task<ActionResult<List<AdsDto>>> GetMyAds(
            [FromBody] GetMyAds query
            ) {            
            return Ok(await Mediator.Send(query));
        }
        [AllowAnonymous]
        [HttpPost("add-Favourite")]
        public async Task<ActionResult<AddFavouriteDto>> AddFavouriteCommand([FromBody] AddFavouriteCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }
        [AllowAnonymous]
        [HttpPost("get-my-favourites")]
        public async Task<ActionResult<List<AdsDto>>> GetMyFavourites(
            [FromBody] GetMyFavourite query
            ) {
            
            return Ok(await Mediator.Send(query));
        }   

        [AllowAnonymous]
        [HttpPost("get-ad-intervalsInstallment")]
        public async Task<ActionResult<List<AdIntervalInstallmentDto>>> GetIntervalInstallments(
            [FromBody] GetAdIntervalInstallments query
            ) { 
            return Ok(await Mediator.Send(query));
        }   
        [AllowAnonymous]
        [HttpPost("get-my-Spaces")]
        public async Task<ActionResult<List<AdsDto>>> GetMySpaces(
            [FromBody] GetMySpaces query
            ) {
            
            return Ok(await Mediator.Send(query));
        }
        [AllowAnonymous]
        [HttpPost("get-ad-intervals")]
        public async Task<ActionResult<List<AdIntervalsDto>>> GetAdIntervals(
        [FromBody] GetAdIntervals query
        )
        {
            return Ok(await Mediator.Send(query));
        }
        [AllowAnonymous]
        [HttpPost("get-my-services")]
        public async Task<ActionResult<List<ServicesDto>>> GetMyServices(
           [FromBody] GetMyServices query
           )
        {

            return Ok(await Mediator.Send(query));
        }
        [AllowAnonymous]
        [HttpPost("add-rating")]
        public async Task<ActionResult<RatingDto>> AddRatingCommand([FromBody] AddRatingCommand command)
        {
            return Ok((await Mediator.Send(command)));
        }

    }
}