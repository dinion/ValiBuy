using Application.Products.Commands.UpdateProduct;
using FluentValidation.TestHelper;

namespace UnitTests.Products
{
    [TestFixture]
    public class UpdateProductCommandValidatorTests
    {
        private UpdateProductCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UpdateProductCommandValidator();
        }

        [Test]
        public void ShouldHaveValidationErrorWhenIdIsZero()
        {
            var command = new UpdateProductCommand { Id = 0, Name = "Product", Price = 10m };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenNameIsEmpty()
        {
            var command = new UpdateProductCommand { Id = 1, Name = "", Price = 10m };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenPriceIsZero()
        {
            var command = new UpdateProductCommand { Id = 1, Name = "Product", Price = 0m };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Test]
        public void ShouldNotHaveValidationErrorWhenCommandIsValid()
        {
            var command = new UpdateProductCommand { Id = 1, Name = "Product", Price = 10m };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}