using AgentsRest.Dto;
using AgentsRest.Models;
using AgentsRest.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgentsRest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MissionController(IMissionService missionService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MissonModel>>> GetMissions()
        {
            try
            {
                var missions = await missionService.GetMissions();
                return Ok(missions);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> UpdateMissionStatus(int id, MissionDto missionDto)
        {
            try
            {
                var mission = await missionService.UpdateMissionStatus(id, missionDto);
                return Created("sucses", mission);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }


        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<List<MissonModel>>> UpdateAllMission()
        {
            try
            {
                var Missions = await missionService.UpdateMissions();
                return Ok(Missions);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }


        }
    }
}
