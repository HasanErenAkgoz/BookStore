using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
         private readonly BookStoreDBContext _context;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId=0;

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı.");
        }

        [Fact]
        public void WhenGivenNameIsSameWithAnotherGenre_InvalidOperationException_ShouldBeReturn()
        {
            var genre1 = new Genre(){Name="Kurgu"};
            _context.Genres.Add(genre1);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId=2;
            command.Model=new UpdateGenreModel(){Name="Kurgu"};

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli kitap türü zaten mevcut.");
        }


        [Fact]
        public void WhenGivenGenreIdinDB_Genre_ShouldBeUpdate()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model=new UpdateGenreModel(){Name="WhenGivenGenreIdinDB_Genre_ShouldBeUpdate"};
            command.GenreId=1;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var genre=_context.Genres.SingleOrDefault(genre=>genre.Id == command.GenreId);
            genre.Should().NotBeNull();

        }
    }
}