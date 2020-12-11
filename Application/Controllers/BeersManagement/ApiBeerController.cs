using Application.Presenters.BeersManagement;
using Domain.BeersManagement.Services;
using Domain.BeersManagement.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.BeersManagement
{
    [Route("api/beers")]
    [ApiController]
    public class ApiBeerController : ControllerBase
    {
        private readonly IBeerCatalog _catalog;

        public ApiBeerController(IBeerCatalog catalog)
        {
            _catalog = catalog;
        }

        [HttpGet("")]
        public ActionResult GetAllBeers([FromQuery] GetAllBeersRequest request)
        {
            var useCase = new GetAllBeers(_catalog);

            var presenter = new ApiGetAllBeersPresenter();

            useCase.Execute(request, presenter);

            var viewModel = presenter.ViewModel;

            if (viewModel.HttpCode == 200)
                return Ok(viewModel);
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult GetOneBeer([FromRoute] GetOneBeerRequest request)
        {
            var useCase = new GetOneBeer(_catalog);

            var presenter = new ApiGetOneBeerPresenter();

            useCase.Execute(request, presenter);

            var viewModel = presenter.ViewModel;

            if (viewModel.HttpCode == 200)
                return Ok(viewModel);
            return NotFound();
        }

        [HttpPost("")]
        public ActionResult CreateNewBeer([FromBody] CreateNewBeerRequest request)
        {
            var useCase = new CreateNewBeer(_catalog);

            var presenter = new ApiCreateNewBeerPresenter();

            useCase.Execute(request, presenter);

            var viewModel = presenter.ViewModel;

            if (viewModel.HttpCode == 201)
                return Created($"/beers/{viewModel.Data.Id}", viewModel);
            return BadRequest(viewModel);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateExistingBeer([FromRoute] [FromBody] UpdateExistingBeerRequest request)
        {
            var useCase = new UpdateExistingBeer(_catalog);

            var presenter = new ApiUpdateExistingBeerPresenter();

            useCase.Execute(request, presenter);

            var viewModel = presenter.ViewModel;

            if (viewModel.HttpCode == 200)
                return Ok(viewModel);
            return BadRequest(viewModel);
        }

        [HttpPut("{id}")]
        public ActionResult DeleteExistingBeer([FromRoute] DeleteExistingBeerRequest request)
        {
            var useCase = new DeleteExistingBeer(_catalog);

            var presenter = new ApiDeleteExistingBeerPresenter();

            useCase.Execute(request, presenter);

            return NoContent();
        }
    }
}