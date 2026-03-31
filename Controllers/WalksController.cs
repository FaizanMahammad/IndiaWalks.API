using AutoMapper;
using IndiaWalks.API.CustomActionFilters;
using IndiaWalks.API.Models.Domain;
using IndiaWalks.API.Models.DTO;
using IndiaWalks.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndiaWalks.API.Controllers
{
    //----------------------------------------No DbContext,Linq,DTO,Async,Repository Pattern, Auto Mapper, Model Validations, With CustomValidateModel, Filtering, Sorting, Pagination,  Creating an Exception to Test ExceptionHandlerMiddleware (Global Exception Handling) in Get All-------------------------------
    // api/Walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        // CRETE Walk
        // POST: https:...../api/Walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map Dto To Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            // Map Domain Model to DTO and retrun
            return Ok(mapper.Map<WalkDto>(walkDomainModel));  //200
                                                              //Difficulty Not found & Region not found should be handled
        }

        // Get all
        // GET: /api/Walks?filterOn=Name&filerQuery=Track&sorBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, 
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000 )
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            // Creating an Exception to Test ExceptionHandlerMiddleware (Global Exception Handling)
            //throw new Exception("This is a new Exception");

            // Map Domain to Dto and return
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        // Get By Id
        // GET: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            // Map Domain Model to DTo & Return
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Update Walk by ID
        // PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map Dto to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }
            // Map Domain to Dto & Return
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
            // No Validation for incorrect RegionId & DifficultyID also the returned results needs to be corrected
        }

        // Delete a Walk by Id
        // DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound(); // 404
            }
            // Map Domain Model to Dto & Return
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
    /*----------------------------------------No DbContext,Linq,DTO,Async,Repository Pattern, Auto Mapper, Model Validations, With CustomValidateModel, Filtering, Sorting-------------------------------
    // api/Walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        // CRETE Walk
        // POST: https:...../api/Walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map Dto To Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            // Map Domain Model to DTO and retrun
            return Ok(mapper.Map<WalkDto>(walkDomainModel));  //200
                                                              //Difficulty Not found & Region not found should be handled
        }

        // Get all
        // GET: /api/Walks?filterOn=Name&filerQuery=Track&sorBy=Name&isAscending=true
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true);
            // Map Domain to Dto and return
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        // Get By Id
        // GET: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            // Map Domain Model to DTo & Return
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Update Walk by ID
        // PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map Dto to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }
            // Map Domain to Dto & Return
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
            // No Validation for incorrect RegionId & DifficultyID also the returned results needs to be corrected
        }

        // Delete a Walk by Id
        // DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound(); // 404
            }
            // Map Domain Model to Dto & Return
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
    */
    /*----------------------------------------No DbContext,Linq,DTO,Async,Repository Pattern, Auto Mapper, Model Validations, With CustomValidateModel, Filtering-------------------------------
    // api/Walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        // CRETE Walk
        // POST: https:...../api/Walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map Dto To Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            // Map Domain Model to DTO and retrun
            return Ok(mapper.Map<WalkDto>(walkDomainModel));  //200
                                                              //Difficulty Not found & Region not found should be handled
        }

        // Get all
        // GET: /api/Walks?filterOn=Name&filerQuery=Track
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn,filterQuery);
            // Map Domain to Dto and return
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        // Get By Id
        // GET: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            // Map Domain Model to DTo & Return
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Update Walk by ID
        // PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map Dto to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }
            // Map Domain to Dto & Return
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
            // No Validation for incorrect RegionId & DifficultyID also the returned results needs to be corrected
        }

        // Delete a Walk by Id
        // DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound(); // 404
            }
            // Map Domain Model to Dto & Return
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
    */
    /*----------------------------------------No DbContext,Linq,DTO,Async,Repository Pattern, Auto Mapper, Model Validations, With CustomValidateModel-------------------------------
    // api/Walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        // CRETE Walk
        // POST: https:...../api/Walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map Dto To Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            // Map Domain Model to DTO and retrun
            return Ok(mapper.Map<WalkDto>(walkDomainModel));  //200
                                                              //Difficulty Not found & Region not found should be handled
        }

        // Get all
        // GET: /api/Walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();
            // Map Domain to Dto and return
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        // Get By Id
        // GET: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            // Map Domain Model to DTo & Return
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Update Walk by ID
        // PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            // Map Dto to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }
            // Map Domain to Dto & Return
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
            // No Validation for incorrect RegionId & DifficultyID also the returned results needs to be corrected
        }

        // Delete a Walk by Id
        // DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound(); // 404
            }
            // Map Domain Model to Dto & Return
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
    */
    /*----------------------------------------No DbContext,Linq,DTO,Async,Repository Pattern, Auto Mapper, Model Validations-------------------------------
    // api/Walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        // CRETE Walk
        // POST: https:...../api/Walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                // Map Dto To Domain Model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

                // Map Domain Model to DTO and retrun
                return Ok(mapper.Map<WalkDto>(walkDomainModel));  //200
                                                                  //Difficulty Not found & Region not found should be handled

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

            // Get all
            // GET: /api/Walks
            [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel =  await walkRepository.GetAllAsync();
            // Map Domain to Dto and return
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        // Get all
        // GET: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            // Map Domain Model to DTo & Return
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Update Walk by ID
        // PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (!ModelState.IsValid)
            {
                // Map Dto to Domain Model
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }
                // Map Domain to Dto & Return
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
                // No Validation for incorrect RegionId & DifficultyID also the returned results needs to be corrected
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // Delete a Walk by Id
        // DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound(); // 404
            }
            // Map Domain Model to Dto & Return
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }
    }
    */
}
