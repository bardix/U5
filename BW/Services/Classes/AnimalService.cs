using System;
using BW2_Team6.Context;
using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BW2_Team6.Services.Classes
{
	public class AnimalService : IAnimalService
	{
        private readonly DataContext _dbContext;
        private readonly ILogger<AnimalService> _logger;

        public AnimalService(DataContext dbContext, ILogger<AnimalService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Animal> Create(Animal newAnimal)
        {
            if (newAnimal == null)
            {
                throw new ArgumentNullException(nameof(newAnimal));
            }

            try
            {
                await _dbContext.Animals.AddAsync(newAnimal);
                await _dbContext.SaveChangesAsync();
                return newAnimal;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nella creazione dell'animale");
                throw;
            }
        }

        public async Task<Animal> Delete(int id)
        {
            try
            {
                var animal = await GetById(id);
                _dbContext.Remove(animal);
                await _dbContext.SaveChangesAsync();

                return animal;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Animale con ID {id} non trovato per eliminazione");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nella eliminazione dell'animale");
                throw;
            }
        }

        public async Task<List<Animal>> GetAll()
        {
            try
            {
                var animals = await _dbContext.Animals
                    .AsNoTracking()
                    .Include(a => a.Owner)
                    .ToListAsync();

                return animals;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nel recupero di tutti gli animali");
                throw;
            }
        }

        public async Task<Animal> GetById(int id)
        {
            try
            {
                var animal = await _dbContext.Animals
                    .AsNoTracking()
                    .Include(a => a.Owner)
                    .FirstOrDefaultAsync(a => a.Id == id);
                if (animal == null)
                {
                    _logger.LogWarning($"Animale con ID {id} non trovato");
                    throw new KeyNotFoundException($"Animale non ID {id} non trovato");
                }

                return animal;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nel recupero dell'animale con ID {id}");
                throw;
            }
        }

        public async Task<Animal> Update(int id, Animal updateAnimal)
        {
            if (updateAnimal == null)
            {
                throw new ArgumentNullException(nameof(updateAnimal));
            }
            try
            {
                var animal = await GetById(id);

                animal.Name = updateAnimal.Name;
                animal.Type = updateAnimal.Type;
                animal.Fur = updateAnimal.Fur;
                animal.DateBirth = updateAnimal.DateBirth;
                animal.Microchip = updateAnimal.Microchip;
                animal.Owner = updateAnimal.Owner;

                _dbContext.Animals.Update(animal);
                await _dbContext.SaveChangesAsync();

                return animal;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Animale con ID {id} non trovato");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nella modifica dell'animale");
                throw;
            }
        }
    }
}

