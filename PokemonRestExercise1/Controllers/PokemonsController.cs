using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PokemonLib;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokemonRestExercise1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonsController : ControllerBase
    {
        private PokemonsRepository _pokemonsRepository;

        public PokemonsController(PokemonsRepository PokemonsRepository)
        {
            _pokemonsRepository = PokemonsRepository;
        }
        // GET: api/<PokemonsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult <IEnumerable<Pokemon>> Get()
        {
            try
            {
                return Ok(_pokemonsRepository);
            }
            catch 
            {
                return NotFound();
            }
        }

        // GET api/<PokemonsController>/5
        [HttpGet]
        [Route("{id}")]
        public Pokemon Get(int id)
        {
            return _pokemonsRepository.GetByID(id);
        }
        // POST api/<PokemonsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Pokemon> Post([FromBody] Pokemon newPokemon)
        {
            try
            {
                Pokemon createdPokemon = _pokemonsRepository.Add(newPokemon);
                return Created("/" + createdPokemon.Id, createdPokemon);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PokemonsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Pokemon> Put(int id, [FromBody] Pokemon updatedPokemon)
        {
            try
            {
                var existingPokemon = _pokemonsRepository.GetByID(id);
                if (existingPokemon == null)
                {
                    return NotFound($"Pokemon with ID {id} not found.");
                }

                Pokemon updated = _pokemonsRepository.Update(id, updatedPokemon);
                return Ok(updated);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PokemonsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            try
            {
                var pokemon = _pokemonsRepository.GetByID(id);
                if (pokemon == null)
                {
                    return NotFound($"Pokemon with ID {id} not found.");
                }

                _pokemonsRepository.Delete(id);
                return Ok($"Pokemon with ID {id} deleted.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
