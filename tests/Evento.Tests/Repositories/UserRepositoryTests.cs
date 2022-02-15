using System;
using Xunit;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Evento.Tests.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task when_adding_new_user_it_should_be_added_correctly_to_the_list()
        {
            //Arrange
            var user = new User(Guid.NewGuid(), "user", "user", "test@test.com", "secret");
            IUserRepository repository = new UserRepository();

            //Act
            await repository.AddAsync(user);

            //Assert
            var existingUser = await repository.GetAsync(user.Id);
            Assert.Equal(user, existingUser);
        }
    }
}
