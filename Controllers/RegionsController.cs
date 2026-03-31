using AutoMapper;
using IndiaWalks.API.CustomActionFilters;
using IndiaWalks.API.Data;
using IndiaWalks.API.Models.Domain;
using IndiaWalks.API.Models.DTO;
using IndiaWalks.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace IndiaWalks.API.Controllers
{
    //----------------------------------------No DbContext,Linq,DTO,Async,Repository Pattern, Auto Mapper, Model Validations, With CustomValidateModel, Authentication & Authorization, Logging-------------------------------
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]  // Addding Authorize here makes the entires class & it's methods authorized in General(Read + Write), for role based authorization use this attribute on the methods of this class
    public class RegionsController : ControllerBase
    {
        //private readonly IndiaWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            //this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/Regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]  -- Removed Auth for Easy Access From UI Web App
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //throw new Exception("This is a Custom Exception");

                //Get Data From DataBase
                var regionsDomain = await regionRepository.GetAllAync();

                //logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionsDomain)}");

                // Return DTOs
                return Ok(mapper.Map<List<RegionDto>>(regionsDomain));  //200
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
            //logger.LogInformation("GetAllRegions Action Method was invoked");
            //logger.LogWarning("This is a Warning Log");
            //logger.LogError("This is a Error Log");
        }

        // GET REGION BY ID
        // GET: https://localhost:portnumber/api/Regions/{id}
        // https://localhost:portnumber/api/Regions?id=Value
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region=dbContext.Regions.Find(id);  //Find only be used for PK
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound(); //404
            }

            // Return DTO
            return Ok(mapper.Map<RegionDto>(regionDomain));  //200
        }

        // POST To create new region
        // POST: https://localhost:portnumber/api/Regions
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map Dto to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to create region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain Model back to Dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto); //201
        }

        // PUT To Update Region
        // PUT: https://localhost:portnumber/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map Dto to Doamin Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Retrun Dto
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        // DELETE To Delete Region
        // DELETE: https://localhost:portnumber/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound(); //404
            }

            // Return Deleted Region
            //Return Dto
            return Ok(mapper.Map<RegionDto>(regionDomainModel));

            // But Returning Deleted Object is not necessary, we can Just send an Ok response which is Fine
            //return Ok();
        }
    }

    /*----------------------------------------No DbContext,Linq,DTO,Async,Repository Pattern, Auto Mapper, Model Validations, With CustomValidateModel, Authentication & Authorization-------------------------------
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]  // Addding Authorize here makes the entires class & it's methods authorized in General(Read + Write), for role based authorization use this attribute on the methods of this class
    public class RegionsController : ControllerBase
    {
        //private readonly IndiaWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            //this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/Regions
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From DataBase
            var regionsDomain = await regionRepository.GetAllAync();

            // Return DTOs
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));  //200
        }

        // GET REGION BY ID
        // GET: https://localhost:portnumber/api/Regions/{id}
        // https://localhost:portnumber/api/Regions?id=Value
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize( Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region=dbContext.Regions.Find(id);  //Find only be used for PK
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound(); //404
            }

            // Return DTO
            return Ok(mapper.Map<RegionDto>(regionDomain));  //200
        }

        // POST To create new region
        // POST: https://localhost:portnumber/api/Regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map Dto to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to create region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain Model back to Dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto); //201
        }

        // PUT To Update Region
        // PUT: https://localhost:portnumber/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map Dto to Doamin Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Retrun Dto
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        // DELETE To Delete Region
        // DELETE: https://localhost:portnumber/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound(); //404
            }

            // Return Deleted Region
            //Return Dto
            return Ok(mapper.Map<RegionDto>(regionDomainModel));

            // But Returning Deleted Object is not necessary, we can Just send an Ok response which is Fine
            //return Ok();
        }
    }
    */
    /*----------------------------------------No DbContext,Linq,DTO,Async,Repository Pattern, Auto Mapper, Model Validations, With CustomValidateModel-------------------------------
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //private readonly IndiaWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            //this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From DataBase
            var regionsDomain = await regionRepository.GetAllAync();

            // Return DTOs
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));  //200
        }

        // GET REGION BY ID
        // GET: https://localhost:portnumber/api/Regions/{id}
        // https://localhost:portnumber/api/Regions?id=Value
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region=dbContext.Regions.Find(id);  //Find only be used for PK
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound(); //404
            }

            // Return DTO
            return Ok(mapper.Map<RegionDto>(regionDomain));  //200
        }

        // POST To create new region
        // POST: https://localhost:portnumber/api/Regions
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map Dto to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to create region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain Model back to Dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto); //201
        }

        // PUT To Update Region
        // PUT: https://localhost:portnumber/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map Dto to Doamin Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Retrun Dto
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        // DELETE To Delete Region
        // DELETE: https://localhost:portnumber/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound(); //404
            }

            // Return Deleted Region
            //Return Dto
            return Ok(mapper.Map<RegionDto>(regionDomainModel));

            // But Returning Deleted Object is not necessary, we can Just send an Ok response which is Fine
            //return Ok();
        }
    }
    */
    /*----------------------------------------No DbContext,Linq,DTO,Async,Repository Pattern, Auto Mapper, Model Validations-------------------------------
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //private readonly IndiaWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            //this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From DataBase
            var regionsDomain = await regionRepository.GetAllAync();

            // Return DTOs
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));  //200
        }

        // GET REGION BY ID
        // GET: https://localhost:portnumber/api/Regions/{id}
        // https://localhost:portnumber/api/Regions?id=Value
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region=dbContext.Regions.Find(id);  //Find only be used for PK
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound(); //404
            }

            // Return DTO
            return Ok(mapper.Map<RegionDto>(regionDomain));  //200
        }

        // POST To create new region
        // POST: https://localhost:portnumber/api/Regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Model Validations
            if (ModelState.IsValid)
            {
                // Map Dto to Domain Model
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                // Use Domain Model to create region
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                // Map Domain Model back to Dto
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto); //201
            }
            else
            {
                return BadRequest(ModelState);  //400
            }
        }

        // PUT To Update Region
        // PUT: https://localhost:portnumber/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            if (ModelState.IsValid) 
            {
                //Map Dto to Doamin Model
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                // Retrun Dto
                return Ok(mapper.Map<RegionDto>(regionDomainModel));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE To Delete Region
        // DELETE: https://localhost:portnumber/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound(); //404
            }

            // Return Deleted Region
            //Return Dto
            return Ok(mapper.Map<RegionDto>(regionDomainModel));

            // But Returning Deleted Object is not necessary, we can Just send an Ok response which is Fine
            //return Ok();
        }
    }
    */
    /*----------------------------------------No DbContext,Linq,DTO,Async,Repository Pattern, Auto Mapper-------------------------------
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //private readonly IndiaWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            //this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From DataBase
            var regionsDomain = await regionRepository.GetAllAync();

            // Return DTOs
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));  //200
        }

        // GET REGION BY ID
        // GET: https://localhost:portnumber/api/Regions/{id}
        // https://localhost:portnumber/api/Regions?id=Value
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region=dbContext.Regions.Find(id);  //Find only be used for PK
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound(); //404
            }

            // Return DTO
            return Ok(mapper.Map<RegionDto>(regionDomain));  //200
        }

        // POST To create new region
        // POST: https://localhost:portnumber/api/Regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map Dto to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to create region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain Model back to Dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);  

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // PUT To Update Region
        // PUT: https://localhost:portnumber/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map Dto to Doamin Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Retrun Dto
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        // DELETE To Delete Region
        // DELETE: https://localhost:portnumber/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound(); //404
            }

            // Return Deleted Region
            //Return Dto
            return Ok(mapper.Map<RegionDto>(regionDomainModel));

            // But Returning Deleted Object is not necessary, we can Just send an Ok response which is Fine
            //return Ok();
        }
    }
    */
    /*----------------------------------------dbContext,Linq,DTO,Async,Repository Pattern-------------------------------
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IndiaWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(IndiaWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From DataBase
            var regionsDomain = await regionRepository.GetAllAync();

            // Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            // Return DTOs
            return Ok(regionsDto);  //200
        }

        // GET REGION BY ID
        // GET: https://localhost:portnumber/api/Regions/{id}
        // https://localhost:portnumber/api/Regions?id=Value
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region=dbContext.Regions.Find(id);  //Find only be used for PK
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound(); //404
            }

            // Map/Convert Domain Models to DTOs
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);  //200
        }

        // POST To create new region
        // POST: https://localhost:portnumber/api/Regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map Dto to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use Domain Model to create region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
            

            // Map Domain Model back to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // PUT To Update Region
        // PUT: https://localhost:portnumber/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map Dto to Domain Model
            var regionDomainModel = new Region
            {
                Name = updateRegionRequestDto.Name,
                Code = updateRegionRequestDto.Code,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }

        // DELETE To Delete Region
        // DELETE: https://localhost:portnumber/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound(); //404
            }

            // Return Deleted Region
            // Map Domain Model To Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);

            // But Returning Deleted Object is not necessary, we can Just send an Ok response which is Fine
            //return Ok();
        }
    }
    */
    /*----------------------------------------dbContext,Linq,DTO,Async-------------------------------
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IndiaWalksDbContext dbContext;
        public RegionsController(IndiaWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await dbContext.Regions.ToListAsync();

            // Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            // Return DTOs
            return Ok(regionsDto);  //200
        }

        // GET REGION BY ID
        // GET: https://localhost:portnumber/api/Regions/{id}
        // https://localhost:portnumber/api/Regions?id=Value
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region=dbContext.Regions.Find(id);  //Find only be used for PK
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id); //Linq
            if (regionDomain == null)
            {
                return NotFound(); //404
            }

            // Map/Convert Domain Models to DTOs
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);  //200
        }

        // POST To create new region
        // POST: https://localhost:portnumber/api/Regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map Dto to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            // Use Domain Model to create region
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            // Map Domain Model back to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // PUT To Update Region
        // PUT: https://localhost:portnumber/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Check if Region Exist
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound(); //404
            }

            // Map Dto to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            // Map Domain Model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }

        // DELETE To Delete Region
        // DELETE: https://localhost:portnumber/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Check if Region Exist
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound(); //404
            }

            // Delete Region
            dbContext.Regions.Remove(regionDomainModel);  //Remove doesn't have Async version
            await dbContext.SaveChangesAsync();

            // Return Deleted Region
            // Map Domain Model To Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);

            // But Returning Deleted Object is not necessary, we can Just send an Ok response which is Fine
            //return Ok();
        }
    }
    */
    /*----------------------------------------dbContext,Linq,DTO-------------------------------
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IndiaWalksDbContext dbContext;
        public RegionsController(IndiaWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/Regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regionsDomain = dbContext.Regions.ToList();

            // Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            // Return DTOs
            return Ok(regionsDto);  //200
        }

        // GET REGION BY ID
        // GET: https://localhost:portnumber/api/Regions/{id}
        // https://localhost:portnumber/api/Regions?id=Value
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region=dbContext.Regions.Find(id);  //Find only be used for PK
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id); //Linq
            if (regionDomain == null)
            {
                return NotFound(); //404
            }

            // Map/Convert Domain Models to DTOs
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);  //200
        }

        // POST To create new region
        // POST: https://localhost:portnumber/api/Regions
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map Dto to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            // Use Domain Model to create region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            // Map Domain Model back to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // PUT To Update Region
        // PUT: https://localhost:portnumber/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Check if Region Exist
            var regionDomainModel=dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound(); //404
            }

            // Map Dto to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

            // Map Domain Model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }

        // DELETE To Delete Region
        // DELETE: https://localhost:portnumber/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            // Check if Region Exist
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound(); //404
            }

            // Delete Region
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            // Return Deleted Region
            // Map Domain Model To Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);

            // But Returning Deleted Object is not necessary, we can Just send an Ok response which is Fine
            //return Ok();
        }
    }
    */
    /*-------------------------------------------Db Connection(dbContext) with Linq------------------------------------------------
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IndiaWalksDbContext dbContext;
        public RegionsController(IndiaWalksDbContext dbContext)
        {
            this.dbContext=dbContext;
        }
        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/Regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions= dbContext.Regions.ToList();
            return Ok(regions);  //200
        }

        // GET REGION BY ID
        // GET: https://localhost:portnumber/api/Regions/{Id}
        // https://localhost:portnumber/api/Regions?id=Value
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region=dbContext.Regions.Find(id);  //Find only be used for PK
            var region= dbContext.Regions.FirstOrDefault(x=>x.Id==id); //Linq
            if (region==null)
            {
                return NotFound(); //404
            }
            return Ok(region);  //200
        }
    }
    */
    /*    -------------------------------------------HardCoded Without Database----------------------------------------------------

    // https://localhost:portnumber/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        // GET ALL REGIONS
        // GET: // https://localhost:portnumber/api/Regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions= new List<Region>()
            {
                new Region
                {
                    Id=Guid.NewGuid(),
                    Name="Palamaner Region",
                    Code="PLMNR",
                    RegionImageUrl = "https://image2url.com/r2/bucket3/images/1767577638823-e7ed803c-5deb-4595-bfc2-f237749b3c1e.jpg"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Pungnur Region",
                    Code = "PGNR",       
                }
            };
            return Ok(regions);  //200
        }
    }
    */
}
