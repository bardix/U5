using System;
using BW2_Team6.Models;

namespace BW2_Team6.Services.Interfaces
{
	public interface IAnimalService
	{
        public Task<Animal> Create(Animal newAnimal);
        public Task<List<Animal>> GetAll();
        public Task<Animal> GetById(int id);
        public Task<Animal> Update(int id, Animal updateAnimal);
        public Task<Animal> Delete(int id);
    }
}

