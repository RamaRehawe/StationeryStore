using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StationeryStore.Dto;
using StationeryStore.Interfaces;
using StationeryStore.Models;
using StationeryStore.Services;

namespace StationeryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInformationController : BaseController
    {
        private readonly IContactInformationRepository _contactInformationRepository;
        private readonly IMapper _mapper;
        public ContactInformationController(IContactInformationRepository contactInformationRepository, IMapper mapper,
            UserInfoService userInfoService, IUserRepository userRepository) : base(userInfoService, userRepository)
        {
            _contactInformationRepository = contactInformationRepository;
            _mapper = mapper;
        }

        //[HttpPut("setContactInformation")]
        //public IActionResult UpdateContactInformation(int id, ReqContactInformationDto reqContact)
        //{
        //    var existed = _contactInformationRepository.GetInfoById(id);
        //    existed.Value = reqContact.Value;
        //    existed.Description = reqContact.Description;
        //    _contactInformationRepository.UpdateInfos(existed);
        //    return Ok(existed);
        //}
        [HttpPut("setContactInformation")]
        public IActionResult UpdateContactInformation([FromBody] ReqContactInformationDto reqContact)
        {
            ContactInformation contactInfoToUpdate = null;

            switch (reqContact.Type)
            {
                case "storePhone":
                    contactInfoToUpdate = _contactInformationRepository.GetInfos().FirstOrDefault(ci => ci.Type == "Phone");
                    break;
                case "storeEmail":
                    contactInfoToUpdate = _contactInformationRepository.GetInfos().FirstOrDefault(ci => ci.Type == "Email");
                    break;
                case "storeSocial":
                    contactInfoToUpdate = _contactInformationRepository.GetInfos().FirstOrDefault(ci => ci.Type == "Social Media");
                    break;
                default:
                    return NotFound();
            }

            contactInfoToUpdate.Value = reqContact.Value;
            _contactInformationRepository.UpdateInfos(contactInfoToUpdate);

            return Ok(contactInfoToUpdate);
        }





        [HttpGet("getStoreInfos")]
        public IActionResult GetStoreInfos()
        {
            var contactInfo = _mapper.Map<IEnumerable<ResContactInformationDto>>(_contactInformationRepository.GetInfos());
            return Ok(contactInfo);
        }

    }
}
